using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BrightAssistant.ChartAnalyzer.Server
{
    public class HomeController : Controller
    {
        private static string _processedIndexFile;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult Index()
        {
            return ProcessAndReturnIndexFile();
        }

        private IActionResult ProcessAndReturnIndexFile()
        {
            if (_processedIndexFile == null)
            {
                IFileInfo file = _webHostEnvironment.WebRootFileProvider.GetFileInfo("index.html");
                _processedIndexFile = System.IO.File.ReadAllText(file.PhysicalPath);
                _processedIndexFile = _processedIndexFile.Replace("{version}", DateTime.Now.ToString("yyyy'-'MM'-'dd'-'HH'-'mm'-'ss"));
            }
            return Content(_processedIndexFile, "text/html");
        }
    }

}

