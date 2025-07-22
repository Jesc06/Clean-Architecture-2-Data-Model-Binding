using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using testing.Domain.Entities; 

namespace testing.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<InformationDomain> Information { get; set; }
        public DbSet<HobbyDomain> Hobby { get; set; }
    }
}
