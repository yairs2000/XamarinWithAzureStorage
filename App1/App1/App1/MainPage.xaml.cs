using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        string uploadedFilename;
        public MainPage()
        {
            InitializeComponent();
        }

        async void OnUploadButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(uploadEditor.Text))
            {
                activityIndicator.IsRunning = true;

                var byteData = Encoding.UTF8.GetBytes(uploadEditor.Text);
                uploadedFilename = await AzureStorage.UploadFileAsync(ContainerType.Text, new MemoryStream(byteData));

                downloadButton.IsEnabled = true;
                activityIndicator.IsRunning = false;
            }
        }

        async void OnDownloadButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(uploadedFilename))
            {
                activityIndicator.IsRunning = true;

                var byteData = await AzureStorage.GetFileAsync(ContainerType.Text, uploadedFilename);
                var text = Encoding.UTF8.GetString(byteData, 0, byteData.Length);
                downloadEditor.Text = text;

                activityIndicator.IsRunning = false;
            }
        }
    }
}
