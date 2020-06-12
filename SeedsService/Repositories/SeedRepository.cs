using SeedsService.Data;
using SeedsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeedsService.Repositories
{
    /* Checking */
    public class SeedRepository : ISeedRepository
    {
        private readonly SeedDbContext _context;

        public SeedRepository(SeedDbContext context)
        {
            _context = context;
        }

        public Seed Create(Seed seed)
        {
            _context.Seeds.Add(seed);
            _context.SaveChanges();

            return seed;
        }

        public bool Delete(Guid id)
        {
            try
            {
                var seed = GetById(id);
                _context.Seeds.Remove(seed);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Seed GetById(Guid id)
        {
            var seed = _context.Seeds.FirstOrDefault(x => x.SeedId == id);

            return seed;
        }

        public IEnumerable<Seed> GetAll()
        {
            var seeds = _context.Seeds.ToList();

            return seeds;
        }
    }
}
