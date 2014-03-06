using BusinessLayer.Models;
using EndToEnd.BusinessLayer;
using EndToEnd.DataLayer.Models;
using System.Collections.Generic;
using System.ServiceModel;

namespace ServiceLayer {
  // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SessionService" in code, svc and config file together.
  // NOTE: In order to launch WCF Test Client for testing this service, please select SessionService.svc or SessionService.svc.cs at the Solution Explorer and start debugging.
  public class SessionService : ISessionService {

    # region Speaker

    public IList<Speaker> GetSpeakerList() {
      return SessionRepository.Instance.GetSpeakerList();
    }

    public Speaker GetSpeakerById(int id) {
      return SessionRepository.Instance.GetSpeakerById(id);
    }

    public Speaker SearchSpeakerByName(string name) {
      return SessionRepository.Instance.SearchSpeakerByName(name);
    }

    public Speaker AddSpeaker(Speaker speaker) {
      return SessionRepository.Instance.AddSpeaker(speaker);
    }

    public void UpdateSpeaker(Speaker speaker) {
      SessionRepository.Instance.UpdateSpeaker(speaker);
    }

    public void DeleteSpeaker(int id) {
      SessionRepository.Instance.DeleteSpeaker(id);
    }

    # endregion

    # region Session

    public IList<SessionDto> GetSessionList() {
      return SessionRepository.Instance.GetSessionList();
    }

    public SessionDto GetSessionById(int id) {
      return SessionRepository.Instance.GetSessionById(id);
    }

    public SessionDto SearchSessionByTitle(string title) {
      return SessionRepository.Instance.SearchSessionByTitle(title);
    }

    public SessionDto AddSession(SessionDto session) {      
      return SessionRepository.Instance.AddSession(session);
    }
    public void UpdateSession(SessionDto session) {
      SessionRepository.Instance.UpdateSession(session);
    }

    public void DeleteSession(int id) {
      SessionRepository.Instance.DeleteSession(id);
    }

    public IEnumerable<string> GetSessionTypes() {
      return SessionRepository.Instance.GetSessionTypes();
    }

    # endregion

    # region Tracks

    public IList<Track> GetTrackList() {
      return SessionRepository.Instance.GetTrackList();
    }

    public Track GetTrackById(int id) {
      return SessionRepository.Instance.GetTrackById(id);
    }

    public Track SearchTrackByName(string track) {
      return SessionRepository.Instance.SearchTrackByName(track);
    }

    public Track AddTrack(Track track) {
      return SessionRepository.Instance.AddTrack(track);
    }

    public void UpdateTrack(Track track) {
      SessionRepository.Instance.UpdateTrack(track);
    }

    public void DeleteTrack(int id) {
      SessionRepository.Instance.DeleteTrack(id);
    }

    public Track SearchTrackTypeByName(string track) {
      return SessionRepository.Instance.SearchTrackTypeByName(track);
    }

    # endregion

    # region Rating

    public IList<Rating> GetRatingList() {
      return SessionRepository.Instance.GetRatingList();
    }

    public Rating GetRatingById(int id) {
      return SessionRepository.Instance.GetRatingById(id);
    }

    public Rating AddRating(Rating rating) {
      try {
        return SessionRepository.Instance.AddRating(rating);
      } catch (System.Exception ex) {
        throw new FaultException<FaultMessage>(new FaultMessage(ex), new FaultReason("exception"));
      }
    }
    public void UpdateRating(Rating rating) {
      SessionRepository.Instance.UpdateRating(rating);
    }

    public void DeleteRating(int id) {
      SessionRepository.Instance.DeleteRating(id);
    }

    # endregion

    # region Schedule

    public IList<Schedule> GetScheduleList() {
      return SessionRepository.Instance.GetScheduleList();
    }

    public Schedule GetScheduleById(int id) {
      return SessionRepository.Instance.GetScheduleBySessionId(id);
    }

    public Schedule AddSchedule(int id, Schedule schedule) {
      return SessionRepository.Instance.AddScheduleToSession(id, schedule);
    }

    public void DeleteSchedule(int id) {
      SessionRepository.Instance.DeleteScheduleForSession(id);
    }

    public IList<Slot> AllSlots() {
      return SessionRepository.Instance.AvailableSlots();
    }

    public IList<Slot> AllBreaks() {
      return SessionRepository.Instance.AvailableSlots(true);
    }

    public IList<Slot> AllAssignableSlots() {
      return SessionRepository.Instance.AvailableSlots(false);
    }

    public IList<Slot> GetNotAssignedSlots() {
      return SessionRepository.Instance.GetNotAssignedSlots();
    }

    public void UpdateSlot(Slot slot)
    {
        SessionRepository.Instance.UpdateSlot(slot);
    }

    # endregion
  }
}
