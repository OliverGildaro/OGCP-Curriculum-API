using Microsoft.EntityFrameworkCore;

namespace OGCP.Curriculum.API.repositories
{
    public class CertificationRepository
    {
        private readonly DbProfileContext _context;

        public CertificationRepository(DbProfileContext context)
        {
            _context = context;
        }

        public async Task AddCertificationAsync(int profileId, string name, string organization, DateTime dateIssued, DateTime? expirationDate, string description)
        {
            var certification = new Dictionary<string, object>
            {
                ["Id"] = 0, // EF will generate this automatically
                ["ProfileId"] = profileId,
                ["CertificationName"] = name,
                ["IssuingOrganization"] = organization,
                ["DateIssued"] = dateIssued,
                ["ExpirationDate"] = expirationDate,
                ["Description"] = description
            };

            _context.Certifications.Add(certification);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Dictionary<string, object>>> GetCertificationsByProfileIdAsync(int profileId)
        {
            return await _context.Certifications
                .Where(c => (int)c["ProfileId"] == profileId)
                .ToListAsync();
        }

        public async Task UpdateCertificationAsync(int certificationId, string description)
        {
            var certification = await _context.Certifications
                .FirstOrDefaultAsync(c => (int)c["Id"] == certificationId);

            if (certification != null)
            {
                certification["Description"] = description;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCertificationAsync(int certificationId)
        {
            var certification = await _context.Certifications
                .FirstOrDefaultAsync(c => (int)c["Id"] == certificationId);

            if (certification != null)
            {
                _context.Certifications.Remove(certification);
                await _context.SaveChangesAsync();
            }
        }
    }

}
