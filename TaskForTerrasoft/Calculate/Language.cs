using System.Collections.Generic;
using System.Linq;
using TaskForTerrasoft.Abstract;

namespace TaskForTerrasoft.Calculate
{
    /// <summary>Definition of language</summary>
    public class Language : MetricsBase
    {
        private Dictionary<string, int> _language = new Dictionary<string, int>
        {
            {  "English", 0},
            {  "Russian", 0},
            {  "Ukraine", 0},
        };

        public override void Process(string line)
        {
            byte[] b = System.Text.Encoding.Default.GetBytes(line);

            int angl_count = 0, rus_count = 0, ukr_count = 0;

            foreach (byte bt in b)
            {
                if ((bt >= 97) && (bt <= 122)) angl_count++;
                if ((bt >= 192) && (bt <= 239)) rus_count++;
                if ((bt == 180) || (bt == 191) || (bt ==179)) ukr_count++;

            }

            if (angl_count > rus_count) _language["English"]= _language["English"]+1;
            if (angl_count < rus_count) _language["Russian"] = _language["Russian"] + 1;
            if (ukr_count > 0) _language["Ukraine"] = _language["Ukraine"] + 1;
        }

        public override Dictionary<string, string> Result()
        {
            var max = _language.FirstOrDefault(x => x.Value == _language.Values.Max()).Key;
            var _result = new Dictionary<string, string>();
            _result.Add("Language:", $"{max}");
            return _result;
        }
    }
}

