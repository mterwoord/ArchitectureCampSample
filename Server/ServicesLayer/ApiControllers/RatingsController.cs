using System.Collections.Generic;
using System.Web.Http;
using BusinessLogicLayer;
using DataLayer.Model;
using Microsoft.AspNet.SignalR;

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
            GlobalHost.ConnectionManager.GetHubContext<RatingsHub>().Clients.All.ratingUpdated(rating.Id);

            return conferenceManager.AddRating(rating);
        }

        [HttpPut]
        [ActionName("list")]
        public void UpdateRating(Rating rating)
        {
            GlobalHost.ConnectionManager.GetHubContext<RatingsHub>().Clients.All.ratingUpdated(rating.Id);

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