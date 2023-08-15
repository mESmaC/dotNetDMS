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
                var worksheet = workbook.Worksheets.Worksheet(1); 


                
                var chartImage = new Bitmap(200, 200); 

                using (var graphics = Graphics.FromImage(chartImage))
                {
                    
                }

                return chartImage;
            }

            return null; 
        }

        
    }
}