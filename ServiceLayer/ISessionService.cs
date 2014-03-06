using BusinessLayer.Models;
using EndToEnd.DataLayer.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace ServiceLayer {
  // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISessionService" in both code and config file together.
  [ServiceContract]
  public interface ISessionService {

    # region Speaker

    [OperationContract]
    IList<Speaker> GetSpeakerList();

    [OperationContract]
    Speaker GetSpeakerById(int id);

    [OperationContract]
    Speaker SearchSpeakerByName(string name);

    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
    [OperationContract]
    Speaker AddSpeaker(Speaker speaker);

    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
    [OperationContract]
    void UpdateSpeaker(Speaker speaker);

    [OperationContract]
    void DeleteSpeaker(int id);

    # endregion

    # region Sessions

    [OperationContract]
    IList<SessionDto> GetSessionList();

    [OperationContract]
    SessionDto GetSessionById(int id);

    [OperationContract]
    SessionDto SearchSessionByTitle(string title);

    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
    [OperationContract]
    SessionDto AddSession(SessionDto session);

    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
    [OperationContract]
    void UpdateSession(SessionDto session);

    [OperationContract]
    void DeleteSession(int id);

    [OperationContract]
    IEnumerable<string> GetSessionTypes();

    # endregion

    # region Tracks

    [OperationContract]
    IList<Track> GetTrackList();

    [OperationContract]
    Track GetTrackById(int id);

    [OperationContract]
    Track SearchTrackByName(string track);

    [OperationContract]
    Track AddTrack(Track track);

    [OperationContract]
    void UpdateTrack(Track track);

    [OperationContract]
    void DeleteTrack(int id);

    [OperationContract]
    Track SearchTrackTypeByName(string track);

    # endregion

    # region Rating

    [OperationContract]
    IList<Rating> GetRatingList();

    [OperationContract]
    Rating GetRatingById(int id);

    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
    [OperationContract]
    [FaultContract(typeof(FaultMessage))]
    Rating AddRating(Rating rating);

    [OperationContract]
    void UpdateRating(Rating rating);

    [OperationContract]
    void DeleteRating(int id);

    # endregion

    # region Schedule

    [OperationContract]
    IList<Schedule> GetScheduleList();
    [OperationContract]
    Schedule GetScheduleById(int id);

    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
    [OperationContract]
    Schedule AddSchedule(int id, Schedule schedule);

    [OperationContract]
    void DeleteSchedule(int id);

    [OperationContract]
    IList<Slot> AllSlots();

    [OperationContract]
    IList<Slot> AllBreaks();

    [OperationContract]
    IList<Slot> AllAssignableSlots();

    [OperationContract]
    IList<Slot> GetNotAssignedSlots();

    [OperationContract(IsOneWay=true)]
    void UpdateSlot(Slot slot);

    # endregion

  }
}
