using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Linq;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExcelProcessorController : ControllerBase
    {
        public ExcelProcessorController()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        [HttpPost("processexcel")]
        public IActionResult ProcessExcel()
        {
            try
            {
                var file = Request.Form.Files[0];
                using var stream = file.OpenReadStream();

                using var excelPackage = new ExcelPackage(stream);
                var worksheet = excelPackage.Workbook.Worksheets[0];

                var rows = new List<Dictionary<string, object>>();
                var headers = worksheet.Cells["1:1"].Select(cell => cell.Value.ToString()).ToList();

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    var rowData = new Dictionary<string, object>();
                    for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                    {
                        rowData[headers[col - 1]] = worksheet.Cells[row, col].Value;
                    }
                    rows.Add(rowData);
                }

                return Ok(rows);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}