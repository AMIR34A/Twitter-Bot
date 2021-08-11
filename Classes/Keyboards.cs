using Telegram.Bot.Types.ReplyMarkups;

namespace TwitterBot.Classes
{
    class Keyboards
    {
        #region InlineKeyBoards
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
