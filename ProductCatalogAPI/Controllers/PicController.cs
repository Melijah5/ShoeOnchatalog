using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductCatalogAPI.Controllers
{
    //just a data send back ... json
    [Produces("application/json")]
    [Route("api/Pic")]
   
    public class PicController : ControllerBase
    {

        private readonly IHostingEnvironment _env;

        //constractor
        // hosting environment //like a folder
        //toget wwwroot folder
        //IhostingEnvironment- location of hosting
        public PicController(IHostingEnvironment env) 
        {
            _env = env;
        }

        // defualt action in the controller
        // take this variable and put in to id paramater
        //method getImage
        [Route("{id}")]
        public IActionResult GetImage(int id)
        {
           var webRoot= _env.WebRootPath;
            //using ststem.IO
           var path = Path.Combine(webRoot + "/pics/", "shoes-" + id + ".png");
            // open the path and Read a file
           var buffer = System.IO.File.ReadAllBytes(path); 
            // buffer is a temporary space
           return File(buffer, "image/png");
        }
    }
}