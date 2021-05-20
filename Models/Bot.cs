using System.Net;
using System.Collections.Generic;
using Telegram.Bot;
using Hangman_workshop_Bot.Models.Commands;

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
                    new PlayShortCommand(),
                    new PlayCommand(),
                    new LetterShortCommand(),
                    new LetterCommand(),
                    new WordShortCommand(),
                    new WordCommand(),
                    new HelpCommand()
                }
            );

            client = new TelegramBotClient(BotSettings.Key);

            SetWebhook();

            return client;
        }
        internal static void SetWebhook()
        {
            WebRequest request = WebRequest.Create($"https://api.telegram.org/bot{BotSettings.Key}/setWebhook?url={BotSettings.Url}/api/bot");
            request.GetResponse();
            
        }
    }
}
