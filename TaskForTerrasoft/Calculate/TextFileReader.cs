using System;
using System.IO;
using System.Text;
using TaskForTerrasoft.Abstract;

namespace TaskForTerrasoft.Calculate
{
    class TextFileReader : ITextReader
    {
        private string _path;

        public event Action<string> StringReader;

        public TextFileReader(string path)
        {
            if (!File.Exists(path))
                throw new ArgumentException("File not exist", nameof(path));

            _path = path;
        }

        public void Read()
        {
            using (FileStream fs = File.Open(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs,Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    StringReader?.Invoke(line);
                }
            }
        }
    }
}

