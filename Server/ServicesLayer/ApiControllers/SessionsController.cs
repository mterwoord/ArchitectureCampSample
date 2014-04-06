using System.Collections.Generic;
using System.Web.Http;
using BusinessLogicLayer;

namespace ServicesLayer.ApiControllers
{
    public class SessionsController : ApiController
    {
        private ConferenceManager conferenceManager;

        public SessionsController()
        {
            conferenceManager = new ConferenceManager();
        }

        [HttpGet]
        [ActionName("list")]
        public IList<SessionDto> GetSessionList()
        {
            return conferenceManager.GetSessionList();
        }

        [HttpGet]
        [ActionName("list")]
        public SessionDto GetSessionById(int id)
        {
            return conferenceManager.GetSessionById(id);
        }

        [HttpGet]
        [ActionName("search")]
        public SessionDto SearchSessionByTitle(string title)
        {
            return conferenceManager.SearchSessionByTitle(title);
        }

        [HttpGet]
        [ActionName("types")]
        public IEnumerable<string> GetSessionTypes()
        {
            return conferenceManager.GetSessionTypes();
        }

        [HttpPost]
        [ActionName("list")]
        public SessionDto AddSession(SessionDto session)
        {
            return conferenceManager.AddSession(session);
        }

        [HttpPut]
        [ActionName("list")]
        public void UpdateSession(SessionDto session)
        {
            conferenceManager.UpdateSession(session);
        }

        [HttpDelete]
        [ActionName("list")]
        public void DeleteSession(int id)
        {
            conferenceManager.DeleteSession(id);
        }
    }
}