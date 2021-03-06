using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
namespace TwitterBot.Classes
{
    class Methods
    {
        StringBuilder stringBuilder = new StringBuilder();
        Process process = new Process();
        Keyboards keyboards = new Keyboards();

        #region RespoceToStart
        public void ResponceToStart(Update update, TelegramBotClient bot)
        {
            stringBuilder.AppendLine("Hello dear friend👋");
            stringBuilder.AppendLine("🔗Please Send your tweet id to me");
            stringBuilder.AppendLine("**📌For example : https://twitter.com/TwitterLive/status/925770404068601856**");
            bot.SendTextMessageAsync(update.Message.Chat.Id, stringBuilder.ToString(),
                ParseMode.Markdown, null, true, false, null, null, keyboards.ReplyKeyboard());
        }
        #endregion

        #region GetTweet
        public void GetTweet(Update update, TelegramBotClient bot)
        {
            bot.SendChatActionAsync(update.Message.Chat.Id, ChatAction.Typing);
            string screenName = ""; string name = ""; string text = "";
            int likeCount = 0; int retweetCount = 0;
            bool valid = process.GetTweet(ref screenName, ref text, ref name, update.Message.Text.TweetIdMethod(), ref likeCount, ref retweetCount, update, bot);
            if (valid == false)
                bot.SendTextMessageAsync(update.Message.Chat.Id, "*🔍The tweet didn't find...*",
                    ParseMode.Markdown, null, false, false, update.Message.MessageId, false, null);
            else
            {
                stringBuilder.AppendLine(text);
                stringBuilder.AppendLine($"🖋<a href=\"{update.Message.Text}\">{name}</a>");
                bot.SendTextMessageAsync(update.Message.Chat.Id, stringBuilder.ToString(),
                    ParseMode.Html, null, true, false, update.Message.MessageId, false, keyboards.ShowDetail(screenName, name, update.Message.Text.TweetIdMethod(), likeCount, retweetCount, update.Message.Text));
            }
        }
        #endregion

        #region GetTrends
        public void GetTrends(Update update, TelegramBotClient bot)
        {
            var medal = new[] { "🥇", "🥈", "🥉" };
            var coordinate = update.Message.Text.Split(' ');
            var items = (coordinate.Length == 3) ? process.GetTrends(double.Parse(coordinate[1]), double.Parse(coordinate[2])).Result.Select(i => i) :
                process.GetTrends().Result.Select(i => i);
            int index = 0;
            stringBuilder.AppendLine($"<b>🗺{process.Country} :</b>");
            foreach (var item in items)
            {
                stringBuilder.AppendFormat("{0}{1}\n", (index < 3) ? medal[index] : "•", item.Name);
                if (item.TweetVolume.HasValue)
                    stringBuilder.AppendLine($"<b>🎉{item.TweetVolume.Value}</b> Tweets");
                index++;
            }

            bot.SendTextMessageAsync(update.Message.Chat.Id, stringBuilder.ToString(),
                ParseMode.Html, null, false, false, update.Message.MessageId, false, null);
        }
        #endregion
    }

    #region GetTweetId
    static class TweetIdExtention
    {
        public static string TweetIdMethod(this string field)
        {
            string id = "";
            for (int index = field.Length - 1; index >= 0; index--)
            {
                if (char.IsDigit(field[index]))
                    id = field[index] + id;
                else
                    break;
            }
            return id;
        }
    }
    #endregion
}
