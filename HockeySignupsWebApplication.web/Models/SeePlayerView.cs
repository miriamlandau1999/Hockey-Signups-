using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HockeySignupsWebApplication.data;
namespace HockeySignupsWebApplication.web.Models
{
    public class SeePlayerView
    {
       public IEnumerable<Player> Players { get; set; }
       public int GameId { get; set; }
    }
}