using Contracts;
using Services.SessionServiceReference;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows.Data;

namespace ScheduleModule.ViewModels
{
    [ModuleMetadata("ScheduleModule", "Zeitplaner", "Schedule", 3)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ScheduleViewModel : ViewModelBase, IModule
    {
        private ISessionService _sessionService;

        public async void Initialize(IServicePool servicePool)
        {
            _sessionService = servicePool.GetService<ISessionService>();

            this.AddCommand = new DelegateCommand(p => true, this.ExecuteAddCommand);
            this.DeleteCommand = new DelegateCommand(p => true, this.ExecuteDeleteCommand);
            this.CancelDeleteCommand = new DelegateCommand(p => true, this.ExecuteCancelDeleteCommand);
            this.IsBusy = true;

            _sessionList = await _sessionService.GetSessionListAsync();
            _sessionList.Insert(0, new SessionDto { Title = "" });
            this.SessionsView = new ListCollectionView(_sessionList);

            this.Slots = await _sessionService.AllSlotsAsync();
            this.SlotsView = new ListCollectionView(this.Slots);
            foreach (var item in this.Slots)
            {
                item.PropertyChanged += this.OnScheduleItemPropertyChanged;
            }
            this.IsBusy = false;
            this.ErrorText = "Mindestens eine Session wurde mehrfach zugewiesen!";
        }

        private async void OnScheduleItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SessionId")
            {
                foreach (var schedule in this.Slots)
                {
                    foreach (var item in this.Slots)
                    {
                        if (item.SessionId == schedule.SessionId &&
                            item.SessionId > 0 &&
                            item != schedule)
                        {
                            this.HasErrors = true;
                            return;
                        }
                    }
                }
                this.HasErrors = false;
                var currentSlot = sender as Slot;
                this.IsBusy = true;
                await _sessionService.UpdateSlotAsync(currentSlot);
                this.IsBusy = false;
            }
        }
                
        public IView GetView()
        {
            return ViewFactory.CreateView(this);
        }

        public void Close()
        {
        }

        #region Properties
        
        private ObservableCollection<Slot> _slots;
        public ObservableCollection<Slot> Slots
        {
            get { return _slots; }
            set { _slots = value; this.OnPropertyChanged(); }
        }

        private ICollectionView _slotsView;
        public ICollectionView SlotsView
        {
            get { return _slotsView; }
            set { _slotsView = value; this.OnPropertyChanged(); }
        }

        private ObservableCollection<SessionDto> _sessionList;
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

        private bool _isWarningBoxOpen;
        public bool IsWarningBoxOpen
        {
            get { return _isWarningBoxOpen; }
            set { _isWarningBoxOpen = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Command Handling

        private void ExecuteAddCommand(object parameter)
        {
        }
        
        private void ExecuteDeleteCommand(object parameter)
        {
        }

        private void ExecuteCancelDeleteCommand(object parameter)
        {
        }

        #endregion

    }
}
