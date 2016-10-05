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
            return "Your current deduction is being calculated with '(price x usage + base_price) / 12. For you personally, that means (50x50+50)/12 = 212,5€.";
        }

    }
}