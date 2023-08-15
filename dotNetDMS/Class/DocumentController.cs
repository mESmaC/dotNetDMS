using DocumentFormat.OpenXml;
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

                // Process paragraphs and extract text content with formatting
                string html = ProcessParagraphs(body);
                return html;
            }
        }

        private string ProcessParagraphs(Body body)
        {
            StringWriter htmlWriter = new StringWriter();
            foreach (var paragraph in body.Elements<Paragraph>())
            {
                ProcessParagraph(paragraph, htmlWriter);
            }
            return htmlWriter.ToString();
        }

        private void ProcessParagraph(Paragraph paragraph, StringWriter htmlWriter)
        {
            htmlWriter.Write("<p>");
            foreach (var run in paragraph.Elements<Run>())
            {
                ProcessRun(run, htmlWriter);
            }
            htmlWriter.Write("</p>");
        }

        private void ProcessRun(Run run, StringWriter htmlWriter)
        {
            foreach (var runChild in run.Elements())
            {
                switch (runChild)
                {
                    case Text text:
                        htmlWriter.Write(text.Text);
                        break;
                    case Break _:
                        htmlWriter.Write("<br/>");
                        break;
                    case TabChar _:
                        htmlWriter.Write("&nbsp;&nbsp;&nbsp;&nbsp;");
                        break;
                    case Bold _:
                    case Italic _:
                    case Underline _:
                        ProcessRunProperties(runChild, htmlWriter);
                        break;
                    case Justification justification:
                        ProcessJustification(justification, htmlWriter);
                        break;
                    case DocumentFormat.OpenXml.Drawing.Paragraph drawingParagraph:
                        ProcessDrawingParagraph(drawingParagraph, htmlWriter);
                        break;
                        // Add other formatting elements within Run here
                }
            }
        }

        private void ProcessRunProperties(OpenXmlElement runChild, StringWriter htmlWriter)
        {
            string tag = "";

            switch (runChild)
            {
                case Bold _:
                    tag = "strong";
                    break;
                case Italic _:
                    tag = "em";
                    break;
                case Underline _:
                    tag = "u";
                    break;
            }

            htmlWriter.Write($"<{tag}>");
            foreach (var text in runChild.Descendants<Text>())
            {
                htmlWriter.Write(text.Text);
            }
            htmlWriter.Write($"</{tag}>");
        }

        private void ProcessJustification(Justification justification, StringWriter htmlWriter)
        {
            string alignmentTag = "";

            switch (justification.Val.Value)
            {
                case JustificationValues.Left:
                    alignmentTag = "text-align: left;";
                    break;
                case JustificationValues.Center:
                    alignmentTag = "text-align: center;";
                    break;
                case JustificationValues.Right:
                    alignmentTag = "text-align: right;";
                    break;
                    // Handle other alignment values as needed
            }

            if (!string.IsNullOrEmpty(alignmentTag))
            {
                htmlWriter.Write($"<div style=\"{alignmentTag}\">");
            }
            else
            {
                htmlWriter.Write("<div>");
            }
        }

        private void ProcessDrawingParagraph(DocumentFormat.OpenXml.Drawing.Paragraph drawingParagraph, StringWriter htmlWriter)
        {
            // Process and handle drawing paragraphs here, if needed
        }
    }
}
