using HockeySignupsWebApplication.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HockeySignupsWebApplication.web.Models;

namespace HockeySignupsWebApplication.web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddGame(Game game)
        {
            HockeyDb db = new HockeyDb(Properties.Settings.Default.ConStr);
            db.AddGame(game);

            IEnumerable<Player> signUps = db.GetSignUps();
            Email sendEmails = new Email();
            foreach (Player signUp in signUps)
            {
                string emailBody = $"Hi {signUp.FirstName} {signUp.LastName}, \n \t Great news.... We've posted a new upcoming game scheduled for {game.Date.ToShortDateString()}. SignUp as long as there's still room. Hope to see you!!";
                sendEmails.SendEmail(signUp.Email, "New Game Posted!!", emailBody);
            }
            return Redirect("/");
        }
        public ActionResult CreateGame()
        {
            
            return View();
        }
        public ActionResult JoinGame()
        {
            HockeyDb db = new HockeyDb(Properties.Settings.Default.ConStr);
            JoinGameView jgv = new JoinGameView();
            jgv.game = db.GetUpcomingGame();
            jgv.PlayersJoined = db.PlayersJoined(jgv.game.Id);
            jgv.player = new Player();
            HttpCookie FirstName = Request.Cookies["FirstName"];
            HttpCookie LastName = Request.Cookies["LastName"];
            HttpCookie Email = Request.Cookies["Email"];
            if (FirstName != null)
            {
                jgv.player.FirstName = FirstName.Value;
            }
            if (LastName != null)
            {
                jgv.player.LastName = LastName.Value;
            }
            if (Email != null)
            {
                jgv.player.Email = Email.Value;
            }
            
            return View(jgv);
        }
        [HttpPost]
        public ActionResult AddPlayer(Player player)
        {
            HttpCookie FirstName = new HttpCookie("FirstName", player.FirstName);
            HttpCookie LastName = new HttpCookie("LastName", player.LastName);
            HttpCookie Email = new HttpCookie("Email", player.Email);
            Response.Cookies.Add(FirstName);
            Response.Cookies.Add(LastName);
            Response.Cookies.Add(Email);
            HockeyDb db = new HockeyDb(Properties.Settings.Default.ConStr);
            db.AddPlayer(player);
            return Redirect("/");
        }
        public ActionResult SeeHistory()
        {
            SeeHistoryModel shm = new SeeHistoryModel();
            HockeyDb db = new HockeyDb(Properties.Settings.Default.ConStr);
            shm.Games = db.GetGamesWithPlayerCount();
            return View(shm);
        }
        public ActionResult SeePlayers(int Id)
        {
            SeePlayerView spv = new SeePlayerView();
            HockeyDb db = new HockeyDb(Properties.Settings.Default.ConStr);
            spv.Players = db.GetPlayers(Id);
            spv.GameId = Id;
           return View(spv);
        }
        public ActionResult NotificationSignUp()
        {
            NotificationSignUpView signUpView = new NotificationSignUpView();
            signUpView.SignUp = new Player();
            HttpCookie FirstName = Request.Cookies["FirstName"];
            HttpCookie LastName = Request.Cookies["LastName"];
            HttpCookie Email = Request.Cookies["Email"];
            if (FirstName != null)
            {
                signUpView.SignUp.FirstName = FirstName.Value;
            }
            if (LastName != null)
            {
                signUpView.SignUp.LastName = LastName.Value;
            }
            if (Email != null)
            {
                signUpView.SignUp.Email = Email.Value;
            }
            return View(signUpView);
        }
        public ActionResult AddSignUP(Player signUp)
        {
            HttpCookie FirstName = new HttpCookie("FirstName", signUp.FirstName);
            HttpCookie LastName = new HttpCookie("LastName", signUp.LastName);
            HttpCookie Email = new HttpCookie("Email", signUp.Email);
            Response.Cookies.Add(FirstName);
            Response.Cookies.Add(LastName);
            Response.Cookies.Add(Email);
            HockeyDb db = new HockeyDb(Properties.Settings.Default.ConStr);
            db.AddSignUp(signUp);
            Email email = new Email();
            email.SendEmail(signUp.Email, "Sign Up confirmation email", $"Welcome {signUp.FirstName} {signUp.LastName} to our Games. You've been signed up for weekly notifications");
            return Redirect("/");
        }


    }
}