using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;

namespace StockLuisDlg
{
    internal class DeductionInfo : ITaskHandler
    {
        public async Task<string> handle(IDialogContext context, LuisResult result)
        {

            string strRet;
            if (result.Entities.Count != 0)
            {
                strRet = result.Entities[0].Entity;
            }
            else
            {
                strRet = "goog";
            }
            context.ConversationData.SetValue<string>("LastStock", strRet);

            return await YahooStock.Yahoo.GetStock(strRet);
        }
    }
}