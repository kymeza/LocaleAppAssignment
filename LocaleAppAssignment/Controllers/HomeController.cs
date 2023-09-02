using LocaleAppAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using System.Globalization;

namespace LocaleAppAssignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<HomeController> _localizer;


        public HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> localizer)
        {
            _logger = logger;
            _localizer = localizer;

        }

        // HomeController.cs

        public IActionResult Index(string culture)
        {
            if (!string.IsNullOrEmpty(culture))
            {
                try
                {
                    CultureInfo.CurrentCulture = new CultureInfo(culture);
                    CultureInfo.CurrentUICulture = new CultureInfo(culture);
                }
                catch (CultureNotFoundException)
                {
                    // Handle the exception or set a default culture
                    CultureInfo.CurrentCulture = new CultureInfo("uk-UA");
                    CultureInfo.CurrentUICulture = new CultureInfo("uk-UA");
                }
            }
            else
            {
                // If 'culture' is not specified in the URL, set a default one
                CultureInfo.CurrentCulture = new CultureInfo("uk-UA");
                CultureInfo.CurrentUICulture = new CultureInfo("uk-UA");
            }

            
            var cultureInfo = CultureInfo.CurrentCulture;

            // Date and Time Formatting
            ViewData["DateLong"] = DateTime.Now.ToString("D", cultureInfo);
            ViewData["DateShort"] = DateTime.Now.ToString("d", cultureInfo);
            ViewData["TimeLong"] = DateTime.Now.ToString("T", cultureInfo);
            ViewData["TimeShort"] = DateTime.Now.ToString("t", cultureInfo);

            // Number Formatting
            ViewData["Number1"] = (123456789.12).ToString("N", cultureInfo);
            ViewData["Number2"] = (987654321.12).ToString("N", cultureInfo);
            ViewData["Number3"] = (1000000000).ToString("N", cultureInfo);
            ViewData["Number4"] = (1).ToString("N", cultureInfo);

            // Units of measurement (consider using a library for unit conversion)
            ViewData["Inches"] = "12 inches";
            ViewData["Centimeters"] = (12 * 2.54).ToString("N2") + " cm";
            ViewData["Pounds"] = "150 pounds";
            ViewData["Kilograms"] = (150 / 2.20462).ToString("N2") + " kg";
            ViewData["Liters"] = "1 liter";
            ViewData["Oz"] = (1 * 33.814).ToString("N2") + " oz";

            
            ViewData["Message"] = _localizer["Hello"];
            ViewData["DateLongFormat"] = _localizer["DateLong"];
            ViewData["DateShortFormat"] = _localizer["DateShort"];
            ViewData["TimeLongFormat"] = _localizer["TimeLong"];
            ViewData["TimeShortFormat"] = _localizer["TimeShort"];
            
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}