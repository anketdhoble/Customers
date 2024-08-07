using Customers.Web.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;

namespace Customers.Web.Pages.Customer
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public IndexModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public List<CustomerDetails>? data { get; set; }
        public async Task OnGetAsync()
        {
            string? baseUrl = _configuration.GetValue<string>("baseUrl");
            if (baseUrl == null)
            {
                throw new ArgumentNullException(nameof(baseUrl));
            }
            var response = await _httpClient.GetAsync($"{baseUrl}/customer");
            response.EnsureSuccessStatusCode();
            var customers = await response.Content.ReadFromJsonAsync<List<CustomerDetails>>();
            data = customers;
        }
    }
}
