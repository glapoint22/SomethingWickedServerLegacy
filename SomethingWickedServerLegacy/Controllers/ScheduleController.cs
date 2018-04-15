using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace SomethingWickedServerLegacy.Controllers
{
    public class ScheduleController : ApiController
    {
        private Something_WickedEntities db = new Something_WickedEntities();

        public async Task<IHttpActionResult> Get()
        {
            var showsDB = await db.Schedule
                .Where(x => DbFunctions.TruncateTime(x.DateTime) >= DbFunctions.TruncateTime(DateTime.Now))
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
                .AsNoTracking()
                .ToListAsync();

            //Shows
            var shows = showsDB.Select(x => new
            {
                date = x.dateTime.ToString("MMMM dd"),
                time = x.dateTime.ToString("h:mm tt") + " - " + x.dateTime.AddHours(x.Duration).ToString("h:mm tt"),
                venue = x.venue
            });

            return Ok(shows);
        }
    }
}
