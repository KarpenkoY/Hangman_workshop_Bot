using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace Hangman_workshop_Bot.Models.Commands
{
    public class PlayCommand : Command
    {
        public override string Name => "/play";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            long chatId = message.Chat.Id;
            Game game = Game.GetGame(message.Chat.Id);
            int imageIndex = game.NotGuessedCharacters.Count;
            string imagePath = Game.Images[imageIndex];

            string answer = $"Lets begin!\n" +
                            $"{game.ProcessedWord}";

            using (var fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                await client.SendPhotoAsync(chatId, new InputOnlineFile(fileStream), answer);
            }

        }
    }
}
