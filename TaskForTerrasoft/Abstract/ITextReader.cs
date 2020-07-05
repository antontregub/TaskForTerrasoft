using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskForTerrasoft.Abstract
{
    interface ITextReader
    {
        event Action<string> StringReader;
        void Read();
    }
}
