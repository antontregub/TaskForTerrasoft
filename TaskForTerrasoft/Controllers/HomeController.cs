using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using TaskForTerrasoft.Abstract;
using TaskForTerrasoft.Calculate;
using TaskForTerrasoft.Models;

namespace TaskForTerrasoft.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddFile(IFormFile uploadedFile)
        {
            string path;
            var res = new List<Results>();
            if (uploadedFile != null)
            {
                var a = Assembly.GetExecutingAssembly().Location;
                a = a.Substring(0, a.Length - 45);
                a = a + "/Files/";

                // create directory if not exist
                if (!Directory.Exists(a))
                {
                    Directory.CreateDirectory(a);
                }

                path = a + uploadedFile.FileName;

                // save file
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    uploadedFile.CopyToAsync(fileStream);
                }

                res = CalculateMetrics(path);
                return View(res);
            }
            else
                return RedirectToAction("Index");
        }

        private List<Results> CalculateMetrics(string path)
        {
            var res = new List<Results>();
            ITextReader reader1 = new TextFileReader(path);

            TextAnalyzer textAnalyzer = new TextAnalyzer(reader1);
            textAnalyzer.AddMetrics(new MostCommonCharacter());
            textAnalyzer.AddMetrics(new Language());
            textAnalyzer.AddMetrics(new MostCommonWorld());
            textAnalyzer.AddMetrics(new MostCommonNotSimpleWord());
            textAnalyzer.AddMetrics(new CountSentence());
            textAnalyzer.Analyz();

            foreach (MetricsBase metrics in textAnalyzer.Metrics)
            {
                res.Add(new Results { Name = metrics.Result().Keys.FirstOrDefault(), Value = metrics.Result().Values.FirstOrDefault() });
            }

            return res;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
