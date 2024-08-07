using Customers.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Customers.Web.Pages.Customer
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public EditModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        [BindProperty]
        public EditCustomer editCustomer { get; set; }
        public async Task<IActionResult> OnGet(Guid itemid)
        {
            string? baseUrl = _configuration.GetValue<string>("baseUrl");
            if (baseUrl == null)
            {
                throw new ArgumentNullException(nameof(baseUrl));
            }
            var response = await _httpClient.GetAsync($"{baseUrl}/customer/{itemid}");
            response.EnsureSuccessStatusCode();
            var customers = await response.Content.ReadFromJsonAsync<EditCustomer>();
            editCustomer = customers;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string? baseUrl = _configuration.GetValue<string>("baseUrl");
            if (baseUrl == null)
            {
                throw new ArgumentNullException(nameof(baseUrl));
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _httpClient.PutAsync($"{baseUrl}/customer", new StringContent(JsonConvert.SerializeObject(editCustomer), Encoding.UTF8,
            "application/json"));
            return RedirectToPage(nameof(Index));
        }
    }
}
