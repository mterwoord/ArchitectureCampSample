using Contracts;
using Services.SessionServiceReference;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ImagesModule.ViewModels
{
    [ModuleMetadata("SpeakerModule", "Sprecher", "Speaker", 0)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SpeakerViewModel : ViewModelBase, IModule
    {
        private const string NORMAL_STATE = "Normal";
        private const string EDIT_STATE = "Edit";
        private ISessionService _sessionService;
        private IEditableCollectionViewAddNewItem _editView;
        private bool _hasChanges;
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

            this.Speakers = await _sessionService.GetSpeakerListAsync();
            this.SpeakersView = new ListCollectionView(this.Speakers);
            this.SpeakersView.CurrentChanging += this.OnCurrentSpeakerChanging;
            this.SpeakersView.CurrentChanged += this.OnCurrentSpeakerChanged;
            _editView = this.SpeakersView as IEditableCollectionViewAddNewItem;
            this.OnCurrentSpeakerChanged(null, EventArgs.Empty);

            this.IsBusy = false;
        }

        private async void OnCurrentSpeakerChanging(object sender, CurrentChangingEventArgs e)
        {
            if (await this.SaveChangesAsync() == false && e.IsCancelable)
            {
                e.Cancel = true;
            }
        }

        private void OnCurrentSpeakerChanged(object sender, System.EventArgs e)
        {
            if (this.SpeakersView.CurrentItem != null)
            {
                this.CurrentSpeaker = this.SpeakersView.CurrentItem as Speaker;
                this.CurrentSpeaker.PropertyChanged += this.OnCurrentSpeakerPropertyChanged;
            }
        }

        private void OnCurrentSpeakerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!_isChanging)
                _hasChanges = true;
        }

        private bool Validate()
        {
            string errorText = null;
            var speaker = this.CurrentSpeaker;
            if (speaker != null)
            {
                if (string.IsNullOrEmpty(speaker.Name))
                    errorText = "Sie haben noch keinen Namen eingegeben!";
                else if (string.IsNullOrEmpty(speaker.EMail))
                    errorText = "Sie haben noch keine E-Mail eingegeben!";
                else if (!IsValidEmail(speaker.EMail))
                    errorText = "Die eingegebene E-Mail ist ungültig!";
            }
            this.ErrorText = errorText;
            this.HasErrors = errorText != null;
            return !this.HasErrors;
        }

        public static bool IsValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            return new Regex(strRegex).IsMatch(inputEmail);
        }

        private async Task<bool> SaveChangesAsync()
        {
            try
            {
                if (this.CurrentSpeaker != null)
                {
                    this.CurrentSpeaker.PropertyChanged -= this.OnCurrentSpeakerPropertyChanged;
                    if (_editView.IsAddingNew && this.CurrentSpeaker != _editView.CurrentAddItem)
                        return true;
                    if (_hasChanges || _editView.IsAddingNew)
                    {
                        if (!this.Validate()) return false;
                        this.IsBusy = true;
                        var speaker = this.CurrentSpeaker;
                        if (_editView.IsAddingNew)
                        {
                            _isChanging = true;
                            var newSpeaker = await _sessionService.AddSpeakerAsync(speaker);
                            speaker.Id = newSpeaker.Id;
                            _editView.CommitNew();
                            _isChanging = false;
                        }
                        else
                        {
                            await _sessionService.UpdateSpeakerAsync(speaker);
                        }
                        this.CurrentState = NORMAL_STATE;
                    }
                }
                this.IsBusy = false;
                _hasChanges = false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IView GetView()
        {
            return ViewFactory.CreateView(this);
        }

        public async void Close()
        {
            if (this.SpeakersView != null)
            {
                this.SpeakersView.CurrentChanged -= this.OnCurrentSpeakerChanged;
                this.SpeakersView.CurrentChanging -= this.OnCurrentSpeakerChanging;
            }
            await this.SaveChangesAsync();
            this.ExecuteCancelEditCommand(null);
        }

        #region Properties

        private Speaker _currentSpeaker;
        public Speaker CurrentSpeaker
        {
            get { return _currentSpeaker; }
            set { _currentSpeaker = value; this.OnPropertyChanged(); }
        }

        private ObservableCollection<Speaker> _speakers;
        public ObservableCollection<Speaker> Speakers
        {
            get { return _speakers; }
            set { _speakers = value; this.OnPropertyChanged(); }
        }

        private ICollectionView _speakersView;
        public ICollectionView SpeakersView
        {
            get { return _speakersView; }
            set { _speakersView = value; this.OnPropertyChanged(); }
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
            var speaker = new Speaker();
            _editView.AddNewItem(speaker);
            this.RefreshCommands();
            this.IsDeleteDialogOpen = false;
            this.CurrentState = EDIT_STATE;
        }

        private void ExecuteEditCommand(object parameter)
        {
            if (this.CurrentSpeaker != null)
            {
                _editView.EditItem(this.CurrentSpeaker);
                this.CurrentState = EDIT_STATE;
            }
            this.RefreshCommands();
        }

        private async void ExecuteOkCommand(object parameter)
        {
            if (await this.SaveChangesAsync())
            {
                this.CurrentState = NORMAL_STATE;
                this.RefreshCommands();
            }
        }

        private void ExecuteCancelEditCommand(object parameter)
        {
            if (_editView != null)
            {
                if (_editView.IsAddingNew)
                    _editView.CancelNew();
                else if (_editView.IsEditingItem && _editView.CanCancelEdit)
                    _editView.CancelEdit();
            }
            this.CurrentState = NORMAL_STATE;
            this.HasErrors = false;
            this.RefreshCommands();
        }

        private async void ExecuteDeleteCommand(object parameter)
        {
            var speaker = this.SpeakersView.CurrentItem as Speaker;
            if (speaker != null)
            {
                if (this.IsDeleteDialogOpen)
                {
                    this.IsBusy = true;
                    await _sessionService.DeleteSpeakerAsync(speaker.Id);
                    this.Speakers.Remove(speaker);
                    this.IsBusy = false;
                    _hasChanges = false;
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
}
