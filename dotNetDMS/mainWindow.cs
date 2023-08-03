﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spire.Xls;
using Spire.Pdf;
using Spire.Doc;
using Spire.Xls.Charts;
using System.Threading;
using dotNetDMS.Class;
using OfficeOpenXml.Drawing;

namespace dotNetDMS
{
    public partial class mainWindow : Form
    {

        public string documentDirectory = @"Data\Documents";
        public string thumbnailDirectory = @"Data\Thumbnails";
        public mainWindow()
        {
            InitializeComponent();
            this.FormClosing += MainForm_FormClosing;
        }

        private void mainWindow_Load(object sender, EventArgs e)
        {
            using (LoginForm loginForm = new LoginForm())
            {
                if (loginForm.ShowDialog() != DialogResult.OK)
                {
                    // If the login is not successful, close the main form.
                    this.Close();
                } else
                {
                    PopulateListView();
                }
            }

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(thumbnailDirectory);

            var timedMessagebox = new TimedMessageBox("Exiting", "Program is closing, please wait for temporary files to be removed.", 3000); // 5 seconds
            timedMessagebox.ShowDialog();

            foreach (FileInfo file in di.GetFiles())
            {
                bool deleted = false;
                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        File.Delete(file.FullName);
                        deleted = true;
                        break; // break the loop if file deleted
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    // if file was not deleted wait for 100 ms before retrying
                    if (!deleted)
                        Thread.Sleep(100);
                }
            }
        }

        private Image CreateTextThumbnail(string text, Font font, Color textColor, Color backgroundColor, int width, int height)
        {
            // Create a bitmap with the specified width and height
            Bitmap bitmap = new Bitmap(width, height);

            // Create a graphics object from the bitmap
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                // Set the background color
                graphics.Clear(backgroundColor);

                // Create a brush with the specified text color
                using (Brush brush = new SolidBrush(textColor))
                {
                    // Create a rectangle to fit the text within the bitmap
                    RectangleF rectangle = new RectangleF(0, 0, width, height);

                    // Create a StringFormat object for center alignment
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    // Draw the text on the bitmap
                    graphics.DrawString(text, font, brush, rectangle, stringFormat);
                }
            }

