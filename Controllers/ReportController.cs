using Microsoft.AspNetCore.Mvc;
using ReportPDF.Reports;

namespace ReportPDF.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReport _demoReport;

        public ReportController(DemoReport demoReport)
        {
            _demoReport = demoReport;
        }

        [HttpGet("GenerateDemoReport")]
        public IActionResult GenerateDemoReport()
        {
            _demoReport.GeneratePDF();
            return Ok();
        }
    }
}
