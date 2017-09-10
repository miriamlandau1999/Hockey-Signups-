using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace HockeySignupsWebApplication.data
{
    public class HockeyDb
    {
        private string _connectionString;
        public HockeyDb(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddGame(Game Game)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Games VALUES(@date, @maxPlayers)";
            command.Parameters.AddWithValue("@date", Game.Date);
            command.Parameters.AddWithValue("@maxPlayers", Game.MaxPlayers);
            connection.Open();
            command.ExecuteNonQuery();

        }
        public Game GetUpcomingGame()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT TOP(1)* FROM Games WHERE Date >= @date";
            command.Parameters.AddWithValue("@date", DateTime.Now);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            Game game = new Game();
            while (reader.Read())
            {
                game.Id = (int)reader["Id"];
                game.Date = (DateTime)reader["Date"];
                game.MaxPlayers = (int)reader["MaxPlayers"];
            }
            return game;
        }
        public int PlayersJoined(int gameId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT (Id) AS TotalPlayers FROM Players WHERE GameId = @gameId";
            command.Parameters.AddWithValue("@gameId", gameId);
            connection.Open();
            return int.Parse(command.ExecuteScalar().ToString());
        }
        public void AddPlayer(Player player)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Players VALUES(@gameId, @FirstName, @LastName, @Email)";
            command.Parameters.AddWithValue("@gameId", player.GameId);
            command.Parameters.AddWithValue("@FirstName", player.FirstName);
            command.Parameters.AddWithValue("@LastName", player.LastName);
            command.Parameters.AddWithValue("@Email", player.Email);
            connection.Open();
            command.ExecuteNonQuery();
        }
        public IEnumerable<GamesWithPlayerCount> GetGamesWithPlayerCount()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "GamesWithPlayers";
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            List<GamesWithPlayerCount> games = new List<GamesWithPlayerCount>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                GamesWithPlayerCount game = new GamesWithPlayerCount
                {
                    Id = (int)reader["Id"],
                    Date = (DateTime)reader["Date"],
                    MaxPlayers = (int)reader["MaxPlayers"],
                    PlayerCount = (int)reader["TotalPlayers"],
                };
                games.Add(game);
                
            }
            return games;
        }
        public IEnumerable<Player> GetPlayers(int gameId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Players WHERE GameId = @gameId";
            command.Parameters.AddWithValue("@gameId", gameId);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Player> players = new List<Player>();
            while (reader.Read())
            {
                Player player = new Player
                {
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Email = (string)reader["Email"]
                };
                players.Add(player);
            }
            return players;
        }
        public void AddSignUp(Player player)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO NotificationSignUps VALUES(@FirstName, @LastName, @Email)";
            command.Parameters.AddWithValue("@FirstName", player.FirstName);
            command.Parameters.AddWithValue("@LastName", player.LastName);
            command.Parameters.AddWithValue("@Email", player.Email);
            connection.Open();
            command.ExecuteNonQuery();
        }
        public IEnumerable<Player> GetSignUps()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM NotificationSignUps";
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Player> players = new List<Player>();
            while (reader.Read())
            {
                Player player = new Player
                {
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Email = (string)reader["Email"]
                };
                players.Add(player);
            }
            return players;
        }

    }
}
