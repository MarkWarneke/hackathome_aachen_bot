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
            return await Task.FromResult("Dein aktueller Abschlag ist: 42 Euro!");
        }

    }
}