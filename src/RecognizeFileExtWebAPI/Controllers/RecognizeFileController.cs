using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecognizeFileExtensionBL;
using RecognizerPlugin;

namespace RecognizeFileExtWebAPI.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class RecognizeFileController : ControllerBase
    {
        private readonly ILogger<RecognizeFileController> _logger;

        public RecognizeFileController(ILogger<RecognizeFileController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IEnumerable<string> GetExtensionsRecognized()
        {
            var all = new RecognizePlugins();
            return all.AllExtensions();
        } 
        
        [HttpPost]
        public IEnumerable<string> PossibleExtensions(byte[] fileContent)
        {
            var all = new RecognizePlugins();
            return all.PossibleExtensions(fileContent);
        }
        [HttpPost]
        public Recognize IsCorrectExtensionSendByte(string extension, byte[] fileContent)
        {
            var all = new RecognizePlugins();
            return all.RecognizeTheFile(fileContent,extension);
        }
        [HttpPost]
        public async Task<Recognize> IsCorrectExtensionSendFile(IFormFile file)
        {
            if (file.Length == 0)
                return Recognize.GiveMeMoreInfo;
            byte[] bContent;
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                bContent = stream.ToArray();

            }
            var ext = Path.GetExtension(file.FileName);
            return IsCorrectExtensionSendByte(ext, bContent);
        }
    }
}
