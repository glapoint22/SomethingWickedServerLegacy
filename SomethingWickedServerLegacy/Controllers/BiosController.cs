using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;

namespace SomethingWickedServerLegacy.Controllers
{
    public class BiosController : ApiController
    {
        private Something_WickedEntities db = new Something_WickedEntities();

        // GET: api/Bios/Amy
        public async Task<IHttpActionResult> Get(string id)
        {
            var bio = await db.Bios
                .Where(b => b.Members.Name == id)
                    .Select(a => new {
                        title = a.Members.Name,
                        bio = a.Bio,
                        thumbnail = a.Members.Thumbnail
                    })
                .SingleOrDefaultAsync();

            if (bio == null)
            {
                return NotFound();
            }

            return Ok(bio);
        }
    }
}