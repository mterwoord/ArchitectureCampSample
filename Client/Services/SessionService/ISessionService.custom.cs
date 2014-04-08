using System;

namespace Services.SessionServiceReference
{
    public partial interface ISessionService
    {
        event EventHandler<RatingUpdatedEventArgs> RatingUpdated;
    }

    public class RatingUpdatedEventArgs : EventArgs
    {
        private readonly RatingUpdate _rating;

        public RatingUpdatedEventArgs(RatingUpdate rating)
        {
            _rating = rating;
        }
    }

    public class RatingUpdate
    {
        public int SessionId { get; set; }
        public int SpeakerId { get; set; }
    }

}
