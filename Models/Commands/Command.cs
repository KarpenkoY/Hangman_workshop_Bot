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
            Game game = Game.GetGame(message.Chat.Id);

            string[] temp = message.Text.Split(" ");

            if (temp.Length == 2 && temp[1].Length == 1)
            {
                game.Attempt(temp[1].ToLower().FirstOrDefault());
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
