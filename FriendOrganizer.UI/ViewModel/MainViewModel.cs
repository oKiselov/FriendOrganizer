using System;
using System.Threading.Tasks;
using System.Windows.Input;
using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.View.Services;
using Prism.Commands;
using Prism.Events;

namespace FriendOrganizer.UI.ViewModel
{
    public class MainViewModel: ViewModelBase
    {
        private IEventAggregator _eventAggregator;

        private Func<IFriendDetailViewModel> _friendDetailViewModelCreator { get; }

        private IMessageDialogService _messageDialogService;

        private IFriendDetailViewModel _friendDetailViewModel;

        public IFriendDetailViewModel FriendDetailViewModel
        {
            get { return _friendDetailViewModel; }
            private set
            {
                _friendDetailViewModel = value; 
                OnPropertyChanged();
            }
        }

        public MainViewModel(INavigationViewModel navigationViewModel,
            Func<IFriendDetailViewModel> friendDetailViewModelCreator, 
            IEventAggregator eventAggregator, 
            IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;

            _friendDetailViewModelCreator = friendDetailViewModelCreator;

            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>()
                .Subscribe(OnOpenFriendDetailView);
            _eventAggregator.GetEvent<AfterFriendDeletedEvent>()
                .Subscribe(AfterFriendDeleted);

            _messageDialogService = messageDialogService;

            CreateNewFriendCommand = new DelegateCommand(OnCreateNewFriendExecute);

            NavigationViewModel = navigationViewModel;
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        public INavigationViewModel NavigationViewModel { get; }

        public ICommand CreateNewFriendCommand { get; }

        private async void OnOpenFriendDetailView(int? friendId)
        {
            if (FriendDetailViewModel != null && FriendDetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog("You've made changes. Navigatge anyway?", "Question");
                if (result == MessageDialogResult.CANCEL)
                {
                    return;
                }
            }
            FriendDetailViewModel = _friendDetailViewModelCreator();
            await FriendDetailViewModel.LoadAsync(friendId);
        }

        private void OnCreateNewFriendExecute()
        {
            OnOpenFriendDetailView(null);
        }

        private void AfterFriendDeleted(int friendId)
        {
            FriendDetailViewModel = null;

        }
    }
}