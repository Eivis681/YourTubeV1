using System.Collections.Generic;
using System.IO;
using System.Windows;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;


namespace YourTube.Downloads
{
    public delegate void ProgressBarSong(int data);
    public class SongDownload
    {
        public readonly ProgressBarSong progressBarSong;
        public SongDownload(ProgressBarSong progressBar)
        {
            progressBarSong = progressBar;
        }
        public async void DownloadSongs(List<JsonList> songId, string directory)
        {
            int counter = 0;
            foreach (var id in songId)
            {
                var youtube = new YoutubeClient();
                string url = "https://www.youtube.com/watch?v=" + id.videoId;
                var vid = await youtube.Videos.GetAsync(url);
                
                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(id.videoId);
                var streamInfo = streamManifest.GetAudioOnly().WithHighestBitrate();
                if (streamInfo != null)
                {
                    await youtube.Videos.Streams.GetAsync(streamInfo);
                    await youtube.Videos.Streams.DownloadAsync(streamInfo, vid.Title + ".mp3");
                }
                string data = Directory.GetCurrentDirectory() + "\\";
                
                File.Move(data + vid.Title + ".mp3", directory + "\\" + vid.Title + ".mp3");
                
                counter++;
                progressBarSong.Invoke(counter);
            }
        }
    }
}
