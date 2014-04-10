using System.Collections.Generic;
using DataLayer.Model;

namespace BusinessLogicLayer
{
    public interface IConferenceManager
    {
        IList<Speaker> GetSpeakerList();
        Speaker GetSpeakerById(int id);
        Speaker SearchSpeakerByName(string name);
        Speaker AddSpeaker(Speaker speaker);
        int UpdateSpeaker(Speaker speaker);
        void DeleteSpeaker(int id);
        IList<SessionDto> GetSessionList();
        SessionDto GetSessionById(int id);
        SessionDto SearchSessionByTitle(string title);
        SessionDto AddSession(SessionDto dto);
        int UpdateSession(SessionDto dto);
        void DeleteSession(int id);
        IEnumerable<string> GetSessionTypes();
        IList<Track> GetTrackList();
        Track GetTrackById(int id);
        Track SearchTrackByName(string track);
        Track AddTrack(Track track);
        int UpdateTrack(Track track);
        void DeleteTrack(int id);
        Track SearchTrackTypeByName(string track);
        IList<Rating> GetRatingList();
        Rating GetRatingById(int id);
        Rating AddRating(Rating rating);
        int UpdateRating(Rating rating);
        void DeleteRating(int id);
        IEnumerable<Rating> GetRatingBySessionIs(int sessionId);
        IList<Slot> AvailableSlots(bool? onlyBreaks = null);
        IList<Slot> GetNotAssignedSlots();
        IList<Slot> GetAssignedSlots();
        void UpdateSlot(Slot slot);
        IList<Schedule> GetScheduleList();
        Schedule GetScheduleBySessionId(int id);
        Schedule AddScheduleToSession(int id, Schedule schedule);
        void DeleteScheduleForSession(int id);
    }
}