using System.Collections.Generic;
using System.ServiceModel;
using System.Web.Http;
using System.Web.Http.Controllers;
using EndToEnd.BusinessLayer;
using EndToEnd.DataLayer.Models;

namespace ServiceLayer.ApiControllers
{
    public class RatingsController : ApiController
    {
        [HttpGet]
        [ActionName("list")]
        public IList<Rating> GetRatingList()
        {
            return SessionRepository.Instance.GetRatingList();
        }

        [HttpGet]
        [ActionName("list")]
        public Rating GetRatingById(int id)
        {
            return SessionRepository.Instance.GetRatingById(id);
        }

        [HttpPost]
        [ActionName("list")]
        public Rating AddRating(Rating rating)
        {
            return SessionRepository.Instance.AddRating(rating);
        }

        [HttpPut]
        [ActionName("list")]
        public void UpdateRating(Rating rating)
        {
            SessionRepository.Instance.UpdateRating(rating);
        }

        [HttpDelete]
        [ActionName("list")]
        public void DeleteRating(int id)
        {
            SessionRepository.Instance.DeleteRating(id);
        }
    }
}