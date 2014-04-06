using System.Collections.Generic;
using System.Web.Http;
using BusinessLogicLayer;
using DataLayer.Model;

namespace ServicesLayer.ApiControllers
{
    public class RatingsController : ApiController
    {
        private ConferenceManager conferenceManager;

        public RatingsController()
        {
            conferenceManager = new ConferenceManager();
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
            return conferenceManager.AddRating(rating);
        }

        [HttpPut]
        [ActionName("list")]
        public void UpdateRating(Rating rating)
        {
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