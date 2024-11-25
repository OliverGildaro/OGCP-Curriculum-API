using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.repositories;
using OGCP.Profiles.UnitTests.serviceTests.GeneralProfUnitTests;

namespace OGCP.Profiles.UnitTests.EntityFrameworkContext.GeneralProfileUnitTests;

[Collection("DBProfileContectFixtureCollection")]
public class GeneralProfileRepo_UT
{
    private readonly DbProfileContext dbContext;

    public GeneralProfileRepo_UT(DBProfileContextFixtureClass contectFixtureClass)
    {
        this.dbContext = contectFixtureClass.context;
    }

    //Reusing test data
    [Theory]
    [ClassData(typeof(CreateGeneralProfileRequestTestData))]
    public async Task Test(GeneralProfile profile)
    {
        dbContext.GeneralProfiles.Add(profile);
        await dbContext.SaveChangesAsync();
        Assert.NotEqual(0, profile.Id);

        // Detach entities to reset tracking
        DetachAllEntities();
    }

    //Reusing test data
    [Theory]
    [ClassData(typeof(CreateGeneralProfileRequestTestData))]
    public async Task Test22(GeneralProfile profile)
    {
        dbContext.GeneralProfiles.Add(profile);
        Assert.Equal(EntityState.Added, dbContext.Entry(profile).State);

        // Detach entities to reset tracking
        DetachAllEntities();
    }

    private void DetachAllEntities()
    {
        var entries = dbContext.ChangeTracker.Entries().ToList();
        foreach (var entry in entries)
        {
            entry.State = EntityState.Detached;
        }
    }
}

//FIXTURE COLLECTION
[CollectionDefinition("DBProfileContectFixtureCollection")]
public class DBProfileContectFixtureCollection
    : ICollectionFixture<DBProfileContextFixtureClass>
{

}

//FIXTURE CLASS
public class DBProfileContextFixtureClass : IDisposable
{
    private SqliteConnection connection;
    public DbProfileContext context { get; }

    public DBProfileContextFixtureClass()
    {
        connection = new("DataSource=:memory:");
        connection.Open();

        DbContextOptions<DbProfileContext> contextOptions = new DbContextOptionsBuilder<DbProfileContext>()
                   .UseSqlite(connection)
                   .Options;

        context = new DbProfileContext(contextOptions);
        context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        if (connection != null)
        {
            this.connection.Dispose();
        }
    }

}
