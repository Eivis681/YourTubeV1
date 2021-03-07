using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using YourTube.Database;

namespace YourTube
{
    /// <summary>
    /// Interaction logic for EditKeyWindow.xaml
    /// </summary>
    public partial class EditKeyWindow : Window
    {
        public EditKeyWindow()
        {
            InitializeComponent();
            InicializeTextBoxComponent();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Hide();
            mainWindow.Show();
        }

        private void buttonSveKey_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string url = "https://www.googleapis.com/youtube/v3/playlistItems?part=snippet&maxResults=50&playlistId=PLYQ21GguN2ssJfHlwxZxzepHaiu3t798Y&key=" + textBoxApiKey.Text;
                WebClient wc = new WebClient();
                string urlData = wc.DownloadString(url);
                UpdateData updateData = new UpdateData();
                updateData.UpdateApiKey(textBoxApiKey.Text);
                MessageBox.Show("Api Key has been updated");
                MainWindow mainWindow = new MainWindow();
                this.Hide();
                mainWindow.Show();
            }
            catch
            {
                MessageBox.Show("Api key is invalid");
            }
        }
        private void InicializeTextBoxComponent()
        {
            UserGetSet.input();
            textBoxApiKey.Text = UserGetSet.apiKey;
        }
    }
}
