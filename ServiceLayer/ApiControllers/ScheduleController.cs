﻿using System.Collections.Generic;
using System.Web.Http;
using EndToEnd.BusinessLayer;
using EndToEnd.DataLayer.Models;

namespace ServiceLayer.ApiControllers
{
    public class ScheduleController : ApiController
    {
        [HttpGet]
        [ActionName("list")]
        public IList<Schedule> GetScheduleList()
        {
            return SessionRepository.Instance.GetScheduleList();
        }

        [HttpGet]
        [ActionName("list")]
        public Schedule GetScheduleById(int id)
        {
            return SessionRepository.Instance.GetScheduleBySessionId(id);
        }

        [HttpPost]
        [ActionName("list")]
        public Schedule AddSchedule(int id, Schedule schedule)
        {
            return SessionRepository.Instance.AddScheduleToSession(id, schedule);
        }

        [HttpDelete]
        [ActionName("list")]
        public void DeleteSchedule(int id)
        {
            SessionRepository.Instance.DeleteScheduleForSession(id);
        }

        [HttpGet]
        [ActionName("slots/list")]
        public IList<Slot> AllSlots()
        {
            return SessionRepository.Instance.AvailableSlots();
        }

        public IList<Slot> AllBreaks()
        {
            return SessionRepository.Instance.AvailableSlots(true);
        }

        public IList<Slot> AllAssignableSlots()
        {
            return SessionRepository.Instance.AvailableSlots(false);
        }

        public IList<Slot> GetNotAssignedSlots()
        {
            return SessionRepository.Instance.GetNotAssignedSlots();
        }

        public void UpdateSlot(Slot slot)
        {
            SessionRepository.Instance.UpdateSlot(slot);
        }
    }
}