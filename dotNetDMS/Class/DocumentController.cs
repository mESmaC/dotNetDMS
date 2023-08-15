using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;

namespace dotNetDMS.Class
{
    public class DocumentController
    {
        public string ConvertDocxToHtml(string filePath)
        {
            using (WordprocessingDocument doc = WordprocessingDocument.Open(filePath, false))
            {
                Body body = doc.MainDocumentPart.Document.Body;

                // Process paragraphs and extract text content
                string html = ProcessParagraphs(body);
                return html;
            }
        }

        private string ProcessParagraphs(Body body)
        {
            StringWriter htmlWriter = new StringWriter();
            foreach (var paragraph in body.Elements<Paragraph>())
            {
                htmlWriter.Write("<p>");
                foreach (var run in paragraph.Elements<Run>())
                {
                    htmlWriter.Write("<span style=\"");

                    if (run.RunProperties != null)
                    {
                        if (run.RunProperties.Bold != null && run.RunProperties.Bold.Val != null)
                            htmlWriter.Write("font-weight:bold;");

                        if (run.RunProperties.Italic != null && run.RunProperties.Italic.Val != null)
                            htmlWriter.Write("font-style:italic;");

                        if (run.RunProperties.Underline != null && run.RunProperties.Underline.Val != null)
                            htmlWriter.Write("text-decoration:underline;");
                    }

                    htmlWriter.Write("\">");

                    foreach (var text in run.Elements<Text>())
                    {
                        htmlWriter.Write(text.Text);
                    }

                    htmlWriter.Write("</span>");
                }
                htmlWriter.Write("</p>");
            }
            return htmlWriter.ToString();
        }
    }
}