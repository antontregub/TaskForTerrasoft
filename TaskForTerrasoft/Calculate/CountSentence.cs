using System.Collections.Generic;
using TaskForTerrasoft.Abstract;

namespace TaskForTerrasoft.Calculate
{
    /// <summary>Number of exclamation sentences</summary>
    public class CountSentence : MetricsBase
    {
        private int _counter = 0;

        public override void Process(string line)
        {
            //filter out tapes consisting of 2 characters
            if (line.Length > 2) {
                for (int i = 1; i < line.Length; i++)
                {
                    // filter not last exclamation sentence
                    if (i + 1 < line.Length)
                    {
                        if (line[i] == '!' && line[i - 1] != ' ' && line[i + 1] == ' ')
                        {
                            _counter++;
                        }
                    }
                    // last exclamation sentence
                    else if (line[i] == '!' && line[i - 1] != ' ' )
                    {
                        _counter++;
                    }

                }
            }
        }

        public override Dictionary<string, string> Result()
        {
            var _result = new Dictionary<string, string>();
            _result.Add("Count of exclamation sentences:", $"{_counter}");
            return _result;
        }
    }
}

