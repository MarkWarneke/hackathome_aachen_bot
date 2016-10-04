using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using System.Threading.Tasks;

namespace StockLuisDlg
{
    internal interface ITaskHandler
    {
        Task<string> handle(IDialogContext context, LuisResult result);
    }
}