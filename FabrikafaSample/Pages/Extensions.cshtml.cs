using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FabrikafaSample.Pages
{
    public class ExtensionsModel : PageModel
    {
        private readonly ILogger<ExtensionsModel> _logger;

        public ExtensionsModel(ILogger<ExtensionsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}