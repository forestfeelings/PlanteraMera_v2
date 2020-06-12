using PlanteraMera_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanteraMera_v2.ViewModels
{
    public class SeedViewModel
    {
        public List<Seed> Seeds { get; set; }
        public SeedModel Model { get; set; }
    }

    public class SeedModel
    {
        public Guid SeedId { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public string BotanicalFamily { get; set; }
        public int DaysToDevelop { get; set; }
        public string Annuality { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int HeightCm { get; set; }
        public decimal Price { get; set; }
        public bool IsBeginnerSeed { get; set; }
    }
}
