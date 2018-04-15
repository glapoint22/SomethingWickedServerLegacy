using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace SomethingWickedServerLegacy.Controllers
{
    public class SongsController : ApiController
    {
        private Something_WickedEntities db = new Something_WickedEntities();

        public async Task<IHttpActionResult> Get()
        {
            var songs = await db.Songs
                .OrderBy(s => s.Song)
                .Select(s => new
                {
                    name = s.Song,
                    genre = s.Genres.Genre,
                    artist = s.Artist,
                    videoGroup = s.VideoGroup,
                    videoID = s.VideoID == null ? "z" : s.VideoID
                })
                .AsNoTracking()
                .ToListAsync();

            return Ok(songs);
        }
    }
}
