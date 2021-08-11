using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;

namespace TwitterBot.Classes
{
    class Process
    {
        public string Country { get; set; }
        TwitterClient userClient = new TwitterClient("zAra0T5tcxYqoZzGbzNn8zB1h", "2VUh6nBQO2OX7GHDMPLWaxmRziXrZyjaUWOn0COVtIP6MxoV8O", "1297246709253394432-HZd5WrvERipQyVNwcmsYS7kGrikeje", "FhNO7jinFZmHKPyJoRLnAsDXmK8QWK9VF6NwnSz38myXO");

        #region GetTweet
        public bool GetTweet(ref string text, ref string name, ref int likeCount, ref int retweetCount, string id, Telegram.Bot.Types.Update update, Telegram.Bot.TelegramBotClient bot)
        {
            bool valid = true;
            //userClient.Config.ProxyConfig = new ProxyConfig("http://fast.iproxypersonalproxy.club")
            //{
            //    Credentials = new NetworkCredential("3dljg25v", "q1kHa7fZ")
            //};
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                var tweet = userClient.Tweets.GetTweetAsync(long.Parse(id));
                text = tweet.Result.FullText;
                name = tweet.Result.CreatedBy.Name;
                name = tweet.Result.CreatedBy.Name;
                likeCount = tweet.Result.FavoriteCount;
                retweetCount = tweet.Result.RetweetCount;
            }
            catch
            {
                valid = false;
            }
            return valid;
        }
        #endregion 

        #region GetTrends
        public async Task<List<ITrend>> GetTrends(double latitude = 32.5176, double longitude = 59.1042)
        {
            var coordinates = new Coordinates(latitude, longitude);
            var trendingLocations = await userClient.Trends.GetTrendsLocationCloseToAsync(coordinates);
            var trends = await userClient.Trends.GetPlaceTrendsAtAsync(trendingLocations[0].WoeId);
            var result = trends.Trends.Where((t, i) => i <= 4).Select(t => t).ToList();
            Country = trendingLocations[0].Country;
            return await Task.FromResult(result);
        }
        #endregion
    }
}
