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
using Aspose.Words;
using Aspose.Words.Rendering;
using Aspose.Words.Saving;

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
            // Assuming previewList and docuView are already declared and accessible
            previewList.ImageSize = new Size(32, 32); // set the size of the icons here

            docuView.View = View.Details;
            docuView.SmallImageList = previewList;

            // Specify the directory containing the documents
            string documentDirectory = "Data/Documents";

            // Specify the directory where the thumbnails will be saved
            string thumbnailDirectory = "Data/Thumbnails";

            // Get all .docx files in the document directory
            string[] documentFiles = Directory.GetFiles(documentDirectory, "*.docx");

            // For each document...
            foreach (string documentFile in documentFiles)
            {
                // Generate a thumbnail
                Aspose.Words.Document doc = new Aspose.Words.Document(documentFile);
                Aspose.Words.Saving.ImageSaveOptions options = new Aspose.Words.Saving.ImageSaveOptions(Aspose.Words.SaveFormat.Png);
                options.PageSet = new PageSet(0, 0); // Only render the first page

                // Save the thumbnail in the thumbnail directory
                string thumbnailFile = Path.Combine(thumbnailDirectory, Path.GetFileNameWithoutExtension(documentFile) + ".png");
                doc.Save(thumbnailFile, options);

                // Add the thumbnail to the ImageList
                previewList.Images.Add(Image.FromFile(thumbnailFile));

                // Create a ListViewItem and add it to the ListView
                ListViewItem listViewItem = new ListViewItem(Path.GetFileName(documentFile));
                listViewItem.ImageIndex = previewList.Images.Count - 1; // Set this to the index of the image in the ImageList
                docuView.Items.Add(listViewItem);
            }
        }
    }
}
