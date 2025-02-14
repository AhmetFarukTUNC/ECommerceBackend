using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ETicaretDbContext>
    {
        public ETicaretDbContext CreateDbContext(string[] args)
        {

            

            DbContextOptionsBuilder<ETicaretDbContext> dbContextOptionsBuilder = new();

            dbContextOptionsBuilder.UseSqlServer(Configuration.ConnectionString);

            return new (dbContextOptionsBuilder.Options); 
        }
    }
}
