using System;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TwitterBot.Classes
{
    class Handler
    {
        Methods method = new Methods();
        public event EventHandler<HandleEventArgs> Handle;

        #region ResponseToMessage
        public void ResponseToText(object sender, HandleEventArgs args)
        {
            switch (args.Update.Message.Text.ToLower())
            {
                case "/start":
                    method.ResponceToStart(args.Update, args.TelegramBotClient);
                    break;

                case "⚙️Settings":
                    break;

                default:
                    if (args.Update.Message.Text.Contains("https://twitter.com/") || args.Update.Message.Text.Contains("http://twitter.com/"))
                        method.GetTweet(args.Update, args.TelegramBotClient);
                    else if (args.Update.Message.Text.Contains("/trends"))
                        method.GetTrends(args.Update, args.TelegramBotClient);
                    break;
            }
        }
        #endregion

        #region ResponseToCallbackQuery
        public void ResponseToCallbackQuery(object sender, HandleEventArgs args)
        {
            switch (args.Update.CallbackQuery.Data)
            {
                case "SendToChannel":
                    if (args.Update.CallbackQuery.From.Id == 907872086)
                        method.ResponseToSettings(args.Update, args.TelegramBotClient);
                    break;
            }
        }
        #endregion

        public void Run(Update update, TelegramBotClient bot) => Handle(this, new HandleEventArgs { Update = update, TelegramBotClient = bot });
    }

    #region HandleEventArgs
    public class HandleEventArgs : EventArgs
    {
        public Update Update { get; set; }
        public TelegramBotClient TelegramBotClient { get; set; }
    }
    #endregion
}

