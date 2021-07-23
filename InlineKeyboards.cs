using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace TwitterBot.Classes
{
    class InlineKeyboards
    {
        public InlineKeyboardMarkup ShowDetail(int likeCount, int? retweetCount, string url)
        {
            var keyboard = new InlineKeyboardButton[][]
            {
                new InlineKeyboardButton[]
                {
                     InlineKeyboardButton.WithCallbackData($"🔄{retweetCount}") , InlineKeyboardButton.WithCallbackData($"❤️{likeCount}")
                }
            };
            return new InlineKeyboardMarkup(keyboard);
        }
    }
}
