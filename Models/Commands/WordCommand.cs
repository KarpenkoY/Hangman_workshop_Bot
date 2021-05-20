using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace Hangman_workshop_Bot.Models.Commands
{
    public class WordCommand : Command
    {
        public override string Name => "/w";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            long chatId = message.Chat.Id;
            Game game = Game.GetGame(message.Chat.Id);

            string answer = default;
            string imagePath;

            if (!game.GuessWholeWord(message.Text))
            {
                answer = game.Lose(answer);
                imagePath = Game.Images[Game.Images.Count - 1];
            }
            else
            {
                answer = game.Win(answer);
                imagePath = Game.Images[0];
            }

            using (var fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                await client.SendPhotoAsync(chatId, new InputOnlineFile(fileStream), answer);
            }

        }
    }
}
