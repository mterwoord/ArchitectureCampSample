using System.Collections.Generic;
using System.Web.Http;
using BusinessLayer.Models;
using EndToEnd.BusinessLayer;

namespace ServiceLayer.ApiControllers
{
    public class SessionsController : ApiController
    {
        [HttpGet]
        [ActionName("list")]
        public IList<SessionDto> GetSessionList()
        {
            return SessionRepository.Instance.GetSessionList();
        }

        [HttpGet]
        [ActionName("list")]
        public SessionDto GetSessionById(int id)
        {
            return SessionRepository.Instance.GetSessionById(id);
        }

        [HttpGet]
        [ActionName("search")]
        public SessionDto SearchSessionByTitle(string title)
        {
            return SessionRepository.Instance.SearchSessionByTitle(title);
        }

        [HttpGet]
        [ActionName("types")]
        public IEnumerable<string> GetSessionTypes()
        {
            return SessionRepository.Instance.GetSessionTypes();
        }

        [HttpPost]
        [ActionName("list")]
        public SessionDto AddSession(SessionDto session)
        {
            return SessionRepository.Instance.AddSession(session);
        }

        [HttpPut]
        [ActionName("list")]
        public void UpdateSession(SessionDto session)
        {
            SessionRepository.Instance.UpdateSession(session);
        }

        [HttpDelete]
        [ActionName("list")]
        public void DeleteSession(int id)
        {
            SessionRepository.Instance.DeleteSession(id);
        }
    }
}