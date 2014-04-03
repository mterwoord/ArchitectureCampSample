using EndToEnd.BusinessLayer;
using EndToEnd.DataLayer.Model;
using EndToEnd.DataLayer.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace ServiceLayer.ApiControllers
{
    public class TracksController : ApiController
    {
        private ConferenceManager conferenceManager;

        public TracksController()
        {
            conferenceManager = new ConferenceManager();
        }

        [HttpGet]
        [ActionName("list")]
        public IList<Track> GetTrackList()
        {
            return conferenceManager.GetTrackList();
        }

        [HttpGet]
        [ActionName("list")]
        public Track GetTrackById(int id)
        {
            return conferenceManager.GetTrackById(id);
        }

        [HttpGet]
        [ActionName("search")]
        public Track SearchTrackByName(string track)
        {
            return conferenceManager.SearchTrackByName(track);
        }

        [HttpGet]
        [ActionName("type")]
        public Track SearchTrackTypeByName(string track)
        {
            return conferenceManager.SearchTrackTypeByName(track);
        }

        [HttpPost]
        [ActionName("list")]
        public Track AddTrack(Track track)
        {
            return conferenceManager.AddTrack(track);
        }

        [HttpPut]
        [ActionName("list")]
        public void UpdateTrack(Track track)
        {
            conferenceManager.UpdateTrack(track);
        }

        [HttpDelete]
        [ActionName("list")]
        public void DeleteTrack(int id)
        {
            conferenceManager.DeleteTrack(id);
        }
    }
}