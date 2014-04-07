using System;

namespace Services.SessionServiceReference
{
    public partial interface ISessionService
    {
        event EventHandler<RatingUpdatedEventArgs> RatingUpdated;
    }

    public class RatingUpdatedEventArgs : EventArgs
    {
        private readonly int _ratingId;

        public RatingUpdatedEventArgs(int ratingId)
        {
            _ratingId = ratingId;
        }
    }
}
