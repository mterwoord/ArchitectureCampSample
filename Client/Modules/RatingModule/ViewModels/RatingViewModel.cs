using Contracts;
using Services.SessionServiceReference;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Data;

namespace ImagesModule.ViewModels
{
    [ModuleMetadata("RatingModule", "Ratings", "Rating", 2)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RatingViewModel : ViewModelBase, IModule
    {
        private ISessionService _sessionService;
        private bool _currentRatingHasChanges;

        public async void Initialize(IServicePool servicePool)
        {
            _sessionService = servicePool.GetService<ISessionService>();
            
            this.AddCommand = new DelegateCommand(p => !this.IsBusy, this.ExecuteAddCommand);
            this.DeleteCommand = new DelegateCommand(p => !this.IsBusy, this.ExecuteDeleteCommand);
            this.CancelDeleteCommand = new DelegateCommand(p => !this.IsBusy, this.ExecuteCancelDeleteCommand);
            this.IsBusy = true;

            this.Ratings = await _sessionService.GetRatingListAsync();
            this.RatingsView = new ListCollectionView(this.Ratings);

            var sessions = await _sessionService.GetSessionListAsync();
            this.SessionsView = new ListCollectionView(sessions);
            this.SessionsView.CurrentChanged += this.OnCurrentSessionChanged;

            this.OnCurrentSessionChanged(null, EventArgs.Empty);
            this.IsBusy = false;
        }

        private async void OnCurrentSessionChanged(object sender, EventArgs e)
        {
            if (this.Ratings == null) return;

            // Änderungen des zuletzt selektierten Ratings speichern
            if (this.CurrentRating != null)
            {
                if (_currentRatingHasChanges)
                {
                    this.IsBusy = true;
                    if (this.CurrentRating.Id == 0)
                    {
                        var rating = this.CurrentRating;
                        var newRating = await _sessionService.AddRatingAsync(rating);
                        rating.Id = newRating.Id;
                    }
                    else
                        await _sessionService.UpdateRatingAsync(this.CurrentRating);
                    _currentRatingHasChanges = false;
                    this.IsBusy = false;
                }
                this.CurrentRating.PropertyChanged -= this.OnCurrentRatingPropertyChanged;
            }

            var session = this.SessionsView.CurrentItem as SessionDto;
            if (session != null)
            {
                this.CurrentRating = null;
                var rating = (from r in this.Ratings where r.SessionId.Equals(session.SessionBaseId) select r).FirstOrDefault();
                this.CurrentRating = rating;
                if (this.CurrentRating == null && session.Speaker1Id > 0)
                {
                    var newRating = new Rating() { SessionId = session.SessionBaseId, SpeakerId = session.Speaker1Id };
                    this.Ratings.Add(newRating);
                    this.CurrentRating = newRating;
                }
                if (this.CurrentRating != null)
                    this.CurrentRating.PropertyChanged += this.OnCurrentRatingPropertyChanged;
            }
        }

        private void OnCurrentRatingPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _currentRatingHasChanges = true;
        }

        public IView GetView()
        {
            return ViewFactory.CreateView(this);
        }

        public void Close()
        {
            this.OnCurrentSessionChanged(null, EventArgs.Empty);
        }

        #region Properties

        private Rating _currentRating;
        public Rating CurrentRating
        {
            get { return _currentRating; }
            set { _currentRating = value; this.OnPropertyChanged(); }
        }

        private ObservableCollection<Rating> _ratings;
        public ObservableCollection<Rating> Ratings
        {
            get { return _ratings; }
            set { _ratings = value; this.OnPropertyChanged(); }
        }

        private ICollectionView _ratingsView;
        public ICollectionView RatingsView
        {
            get { return _ratingsView; }
            set { _ratingsView = value; this.OnPropertyChanged(); }
        }

        private ICollectionView _sessionsView;
        public ICollectionView SessionsView
        {
            get { return _sessionsView; }
            set { _sessionsView = value; this.OnPropertyChanged(); }
        }

        private DelegateCommand _addCommand;
        public DelegateCommand AddCommand
        {
            get { return _addCommand; }
            set { _addCommand = value; this.OnPropertyChanged(); }
        }

        private DelegateCommand _deleteCommand;
        public DelegateCommand DeleteCommand
        {
            get { return _deleteCommand; }
            set { _deleteCommand = value; this.OnPropertyChanged(); }
        }

        private DelegateCommand _cancelDeleteCommand;
        public DelegateCommand CancelDeleteCommand
        {
            get { return _cancelDeleteCommand; }
            set { _cancelDeleteCommand = value; this.OnPropertyChanged(); }
        }

        private bool _isDeleteDialogOpen;
        public bool IsDeleteDialogOpen
        {
            get { return _isDeleteDialogOpen; }
            set { _isDeleteDialogOpen = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Command Handling

        private void ExecuteAddCommand(object parameter)
        {
        }

        private async void ExecuteDeleteCommand(object parameter)
        {
            var rating = this.CurrentRating;
            if (rating != null)
            {
                this.IsBusy = true;
                if (rating.Id > 0)
                    await _sessionService.DeleteRatingAsync(rating.Id);
                this.Ratings.Remove(rating);
                this.IsBusy = false;
                this.OnCurrentSessionChanged(null, EventArgs.Empty);
                this.RefreshCommands();
            }
        }

        private void ExecuteCancelDeleteCommand(object parameter)
        {
            this.IsDeleteDialogOpen = false;
        }

        #endregion

    }
}