using Customers.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Customers.Web.Pages.Customer
{
    public class DetailModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public DetailModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public CustomerDetails customerDetails { get; set; }
        public async Task<IActionResult> OnGetAsync(Guid itemid)
        {
            string? baseUrl = _configuration.GetValue<string>("baseUrl");
            if (baseUrl == null)
            {
                throw new ArgumentNullException(nameof(baseUrl));
            }
            var response = await _httpClient.GetAsync($"{baseUrl}/customer/{itemid}");
            response.EnsureSuccessStatusCode();
            var customers = await response.Content.ReadFromJsonAsync<CustomerDetails>();
            customerDetails = customers;
            return Page();
        }
    }
}
