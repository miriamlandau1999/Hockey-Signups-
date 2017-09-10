using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HockeySignupsWebApplication.data
{
    public class Game
    {
        public int Id { get; set; }
        public int MaxPlayers { get; set; }
        public DateTime Date { get; set; }
    }
}
