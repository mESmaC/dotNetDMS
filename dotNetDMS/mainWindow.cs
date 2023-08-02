using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.Pdf.Devices;
using Aspose.Words;
using Aspose.Words.Rendering;
using Aspose.Words.Saving;
using Spire.Xls;
using System.Drawing.Imaging;

namespace dotNetDMS
{
    public partial class mainWindow : Form
    {
        public mainWindow()
        {
            InitializeComponent();
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

        private void PopulateListView()
        {
            this.Invoke((MethodInvoker)delegate {
                string documentDirectory = @"Data\Documents";
                string thumbnailDirectory = @"Data\Thumbnails";

                // Get all document files
                string[] documentFiles = Directory.GetFiles(documentDirectory);

                Console.WriteLine("PopulateListView called. Found {0} documents.", documentFiles.Length);

                void HandleDocx(string documentFile)
                {
                    // Load Document
                    var document = new Aspose.Words.Document(documentFile);
                    // Thumbnail options
                    var options = new Aspose.Words.Saving.ImageSaveOptions(Aspose.Words.SaveFormat.Png)
                    {
                        // We want the size of the rendered page to be the same as the size of the page in points.
                        // To achieve this we need to specify a resolution of 72 DPI.
                        Resolution = 72
                    };

                    // Let's say we want the thumbnails to be generated in color rather than as grayscale images.
                    options.ImageColorMode = Aspose.Words.Saving.ImageColorMode.None;

                    // Create a thumbnail per page.
                    for (int i = 0; i < document.PageCount; i++)
                    {
                        options.PageSet = new Aspose.Words.Saving.PageSet(i);
                        string thumbnailPath = Path.Combine(thumbnailDirectory, $"{Path.GetFileNameWithoutExtension(documentFile)}_thumb_{i + 1}.png");
                        document.Save(thumbnailPath, options);
                    }
                }

                void HandlePdf(string documentFile)
                {
                    // Handle PDFs
                    Aspose.Pdf.Document pdf = new Aspose.Pdf.Document(documentFile);

                    for (int pageCount = 1; pageCount <= pdf.Pages.Count; pageCount++)
                    {
                        using (FileStream imageStream = new FileStream(thumbnailDirectory + "\\" + Path.GetFileNameWithoutExtension(documentFile) + "_page_" + pageCount + ".png", FileMode.Create))
                        {
                            // Create Resolution object
                            Resolution resolution = new Resolution(300);
                            // Create JPEG device with specified attributes (Width, Height, Resolution, Quality)
                            // where Quality [0-100], 100 is Maximum quality
                            JpegDevice jpegDevice = new JpegDevice(500, 700, resolution, 100);
                            // Convert a particular page and save the image to stream
                            jpegDevice.Process(pdf.Pages[pageCount], imageStream);
                            // Close stream
                            imageStream.Close();
                        }
                    }
                }

                void HandleXlsx(string documentFile)
                {
                    // Handle Excel spreadsheets
                    Spire.Xls.Workbook workbook = new Spire.Xls.Workbook();
                    workbook.LoadFromFile(documentFile);

                    // Convert the first worksheet into an image
                    Spire.Xls.Worksheet sheet = workbook.Worksheets[0];
                    Image image = sheet.ToImage(1, 1, sheet.LastRow, sheet.LastColumn);

                    // Save the image in the thumbnail directory
                    string thumbnailFile = Path.Combine(thumbnailDirectory, Path.GetFileNameWithoutExtension(documentFile) + ".png");
                    image.Save(thumbnailFile, ImageFormat.Png);
                }

                // For each document...
                foreach (string documentFile in documentFiles)
                {
                    string extension = Path.GetExtension(documentFile).ToLower();

                    switch (Path.GetExtension(documentFile))
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
                        default:
                            Console.WriteLine($"Document type {Path.GetExtension(documentFile)} not supported.");
                            break;
                    }

                }

                Console.WriteLine("Added {0} items to the ListView.", docuView.Items.Count);
            });
        }
    }
}
