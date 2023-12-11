using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using CustomerManagementMvc.Models;
using CustomerManagementMvc.Validators;

namespace CustomerManagementMvc.Controllers
{
    public class CustomersController : Controller
    {
        private readonly HttpClient httpClient;

        public CustomersController(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("ApiClient");
        }
        public async Task<IActionResult> Index()
        {
            
            var customers = await httpClient.GetFromJsonAsync<IEnumerable<Customer>>("api/customers");
            return View(customers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            var validator = new CustomerValidator();
            var validationResult = validator.Validate(customer);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }

                return View(customer);
            }

            var response = await httpClient.PostAsJsonAsync("api/customers", customer);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to create customer. Please try again.");
            }

            return View(customer);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var customer = await httpClient.GetFromJsonAsync<Customer>($"api/customers/{id}");

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Customer customer)
        {
            var validator = new CustomerValidator();
            var validationResult = validator.Validate(customer);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }

                return View(customer);
            }

            var response = await httpClient.PutAsJsonAsync($"api/customers/{customer.Id}", customer);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to update customer. Please try again.");
            }

            return View(customer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var customer = await httpClient.GetFromJsonAsync<Customer>($"api/customers/{id}");

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await httpClient.DeleteAsync($"api/customers/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to delete customer. Please try again.");
                return RedirectToAction(nameof(Delete), new { id });
            }
        }
    }
}
