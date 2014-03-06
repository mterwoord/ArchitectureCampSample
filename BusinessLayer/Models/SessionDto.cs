using EndToEnd.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models {
  public class SessionDto {

    public SessionDto() {
      RatingIds = new List<int>();
      TrackIds = new List<int>();
    }

    public SessionDto(EndToEnd.DataLayer.Models.SessionBase s)
      : this() {
      SessionBaseId = s.Id;
      Abstract = s.Abstract;
      Duration = s.Duration;
      SessionType = s.GetType().Name;
      Speaker1Id = s.Speaker1Id;
      Speaker1 = s.Speaker1;
      Speaker2Id = s.Speaker2Id;
      Speaker2 = s.Speaker2;
      Title = s.Title;
      if (s.Ratings == null)
          s.Ratings = new List<EndToEnd.DataLayer.Models.Rating>();
      RatingIds = s.Ratings.Select(r => r.Id);
      TrackIds = s.Tracks.Select(t => t.Id);
      if (s.Schedule != null) {
        StartTime = s.Schedule.StartTime.GetValueOrDefault();
        Room = s.Schedule.Room;
      }
    }

    public int SessionBaseId { get; set; }

    public string Title { get; set; }

    public int Speaker1Id { get; set; }
    public Speaker Speaker1 { get; set; }

    public int Speaker2Id { get; set; }
    public Speaker Speaker2 { get; set; }

    public string Abstract { get; set; }

    public TimeSpan Duration { get; set; }

    public IEnumerable<int> TrackIds { get; set; }

    public IEnumerable<int> RatingIds { get; set; }

    public string SessionType { get; set; }

    public DateTime StartTime { get; set; }

    public string Room { get; set; }

  }
}