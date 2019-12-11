using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecognizeFileExtensionBL;
using RecognizerPlugin;

namespace RecognizeFileExtWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RecognizeFileController : ControllerBase
    {
        private readonly ILogger<RecognizeFileController> _logger;

        public RecognizeFileController(ILogger<RecognizeFileController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IEnumerable<string> GetExtensions()
        {
            var all = new RecognizePlugins();
            return all.AllExtensions();
        } 
        [HttpPost]
        public IEnumerable<string> PossibleExtensions(byte[] file)
        {
            var all = new RecognizePlugins();
            return all.PossibleExtensions(file);

        }
        [HttpPost]
        public Recognize IsCorrectExtension(string extension, byte[] file)
        {
            var all = new RecognizePlugins();
            return all.RecognizeTheFile(file,extension);
        }
    }
}
