using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.Data.Lookups;
using FriendOrganizer.UI.Event;
using Prism.Events;

namespace FriendOrganizer.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private IFriendLookupDataService _friendLookupDataService;
        private IEventAggregator _eventAggregator;

        public ObservableCollection<NavigationItemViewModel> Friends { get; }

        public NavigationViewModel(IFriendLookupDataService friendLookupDataService,
            IEventAggregator eventAggregator)
        {
            _friendLookupDataService = friendLookupDataService;
            _eventAggregator = eventAggregator;
            Friends = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(AfteDetailSaved);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
        }

        public async Task LoadAsync()
        {
            var lookup = await _friendLookupDataService.GetFriendLookupAsync();
            Friends.Clear();
            foreach (var item in lookup)
            {
                Friends.Add(new NavigationItemViewModel(item.Id, item.DisplayMember, nameof(FriendDetailViewModel), _eventAggregator));
            }
        }

        private void AfteDetailSaved(AfterDetailSavedEventArgs obj)
        {
            switch (obj.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    var lookup = Friends.SingleOrDefault(l => l.Id.Equals(obj.Id));
                    if (lookup == null)
                    {
                        Friends.Add(new NavigationItemViewModel(obj.Id, obj.DisplayMember,
                            nameof(FriendDetailViewModel), _eventAggregator));
                    }
                    else
                    {
                        lookup.DisplayMember = obj.DisplayMember;
                    }
                    break;
            }
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):

                    var friend = Friends.SingleOrDefault(entity => entity.Id == args.Id);
                    if (friend != null)
                    {
                        Friends.Remove(friend);
                    }
                    break;
            }
        }
    }
}