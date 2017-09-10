using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HockeySignupsWebApplication.data
{
    public class Player
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
