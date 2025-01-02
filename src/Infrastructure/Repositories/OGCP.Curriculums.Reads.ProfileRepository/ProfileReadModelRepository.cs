using ArtForAll.Shared.ErrorHandler.Maybe;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.DAL.Queries.context;
using OGCP.Curriculum.API.DAL.Queries.interfaces;
using OGCP.Curriculum.API.DAL.Queries.Models;
using OGCP.Curriculum.API.DAL.Queries.utils;
using OGCP.Curriculum.API.DAL.Queries.utils.expand;
using OGCP.Curriculum.API.DAL.Queries.utils.pagination;
using OGCP.Curriculums.Ports;
using OGCP.Curriculums.Reads.ProfileRepository.DTOs;
using System.Data;

namespace OGCP.Curriculum.API.DAL.Queries;

public class ProfileReadModelRepository : IProfileReadModelRepository
{
    private readonly ApplicationReadDbContext context;
    private readonly IApplicationInsights insights;
    private readonly Dictionary<string, IOrderBy> OrderFunctions =
        new Dictionary<string, IOrderBy>
        {
                    { "GivenName", new OrderBy<string>(x => x.GivenName) },
                    { "FamilyNames",  new OrderBy<string>(x => x.FamilyNames) },
                    { "Summary",   new OrderBy<string>(x => x.Summary) },
                    { "CareerGoals",   new OrderBy<string>(x => x.CareerGoals) },
                    { "DesiredJobRole",   new OrderBy<string>(x => x.DesiredJobRole) },
                    { "Discriminator",   new OrderBy<string>(x => x.Discriminator) },
        };

    public ProfileReadModelRepository(
        ApplicationReadDbContext profileContext,
        IApplicationInsights insights)
    {
        this.context = profileContext;
        this.insights = insights;
    }

    public async Task<IReadOnlyList<ProfileReadModel>> FindAsync(QueryParameters parameters)
    {
        try
        {
            //IQUERYABLE
            //IQueryable works with expression trees
            //The LINQ expressin will be translated to sql expressions
            //We can builde the expressions trees dynamically in runtime
            //IENUMERABLE
            //IEnumerable works with delegates
            //That is compiled code
            insights.LogInformation("MY_TRACKINGS: Ready to execute my sql command");
            var collection = context.Profiles
                .Include(p => p.ProfileEducations)
                .ThenInclude(pe => pe.Education)
                .Include(p => p.ProfileLanguages)
                .ThenInclude(pl => pl.Language) as IQueryable<ProfileReadModel>;

            //Filtering
            if (!string.IsNullOrEmpty(parameters.FilterBy))
            {
                collection = collection.Where(c => c.Discriminator == parameters.FilterBy.Trim());
            }

            //Searching
            if (!string.IsNullOrEmpty(parameters.SearchBy))
            {
                collection = collection.Where(c => c.GivenName.Contains(parameters.SearchBy.Trim())
                    || c.FamilyNames.Contains(parameters.SearchBy.Trim()));
            }

            //Ordering
            if (!string.IsNullOrWhiteSpace(parameters.OrderBy)
                && OrderFunctions.ContainsKey(parameters.OrderBy))
            {
                collection = parameters.Desc
                    ? collection.OrderByDescending(OrderFunctions[parameters.OrderBy])
                    : collection.OrderBy(OrderFunctions[parameters.OrderBy]);
            }

            if (parameters.Fields != null && parameters.Fields.Length != 0)
            {
                var projection = Expand.BuildProjection3(parameters.SelectFields);
                collection = collection.Select(projection);
            }

            //Paging
            var profiles = await PagedList<ProfileReadModel>.CreateAsync(collection,
                parameters.PageNumber, parameters.PageSize);

            return profiles;
        }
        catch (Exception ex)
        {
            insights.LogInformation("MY_TRACKINGS: Exception was catched in sql execution");
            insights.LogInformation(string.Format("MY_TRACKINGS message: {0}", ex.Message));
            insights.LogInformation(string.Format("MY_TRACKINGS stack trace: {0}", ex.StackTrace));
            //insights.LogException(ex);
            throw;
        }
    }

