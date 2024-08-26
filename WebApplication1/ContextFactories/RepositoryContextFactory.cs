using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repositories.Concretes.EfCore;

namespace WebApplication1.ContextFactories
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().
                SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("appsettings.json", reloadOnChange: true, optional: true).
                Build();

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();

            dbContextOptionsBuilder.
                UseSqlServer(configuration.GetConnectionString("SqlCon"),
                opt=>opt.MigrationsAssembly("WebApplication1"));

            return new RepositoryContext(dbContextOptionsBuilder.Options);
        }
    }
}
