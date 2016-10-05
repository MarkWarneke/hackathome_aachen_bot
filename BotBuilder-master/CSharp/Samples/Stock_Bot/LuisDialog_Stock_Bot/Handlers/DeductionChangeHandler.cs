using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;

namespace StockLuisDlg
{
    internal class DeductionChangeHandler : ITaskHandler
    {
        public async Task<string> handle(IDialogContext context, LuisResult result)
        {
            PromptDialog.Number(context, AfterConfirming_TurnOffAlarm, "Are you sure?");

            return "!!!";

        }
        public async Task AfterConfirming_TurnOffAlarm(IDialogContext context, IAwaitable<long> value)
        {
                await context.PostAsync($"Ok, ich habe deinen Abschlag auf {value} geändert.");
            

        }
        }
    }