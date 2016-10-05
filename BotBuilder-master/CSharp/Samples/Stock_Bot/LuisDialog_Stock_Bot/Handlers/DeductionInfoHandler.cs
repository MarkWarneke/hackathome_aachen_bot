using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using LuisDialog_Stock_Bot;

namespace StockLuisDlg
{
    internal class DeductionInfoHandler : ITaskHandler
    {
        public async Task<string> handle(IDialogContext context, LuisResult result)
        {
            return TextPersonalizer.parse(context, "Dein aktueller Abschlag liegt bei 212,51€.");
        }

    }
}