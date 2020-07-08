using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TaskForTerrasoft.Abstract;

namespace TaskForTerrasoft.Calculate
{
    /// <summary>Most common word</summary>
    public class MostCommonWorld : MetricsBase
    {
        private Dictionary<string, int> _world = new Dictionary<string, int>();

        public override void Process(string line)
        {
            line = line.ToLower();
            string pattern = "[-—''»«.,(){}@#$%^&*!+=:;/\\[\\]]+";
            string replacement = "";
            Regex rgx = new Regex(pattern);
            line = rgx.Replace(line, replacement);


            string[] lineMass;
            lineMass = line.Split(' ');

            for (int i = 0; i < lineMass.Length; i++)
            {
                if (lineMass[i] != "")
                {
                    if (_world.ContainsKey(lineMass[i]))
                    {
                        _world[lineMass[i]]++;
                    }
                    else
                    {
                        _world.Add(lineMass[i], 1);
                    }
                }
            }
        }

        public override Dictionary<string, string> Result()
        {
            var item = _world.OrderByDescending(kv => kv.Value).FirstOrDefault();
            var _result = new Dictionary<string, string>();
            _result.Add($"Most common word '{item.Key}'", $"{item.Value}");
            return _result;
        }
    }
}

