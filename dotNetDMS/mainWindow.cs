using dotNetDMS.Class;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dotNetDMS
{
    public partial class mainWindow : Form
    {

        public static string documentDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Documents");
        public string thumbnailDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Thumbnails");
        public string[] documentFiles = Directory.GetFiles(documentDirectory);

        public string loadStatus = @"Loading: |"; //Where "|" will equal the number files loaded if the amount of files doesnt exceed 10

        private DocumentController documentController;

        public mainWindow()
        {
            InitializeComponent();

            // Populate documentFiles array
            documentFiles = Directory.GetFiles(documentDirectory);
            documentController = new DocumentController();
            /*
            // Debug: Print raw bytes of file paths
            foreach (string filePath in documentFiles)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(filePath);
                Console.WriteLine("File Path Bytes: " + string.Join(" ", bytes));
            }

            // Debug: Print document and thumbnail paths
            foreach (string filePath in documentFiles)
            {
                string thumbnailPath = Path.Combine(thumbnailDirectory, $"{Path.GetFileNameWithoutExtension(filePath)}.png");
                Console.WriteLine("Document Path: " + filePath);
                Console.WriteLine("Thumbnail Path: " + thumbnailPath);
            }
            */
            this.FormClosing += MainForm_FormClosing;
            
        }

        private void mainWindow_Load(object sender, EventArgs e)
        {
            this.Width = (int)(Screen.PrimaryScreen.WorkingArea.Width / 1.3);
            this.Height = (int)(Screen.PrimaryScreen.WorkingArea.Height / 1.3);
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;

            using (LoginForm loginForm = new LoginForm())
            {
                if (loginForm.ShowDialog() != DialogResult.OK)
                {
                    // If the login is not successful, close the main form.
                    this.Close();
                } else
                {
                    Task.Run(() => PopulateListView());

                    if (docuView.Items.Count > 0)
                    {
                        docuView.Items[0].Selected = true;
                        docuView_SelectedIndexChanged(sender, e); // Manually call the event handler to load the file preview
                    }
                }
            }

        }



        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            DirectoryInfo di = new DirectoryInfo(thumbnailDirectory);

            //var timedMessagebox = new TimedMessageBox("Exiting", "Program is closing, please wait for temporary files to be removed.", 3000); // 5 seconds
            //timedMessagebox.ShowDialog();
            /*
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
            */
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
            
            this.Invoke((MethodInvoker)delegate{

                byte[] CompressImage(Image image)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters(1);
                    encoderParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L); // Adjust the quality value as needed

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

                        //compressedImage.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Png);

                        AddToImageListAndListView(compressedImage, $"{Path.GetFileNameWithoutExtension(documentFile)}", documentFile);
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

                        //compressedImage.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Png);

                        AddToImageListAndListView(compressedImage, $"{Path.GetFileNameWithoutExtension(documentFile)}", documentFile);
                    }
            }
                /*
                void HandleXlsx(string documentFile)
                {
                    string thumbnailPath = Path.Combine(thumbnailDirectory, $"{Path.GetFileNameWithoutExtension(documentFile)}.png");

                    Task.Run(() =>
                    {
                        Image chartImage = XlsxController.GenerateChartThumbnail(documentFile);

                        if (chartImage != null)
                        {
                            // Save the image to a file and add it to the image list and list view
                            chartImage.Save(thumbnailPath, ImageFormat.Png);
                            AddToImageListAndListView(chartImage, $"{Path.GetFileNameWithoutExtension(documentFile)}", documentFile);
                        }
                        else
                        {
                            // Handle case when image is not generated
                        }
                    });
                }
                */

                void HandleTxt(string documentFile)
                {
                    string thumbnailPath = Path.Combine(thumbnailDirectory, $"{Path.GetFileNameWithoutExtension(documentFile)}.png");

                    Font font = new Font("Arial", 12);

                    string text = File.ReadAllText(documentFile);

                    Image image = CreateTextThumbnail(text, font, Color.Black, Color.White, 200, 200);

                    byte[] compressedImageBytes = CompressImage(image);

                    using (MemoryStream ms = new MemoryStream(compressedImageBytes))
                    {
                        Image compressedImage = Image.FromStream(ms);

                        //compressedImage.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Png);

                        AddToImageListAndListView(compressedImage, $"{Path.GetFileNameWithoutExtension(documentFile)}", documentFile);
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

                        //compressedImage.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Png);

                        AddToImageListAndListView(compressedImage, $"{Path.GetFileNameWithoutExtension(documentFile)}", documentFile);
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
                        //case ".xlsx":
                            //HandleXlsx(documentFile);
                            //break;
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

        void AddToImageListAndListView(Image image, string imageName, string filePath)
        {
            
            this.previewList.Images.Add(image, Color.Transparent);

            ListViewItem item = new ListViewItem(imageName, this.previewList.Images.Count - 1);
            item.Tag = filePath;
            docuView.Items.Add(item);

            
        }

        private void ClearPreviewControl()
        {
            previewControl.Navigate("about:blank");
            previewControl.DocumentText = string.Empty;
        }

        private void docuView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearPreviewControl();

            if (docuView.SelectedIndices.Count == 1)
            {
                int selectedIndex = docuView.SelectedIndices[0];
                if (selectedIndex < 0 || selectedIndex >= docuView.Items.Count)
                    return;

                ListViewItem selectedItem = docuView.Items[selectedIndex];
                string filePath = selectedItem.Tag as string;

                if (string.IsNullOrEmpty(filePath))
                {
                    statusOut.Text = "Invalid file path.";
                    return;
                }

                string fileExtension = Path.GetExtension(filePath);

                try
                {
                    if (fileExtension.Equals(".docx", StringComparison.OrdinalIgnoreCase))
                    {
                        string htmlContent = documentController.ConvertDocxToHtml(filePath);
                        if (!string.IsNullOrEmpty(htmlContent))
                            previewControl.DocumentText = htmlContent;
                        else
                            statusOut.Text = "Failed to convert .docx file to HTML.";
                    }
                    else
                    {
                        string absolutePath = Path.GetFullPath(filePath);
                        Uri fileUri = new Uri("file:///" + Uri.EscapeDataString(absolutePath));

                        switch (fileExtension)
                        {
                            case ".txt":
                            case ".pdf":
                                previewControl.Navigate(fileUri.AbsoluteUri);
                                break;
                            case ".jpeg":
                            case ".jpg":
                            case ".png":
                            case ".gif":
                                previewControl.DocumentText = $"<html><body><img src=\"{fileUri.AbsoluteUri}\" /></body></html>";
                                break;
                            default:
                                statusOut.Text = "Unsupported file type.";
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    statusOut.Text = "An error occurred: " + ex.Message;
                    Console.WriteLine("Exception: " + ex.ToString());
                }
            }
            else if (docuView.SelectedIndices.Count > 1)
            {
                statusOut.Text = "Items Selected: " + docuView.SelectedIndices.Count;
            }
        }
    }
}
