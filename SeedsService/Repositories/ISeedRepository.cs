using SeedsService.Models;
using System;
using System.Collections.Generic;

namespace SeedsService.Repositories
{
    public interface ISeedRepository
    {
        public Seed GetById(Guid id);

        public IEnumerable<Seed> GetAll();

        public Seed Create(Seed seed);

        public bool Delete(Guid id);
    }
}