using Contracts;
using Services.SessionServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ImagesModule.ViewModels
{
    [ModuleMetadata("SessionModule", "Sessions", "Session", 1)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SessionViewModel : ViewModelBase, IModule
    {
        private const string NORMAL_STATE = "Normal";
        private const string EDIT_STATE = "Edit";
        private ISessionService _sessionService;
        private bool _hasChanges;
        private List<UITrack> _uiTrackList;
        private ObservableCollection<Speaker> _speakerList;
        private IEditableCollectionViewAddNewItem _editView;
        private bool _isChanging;

        public async void Initialize(IServicePool servicePool)
        {
            _sessionService = servicePool.GetService<ISessionService>();

            this.AddCommand = new DelegateCommand(p => !this.IsBusy, this.ExecuteAddCommand);
            this.EditCommand = new DelegateCommand(p => !this.IsBusy, this.ExecuteEditCommand);
            this.OkCommand = new DelegateCommand(p => !this.IsBusy, this.ExecuteOkCommand);
            this.CancelEditCommand = new DelegateCommand(p => !this.IsBusy, this.ExecuteCancelEditCommand);
            this.DeleteCommand = new DelegateCommand(p => !this.IsBusy, this.ExecuteDeleteCommand);
            this.CancelDeleteCommand = new DelegateCommand(p => !this.IsBusy, this.ExecuteCancelDeleteCommand);
            this.IsBusy = true;

            _speakerList = await _sessionService.GetSpeakerListAsync();
            _speakerList.Insert(0, new Speaker { Id = 0, Name = " " });
            this.SpeakersView = new ListCollectionView(_speakerList);

            var trackList = await _sessionService.GetTrackListAsync();
            _uiTrackList = new List<UITrack>();
            foreach (var track in trackList)
            {
                var uiTrack = new UITrack(track);
                _uiTrackList.Add(uiTrack);
                uiTrack.PropertyChanged += this.OnUITrackPropertyChanged;
            }
            this.TracksView = new ListCollectionView(_uiTrackList);

            var sessionTypeList = await _sessionService.GetSessionTypesAsync();
            this.SessionTypesView = new ListCollectionView(sessionTypeList);

            this.Sessions = await _sessionService.GetSessionListAsync();
            this.SessionsView = new ListCollectionView(this.Sessions);
            this.SessionsView.CurrentChanged += this.OnCurrentSessionChanged;
            this.SessionsView.CurrentChanging += this.OnCurrentSessionChanging;
            _editView = this.SessionsView as IEditableCollectionViewAddNewItem;

            if (this.SessionsView.CurrentItem != null)
                this.SetCurrentSession(this.SessionsView.CurrentItem as SessionDto);

            this.IsBusy = false;
        }

        private void OnUITrackPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!_isChanging)
                _hasChanges = true;
        }

        private bool Validate()
        {
            string errorText = null;
            var session = this.CurrentSession;
            if (session != null)
            {
                var trackCount = (from t in _uiTrackList where t.IsChecked select t).Count();
                if (string.IsNullOrWhiteSpace(session.Title))
                    errorText = "Sie haben noch keinen Titel angegeben!";
                if (string.IsNullOrWhiteSpace(session.SessionType))
                    errorText = "Sie haben noch keinen Session-Typ zugewiesen!";
                if (string.IsNullOrWhiteSpace(session.Abstract))
                    errorText = "Sie haben noch keinen Abstract angegeben!";
                else if (session.Speaker1Id == 0 && session.Speaker2Id == 0)
                    errorText = "Sie haben noch keinen Sprecher zugewiesen!";
                else if (trackCount == 0)
                    errorText = "Sie haben noch keine Tracks zugewiesen!";
            }
            this.ErrorText = errorText;
            this.HasErrors = (errorText != null);
            return !this.HasErrors;
        }

        private async Task<bool> SaveChangesAsync()
        {
            try
            {
                if (this.CurrentSession != null)
                {
                    var session = this.CurrentSession;
                    if (_editView.IsAddingNew && this.CurrentSession != _editView.CurrentAddItem)
                        return true;
                    if (_hasChanges || _editView.IsAddingNew)
                    {
                        if (!this.Validate()) return false;

                        _isChanging = true;
                        this.IsBusy = true;
                        session.TrackIds = new ObservableCollection<int>();
                        foreach (var uitrack in _uiTrackList)
                        {
                            if (uitrack.IsChecked)
                            {
                                session.TrackIds.Add(uitrack.Track.Id);
                            }
                        }

                        if (_editView.IsAddingNew)
                        {
                            var newSession = await _sessionService.AddSessionAsync(session);
                            session.SessionBaseId = newSession.SessionBaseId;
                            _editView.CommitNew();
                        }
                        else
                        {
                            await _sessionService.UpdateSessionAsync(session);
                        }
                        session.Speaker1 = (from s in _speakerList where s.Id == session.Speaker1Id select s).FirstOrDefault();
                        session.Speaker2 = (from s in _speakerList where s.Id == session.Speaker2Id select s).FirstOrDefault();
                        this.CurrentState = NORMAL_STATE;
                        this.IsBusy = false;
                        _isChanging = false;
                        _hasChanges = false;
                    }
                    this.CurrentSession.PropertyChanged -= this.OnSessionPropertyChanged;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private async void OnCurrentSessionChanging(object sender, CurrentChangingEventArgs e)
        {
            if (this.IsBusy) return;
            // vorherige Session speichern
            if (await this.SaveChangesAsync() == false && e.IsCancelable)
                e.Cancel = true;
        }

        private void OnCurrentSessionChanged(object sender, EventArgs e)
        {
            if (this.IsBusy) return;
            // aktuelle Session ermitteln
            var session = this.SessionsView.CurrentItem as SessionDto;
            this.SetCurrentSession(session);
        }

        private void SetCurrentSession(SessionDto session)
        {
            if (session != null)
            {
                this.CurrentSession = session;
                _uiTrackList.ForEach(t => t.IsChecked = false);
                if (session.TrackIds == null)
                    session.TrackIds = new ObservableCollection<int>();
                foreach (var id in session.TrackIds)
                {
                    foreach (var uitrack in _uiTrackList)
                    {
                        if (uitrack.Track.Id == id)
                        {
                            _isChanging = true;
                            uitrack.IsChecked = true;
                            _isChanging = false;
                        }
                    }
                }
                this.CurrentSession.PropertyChanged += this.OnSessionPropertyChanged;
                _hasChanges = false;
            }
        }

        private void OnSessionPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _hasChanges = true;
        }

        public IView GetView()
        {
            return ViewFactory.CreateView(this);
        }

        public async void Close()
        {
            if (this.SessionsView != null)
            {
                this.SessionsView.CurrentChanged -= this.OnCurrentSessionChanged;
                this.SessionsView.CurrentChanging -= this.OnCurrentSessionChanging;
            }
            await this.SaveChangesAsync();
            this.ExecuteCancelEditCommand(null);
        }

        #region Properties

        private SessionDto _currentSession;
        public SessionDto CurrentSession
        {
            get { return _currentSession; }
            set { _currentSession = value; this.OnPropertyChanged(); }
        }

        private ICollectionView _speakersView;
        public ICollectionView SpeakersView
        {
            get { return _speakersView; }
            set { _speakersView = value; this.OnPropertyChanged(); }
        }

        private ObservableCollection<SessionDto> _sessions;
        public ObservableCollection<SessionDto> Sessions
        {
            get { return _sessions; }
            set { _sessions = value; this.OnPropertyChanged(); }
        }

        private ICollectionView _sessionsView;
        public ICollectionView SessionsView
        {
            get { return _sessionsView; }
            set { _sessionsView = value; this.OnPropertyChanged(); }
        }

        private ICollectionView _tracksView;
        public ICollectionView TracksView
        {
            get { return _tracksView; }
            set { _tracksView = value; this.OnPropertyChanged(); }
        }

        private ICollectionView _sessionTypesView;
        public ICollectionView SessionTypesView
        {
            get { return _sessionTypesView; }
            set { _sessionTypesView = value; this.OnPropertyChanged(); }
        }

        private DelegateCommand _addCommand;
        public DelegateCommand AddCommand
        {
            get { return _addCommand; }
            set { _addCommand = value; this.OnPropertyChanged(); }
        }

        private DelegateCommand _editCommand;
        public DelegateCommand EditCommand
        {
            get { return _editCommand; }
            set { _editCommand = value; this.OnPropertyChanged(); }
        }

        private DelegateCommand _deleteCommand;
        public DelegateCommand DeleteCommand
        {
            get { return _deleteCommand; }
            set { _deleteCommand = value; this.OnPropertyChanged(); }
        }

        private DelegateCommand _okCommand;
        public DelegateCommand OkCommand
        {
            get { return _okCommand; }
            set { _okCommand = value; this.OnPropertyChanged(); }
        }

        private DelegateCommand _cancelEditCommand;
        public DelegateCommand CancelEditCommand
        {
            get { return _cancelEditCommand; }
            set { _cancelEditCommand = value; this.OnPropertyChanged(); }
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
            var session = new SessionDto();
            session.TrackIds = new ObservableCollection<int>();
            _editView.AddNewItem(session);
            this.SetCurrentSession(session);
            this.RefreshCommands();
            this.CurrentState = EDIT_STATE;
        }

        private void ExecuteEditCommand(object parameter)
        {
            if (this.CurrentSession != null)
            {
                _editView.EditItem(this.CurrentSession);
                this.CurrentState = EDIT_STATE;
            }
            this.RefreshCommands();
        }

        private async void ExecuteOkCommand(object parameter)
        {
            if (await this.SaveChangesAsync())
            {
                this.RefreshCommands();
            }
        }

        private void ExecuteCancelEditCommand(object parameter)
        {
            if (_editView != null)
            {
                if (_editView.IsAddingNew)
                {
                    _editView.CancelNew();
                }
                else if (_editView.IsEditingItem && _editView.CanCancelEdit)
                    _editView.CancelEdit();
            }
            this.CurrentState = NORMAL_STATE;
            this.HasErrors = false; 
            this.RefreshCommands();
        }

        private async void ExecuteDeleteCommand(object parameter)
        {
            var session = this.SessionsView.CurrentItem as SessionDto;
            if (session != null)
            {
                if (this.IsDeleteDialogOpen)
                {
                    this.IsBusy = true;
                    await _sessionService.DeleteSessionAsync(session.SessionBaseId);
                    this.Sessions.Remove(session);
                    this.IsBusy = false;
                }
                this.IsDeleteDialogOpen = !this.IsDeleteDialogOpen;
            }
            this.RefreshCommands();
        }

        private void ExecuteCancelDeleteCommand(object parameter)
        {
            this.IsDeleteDialogOpen = false;
        }

        #endregion

    }
    public class UITrack : Track
    {
        public readonly Track Track;
        public UITrack(Track track)
        {
            this.Track = track;
            this.Name = track.Name;
        }
        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set { _isChecked = value; this.RaisePropertyChanged("IsChecked"); }
        }
    }
}
