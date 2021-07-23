using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
namespace TwitterBot.Classes
{
    class Methods
    {
        StringBuilder stringBuilder = new StringBuilder();
        Process process = new Process();
        InlineKeyboards inlineKeyboards = new InlineKeyboards();
        public void ResponceToStart(Update update, TelegramBotClient bot)
        {
            stringBuilder.AppendLine("Hello dear friend👋");
            stringBuilder.AppendLine("🔗Please Send your tweet id to me");
            stringBuilder.AppendLine("**📌For example : https://twitter.com/TwitterLive/status/925770404068601856**");
            bot.SendTextMessageAsync(update.Message.Chat.Id, stringBuilder.ToString(),
                ParseMode.Markdown, null, true, false);
        }

        public void GetTweet(Update update, TelegramBotClient bot)
        {
            bot.SendChatActionAsync(update.Message.Chat.Id, ChatAction.Typing);
            string name = ""; string text = "";
            int likeCount = 0; int retweetCount = 0;
            bool valid = process.GetTweet(ref text, ref name, ref likeCount, ref retweetCount, update.Message.Text.SecondTweetIdMethod(), update, bot);
            if(valid == false)
                bot.SendTextMessageAsync(update.Message.Chat.Id, "*🔍The tweet didn't find...*",
                    ParseMode.Markdown, null, false, false, update.Message.MessageId, false, null);
            else
            {
                stringBuilder.AppendLine(text);
                stringBuilder.AppendLine($"[{name}]({update.Message.Text})");
                bot.SendTextMessageAsync(update.Message.Chat.Id, stringBuilder.ToString(),
                    ParseMode.Markdown, null, true, false, update.Message.MessageId, false, inlineKeyboards.ShowDetail(likeCount, retweetCount, update.Message.Text));
            }
        }

        public void GetTrends(Update update, TelegramBotClient bot)
        {
            var items = process.GetTrends().Result.Where((i,n)=>n<5).Select(i => i);
            foreach(var item in items)
                stringBuilder.AppendLine(item);
            bot.SendTextMessageAsync(update.Message.Chat.Id, stringBuilder.ToString(),
                ParseMode.Default, null, false, false, update.Message.MessageId, false, null);
        }
    }

    static class TweetIdExtention
    {
        public static string SecondTweetIdMethod(this string field)
        {
            field.Remove(0, 20);
            string id = "";
            for(int index = field.Length - 1; index >= 0; index--)
            {
                if(char.IsDigit(field[index]))
                    id = field[index] + id;
                else
                    break;
            }
            return id;
        }
    }
}
