using System.Collections.Generic;
using System.Web.Http;
using BusinessLogicLayer;
using DataLayer.Model;

namespace ServicesLayer.ApiControllers
{
    public class SpeakersController : ApiController
    {
        private ConferenceManager conferenceManager;

        public SpeakersController()
        {
            conferenceManager = new ConferenceManager();
        }

        [HttpGet]
        [ActionName("list")]
        public IList<Speaker> GetSpeakerList()
        {
            return conferenceManager.GetSpeakerList();
        }

        [HttpGet]
        [ActionName("list")]
        public Speaker GetSpeakerById(int id)
        {
            return conferenceManager.GetSpeakerById(id);
        }

        [HttpGet]
        [ActionName("search")]
        public Speaker SearchSpeakerByName(string name)
        {
            return conferenceManager.SearchSpeakerByName(name);
        }

        [HttpPost]
        [ActionName("list")]
        public Speaker AddSpeaker(Speaker speaker)
        {
            return conferenceManager.AddSpeaker(speaker);
        }

        [HttpPut]
        [ActionName("list")]
        public void UpdateSpeaker(Speaker speaker)
        {
            conferenceManager.UpdateSpeaker(speaker);
        }

        [HttpDelete]
        [ActionName("list")]
        public void DeleteSpeaker(int id)
        {
            conferenceManager.DeleteSpeaker(id);
        }
    }
}