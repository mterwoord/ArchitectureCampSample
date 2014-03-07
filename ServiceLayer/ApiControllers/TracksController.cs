using System.Collections.Generic;
using System.Web.Http;
using EndToEnd.BusinessLayer;
using EndToEnd.DataLayer.Models;

namespace ServiceLayer.ApiControllers
{
    public class TracksController : ApiController
    {
        [HttpGet]
        [ActionName("list")]
        public IList<Track> GetTrackList()
        {
            return SessionRepository.Instance.GetTrackList();
        }

        [HttpGet]
        [ActionName("list")]
        public Track GetTrackById(int id)
        {
            return SessionRepository.Instance.GetTrackById(id);
        }

        [HttpGet]
        [ActionName("search")]
        public Track SearchTrackByName(string track)
        {
            return SessionRepository.Instance.SearchTrackByName(track);
        }

        [HttpGet]
        [ActionName("type")]
        public Track SearchTrackTypeByName(string track)
        {
            return SessionRepository.Instance.SearchTrackTypeByName(track);
        }

        [HttpPost]
        [ActionName("list")]
        public Track AddTrack(Track track)
        {
            return SessionRepository.Instance.AddTrack(track);
        }

        [HttpPut]
        [ActionName("list")]
        public void UpdateTrack(Track track)
        {
            SessionRepository.Instance.UpdateTrack(track);
        }

        [HttpDelete]
        [ActionName("list")]
        public void DeleteTrack(int id)
        {
            SessionRepository.Instance.DeleteTrack(id);
        }
    }
}