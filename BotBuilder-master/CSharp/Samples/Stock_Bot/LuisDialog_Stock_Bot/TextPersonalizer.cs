using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuisDialog_Stock_Bot
{
    public class TextPersonalizer
    {

        public static string parse(IDialogContext context, string value)
        {
            context.ConversationData.SetValue<bool>("polite", true);

            bool polite = false;
            context.ConversationData.TryGetValue<bool>("polite", out polite);
            if (polite)
            {
                value = value.Replace("dein", "Ihr");
                value = value.Replace("deine", "Ihre");
                value = value.Replace("deinen", "Ihren");
                value = value.Replace("du", "Sie");

                value = value.Replace("Dein", "Ihr");
                value = value.Replace("Deine", "Ihre");
                value = value.Replace("Deinen", "Ihren");
                value = value.Replace("Du", "Sie");
            }
            return value;
        }
    }
}