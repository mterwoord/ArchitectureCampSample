using System.Collections.Generic;
using System.Web.Http;
using EndToEnd.BusinessLayer;
using EndToEnd.DataLayer.Models;

namespace ServiceLayer.ApiControllers
{
    public class ScheduleController : ApiController
    {
        private ConferenceManager conferenceManager;

        public ScheduleController()
        {
            conferenceManager = new ConferenceManager();
        }

        [HttpGet]
        [ActionName("list")]
        public IList<Schedule> GetScheduleList()
        {
            return conferenceManager.GetScheduleList();
        }

        [HttpGet]
        [ActionName("list")]
        public Schedule GetScheduleById(int id)
        {
            return conferenceManager.GetScheduleBySessionId(id);
        }

        [HttpPost]
        [ActionName("list")]
        public Schedule AddSchedule(int id, Schedule schedule)
        {
            return conferenceManager.AddScheduleToSession(id, schedule);
        }

        [HttpDelete]
        [ActionName("list")]
        public void DeleteSchedule(int id)
        {
            conferenceManager.DeleteScheduleForSession(id);
        }
    }
}