using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace YourTube.Database
{
    public class CreateData
    {
        SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=Yourtube.db; Version = 3; New = True; Compress = True; ");
        public void CreatePlaylist(List<JsonList> list, string playlistName)
        {
            sqlite_conn.Open();
            SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO Playlist (PlaylistName) VALUES(?); ";
            sqlite_cmd.Parameters.AddWithValue("PlaylistName", playlistName);
            sqlite_cmd.ExecuteNonQuery();
            AddSongsToPlaylist(list, playlistName);
            sqlite_conn.Close();
        }
        public void AddSongsToPlaylist(List<JsonList> list, string playlistName)
        {
            sqlite_conn.Open();
            SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO Songs (SongId, SongName, Downloaded, PlaylistId) VALUES(?,?,?,?); ";
            foreach(var data in list)
            {
                sqlite_cmd.Parameters.AddWithValue("SongId", data.videoId);
                sqlite_cmd.Parameters.AddWithValue("SongName", data.title);
                sqlite_cmd.Parameters.AddWithValue("Downloaded", "No");
                sqlite_cmd.Parameters.AddWithValue("PlaylistId", playlistName);
                sqlite_cmd.ExecuteNonQuery();
            }
            sqlite_conn.Close();
        }
    }
}
