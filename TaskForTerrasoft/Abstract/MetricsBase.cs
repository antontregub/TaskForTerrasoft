using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskForTerrasoft.Abstract
{
    public abstract class MetricsBase
    {
        public abstract void Process(string line);
        public abstract Dictionary<string, string> Result();
    }
}
