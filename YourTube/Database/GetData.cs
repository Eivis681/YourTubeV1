using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace YourTube.Database
{
    public class GetData
    {
        SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=Yourtube.db; Version = 3; New = True; Compress = True; ");
        public void GetUserData()
        {
            sqlite_conn.Open();
            SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT ApiKey FROM Details";
            SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader();
            UserGetSet.input();
            while (sqlite_datareader.Read())
            {
                UserGetSet.apiKey = sqlite_datareader.GetString(0);
            }
            sqlite_conn.Close();
        }
        public List<PlaylistObject> GetPlaylists()
        {
            sqlite_conn.Open();
            SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT PlaylistName, SongCount FROM Playlist";
            SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader();
            List<PlaylistObject> list = new List<PlaylistObject>();
            while (sqlite_datareader.Read())
            {
                list.Add(new PlaylistObject { playlistName = sqlite_datareader.GetString(0), songCount = sqlite_datareader.GetString(1) });
            }
            sqlite_conn.Close();
            return list;
        }
    }
}
