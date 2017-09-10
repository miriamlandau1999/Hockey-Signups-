using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HockeySignupsWebApplication.data;

namespace HockeySignupsWebApplication.web.Models
{
    public class SeeHistoryModel
    {
        public IEnumerable<GamesWithPlayerCount> Games { get; set; }
    }
}