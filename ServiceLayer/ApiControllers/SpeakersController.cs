using System.Collections.Generic;
using System.Web.Http;
using EndToEnd.BusinessLayer;
using EndToEnd.DataLayer.Models;

namespace ServiceLayer.ApiControllers
{
    public class SpeakersController : ApiController
    {
        [HttpGet]
        [ActionName("list")]
        public IList<Speaker> GetSpeakerList()
        {
            return SessionRepository.Instance.GetSpeakerList();
        }

        [HttpGet]
        [ActionName("list")]
        public Speaker GetSpeakerById(int id)
        {
            return SessionRepository.Instance.GetSpeakerById(id);
        }

        [HttpGet]
        [ActionName("search")]
        public Speaker SearchSpeakerByName(string name)
        {
            return SessionRepository.Instance.SearchSpeakerByName(name);
        }

        [HttpPost]
        [ActionName("list")]
        public Speaker AddSpeaker(Speaker speaker)
        {
            return SessionRepository.Instance.AddSpeaker(speaker);
        }

        [HttpPut]
        [ActionName("list")]
        public void UpdateSpeaker(Speaker speaker)
        {
            SessionRepository.Instance.UpdateSpeaker(speaker);
        }

        [HttpDelete]
        [ActionName("list")]
        public void DeleteSpeaker(int id)
        {
            SessionRepository.Instance.DeleteSpeaker(id);
        }
    }
}