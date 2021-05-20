using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace Hangman_workshop_Bot.Models.Commands
{
    public class LetterCommand : Command
    {
        public override string Name => "/letter";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            long chatId         = message.Chat.Id;
            string answer       = PrepareAnswer(message);
            Game game           = Game.GetGame(message.Chat.Id);
            int imageIndex      = game.NotGuessedCharacters.Count;
            string imagePath    = Game.Images[imageIndex];

            if (game.IsLose())
            {
                answer = game.Lose(answer);
            }
            else if (game.IsWin())
            {
                answer = game.Win(answer);
            }

            using (var fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                await client.SendPhotoAsync(chatId, new InputOnlineFile(fileStream), answer);
            }
        }
    }
}
