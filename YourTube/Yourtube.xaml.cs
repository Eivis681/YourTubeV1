using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using YourTube.Database;
using YourTube.Downloads;

namespace YourTube
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SongDownload songDownload;
        public MainWindow()
        {
            InitializeComponent();
            songDownload = new SongDownload(SongProgressBar);
            UpdateInterface();
            InitiateData();
        }

        private void buttonSongDownload_Click(object sender, RoutedEventArgs e)
        {
            string[] array = textBoxSongUrl.Text.Split('?');
            if (String.IsNullOrEmpty(textBoxSongUrl.Text))
            {
                MessageBox.Show("Please insert a song Url");
                return;
            }
            if (String.IsNullOrEmpty(textBoxSongDirectory.Text))
            {
                MessageBox.Show("Please provide a directory");
                return;
            }
            if (array[0] != "https://www.youtube.com/watch" && array[0] != "https://www.youtube.com/playlist")
            {
                MessageBox.Show("We only support Youtube.com links");
                return;
            }
            if (array[0] == "https://www.youtube.com/watch")
            {
                var result = MessageBox.Show("Do you want to download this one song ?", "Download", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    progressBarSong.Maximum = 1;
                    List<JsonList> songId = new List<JsonList>();
                    var songIds = textBoxSongUrl.Text.Split('=');
                    songId.Add(new JsonList { videoId = songIds[1] });
                    SongDownload songDownload = new SongDownload(SongProgressBar);
                    songDownload.DownloadSongs(songId, textBoxSongDirectory.Text);
                }
                return;
            }
            if (array[0] == "https://www.youtube.com/playlist")
            {
                var result = MessageBox.Show("Do you want to download a song playlist ?", "Download", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var results = MessageBox.Show("Do you want to so this playlist to database ?", "Download", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (results == MessageBoxResult.Yes)
                    {
                        GetSongIdTitle getSongId = new GetSongIdTitle();
                        List<JsonList> resultList = getSongId.SongId(textBoxSongUrl.Text);
                        CreateData createData = new CreateData();
                        //add pop up window for playlist name 
                        createData.AddSongsToPlaylist(resultList, "playlist name");
                        progressBarSong.Maximum = resultList.Count;
                        SongDownload songDownload = new SongDownload(SongProgressBar);
                        songDownload.DownloadSongs(resultList, textBoxSongDirectory.Text);
                    }
                    if (results == MessageBoxResult.No)
                    {

                        GetSongIdTitle getSongId = new GetSongIdTitle();
                        List<JsonList> resultList = getSongId.SongId(textBoxSongUrl.Text);
                        progressBarSong.Maximum = resultList.Count;
                        SongDownload songDownload = new SongDownload(SongProgressBar);
                        songDownload.DownloadSongs(resultList, textBoxSongDirectory.Text);
                    }
                }

            }
        }
        public void SongProgressBar(int number)
        {
            progressBarSong.Value = number;
            if (progressBarSong.Value == progressBarSong.Maximum)
            {
                MessageBox.Show("All songs have been downloaded");
            }
        }

        private void buttonSongBrowse_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            textBoxSongDirectory.Text = dialog.FileName;
        }

        private void buttonEditKey_Click(object sender, RoutedEventArgs e)
        {
            EditKeyWindow editKeyWindow = new EditKeyWindow();
            this.Hide();
            editKeyWindow.Show();
        }

        private void UpdateInterface()
        {
            GetData getData = new GetData();
            List<PlaylistObject> playlists = getData.GetPlaylists();
            labelTotalPlaylists.Content = playlists.Count.ToString();
            // add to list view playlist items 
           // playlistListView.
        }

        private void InitiateData()
        {
            GetData getData = new GetData();
            getData.GetUserData();
            UserGetSet.input();
            labelApiKey.Content = UserGetSet.apiKey;
        }
    }
}
