using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SomethingWickedServerLegacy.Controllers
{
    public class VideosController : ApiController
    {
        public async Task<IHttpActionResult> Get(string id)
        {
            string content = await Facebook.GetContent(id, "title,videos{id}");
            JObject jObject = JObject.Parse(content);

            var facebookContent = new
            {
                title = (string)jObject.SelectToken("title"),
                data = jObject.SelectToken("videos.data")
                    .Select(a => new
                    {
                        id = (string)a["id"],
                        content = ""
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
