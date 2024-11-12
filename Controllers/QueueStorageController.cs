using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class QueueStorageController : Controller
    {
        private readonly QueueStorageService _queueStorageService;

        public QueueStorageController(QueueStorageService queueStorageService)
        {
            _queueStorageService = queueStorageService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string message)
        {
            await _queueStorageService.SendMessageAsync(message);
            return RedirectToAction("QueueStorage"); // Redirects to the action that loads the view
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMessage(string messageId, string popReceipt)
        {
            await _queueStorageService.DeleteMessageAsync(messageId, popReceipt);
            return RedirectToAction("QueueStorage"); // Redirects to the action that loads the view
        }

        // Action to view all messages in the queue
        public async Task<IActionResult> QueueStorage()
        {
            var messages = await _queueStorageService.ViewMessagesAsync();
            var model = new QueueModel
            {
                Messages = messages.Select(m => m.MessageText).ToList()
            };
            return View("~/Views/Home/Queue.cshtml", model);
        }
    }
}
