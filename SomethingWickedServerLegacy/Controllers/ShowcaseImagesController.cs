using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SomethingWickedServerLegacy.Controllers
{
    public class ShowcaseImagesController : ApiController
    {
        public IHttpActionResult Get()
        {
            string dir = HttpRuntime.AppDomainAppPath + "assets\\images\\showcaseImages\\";
            string[] imgs = Directory.GetFiles(dir);

            //Showcase Images
            var showcaseImages = imgs
                .Select(x => new
                {
                    name = Path.GetFileName(x)
                })
                .ToList();
            
            return Ok(showcaseImages);
        }
    }
}
