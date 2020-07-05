using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TaskForTerrasoft.Abstract;

namespace TaskForTerrasoft.Calculate
{
    /// <summary>Most common character</summary>
    public class MostCommonCharacter : MetricsBase
    {
        private Dictionary<char, int> _symbols = new Dictionary<char, int>();

        public override void Process(string line)
        {
            line=line.ToLower();

            // filter string by regular
            string pattern = "[-—''»«.,(){}@#$%^&*!+=:;/\\[\\]]+";
            string replacement = "";
            Regex rgx = new Regex(pattern);
            line = rgx.Replace(line, replacement);

            line = line.Trim('"');
            line = line.Trim('"');
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i]!=' ')
                {

                    if (_symbols.ContainsKey(line[i]))
                    {
                        _symbols[line[i]]++;
                    }
                    else
                        _symbols.Add(line[i], 1);
                }
            }
        }

        public override Dictionary<string, string> Result()
        {
            var item = _symbols.OrderByDescending(kv => kv.Value).FirstOrDefault();
            var _result =  new Dictionary<string, string>();
            _result.Add($"Most common character '{item.Key}':", $"{item.Value}");
            return _result;
        }
    }
}

