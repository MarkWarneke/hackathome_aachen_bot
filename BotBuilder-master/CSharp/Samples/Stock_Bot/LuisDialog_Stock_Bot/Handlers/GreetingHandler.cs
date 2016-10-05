using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using System.Collections.Generic;

namespace StockLuisDlg
{
    internal class GreetingHandler : ITaskHandler
    {
        public async Task<string> handle(IDialogContext context, LuisResult result)
        {
            List<string> list = new List<string>();
            Random rnd = new Random();
            list.Add("Hey Markus! How can I help you today?");
            list.Add("Good Morning Markus. What can I do for you?");
            list.Add("Hi Markus. How can I assist you?");
            return list[rnd.Next(0,2)];


        }
    }
}