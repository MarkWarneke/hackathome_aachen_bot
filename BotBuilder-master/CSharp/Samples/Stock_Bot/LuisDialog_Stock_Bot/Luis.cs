using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockLuisDlg
{
    [LuisModel("82ebb193-6353-425b-bc9d-9fe139b0a967", "f37a8422bd87422a951a498bb57d4d61")]
    [Serializable]
    public class StockDialog : LuisDialog<object>
    {
        [LuisIntent("getDeductionInfo")]
        public async Task getDeductionInfo(IDialogContext context, LuisResult result)
        {
            ITaskHandler handler = new DeductionInfoHandler();
            await context.PostAsync(await handler.handle(context, result));
            context.Wait(MessageReceived);
        }

        [LuisIntent("greeting")]
        public async Task greet(IDialogContext context, LuisResult result)
        {
            ITaskHandler handler = new GreetingHandler();
            await context.PostAsync(await handler.handle(context, result));
            context.Wait(MessageReceived);
        }
        [LuisIntent("goodbye")]
        public async Task goodbye(IDialogContext context, LuisResult result)
        {
            ITaskHandler handler = new GoodbyeHandler();
            await context.PostAsync(await handler.handle(context, result));
            context.Wait(MessageReceived);
        }

        [LuisIntent("None")]
        public async Task NoneHandler(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Sorry, what!?");
            context.Wait(MessageReceived);
        }
    }
}