using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Magick_Docker3_1.Controllers
{
    [Route("ImageTest")]
    public class ImageTestController : Controller
    {
        private string testfile = null;
        private string nativeidr = null;

        public ImageTestController(IWebHostEnvironment hostingEnvironment)
        {
            this.testfile = System.IO.Path.Combine(hostingEnvironment.ContentRootPath, "Sources", "test.jpg");
            this.nativeidr = System.IO.Path.Combine(hostingEnvironment.ContentRootPath, @"bin\Debug\netcoreapp3.1\runtimes\win-x64\native");
             
        }

        [HttpGet("Test1")]
        public IActionResult Test1()
        {
            ImageMagick.MagickImage img = new ImageMagick.MagickImage(this.testfile);

            img.Scale(100, 100);

            byte[] result = img.ToByteArray();

            return File(result, "image/jpeg");
        }


        [HttpGet("Test2")]
        public IActionResult Test2()
        {
            // This time set native dir
            ImageMagick.MagickNET.SetNativeLibraryDirectory(this.nativeidr);
            ImageMagick.MagickImage img = new ImageMagick.MagickImage(this.testfile);

            img.Scale(100, 100);

            byte[] result = img.ToByteArray();

            return File(result, "image/jpeg");
        }


    }
}
