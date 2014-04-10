using Microsoft.AspNet.SignalR;

namespace ServicesLayer.Hubs
{
    public class RatingsHub : Hub
    {
    }

    public class RatingUpdate
    {
        public int SessionId { get; set; }
        public int SpeakerId { get; set; }
    }
}
