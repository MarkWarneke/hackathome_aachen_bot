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
            System.Threading.Thread.Sleep(20000);
            await context.PostAsync("Hi Markus! I have noticed that you've used a lot more power lately. Did you know that you could save 60€/year right now, if you were to switch subscription? Innogy offers great savings for people with hybrid cars. Are you free right now?");
            context.Wait(MessageReceived);
        }

        [LuisIntent("notfree")]//I am currently busy, can we talk later?
        public async Task notfree(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Sure thing! How about next wednesday?");//User seems to have time on wednesday, based on previous time of contact
            context.Wait(MessageReceived);
        }

        [LuisIntent("yeswednesday")]//Wednesday sounds good!
        public async Task wednesday(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Perfect. I will talk to you at around six.");
            context.Wait(MessageReceived);
        }
        [LuisIntent("secondgoodbye")]//Awesome, thank you for your help! That's it. Have a good day.
        public async Task secondgoodbye(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("You are welcome, have a good day, too.");
            System.Threading.Thread.Sleep(4000);
            await context.PostAsync("Hello Markus. Can you spare a moment to talk about your subscription?");//Check if online recently
            context.Wait(MessageReceived);
        }
        [LuisIntent("imfree")]//Yes, tell me about my options!
        public async Task free(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Cool. Well, the most fitting subscription for you would be the 'Ultra Mega Senior Premium Pack for Family and E-Mobility'. You would save 5$/month just by switching. Does that sound good to you?");   
            context.Wait(MessageReceived);
        }
        [LuisIntent("packetplease")]//Wow, Thanks for looking this up. I would like to change my service please.
        public async Task packets(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Okay, I will however need legal authentication from you to confirm your subscription inquiry. Please send a picture of your signiture next to the following text: 'inq12442233'");
            context.Wait(MessageReceived);
        }
        [LuisIntent("picture")]//I am sending you a picture!
        public async Task picture(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Thank you, your account has been changed. Changes will take effect from the next month on. Thank you for re-choosing innogy! Is there anything else on your mind?");
            context.Wait(MessageReceived);
        }
        [LuisIntent("thirdgoodbye")]//No that is it...Actually wait...Tell me a joke!
        public async Task thirdgoodbye(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("How about this one? If Apple made a car.....Would it have Windows?");
            context.Wait(MessageReceived);
        }


        [LuisIntent("None")]
        public async Task NoneHandler(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Sorry, what?");
            context.Wait(MessageReceived);
        }
        [LuisIntent("")]
        public async Task PicHandler(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Thank you, your account has been changed. Changes will take effect from the next month on. Thank you for re-choosing innogy! Is there anything else on your mind?");
            context.Wait(MessageReceived);
        }

    }
}