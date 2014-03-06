using BusinessLayer.Models;
using EndToEnd.DataLayer.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EndToEnd.BusinessLayer {
  public class SessionRepository : Repository<SessionRepository> {

    # region Speaker

    public IList<Speaker> GetSpeakerList() {
      var list = Ctx.Speakers.OrderBy(s=>s.Name).ToList();
      return list;
    }

    public Speaker GetSpeakerById(int id) {
      return Ctx.Speakers.Find(id);
    }

    public Speaker SearchSpeakerByName(string name) {
      return Ctx.Speakers.FirstOrDefault(s => s.Name.Contains(name));
    }

    public Speaker AddSpeaker(Speaker speaker) {
      var result = Ctx.Speakers.Add(speaker);
      SaveChanges();
      return result;
    }

    public int UpdateSpeaker(Speaker speaker) {
      Ctx.Entry(speaker).State = EntityState.Modified;
      Ctx.Entry(speaker).Collection(s => s.Sessions).Load();
      if (speaker.Photo == null) {
        Ctx.Entry(speaker).Property(s => s.Photo).IsModified = false;
      }
      Ctx.Entry(speaker).Property(s => s.CreatedAt).IsModified = false;
      return SaveChanges();
    }

    public void DeleteSpeaker(int id) {
      var speaker = Ctx.Speakers.Find(id);
      if (speaker != null) {
        Ctx.Speakers.Remove(speaker);
        SaveChanges();
      }
    }

    # endregion

    # region Session

    public IList<SessionDto> GetSessionList() {
      var list = Ctx.Sessions
        .Include(s => s.Speakers)
        .Include(s => s.Ratings)
        .Include(s => s.Tracks)
        .ToList();
      list.ForEach(s => s.SetSpeaker());
      return list.Select(s => new SessionDto(s)).ToList();
    }

    public SessionDto GetSessionById(int id) {
      var session = Ctx.Sessions.Find(id);
      Ctx.LoadProperty(session, s => s.Speakers);
      session.SetSpeaker();
      return new SessionDto(session);
    }

    public SessionDto SearchSessionByTitle(string title) {
      var session = Ctx.Sessions.FirstOrDefault(s => s.Title.Contains(title));
      if (session == null) return null;
      return new SessionDto(session);
    }

    public SessionDto AddSession(SessionDto dto) {
      var speaker1 = Ctx.Speakers.Find(dto.Speaker1Id);
      var speaker2 = Ctx.Speakers.Find(dto.Speaker2Id);
      var session = SessionBase.Create(dto.SessionType);
      session.Title = dto.Title;
      session.Abstract = dto.Abstract;
      if (speaker1 != null) {
        session.Speakers.Add(speaker1);
      }
      if (speaker2 != null) {
        session.Speakers.Add(speaker2);
      }
      if (dto.TrackIds.Any())
      {
          foreach (var trackId in dto.TrackIds)
          {
              var track = SessionRepository.Instance.GetTrackById(trackId);
              session.Tracks.Add(track);
          }
      }
      var result = Ctx.Sessions.Add(session);
      SaveChanges();
      return new SessionDto(session);
    }
    public int UpdateSession(SessionDto dto) {
      // Konvertiere DTO in entity 
      var session = Ctx.Sessions.Find(dto.SessionBaseId);
      Ctx.Entry(session).Collection(s => s.Tracks).Load();
      Ctx.Entry(session).Collection(s => s.Speakers).Load();
      Ctx.Entry(session).Collection(s => s.Ratings).Load();
      session.Abstract = dto.Abstract;
      session.Title = dto.Title;
      // prüfe Schedule gegen Slots, die Anforderung ist in den nicht zugewiesenen
      if (GetNotAssignedSlots().Any(slot => slot.Start == dto.StartTime)) {
        session.Schedule = new Schedule {
          StartTime = dto.StartTime,
          Room = dto.Room
        };
      }
      foreach (var track in session.Tracks.ToList()) {
        session.Tracks.Remove(track);
      }
      if (dto.TrackIds.Any()) {
        foreach (var trackId in dto.TrackIds) {
          var track = SessionRepository.Instance.GetTrackById(trackId);
          session.Tracks.Add(track);
        }
      }
      foreach (var speaker in session.Speakers.ToList())
      {
          session.Speakers.Remove(speaker);
      }
      if (dto.Speaker1Id != 0)
      {
          var speaker = Ctx.Speakers.Find(dto.Speaker1Id);
          session.Speaker1 = speaker;
          session.Speakers.Add(speaker);
        }
      if (dto.Speaker2Id != 0)
      {
          var speaker = Ctx.Speakers.Find(dto.Speaker2Id);
          session.Speaker2 = speaker;
          session.Speakers.Add(speaker);
      }

      // Lade navigation properties, sonst ist das Objekt ungültig, wir erwarten hier keinen serialisierten Objektgraphen
      Ctx.Entry(session).Property(s => s.CreatedAt).IsModified = false;
      return SaveChanges();
    }

    public void DeleteSession(int id) {
      var session = Ctx.Sessions.Find(id);
      if (session != null) {
        Ctx.Sessions.Remove(session);
        SaveChanges();
      }
    }

    public IEnumerable<string> GetSessionTypes() {
      var allTypes = typeof(SessionBase).Assembly.GetTypes();
      var sessionTypes = allTypes
        .Where(t => t.IsSubclassOf(typeof(SessionBase)))
        .Select(t => t.Name);
      return sessionTypes;
    }

    # endregion

    # region Tracks

    public IList<Track> GetTrackList() {
      return Ctx.Tracks.ToList();
    }

    public Track GetTrackById(int id) {
      return Ctx.Tracks.Find(id);
    }

    public Track SearchTrackByName(string track) {
      return Ctx.Tracks.FirstOrDefault(t => t.Name.Contains(track));
    }

    public Track AddTrack(Track track) {
      var result = Ctx.Tracks.Add(track);
      SaveChanges();
      return result;
    }

    public int UpdateTrack(Track track) {
      Ctx.Entry(track).State = EntityState.Modified;
      Ctx.Entry(track).Property(s => s.CreatedAt).IsModified = false;
      return SaveChanges();
    }

    public void DeleteTrack(int id) {
      var track = Ctx.Tracks.Find(id);
      if (track != null) {
        Ctx.Tracks.Remove(track);
        SaveChanges();
      }
    }

    public Track SearchTrackTypeByName(string track) {
      return Ctx.Tracks.FirstOrDefault(t => t.Name.Contains(track));
    }

    # endregion

    # region Rating

    public IList<Rating> GetRatingList() {
      var list = Ctx.Ratings.ToList();
      return list;
    }

    public Rating GetRatingById(int id) {
      return Ctx.Ratings.Find(id);
    }

    public Rating AddRating(Rating rating) {
      var session = Ctx.Sessions.Find(rating.SessionId);
      var speaker = Ctx.Speakers.Find(rating.SpeakerId);
      rating.Session = session;
      rating.Speaker = speaker;
      var result = Ctx.Ratings.Add(rating);
      SaveChanges();
      return result;
    }

    public int UpdateRating(Rating rating) {
      Ctx.Entry(rating).State = EntityState.Modified;
      Ctx.Entry(rating).Reference(r => r.Session).Load();
      Ctx.Entry(rating).Reference(r => r.Speaker).Load();
      Ctx.Entry(rating).Property(s => s.CreatedAt).IsModified = false;
      return SaveChanges();
    }

    public void DeleteRating(int id) {
      var rating = Ctx.Ratings.Find(id);
      if (rating != null) {
        Ctx.Ratings.Remove(rating);
        SaveChanges();
      }
    }

    public IEnumerable<Rating> GetRatingBySessionIs(int sessionId) {
      var session = Ctx.Sessions
        .Include(s => s.Ratings)
        .FirstOrDefault(s => s.Id == sessionId);
      if (session != null) {
        return session.Ratings;
      }
      return null;
    }

    # endregion

    # region Schedule

    public IList<Slot> AvailableSlots(bool? onlyBreaks = null) {
      var all = !onlyBreaks.HasValue;
      var slots = Ctx.Slots
        .Where(s => all || s.IsBreak == onlyBreaks)
        .ToList();
      return slots;
    }

    public IList<Slot> GetNotAssignedSlots() {
      var notAssigned = Ctx.Slots
        .ToList()                         // erforderlich, weil Except auf komplexen Typen nicht un Store-Ausdruck gewandelt werden kann
        .Except(GetAssignedSlots(),       // Mengenoperationen im Speicher, wenn geringe Speichermenge
                new SlotComparer());      // Vergleich nach eigenen Kriterien
      return notAssigned.ToList();
    }

    class SlotComparer : IEqualityComparer<Slot> {

      public bool Equals(Slot x, Slot y) {
        return x.Start == y.Start;
      }

      public int GetHashCode(Slot obj) {
        return 0;
      }
    }

    public IList<Slot> GetAssignedSlots() {
      var scheduledSessions = Ctx.Sessions.Where(s => s.Schedule.StartTime != null);
      var assigned = from slot in Ctx.Slots
                     join session in scheduledSessions on slot.Start equals session.Schedule.StartTime
                     where !slot.IsBreak
                     select slot;
      return assigned.ToList();
    }
    public void UpdateSlot(Slot slot)
    {
        Ctx.Entry(slot).State = EntityState.Modified;
        SaveChanges();
    }

    public IList<Schedule> GetScheduleList() {
      var sessions = Ctx.Sessions.ToList();
      var schedules = new List<Schedule>();
      foreach (var session in sessions) {
        session.Schedule.SessionId = session.Id;
        schedules.Add(session.Schedule);
      }
      return schedules;
    }

    public Schedule GetScheduleBySessionId(int id) {
      var session = Ctx.Sessions.Find(id);
      return session.Schedule;
    }

    public Schedule AddScheduleToSession(int id, Schedule schedule) {
      var isInSlot = Ctx.Slots.Any(s => s.Start == schedule.StartTime && !s.IsBreak);
      if (!isInSlot) {
        return null;
      }
      var session = Ctx.Sessions.Find(id);
      if (session != null) {
        return null;
      }
      Ctx.Entry(session).Collection(s => s.Speakers).Load();
      Ctx.Entry(session).Collection(s => s.Tracks).Load();
      Ctx.Entry(session).Collection(s => s.Ratings).Load();
      session.Schedule = schedule;
      SaveChanges();
      return session.Schedule;
    }

    public void DeleteScheduleForSession(int id) {
      var session = Ctx.Sessions.Find(id);
      if (session != null) {
        Ctx.Entry(session).Collection(s => s.Speakers).Load();
        Ctx.Entry(session).Collection(s => s.Tracks).Load();
        Ctx.Entry(session).Collection(s => s.Ratings).Load();
        session.Schedule = null;
        SaveChanges();
      }
    }

    # endregion

  }
}
