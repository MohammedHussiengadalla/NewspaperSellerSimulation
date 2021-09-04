using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperSellerModels
{
    public class DemandDistribution
    {
        public DemandDistribution()
        {
            DayTypeDistributions = new List<DayTypeDistribution>();
        }
       
        public int Demand { get; set; }
        public List<DayTypeDistribution> DayTypeDistributions { get; set; }
    }
}

