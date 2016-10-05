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
            list.Add("You are welcome, have a fantastic weekend.");
            list.Add("You are welcome, have a fantastic weekend.");
            list.Add("You are welcome, have a fantastic weekend.");
            list.Add("You are welcome, have a fantastic weekend.");
            return list[rnd.Next(0,3)];


        }
    }
}