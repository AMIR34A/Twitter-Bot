using Telegram.Bot.Types.ReplyMarkups;

namespace TwitterBot.Classes
{
    class Keyboards
    {
        #region InlineKeyBoards
        public InlineKeyboardMarkup ShowDetail(string screenName, string name, string tweetId, int likeCount, int? retweetCount, string url)
        {
            var keyboard = new InlineKeyboardButton[][]
            {
                new InlineKeyboardButton[]
                {
                     InlineKeyboardButton.WithUrl($"🔄{retweetCount}",$"https://twitter.com/intent/retweet?tweet_id={tweetId}") , InlineKeyboardButton.WithUrl($"❤️{likeCount}",$"https://twitter.com/intent/like?tweet_id={tweetId}")
                },
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithUrl(name,$"https://twitter.com/intent/follow?screen_name={screenName}")
                }
            };
            return new InlineKeyboardMarkup(keyboard);
        }
        #endregion

        #region ReplyKeyboards
        public ReplyKeyboardMarkup ReplyKeyboard()
        {
            var keyboards = new KeyboardButton[][]
            {
                new[] { new KeyboardButton("⚙️Settings") }
            };
            return new ReplyKeyboardMarkup(keyboards) { ResizeKeyboard = true };
        }
        #endregion
    }
}
