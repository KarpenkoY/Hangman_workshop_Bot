using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Hangman_workshop_Bot.Models;

namespace Workshop.HangmanDemo.Api.Controllers
{
    [Route("api/bot")]
    [ApiController]
    public class BotController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            if (update == null)             return Ok();
            if (update.Message == null)     return Ok();

            var botClient   = Bot.GetBotClient();
            var commands    = Bot.Commands;
            var message     = update.Message;
            
            foreach (var command in commands)
            {
                if (command.Contains(message))
                {
                    await command.Execute(message, botClient);
                    break;
                }
            }

            return Ok();
        }
    }
}