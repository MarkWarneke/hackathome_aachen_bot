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
            list.Add("Willkommen! Wie kann ich helfen?");
            list.Add("Guten Tag, Markus. Was kann ich heute für dich tun?");
            list.Add("Hi Markus. Womit kann ich helfen?");
            list.Add("Hey, Markus. Wie kann man helfen? :)");
            list.Add("Guten Tag, Markus. Wie kann ich dir heute helfen?");
            return list[rnd.Next(0,4)];


        }
    }
}