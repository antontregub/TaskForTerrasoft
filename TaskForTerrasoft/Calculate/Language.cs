using System.Collections.Generic;
using TaskForTerrasoft.Abstract;

namespace TaskForTerrasoft.Calculate
{
    /// <summary>Definition of language</summary>
    public class Language : MetricsBase
    {
        private string _language;

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

            if (angl_count > rus_count) _language = "English";
            if (angl_count < rus_count) _language = "Russian";
            if (ukr_count > 0) _language = "Ukraine";
        }

        public override Dictionary<string, string> Result()
        {
            var _result = new Dictionary<string, string>();
            _result.Add("Language:", $"{_language}");
            return _result;
        }
    }
}

