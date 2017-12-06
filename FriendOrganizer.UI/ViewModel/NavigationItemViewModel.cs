using System.Windows.Input;
using FriendOrganizer.UI.Event;
using Prism.Commands;
using Prism.Events;

namespace FriendOrganizer.UI.ViewModel
{
    public class NavigationItemViewModel: ViewModelBase
    {
        private string _displayMember;
        private IEventAggregator _eventAggregator;

        public string DisplayMember
        {
            get { return _displayMember; }
            set
            {
                _displayMember = value; 
                OnPropertyChanged();
            }
        }

        public int Id { get; }

        public NavigationItemViewModel(int id, 
            string displayMember,
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Id = id;
            DisplayMember = displayMember;
            OpenFriendDetailViewCommand = new DelegateCommand(OnOpenFriendDetailViewCommand);
        }

        private void OnOpenFriendDetailViewCommand()
        {
            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>()
                .Publish(Id);
        }

        public ICommand OpenFriendDetailViewCommand { get; }
    }
}