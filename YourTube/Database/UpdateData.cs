using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace YourTube.Database
{
    public class UpdateData
    {
        SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=Yourtube.db; Version = 3; New = True; Compress = True; ");
        public void UpdateApiKey(string apieKey)
        {
            sqlite_conn.Open();
            SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "UPDATE Details Set ApiKey = ? Where Id = 1";
            sqlite_cmd.Parameters.AddWithValue("ApiKey", apieKey);
            sqlite_cmd.ExecuteNonQuery();
            sqlite_conn.Close();
        }

        //public void UpdatePlaylistName(string oldPlaylistName, string newPlaylistName)
        //{
        //    sqlite_conn.Open();
        //    SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();
        //    sqlite_cmd.CommandText = "UPDATE Playlist Set PlaylistName = ? Where PlaylistName = ?";
        //    sqlite_cmd.Parameters.AddWithValue("ApiKey", apieKey);
        //    sqlite_cmd.ExecuteNonQuery();
        //    sqlite_conn.Close();
        //}
    }
}
