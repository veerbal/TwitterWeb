using System.Web.Mvc;
using Tweetinvi;
using Tweetinvi.Core.Credentials;
using Examplinvi.Web.Models;
using System.Collections.Generic;

namespace Examplinvi.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public static string CONSUMER_KEY = "QHoHMuEJazwUxViQvGBM5fI1n";
        public static string CONSUMER_SECRET = "60jQ5WCY8Eaegk49EJvKnmWpNEtLEYrLbbG34RyITKeRXtVhWp";

        public ActionResult TwitterAuth()
        {
            var appCreds = new ConsumerCredentials(CONSUMER_KEY, CONSUMER_SECRET);
            var redirectURL = "http://" + Request.Url.Authority + "/Home/ValidateTwitterAuth";
            var url = CredentialsCreator.GetAuthorizationURL(appCreds, redirectURL);

            return new RedirectResult(url);
        }

        public ActionResult ValidateTwitterAuth()
        {
            var verifierCode = Request.Params.Get("oauth_verifier");
            var authorizationId = Request.Params.Get("authorization_id");

            var userCreds = CredentialsCreator.GetCredentialsFromVerifierCode(verifierCode, authorizationId);
            var user = Tweetinvi.User.GetLoggedUser(userCreds);

            ViewBag.User = user;

            var homeTimeline = user.GetHomeTimeline();            

            List<TweetViewModel> tweetList = new List<TweetViewModel>();
            foreach (var tweet in homeTimeline)
            {
                TweetViewModel tweetView = new TweetViewModel();
                tweetView.Id = tweet.Id.ToString();
                tweetView.Author = tweet.CreatedBy.ToString();
                tweetView.Text = tweet.Text.ToString();
                tweetView.DatePosted = tweet.CreatedAt;
                tweetView.UsersFavouriteCount = tweet.FavouriteCount;
                tweetView.RetweetsCount = tweet.RetweetCount;
                //tweetView.RepliesCount = 
                tweetList.Add(tweetView);
            }

            return View(tweetList);
        }
    }
}