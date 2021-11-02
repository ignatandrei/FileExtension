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
    public partial class RecognizeFileController : ControllerBase
    {
        private readonly ILogger<RecognizeFileController> _logger;
        private readonly RecognizePlugins all;

        public RecognizeFileController(ILogger<RecognizeFileController> logger, RecognizePlugins all)
        {
            _logger = logger;
            this.all = all;
        }
        [HttpGet]
        public IEnumerable<string> GetExtensionsRecognized()
        {
            
            return all.AllExtensions();
        } 
        
        [HttpPost]
        public IEnumerable<string> PossibleExtensions(byte[] fileContent)
        {
            return all.PossibleExtensions(fileContent);
        }
        [HttpPost]
        public Recognize IsCorrectExtensionSendByte(string extension, byte[] fileContent)
        {
            return all.RecognizeTheFile(fileContent,extension);
        }
        private async Task<byte[]> GetFileContents(IFormFile file)
        {
            if ((file?.Length ?? 0) == 0)
                return null;
            byte[] bContent;
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                bContent = stream.ToArray();

            }
            return bContent;
        }
        [HttpPost]
        public async Task<Recognize> IsCorrectExtensionSendFile(IFormFile file)
        {
            var bContent =await  GetFileContents(file);
            if (bContent == null)
                return Recognize.GiveMeMoreInfo;
            
            var ext = Path.GetExtension(file.FileName);
            return IsCorrectExtensionSendByte(ext, bContent);
        }
    }
}
