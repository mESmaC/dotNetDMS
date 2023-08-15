using ClosedXML.Excel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace dotNetDMS.Class
{
    public class XlsxController
    {
        public static Image GenerateChartThumbnail(string documentFilePath)
        {
            using (var workbook = new XLWorkbook(documentFilePath))
            {
                var worksheet = workbook.Worksheets.Worksheet(1); // Assuming you want to process the first worksheet

                //var charts = worksheet.Charts; // This will not work with ClosedXML

                // You'll need to find a way to access and process the charts in ClosedXML
                // For example, you can loop through shapes in the worksheet and identify charts

                // Once you find a chart shape, you can render it to an image
                var chartImage = new Bitmap(200, 200); // Adjust dimensions as needed

                using (var graphics = Graphics.FromImage(chartImage))
                {
                    // Render the chart to the graphics object
                    // This part will depend on how you can access and render the chart in ClosedXML
                }

                return chartImage;
            }

            return null; // Handle case when image is not generated
        }

        // Add methods to handle viewing Xlsx files here
    }
}