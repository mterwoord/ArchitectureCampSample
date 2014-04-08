using System.Collections.Generic;
using System.Web.Http;
using BusinessLogicLayer;
using DataLayer.Model;
using Microsoft.AspNet.SignalR;
using ServicesLayer.Hubs;

namespace ServicesLayer.ApiControllers
{
    public class RatingsController : ApiController
    {
        private IConferenceManager conferenceManager;

        public RatingsController(IConferenceManager manager)
        {
            conferenceManager = manager;
        }

        [HttpGet]
        [ActionName("list")]
        public IList<Rating> GetRatingList()
        {
            return conferenceManager.GetRatingList();
        }

        [HttpGet]
        [ActionName("list")]
        public Rating GetRatingById(int id)
        {
            return conferenceManager.GetRatingById(id);
        }

        [HttpPost]
        [ActionName("list")]
        public Rating AddRating(Rating rating)
        {
            GlobalHost.ConnectionManager.GetHubContext<RatingsHub>().Clients.All.ratingUpdated(
                new RatingUpdate(){ SpeakerId = rating.SpeakerId,SessionId = rating.SessionId });

            return conferenceManager.AddRating(rating);
        }

        [HttpPut]
        [ActionName("list")]
        public void UpdateRating(Rating rating)
        {
            GlobalHost.ConnectionManager.GetHubContext<RatingsHub>().Clients.All.ratingUpdated(
                new RatingUpdate() { SpeakerId = rating.SpeakerId, SessionId = rating.SessionId });

            conferenceManager.UpdateRating(rating);
        }

        [HttpDelete]
        [ActionName("list")]
        public void DeleteRating(int id)
        {
            conferenceManager.DeleteRating(id);
        }
    }
}