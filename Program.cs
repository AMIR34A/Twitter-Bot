using MihaZupan;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using TwitterBot.Classes;

namespace TwitterBot
{
    class Program
    {
        public static Telegram.Bot.TelegramBotClient bot = new Telegram.Bot.TelegramBotClient("TokenBot");
        static HttpToSocks5Proxy proxy = new HttpToSocks5Proxy("HostName", 0, "Username", "Password");

        static void Main(string[] args)
        {
            Console.Title = "Twitter Bot";
            bot.StartReceiving(new DefaultUpdateHandler(Handle.HandleUpdateAsync, Handle.HandleErrorAsync));

            do
            {
                Console.Clear();
            } while (Console.ReadLine() == "cls");
        }

        public class Handle
        {
            public static Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {
                int ofsset = 0;
                while (true)
                {
                    try
                    {
                        var updates = botClient.GetUpdatesAsync(ofsset).Result;
                        foreach (var up in updates)
                        {
                            ofsset = ++up.Id;

                            Handler handler = new Handler();
                            if (up.CallbackQuery != null)
                            {
                                handler.Handle += handler.ResponseToCallbackQuery;
                                handler.Action(up, bot);
                            }
                            else if (up.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
                            {
                                handler.Handle += handler.ResponseToText;
                                handler.Action(up, bot);
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                }
            }
            #region HandleErrorAsync
            public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
            {
                if (exception is ApiRequestException apiRequestException)
                {
                    await botClient.SendTextMessageAsync(123, apiRequestException.ToString());
                }
            }
            #endregion
        }
    }
}
