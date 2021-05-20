using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Hangman_workshop_Bot.Models.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }
        public bool Contains(Message message) 
        {
            if (message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
                return false;

            return message.Text.Contains(this.Name);
        }
        internal string PrepareAnswer(Message message)
        {
            string[] input = message.Text.Split(" ");

            Game game = Game.GetGame(message.Chat.Id);

            if (input.Length == 2 && input[1].Length == 1)
            {
                game.Attempt(input[1].ToLower().FirstOrDefault());
            }

            string answer = game.ProcessedWord;

            if (game.IsMistaken())
            {
                string ngc = string.Concat(game.NotGuessedCharacters.ToArray());
                answer = $"{answer}\n{ngc}";
            }

            return answer;
        }

        public abstract Task Execute(Message message, TelegramBotClient client);
    }
}
