using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using System.Collections.Generic;

namespace StockLuisDlg
{
    internal class GoodbyeHandler : ITaskHandler
    {
        public async Task<string> handle(IDialogContext context, LuisResult result)
        {
            List<string> list = new List<string>();
            Random rnd = new Random();
            list.Add("Ich wünsche noch einen schönen Tag.");
            list.Add("Bis dann!");
            list.Add("Noch ein schönes Wochenende.");
            list.Add("Noch ein schönes Wochenende.");
            list.Add("Noch ein schönes Wochenende.");
            list.Add("Noch ein schönes Wochenende.");
            return list[rnd.Next(0,5)];


        }
    }
}