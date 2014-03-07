using System.Collections.Generic;
using System.Web.Http;
using EndToEnd.BusinessLayer;
using EndToEnd.DataLayer.Models;

namespace ServiceLayer.ApiControllers
{
    public class SlotsController : ApiController
    {
        [HttpGet]
        [ActionName("list")]
        public IList<Slot> AllSlots()
        {
            return SessionRepository.Instance.AvailableSlots();
        }

        [HttpGet]
        [ActionName("breaks")]
        public IList<Slot> AllBreaks()
        {
            return SessionRepository.Instance.AvailableSlots(true);
        }

        [HttpGet]
        [ActionName("assignable")]
        public IList<Slot> AllAssignableSlots()
        {
            return SessionRepository.Instance.AvailableSlots(false);
        }

        [HttpGet]
        [ActionName("notassigned")]
        public IList<Slot> GetNotAssignedSlots()
        {
            return SessionRepository.Instance.GetNotAssignedSlots();
        }

        [HttpPut]
        [ActionName("list")]
        public void UpdateSlot(Slot slot)
        {
            SessionRepository.Instance.UpdateSlot(slot);
        }
    }
}