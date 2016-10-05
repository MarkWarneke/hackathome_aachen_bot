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
            ConsumingRest rest = new ConsumingRest("http://shruggieuserrest.azurewebsites.de/");

            DataObject obj =  await rest.getObj("api/users/111/deduction");

            return TextPersonalizer.parse(context, "Dein aktueller Abschlag liegt bei " + obj.value +  "€.");
        }

    }
}