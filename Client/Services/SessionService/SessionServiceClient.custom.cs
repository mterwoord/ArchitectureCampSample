using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services.SessionServiceReference
{
    public partial class SessionServiceClient : ISessionService
    {
        private HttpClient httpClient;

        public SessionServiceClient()
        {
            httpClient = new HttpClient(new HttpClientHandler { UseProxy = true, Proxy = new WebProxy("http://127.0.0.1:8888", false) });
            //httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://windows8vm/conferences/api/");
        }

        public ObservableCollection<Speaker> GetSpeakerList()
        {
            throw new NotImplementedException();
        }

        public async Task<ObservableCollection<Speaker>> GetSpeakerListAsync()
        {
            var response = await httpClient.GetAsync("speakers/list");
            var result = await response.Content.ReadAsAsync<ObservableCollection<Speaker>>();

            return result;
        }

        public Speaker GetSpeakerById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Speaker> GetSpeakerByIdAsync(int id)
        {
            var response = await httpClient.GetAsync("speakers/list?id=" + id);
            var result = await response.Content.ReadAsAsync<Speaker>();

            return result;
        }

        public Speaker SearchSpeakerByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Speaker> SearchSpeakerByNameAsync(string name)
        {
            var response = await httpClient.GetAsync("speakers/search?name=" + name);
            var result = await response.Content.ReadAsAsync<Speaker>();

            return result;
        }

        public Speaker AddSpeaker(Speaker speaker)
        {
            throw new NotImplementedException();
        }

        public async Task<Speaker> AddSpeakerAsync(Speaker speaker)
        {
            var response = await httpClient.PostAsJsonAsync("speakers/list", speaker);
            var result = await response.Content.ReadAsAsync<Speaker>();

            return result;
        }

        public void UpdateSpeaker(Speaker speaker)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateSpeakerAsync(Speaker speaker)
        {
            await httpClient.PutAsJsonAsync("speakers/list", speaker);
        }

        public void DeleteSpeaker(int id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteSpeakerAsync(int id)
        {
            await httpClient.DeleteAsync("speakers/list?id=" + id);
        }

        public ObservableCollection<SessionDto> GetSessionList()
        {
            throw new NotImplementedException();
        }

        public async Task<ObservableCollection<SessionDto>> GetSessionListAsync()
        {
            var response = await httpClient.GetAsync("sessions/list");
            var result = await response.Content.ReadAsAsync<ObservableCollection<SessionDto>>();

            return result;
        }

        public SessionDto GetSessionById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionDto> GetSessionByIdAsync(int id)
        {
            var response = await httpClient.GetAsync("sessions/list?id=" + id);
            var result = await response.Content.ReadAsAsync<SessionDto>();

            return result;
        }

        public SessionDto SearchSessionByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionDto> SearchSessionByTitleAsync(string title)
        {
            var response = await httpClient.GetAsync("sessions/search?title=" + title);
            var result = await response.Content.ReadAsAsync<SessionDto>();

            return result;
        }

        public SessionDto AddSession(SessionDto session)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionDto> AddSessionAsync(SessionDto session)
        {
            var response = await httpClient.PostAsJsonAsync("sessions/list", session);
            var result = await response.Content.ReadAsAsync<SessionDto>();

            return result;
        }

        public void UpdateSession(SessionDto session)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateSessionAsync(SessionDto session)
        {
            await httpClient.PutAsJsonAsync("sessions/list", session);
        }

        public void DeleteSession(int id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteSessionAsync(int id)
        {
            await httpClient.DeleteAsync("speakers/list?id=" + id);
        }

        public ObservableCollection<string> GetSessionTypes()
        {
            throw new NotImplementedException();
        }

        public async Task<ObservableCollection<string>> GetSessionTypesAsync()
        {
            var response = await httpClient.GetAsync("sessions/types");
            var result = await response.Content.ReadAsAsync<ObservableCollection<string>>();

            return result;
        }

        public ObservableCollection<Track> GetTrackList()
        {
            throw new NotImplementedException();
        }

        public async Task<ObservableCollection<Track>> GetTrackListAsync()
        {
            var response = await httpClient.GetAsync("tracks/list");
            var result = await response.Content.ReadAsAsync<ObservableCollection<Track>>();

            return result;
        }

        public Track GetTrackById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Track> GetTrackByIdAsync(int id)
        {
            var response = await httpClient.GetAsync("tracks/list?id=" + id);
            var result = await response.Content.ReadAsAsync<Track>();

            return result;
        }

        public Track SearchTrackByName(string track)
        {
            throw new NotImplementedException();
        }

        public async Task<Track> SearchTrackByNameAsync(string track)
        {
            var response = await httpClient.GetAsync("tracks/search?track=" + track);
            var result = await response.Content.ReadAsAsync<Track>();

            return result;
        }

        public Track AddTrack(Track track)
        {
            throw new NotImplementedException();
        }

        public async Task<Track> AddTrackAsync(Track track)
        {
            var response = await httpClient.PostAsJsonAsync("tracks/list", track);
            var result = await response.Content.ReadAsAsync<Track>();

            return result;
        }

        public void UpdateTrack(Track track)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateTrackAsync(Track track)
        {
            await httpClient.PutAsJsonAsync("speakers/list", track);
        }

        public void DeleteTrack(int id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteTrackAsync(int id)
        {
            await httpClient.DeleteAsync("tracks/list?id=" + id);
        }

        public Track SearchTrackTypeByName(string track)
        {
            throw new NotImplementedException();
        }

        public async Task<Track> SearchTrackTypeByNameAsync(string track)
        {
            var response = await httpClient.GetAsync("tracks/search?track=" + track);
            var result = await response.Content.ReadAsAsync<Track>();

            return result;
        }

        public ObservableCollection<Rating> GetRatingList()
        {
            throw new NotImplementedException();
        }

        public async Task<ObservableCollection<Rating>> GetRatingListAsync()
        {
            var response = await httpClient.GetAsync("ratings/list");
            var result = await response.Content.ReadAsAsync<ObservableCollection<Rating>>();

            return result;
        }

        public Rating GetRatingById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Rating> GetRatingByIdAsync(int id)
        {
            var response = await httpClient.GetAsync("ratings/list?id=" + id);
            var result = await response.Content.ReadAsAsync<Rating>();

            return result;
        }

        public Rating AddRating(Rating rating)
        {
            throw new NotImplementedException();
        }

        public async Task<Rating> AddRatingAsync(Rating rating)
        {
            var response = await httpClient.PostAsJsonAsync("ratings/list", rating);
            var result = await response.Content.ReadAsAsync<Rating>();

            return result;
        }

        public void UpdateRating(Rating rating)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateRatingAsync(Rating rating)
        {
            await httpClient.PutAsJsonAsync("ratings/list", rating);
        }

        public void DeleteRating(int id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteRatingAsync(int id)
        {
            await httpClient.DeleteAsync("ratings/list?id=" + id);
        }

        public ObservableCollection<Schedule> GetScheduleList()
        {
            throw new NotImplementedException();
        }

        public async Task<ObservableCollection<Schedule>> GetScheduleListAsync()
        {
            var response = await httpClient.GetAsync("schedule/list");
            var result = await response.Content.ReadAsAsync<ObservableCollection<Schedule>>();

            return result;
        }

        public Schedule GetScheduleById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Schedule> GetScheduleByIdAsync(int id)
        {
            var response = await httpClient.GetAsync("schedule/list?id=" + id);
            var result = await response.Content.ReadAsAsync<Schedule>();

            return result;
        }

        public Schedule AddSchedule(int id, Schedule schedule)
        {
            throw new NotImplementedException();
        }

        public async Task<Schedule> AddScheduleAsync(int id, Schedule schedule)
        {
            var response = await httpClient.PostAsJsonAsync("schedule/list", schedule);
            var result = await response.Content.ReadAsAsync<Schedule>();

            return result;
        }

        public void DeleteSchedule(int id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteScheduleAsync(int id)
        {
            await httpClient.DeleteAsync("schedule/list?id=" + id);
        }

        public ObservableCollection<Slot> AllSlots()
        {
            throw new NotImplementedException();
        }

        public async Task<ObservableCollection<Slot>> AllSlotsAsync()
        {
            var response = await httpClient.GetAsync("slots/list");
            var result = await response.Content.ReadAsAsync<ObservableCollection<Slot>>();

            return result;
        }

        public ObservableCollection<Slot> AllBreaks()
        {
            throw new NotImplementedException();
        }

        public async Task<ObservableCollection<Slot>> AllBreaksAsync()
        {
            var response = await httpClient.GetAsync("slots/breaks");
            var result = await response.Content.ReadAsAsync<ObservableCollection<Slot>>();

            return result;
        }

        public ObservableCollection<Slot> AllAssignableSlots()
        {
            throw new NotImplementedException();
        }

        public async Task<ObservableCollection<Slot>> AllAssignableSlotsAsync()
        {
            var response = await httpClient.GetAsync("slots/assignable");
            var result = await response.Content.ReadAsAsync<ObservableCollection<Slot>>();

            return result;
        }

        public ObservableCollection<Slot> GetNotAssignedSlots()
        {
            throw new NotImplementedException();
        }

        public async Task<ObservableCollection<Slot>> GetNotAssignedSlotsAsync()
        {
            var response = await httpClient.GetAsync("slots/notassigned");
            var result = await response.Content.ReadAsAsync<ObservableCollection<Slot>>();

            return result;
        }

        public void UpdateSlot(Slot slot)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateSlotAsync(Slot slot)
        {
            await httpClient.PutAsJsonAsync("slots/list", slot);
        }
    }
}