            // Return the thumbnail image
            return bitmap;
        }
        private void PopulateListView()
        {
            this.Invoke((MethodInvoker)delegate
            {
            string[] documentFiles = Directory.GetFiles(documentDirectory);

            statusOut.Text = "Status: PopulateListView called. Found " + documentFiles.Length + " documents.";

            byte[] CompressImage(Image image)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters(1);
                    encoderParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 50L); // Adjust the quality value as needed

                    ImageCodecInfo jpegCodec = GetEncoderInfo(ImageFormat.Jpeg);

                    image.Save(ms, jpegCodec, encoderParams);

                    return ms.ToArray();
                }
            }

            ImageCodecInfo GetEncoderInfo(ImageFormat format)
            {
                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
                foreach (ImageCodecInfo codec in codecs)
                {
                    if (codec.FormatID == format.Guid)
                    {
                        return codec;
                    }
                }
                return null;
            }

            void HandleDocx(string documentFile)
            {
                string thumbnailPath = Path.Combine(thumbnailDirectory, $"{Path.GetFileNameWithoutExtension(documentFile)}.png");

                Spire.Doc.Document document = new Spire.Doc.Document();
                document.LoadFromFile(documentFile);

                Image image = document.SaveToImages(0, Spire.Doc.Documents.ImageType.Bitmap);
                byte[] compressedImageBytes = CompressImage(image);

                using (MemoryStream ms = new MemoryStream(compressedImageBytes))
                {
                    Image compressedImage = Image.FromStream(ms);

                    compressedImage.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Png);

                    AddToImageListAndListView(thumbnailPath, $"{Path.GetFileNameWithoutExtension(documentFile)}");
                }
            }

            void HandlePdf(string documentFile)
            {
                string thumbnailPath = Path.Combine(thumbnailDirectory, $"{Path.GetFileNameWithoutExtension(documentFile)}.png");

                Spire.Pdf.PdfDocument pdf = new Spire.Pdf.PdfDocument();
                pdf.LoadFromFile(documentFile);

                Image image = pdf.SaveAsImage(0);
                byte[] compressedImageBytes = CompressImage(image);

                using (MemoryStream ms = new MemoryStream(compressedImageBytes))
                {
                    Image compressedImage = Image.FromStream(ms);

                    compressedImage.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Png);

                    AddToImageListAndListView(thumbnailPath, $"{Path.GetFileNameWithoutExtension(documentFile)}");
                }
            }

                void HandleXlsx(string documentFile)
                {
                    string thumbnailPath = Path.Combine(thumbnailDirectory, $"{Path.GetFileNameWithoutExtension(documentFile)}.png");

                    using (var package = new OfficeOpenXml.ExcelPackage(new FileInfo(documentFile)))
                    {
                        var worksheet = package.Workbook.Worksheets.First(); // Assuming the chart is in the first worksheet

                        var chart = worksheet.Drawings.OfType<OfficeOpenXml.Drawing.Chart.ExcelChart>().FirstOrDefault();

                        if (chart != null)
                        {
                            // Retrieve the image from the chart
                            var image = GetChartImage(chart);

                            if (image != null)
                            {
                                image.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Png);
                                AddToImageListAndListView(thumbnailPath, $"{Path.GetFileNameWithoutExtension(documentFile)}");
                            }
                            else
                            {
                                // Handle case when image is not found
                            }
                        }
                        else
                        {
                            // Handle case when chart is not found
                        }
                    }
                }

                Image GetChartImage(OfficeOpenXml.Drawing.Chart.ExcelChart chart)
                {
                    var imagePart = chart.Chart.Drawings.FirstOrDefault()?.ImagePart;

                    if (imagePart != null)
                    {
                        Image image;
                        using (var stream = imagePart.GetStream())
                        {
                            image = Image.FromStream(stream);
                        }
                        return image;
                    }
                    else
                    {
                        // Handle case when image part is not found
                        return null;
                    }
                }

                void HandleTxt(string documentFile)
            {
                string thumbnailPath = Path.Combine(thumbnailDirectory, $"{Path.GetFileNameWithoutExtension(documentFile)}.png");

                Font font = new Font("Arial", 12);

                Image image = CreateTextThumbnail(documentFile, font, Color.Black, Color.White, 200, 200);

                byte[] compressedImageBytes = CompressImage(image);

                using (MemoryStream ms = new MemoryStream(compressedImageBytes))
                {
                        Image compressedImage = Image.FromStream(ms);

                        compressedImage.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Png);

                        AddToImageListAndListView(thumbnailPath, $"{Path.GetFileNameWithoutExtension(documentFile)}");
                    }
                }

                void HandleImage(string documentFile)
                {
                    string thumbnailPath = Path.Combine(thumbnailDirectory, $"{Path.GetFileNameWithoutExtension(documentFile)}.png");

                    Image image = Image.FromFile(documentFile);
                    byte[] compressedImageBytes = CompressImage(image);

                    using (MemoryStream ms = new MemoryStream(compressedImageBytes))
                    {
                        Image compressedImage = Image.FromStream(ms);

                        compressedImage.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Png);

                        AddToImageListAndListView(thumbnailPath, $"{Path.GetFileNameWithoutExtension(documentFile)}");
                    }
                }

                foreach (string documentFile in documentFiles)
                {
                    string extension = Path.GetExtension(documentFile).ToLower();

                    switch (extension)
                    {
                        case ".docx":
                            HandleDocx(documentFile);
                            break;
                        case ".pdf":
                            HandlePdf(documentFile);
                            break;
                        case ".xlsx":
                            HandleXlsx(documentFile);
                            break;
                        case ".txt":
                            HandleTxt(documentFile);
                            break;
                        case ".jpg":
                        case ".jpeg":
                        case ".png":
                        case ".gif":
                            HandleImage(documentFile);
                            break;
                        default:
                            // Unsupported file type
                            break;
                    }
                }
            });
        }

        void AddToImageListAndListView(string imagePath, string imageName)
        {
            this.previewList.Images.Add(Image.FromFile(imagePath), Color.Transparent);

            ListViewItem item = new ListViewItem(imageName, this.previewList.Images.Count - 1);
            docuView.Items.Add(item);
        }


    }
}
