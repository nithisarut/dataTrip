using dataTrip.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace dataTrip.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetReport (DateTime? dateStart , DateTime? dateEnd)
        {
            //if (dateStart is null)
            //    dateStart = Convert.ToDateTime(DateTime.Now.ToString("d", new CultureInfo("en-US"))) ;
            //if (dateEnd is null)
            //    dateEnd = Convert.ToDateTime(DateTime.Now.ToString("d", new CultureInfo("en-US")));
            var result = await _reportService.TripStatisticeItem(dateStart , dateEnd);
            return Ok(new { data = result});
        }
    }
}
