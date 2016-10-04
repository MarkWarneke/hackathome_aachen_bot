using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;

namespace StockLuisDlg
{
    internal class DeductionInfoHandler : ITaskHandler
    {
        public async Task<string> handle(IDialogContext context, LuisResult result)
        {
            return "Dein aktueller Abschlag ist: unendlich Euro!";
        }
    }
}