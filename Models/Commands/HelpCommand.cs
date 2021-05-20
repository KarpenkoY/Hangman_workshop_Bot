using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace Hangman_workshop_Bot.Models.Commands
{
    public class HelpCommand : Command
    {
        public override string Name => "/help";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            long chatId = message.Chat.Id;

            string answer =     $"/new, /play - Start a new game\n" +
                                $"/l, /letter - To guess a letter\n" +
                                $"/w, /word - To guess a whole word\n" +
                                $"/help - Displays the list of available commands";

            await client.SendTextMessageAsync(chatId, answer,
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
        }
    }
}
