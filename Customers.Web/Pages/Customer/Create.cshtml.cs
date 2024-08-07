using Customers.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Customers.Web.Pages.Customer
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public CreateModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new NullReferenceException(nameof(httpClient));
            _configuration = configuration ?? throw new NullReferenceException(nameof(httpClient)); ;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreateCustomer createCustomer { get; set; }

        public async Task<IActionResult> OnPost()
        {
            string? baseUrl = _configuration.GetValue<string>("baseUrl");
            if (baseUrl == null)
            {
                throw new ArgumentNullException(nameof(baseUrl));
            }
            if (!ModelState.IsValid || createCustomer == null)
            {
                return Page();
            }

            await _httpClient.PostAsync($"{baseUrl}/customer", new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8,
            "application/json"));
            return RedirectToPage(nameof(Index));
        }
    }
}
