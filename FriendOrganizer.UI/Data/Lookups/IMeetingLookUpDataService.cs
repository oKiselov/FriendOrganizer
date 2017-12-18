﻿using System.Collections.Generic;
using System.Threading.Tasks;
using FriendOrganizer.Model;

namespace FriendOrganizer.UI.Data.Lookups
{
    public interface IMeetingLookUpDataService
    {
        Task<List<LookupItem>> GetMeetingLookupAsync();
    }
}