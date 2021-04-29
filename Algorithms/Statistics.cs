using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP717.Algorithms {
    public class Statistics {
        public List<long> OperationTimes = new List<long>();

        public long AverageOperationTime => OperationTimes.Sum() / OperationTimes.Count();
        public long MaxOperationTime = 0;
        public long MinOperationTime = 0;

        public int Wins = 0;
        public int Losses = 0;
        public int Ties = 0;

    }
}
