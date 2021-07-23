using MihaZupan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterBot.Classes;
namespace TwitterBot
{
    class Program
    {

        static HttpToSocks5Proxy proxy = new HttpToSocks5Proxy("fast.iproxypersonalproxy.club", 8443, "3dljg25v", "q1kHa7fZ");

        public static Telegram.Bot.TelegramBotClient bot = new Telegram.Bot.TelegramBotClient("1874331325:AAHMw8_QNcIIwI2mNvnY3bWiZ4U0PmWJJkk");
        static void Main(string[] args)
        {
            bot.OnMessage += Bot_OnMessage;
            bot.StartReceiving();
            Console.ReadKey();
        }

        private static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            int offset = 0;

            while(true)
            {
                try
                {
                    var updates = bot.GetUpdatesAsync(offset).Result;
                    foreach(var update in updates)
                    {
                        offset = ++update.Id;
                        Handler messagesHandler = new Handler(update, bot);
                    }
                }
                catch(Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }
    }
}
