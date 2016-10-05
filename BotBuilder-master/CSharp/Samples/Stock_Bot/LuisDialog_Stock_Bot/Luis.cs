using LuisDialog_Stock_Bot;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StockLuisDlg
{
    [LuisModel("82ebb193-6353-425b-bc9d-9fe139b0a967", "f37a8422bd87422a951a498bb57d4d61")]
    [Serializable]
    public class StockDialog : LuisDialog<object>
    {
        public object DefaultRequestHeaders { get; private set; }

        private async Task<decimal> checkEmotionalStatus(string message)
        {
            const string apiKey = "f363d4d516db4f0d9a9d25c51be9e638";
            const string queryUri = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment";

            var client = new HttpClient
            {
                DefaultRequestHeaders = {
                {"Ocp-Apim-Subscription-Key", apiKey},
                {"Accept", "application/json"}
            }
            };
            var sentimentInput = new BatchInput
            {
                Documents = new List<DocumentInput> {
                new DocumentInput {
                    Id = 1,
                    Text = message,
                }
            }
            };
            var json = JsonConvert.SerializeObject(sentimentInput);
            var sentimentPost = await client.PostAsync(queryUri, new StringContent(json, Encoding.UTF8, "application/json"));
            var sentimentRawResponse = await sentimentPost.Content.ReadAsStringAsync();
            var sentimentJsonResponse = JsonConvert.DeserializeObject<BatchResult>(sentimentRawResponse);
            //var sentimentScore = sentimentJsonResponse?.Documents?.FirstOrDefault()?.Score ?? 0;
            return 0;
        }

        [LuisIntent("getDeductionInfo")]
        public async Task getDeductionInfo(IDialogContext context, LuisResult result)
        {
            ITaskHandler handler = new DeductionInfoHandler();
            await context.PostAsync(await handler.handle(context, result));
            context.Wait(MessageReceived);
        }

        [LuisIntent("changeDeduction")]
        public async Task deductionChange(IDialogContext context, LuisResult result)
        {
            PromptDialog.Number(context, AfterConfirming_TurnOffAlarm, "Auf welchen Betrag möchtest du deinen Abschlag ändern?");
        }

        public async Task AfterConfirming_TurnOffAlarm(IDialogContext context, IAwaitable<long> value)
        {
            await context.PostAsync($"Kein Problem, ich habe deinen Abschlag auf {await value}€ geändert.");
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

        [LuisIntent("thankYou")]
        public async Task ThankYouHandler(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Kein Problem, ich helfe immer gerne!");
            context.Wait(MessageReceived);
        }

        [LuisIntent("")]
        public async Task DefaultHandler(IDialogContext context, LuisResult result)
        {
            decimal score = await checkEmotionalStatus("Echt scheiße hier");
            await context.PostAsync("Sorry, what!?");
            context.Wait(MessageReceived);
        }

        [LuisIntent("HeyDu")]
        public async Task HeyDuHandler(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Hey du!");
            context.Wait(MessageReceived);
        }
    }
}