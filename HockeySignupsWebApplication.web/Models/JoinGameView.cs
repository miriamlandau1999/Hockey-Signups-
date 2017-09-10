using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HockeySignupsWebApplication.data;
namespace HockeySignupsWebApplication.web.Models
{
    public class JoinGameView
    {
        public Game game { get; set; }
        public int PlayersJoined { get; set; }
        public Player player { get; set; }
    }
}