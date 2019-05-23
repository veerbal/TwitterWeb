namespace Examplinvi.Web.Models
{
    using System;

    public class TweetViewModel
    {
        public string Id { get; set; }

        public string Author { get; set; }

        public string Text { get; set; }

        public int UsersFavouriteCount { get; set; }

        public int RepliesCount { get; set; }

        public int RetweetsCount { get; set; }

        public DateTime DatePosted { get; set; }
    }
}