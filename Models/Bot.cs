using Hangman_workshop_Bot.Models.Commands;
using System.Collections.Generic;
using System.Net;
using Telegram.Bot;

namespace Hangman_workshop_Bot.Models
{
    public class Bot
    {
        private static TelegramBotClient client;
        private static List<Command> commandsList;

        public static IReadOnlyList<Command> Commands => commandsList.AsReadOnly();

        public static TelegramBotClient GetBotClient()
        {
            if (client != null) return client;

            commandsList = new List<Command>();
            commandsList.AddRange
            (
                new Command[]
                {
                    new StartCommand(),
                    new PlayCommand(),
                    new LetterCommand(),
                    new WordCommand()
                }
            );

            client = new TelegramBotClient(BotSettings.Key);

            SetWebhook();

            return client;
        }
        internal static void SetWebhook()
        {
            WebRequest request = WebRequest.Create($"https://api.telegram.org/bot{BotSettings.Key}/setWebhook?url={BotSettings.Url}/api/bot");
            WebResponse response = request.GetResponse();
            
        }
    }
}
