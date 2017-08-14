using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SomethingWickedServerLegacy;
using System.Threading.Tasks;
using System.Data.Entity.SqlServer;

namespace SomethingWickedServerLegacy.Controllers
{
    public class DataController : ApiController
    {
        private Something_WickedEntities db = new Something_WickedEntities();

        public async Task<IHttpActionResult> Get()
        {
            //Get the shows from the database
            var showsDB = await db.Schedule.Include(s => s.Venues)
                    .Where(x => DbFunctions.TruncateTime(x.DateTime) > DbFunctions.TruncateTime(DateTime.Now))
                    .Select(x => new
                    {
                        dateTime = x.DateTime,
                        Duration = x.Duration,
                        venue = new
                        {
                            name = x.Venues.Name,
                            location = x.Venues.Location,
                            url = x.Venues.URL
                        }
                    })
                    .AsNoTracking().ToListAsync();

            var data = new
            {
                //Showcase Images
                showcaseImages = await db.ShowcaseImages
                    .Select(m => new
                    {
                        name = m.Name,
                    })
                    .AsNoTracking()
                    .ToListAsync(),

                //Shows
                shows = showsDB.Select(x => new {
                    date = x.dateTime.ToString("MMMM dd"),
                    time = x.dateTime.ToString("h:mm tt") + " - " + x.dateTime.AddHours(x.Duration).ToString("h:mm tt"),
                    venue = x.venue
                }),

                //Songs
                songs = await db.Songs.Include(s => s.Genres)
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
                    .ToListAsync(),

                //Members
                members = await db.Members
                    .Select(m => new
                    {
                        name = m.Name,
                        thumbnail = m.Thumbnail
                    })
                    .AsNoTracking()
                    .ToListAsync()
            };


            return Ok(data);
        }

    }
}