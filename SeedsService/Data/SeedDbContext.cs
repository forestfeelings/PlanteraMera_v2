using Microsoft.EntityFrameworkCore;
using SeedsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedsService.Data
{
    public class SeedDbContext : DbContext
    {
        public SeedDbContext(DbContextOptions<SeedDbContext> options) : base(options)
        {

        }

        public DbSet<Seed> Seeds { get; set; }
    }
}
