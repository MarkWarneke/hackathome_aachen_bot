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
using System.Text.RegularExpressions;
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
            PromptDialog.Number(context, AfterConfirming_TurnOffAlarm, TextPersonalizer.parse(context, "Auf welchen Betrag möchtest du deinen Abschlag ändern?"));
        }

        public async Task AfterConfirming_TurnOffAlarm(IDialogContext context, IAwaitable<long> argument)
        {
            ConsumingRest rest = new ConsumingRest("http://shruggieuserrest.azurewebsites.de/");

            await rest.postObj("api/users/111/deduction", new DataObject() { value = await argument });
            
            await context.PostAsync(TextPersonalizer.parse(context, $"Kein Problem, ich habe deinen Abschlag auf {await argument}€ geändert."));
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
            PromptDialog.Text(context, getPhoneNumber, "Eine Sache noch: Wir haben bemerkt, dass du deine Telefonnummer noch nicht hinterlegt hast. Möchtest du uns deine Telefonnummer geben und teil unseres aktiven Kundenbetreuungsprogramms werden? Das würde unter anderem bedeuten, dass wir dich besser verstehen und beraten können. Infos unter: URL");
        }
        //Ja, meine Nummer lautet 001203010231 und ich würde gerne teilnehmen.
        public async Task getPhoneNumber(IDialogContext context, IAwaitable<string> value)
        {
            await context.PostAsync("Danke dir, ich vermerke das. Bis zum nächsten mal!");
            context.Wait(MessageReceived);
        }

        public async Task startSecondHalf(IDialogContext context, LuisResult result)
        {
            System.Threading.Thread.Sleep(20000);
            PromptDialog.Text(context, notfree, "Hi Markus! Wir haben bemerkt, dass du neuerdings sehr viel Energie verbrauchst. Grade für Haushalte mit Elektroautos haben wir ein gutes Angebot, und du könntest bis zu 60€/Jahr sparen! Hast du grade Zeit?");

        }

        //I am currently busy, can we talk later?
        public async Task notfree(IDialogContext context, IAwaitable<string> value)
        {
            await context.PostAsync("Okay, wie wäre es mit nächstem Mittwoch?");//User seems to have time on wednesday, based on previous time of contact

        }

        //Wednesday sounds good!
        public async Task wednesday(IDialogContext context, IAwaitable<string> value)
        {
            PromptDialog.Text(context, free, "Perfekt, ich melde mich wie immer gegen sechs.");

        }
        //Awesome, thank you for your help! That's it. Have a good day.
        public async Task secondgoodbye(IDialogContext context, IAwaitable<string> value)
        {
            PromptDialog.Text(context,free,"Kein Problem, hab noch einen schönen Tag.");

        }
        public async Task startSecondHalf(IDialogContext context, IAwaitable<string> value)//CAll on typing
        {
            System.Threading.Thread.Sleep(5000);
            PromptDialog.Text(context, free, "Hi Markus! Hast du heute kurz Zeit um über deinen Vertrag zu sprechen?");//Check if online recently
        }
        //Yes, tell me about my options!
        public async Task free(IDialogContext context, IAwaitable<string> value)
        {
            PromptDialog.Text(context, packets,"Cool. Der zu dir passenste Vertrag wäre der 'SuperMegaSeniorenEmobility-Paket für Familien'. So könntest du 67€ im Jahr sparen! Möchtest du wechseln? Du könntest auch genauere Informationen anfordern.");
            
        }
        //Wow, Thanks for looking this up. I would like to change my service please.
        public async Task packets(IDialogContext context, IAwaitable<string> value)
        {
            PromptDialog.Text(context,picture, "Das freut mich. Ich brauche für die Vertragsänderung noch deine Unterschrift. Dafür schick uns bitte einfach hier ein Foto von deiner Unterschrift und dem Sicherheitscode 3123121.");
        }
        //User sends a picture
        public async Task picture(IDialogContext context, IAwaitable<string> value)
        {
            PromptDialog.Text(context, thirdgoodbye, "Danke, dein Account wurde geändert. Das neue Paket beginnt ab dem nächsten Monat. Danke, dass du dich wieder für uns entschieden hast! Gibt es noch etwas, dass ich für dich tun kann?");
        }
        //No that is it...Actually wait...Tell me a joke!
        public async Task thirdgoodbye(IDialogContext context, IAwaitable<string> value)
        {
            await context.PostAsync("Wie wäre es mit: Wenn Apple ein Auto baut");
            context.Wait(MessageReceived);
        }


        [LuisIntent("None")]
        public async Task NoneHandler(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Sorry, what?");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Keyword")]
        public async Task KeywordHandler(IDialogContext context, LuisResult result)
        {
            PromptDialog.Text(context, AfterConfirming_TurnOffAlarm2, "Hier ist die erste Nachricht!");
        }

        public async Task AfterConfirming_TurnOffAlarm2(IDialogContext context, IAwaitable<string> value)
        {
            await context.PostAsync($"Zweite Nachricht. Keine Analyse!");
            context.Wait(MessageReceived);
        }

        [LuisIntent("")]
        public async Task PicHandler(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Thank you, your account has been changed. Changes will take effect from the next month on. Thank you for re-choosing innogy! Is there anything else on your mind?");
            //await context.PostAsync("Sorry, what!?");

            context.Wait(MessageReceived);
        }
    }
}