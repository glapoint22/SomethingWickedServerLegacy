using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace SomethingWickedServerLegacy.Controllers
{
    public class MembersController : ApiController
    {
        private Something_WickedEntities db = new Something_WickedEntities();

        public async Task<IHttpActionResult> Get()
        {
            var members = await db.Members
                .Select(m => new
                {
                    name = m.Name,
                    thumbnail = m.Thumbnail
                })
                .AsNoTracking()
                .ToListAsync();

            return Ok(members);
        }
    }
}
