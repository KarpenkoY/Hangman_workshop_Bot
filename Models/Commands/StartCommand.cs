using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Hangman_workshop_Bot.Models.Commands
{
    public class StartCommand : Command
    {
        public override string Name => @"/start";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            
            await client.SendTextMessageAsync
            (
                message.Chat.Id, 
                "Welcome to Hagman game.", 
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown
            );
        }
    }
}
