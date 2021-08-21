using System;

namespace TwitterBot.Classes
{
    class Handler
    {
        Action<Telegram.Bot.Types.Update, Telegram.Bot.TelegramBotClient> responce;
        Methods method = new Methods();
        public Handler(Telegram.Bot.Types.Update update, Telegram.Bot.TelegramBotClient bot)
        {
            if (update.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                switch (update.Message.Text.ToLower())
                {
                    case "/start":
                        responce += method.ResponceToStart;
                        break;

                    case "/trends":
                        responce += method.GetTrends;
                        break;

                    case "⚙️Settings":
                        break;

                    default:
                        if (update.Message.Text.Contains("https://twitter.com/") || update.Message.Text.Contains("http://twitter.com/"))
                            responce += method.GetTweet;
                        break;
                }

                if (responce != null)
                    responce(update, bot);
            }
        }
    }
}
