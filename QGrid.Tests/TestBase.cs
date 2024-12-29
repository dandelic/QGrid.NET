using Microsoft.EntityFrameworkCore;
using QGrid.Tests.Data;

namespace QGrid.Tests
{
    public class TestBase : IDisposable
    {
        private const string DB_NAME = "QGridTestDB";
        protected InMemoryDb _context = null!;

        protected TestBase()
        {
            Init();
        }
        private void Init()
        {
            var options = new DbContextOptionsBuilder<InMemoryDb>()
                .UseInMemoryDatabase(databaseName: DB_NAME)
                .EnableSensitiveDataLogging()
                .Options;

            _context = new InMemoryDb(options);
            SeedData.Seed(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        protected static string GetAsset(string name)
        {
            string directory = AppDomain.CurrentDomain.BaseDirectory;

            string jsonPath = Path.Combine(directory, "Assets", name);
            if (!File.Exists(jsonPath))
            {
                throw new FileNotFoundException($"File not found: {jsonPath}");
            }

            return File.ReadAllText(jsonPath);
        }
    }
}
