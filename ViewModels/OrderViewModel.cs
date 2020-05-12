using PlanteraMera_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanteraMera_v2.ViewModels
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public ApplicationUser User { get; set; }
    }
}