    public async Task<Maybe<ProfileReadModel>> FindAsync(int id)
    {
        try
        {
            //25ms average frm postman
            var result = await context.Profiles
                .Include(p => p.ProfileEducations)
                .ThenInclude(pe => pe.Education)
                .Include(p => p.ProfileLanguages)
                .ThenInclude(pl => pl.Language)
                .AsSplitQuery()
                .FirstOrDefaultAsync(p => p.Id.Equals(id));
            return result;
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task<IReadOnlyList<EducationReadModel>> FindEducationsFromProfile(int id)
    {
        return await this.context.Educations
                .Where(l => l.ProfileEducations.Any(pe => pe.ProfileId == id))
                .ToListAsync();
    }

    public async Task<IReadOnlyList<LanguageReadModel>> FindLanguagesFromProfile(int profileId)
    {
        return await this.context.Languages
                .Where(l => l.ProfileLanguages.Any(pl => pl.ProfileId == profileId))
                .ToListAsync();
    }

    //Just to learn
    //Ej1: Cuantos saben un idioma, detallando su nivel
    public Task FindLanguagesGrouped()
    {
        //The problem with this groupBy is that the list of leves have duplicated values
        var languagesGrouped = this.context.Languages
            .AsNoTracking()
            .GroupBy(l => new { Name = l.Name, Level = l.Level },//We can add a second groupBy argument to avoid duplicate data
            (langGroup, languages) => new
            {
                Key = langGroup.Name,
                //Levels = languages.Select(l => l.Level),//execute left join with the groupBy table by name
                Level = langGroup.Level,
                Count = languages.Count(),
            });

        foreach (var item in languagesGrouped.Distinct())
        {

        }
        return Task.FromResult(languagesGrouped);
    }

    //SelectMany: WE can use to build diferent kind of responses
    public async Task<IReadOnlyList<ProfileEducationDto>> FindEducationsAsync()
    {
        return await this.context.Profiles
            .Include(p => p.ProfileEducations)
            .ThenInclude(pe => pe.Education)
            .SelectMany(p => p.ProfileEducations, (profile, profileEducation) => new ProfileEducationDto
            {
                GivenName = profile.GivenName,
                Institution = profileEducation.Education.Institution,
            }).ToListAsync();
    }

    public async Task<EducationByRangeResponse> FindEducationsByRange(
        DateOnly startDate, DateOnly endDate)
    {
        var sql = "EXEC [dbo].[GetEducationsByRangeDate] @startDate, @endDate";

        var parameters = new[]
        {
            new SqlParameter("@startDate", startDate),
            new SqlParameter("@endDate", endDate)
        };

        using (var command = context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = sql;
            command.CommandType = CommandType.Text;
            command.Parameters.AddRange(parameters);

            await context.Database.OpenConnectionAsync();

            using (var reader = await command.ExecuteReaderAsync())
            {
                var educationDetails = new List<EducationDetailDto>();
                var institutionSummaries = new List<InstitutionSummaryDto>();
                int totalEducationsInRange = 0;

                while (await reader.ReadAsync())
                {
                    educationDetails.Add(new EducationDetailDto
                    {
                        FirstName = reader.GetString(0),
                        LastName = reader.GetString(1),
                        Institution = reader.GetString(2),
                        StartDate = DateOnly.FromDateTime(reader.GetDateTime(3)),
                        EndDate = reader.IsDBNull(4) ? null : DateOnly.FromDateTime(reader.GetDateTime(4)),
                        Degree = reader.IsDBNull(5) ? null : reader.GetString(5),
                        ProjectTitle = reader.IsDBNull(6) ? null : reader.GetString(6),
                    });
                }

                if (await reader.NextResultAsync() && await reader.ReadAsync())
                {
                    totalEducationsInRange = reader.GetInt32(0);
                }

                if (await reader.NextResultAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        institutionSummaries.Add(new InstitutionSummaryDto
                        {
                            Institution = reader.GetString(0),
                            ProfileCount = reader.GetInt32(1)
                        });
                    }
                }

                return new EducationByRangeResponse
                {
                    EducationDetails = educationDetails,
                    TotalEducationsInRange = totalEducationsInRange,
                    InstitutionSummary = institutionSummaries
                };
            }
        }
    }
}
