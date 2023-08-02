using System;
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

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }

        private void PopulateListView()
        {
            this.Invoke((MethodInvoker)delegate
            {
                //string documentDirectory = @"Data\Documents";
                //string thumbnailDirectory = @"Data\Thumbnails";

                // Get all document files
                string[] documentFiles = Directory.GetFiles(documentDirectory);

                Console.WriteLine("PopulateListView called. Found {0} documents.", documentFiles.Length);

                void HandleDocx(string documentFile)
                {
                    string thumbnailPath = Path.Combine(thumbnailDirectory, $"{Path.GetFileNameWithoutExtension(documentFile)}.png");

                    // Load Word document
                    Spire.Doc.Document document = new Spire.Doc.Document();
                    document.LoadFromFile(documentFile);

                    // Save the first page of the Word document as a thumbnail image
                    Image image = document.SaveToImages(0, Spire.Doc.Documents.ImageType.Bitmap);

                    // Save the image as a PNG file
                    image.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Png);

                    // Load the thumbnail into the ImageList and ListView
                    AddToImageListAndListView(thumbnailPath, $"{Path.GetFileNameWithoutExtension(documentFile)}");
                }

                void HandlePdf(string documentFile)
                {
                    string thumbnailPath = Path.Combine(thumbnailDirectory, $"{Path.GetFileNameWithoutExtension(documentFile)}.png");

                    // Load PDF document
                    Spire.Pdf.PdfDocument pdf = new Spire.Pdf.PdfDocument();
                    pdf.LoadFromFile(documentFile);

                    // Save the first page of the PDF as a thumbnail image
                    Image image = pdf.SaveAsImage(0);

                    // Save the image as a PNG file
                    image.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Png);

                    // Load the thumbnail into the ImageList and ListView
                    AddToImageListAndListView(thumbnailPath, $"{Path.GetFileNameWithoutExtension(documentFile)}");
                }

                void HandleXlsx(string documentFile)
                {
                    string thumbnailPath = Path.Combine(thumbnailDirectory, $"{Path.GetFileNameWithoutExtension(documentFile)}.png");

                    // Load Excel workbook
                    Spire.Xls.Workbook workbook = new Spire.Xls.Workbook();
                    workbook.LoadFromFile(documentFile);

                    // Get the first worksheet
                    Spire.Xls.Worksheet sheet = workbook.Worksheets[0];

                    // Identify the range of the worksheet
                    int firstRow = 1;
                    int firstColumn = 1;
                    int lastRow = sheet.LastRow;
                    int lastColumn = sheet.LastColumn;

                    // Save the worksheet as an image
                    Image image = sheet.ToImage(firstRow, firstColumn, lastRow, lastColumn);

                    // Save the image as a PNG file
                    image.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Png);

                    // Load the thumbnail into the ImageList and ListView
                    AddToImageListAndListView(thumbnailPath, $"{Path.GetFileNameWithoutExtension(documentFile)}");
                }

                void HandleTxt(string documentFile)
                {
                    string thumbnailPath = Path.Combine(thumbnailDirectory, $"{Path.GetFileNameWithoutExtension(documentFile)}.png");

                    using (var bitmap = new Bitmap(500, 500))
                    {
                        // Read the text from the text file
                        string text = File.ReadAllText(documentFile);

                        // Create a graphics object from the bitmap
                        using (var graphics = Graphics.FromImage(bitmap))
                        {
                            // Set the text formatting
                            var format = new StringFormat()
                            {
                                Alignment = StringAlignment.Near,
                                LineAlignment = StringAlignment.Near,
                                Trimming = StringTrimming.EllipsisCharacter,
                                FormatFlags = StringFormatFlags.LineLimit
                            };

                            // Draw the text onto the bitmap
                            graphics.DrawString(text, new Font("Courier New", 10), Brushes.Black, new RectangleF(0, 0, bitmap.Width, bitmap.Height), format);

                            // Save the bitmap as a PNG file
                            bitmap.Save(thumbnailPath, ImageFormat.Png);
                        }
                    }

                    // Load the thumbnail into the ImageList and ListView
                    AddToImageListAndListView(thumbnailPath, $"{Path.GetFileNameWithoutExtension(documentFile)}");
                }

                void AddToImageListAndListView(string imagePath, string imageName)
                {
                    // Load the image into the ImageList
                    this.previewList.Images.Add(Image.FromFile(imagePath), Color.Transparent);

                    // Create a ListViewItem and add it to the ListView
                    ListViewItem item = new ListViewItem(imageName, this.previewList.Images.Count - 1);
                    docuView.Items.Add(item);
                }

                // For each document...
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
                        default:
                            Console.WriteLine($"Document type {extension} not supported.");
                            break;
                    }
                }

                Console.WriteLine("Added {0} items to the ListView.", docuView.Items.Count);
            });
        }
    }
}
