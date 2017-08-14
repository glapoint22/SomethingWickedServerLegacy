using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SomethingWickedServerLegacy.Controllers
{
    public class PhotosController : ApiController
    {
        public async Task<IHttpActionResult> Get(string id)
        {
            string content = await Facebook.GetContent(id, "name,photos{images}");
            JObject jObject = JObject.Parse(content);

            var facebookContent = new
            {
                title = (string)jObject.SelectToken("name"),
                data = jObject.SelectToken("photos.data")
                    .Select(a => new
                    {
                        id = (string)a["id"],
                        content = a["images"]
                            .Select(b => (string)b["source"])
                            .FirstOrDefault()
                    }
                    )
                    .ToArray()
            };

            if (facebookContent == null)
            {
                return NotFound();
            }

            return Ok(facebookContent);

        }
    }
}
