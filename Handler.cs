using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterBot.Classes
{
    class Handler
    {
        Action<Telegram.Bot.Types.Update, Telegram.Bot.TelegramBotClient> responce;
        Methods method = new Methods();
        public Handler(Telegram.Bot.Types.Update update, Telegram.Bot.TelegramBotClient bot)
        {
            if(update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                if(update.Message.Text == "/start")
                    responce += method.ResponceToStart;

                else if(update.Message.Text.StartsWith("https://twitter.com/"))
                    responce += method.GetTweet;
                else if(update.Message.Text == "/Trends")
                    responce += method.GetTrends;
                else
                    responce = null;

                responce(update, bot);
            }
        }
    }
}
