using Fabrikafa.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FabrikafaSample.Pages.SampleServices;

public class CheckServerTimeModel : PageModel
{
    private IConfiguration _configuration;
    private readonly IDateTime _dateTime;

    public CheckServerTimeModel(IConfiguration Configuration, IDateTime dateTime)
    {
        _configuration = Configuration;
        _dateTime = dateTime;
        Message = String.Empty;

    }

    public string Message { get; set; }

    public async Task<IActionResult> OnGet([FromServices] IDateTime dateTime)
    {
        var serverTime = dateTime.Now;
        ViewData["Message"] = $"Currently on the server UTC time is  : {serverTime}";

        await Task.CompletedTask;

        return Page();
    }
}
