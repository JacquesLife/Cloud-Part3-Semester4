using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Services;
using System.Collections.Generic;

namespace WebApplication2.Controllers
{
    public class TableStorageController : Controller
    {
        private readonly TableStorageService _tableStorageService;

        public TableStorageController(TableStorageService tableStorageService)
        {
            _tableStorageService = tableStorageService;
        }

        // Action to view all entities in the table
        public async Task<IActionResult> TableStorage()
        {
            // Retrieve all entities from the table storage service
            IEnumerable<TableEntityModel> entities = await _tableStorageService.QueryEntitiesAsync();
            return View("~/Views/Home/Tables.cshtml", entities);
        }

        // Action to add an entity to the table
        [HttpPost]
        public async Task<IActionResult> AddEntity(string name, string surname, string country)
        {
            // Define partition and row keys based on user input or logic
            string partitionKey = country;
            string rowKey = $"{surname}_{DateTime.UtcNow.Ticks}"; 

            // Create a new TableEntityModel instance with provided data
            var entity = new TableEntityModel(partitionKey, rowKey)
            {
                Name = name,
                Surname = surname,
                Country = country
            };

            // Add the entity using the service
            await _tableStorageService.AddEntityAsync(entity);

            // Redirect to the TableStorage action to display the updated list
            return RedirectToAction("TableStorage"); 
        }
    }
}

