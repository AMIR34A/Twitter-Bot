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
        public static Telegram.Bot.TelegramBotClient bot = new Telegram.Bot.TelegramBotClient("Tokeb Bot");
        static HttpToSocks5Proxy proxy = new HttpToSocks5Proxy("fast.iproxypersonalproxy.club", 8443, "3dljg25v", "q1kHa7fZ");

        static void Main(string[] args)
        {
            Console.Title = "Twitter Bot";
            Handle handle = new Handle();
            bot.StartReceiving(new DefaultUpdateHandler(handle.HandleUpdateAsync, handle.HandleErrorAsync));

            do
            {
                Console.Clear();
            } while (Console.ReadLine() == "cls");
        }

        public class Handle
        {
            public Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
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
                            Handler handler = new Handler(up, bot);
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                }

            }
            #region HandleErrorAsync
            public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
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
