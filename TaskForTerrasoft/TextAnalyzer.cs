using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskForTerrasoft.Abstract;

namespace TaskForTerrasoft
{
    class TextAnalyzer
    {
        private ITextReader _reader;
        private List<MetricsBase> _metrics;

        public IReadOnlyList<MetricsBase> Metrics => _metrics;

        public TextAnalyzer(ITextReader reader)
        {
            _reader = reader;
            _metrics = new List<MetricsBase>();
        }

        public void AddMetrics(MetricsBase metrics)
        {
            _reader.StringReader += metrics.Process;
            _metrics.Add(metrics);
        }

        public void Analyz()
        {
            _reader.Read();
        }
    }
}
