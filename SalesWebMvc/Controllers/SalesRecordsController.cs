using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using System;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordsService _salesRecordsService;

        public SalesRecordsController(SalesRecordsService salesRecordsService)
        {
            _salesRecordsService = salesRecordsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? firstDate, DateTime? lastDate)
        {
            if (firstDate == null)
            {
                firstDate = DateTime.Parse("01/01/1000");
            }
            if (!lastDate.HasValue)
            {
                lastDate = new DateTime(9999, 12, 31);
            }

            ViewData["firstDate"] = firstDate.Value.ToString("yyyy-MM-dd");
            ViewData["lastDate"] = lastDate.Value.ToString("yyyy-MM-dd");
            var sales = await _salesRecordsService.FindByDateAsync(firstDate, lastDate);

            return View(sales);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? firstDate, DateTime? lastDate)
        {
            if (firstDate == null)
            {
                firstDate = DateTime.Parse("01/01/1000");
            }
            if (!lastDate.HasValue)
            {
                lastDate = new DateTime(9999, 12, 31);
            }

            ViewData["firstDate"] = firstDate.Value.ToString("yyyy-MM-dd");
            ViewData["lastDate"] = lastDate.Value.ToString("yyyy-MM-dd");
            var sales = await _salesRecordsService.FindByDateGroupingAsync(firstDate, lastDate);

            return View(sales);
        }
    }
}
