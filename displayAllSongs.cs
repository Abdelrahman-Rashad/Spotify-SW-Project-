using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace spotify1
{
    public partial class displayAllSongs : Form
    {
        string ordb = "Data Source=orcl;User Id=hr;Password=Administrator1;";
        OracleConnection conn;
        public displayAllSongs()
        {
            InitializeComponent();
        }

        private void displayAllSongs_Load(object sender, EventArgs e)
        {

            conn = new OracleConnection(ordb);
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"SELECT s.songid,
                                       s.songtitle,
                                       s.publishdate,
                                       a.artistid,
                                       a.artistname,
                                       a.gender
                                FROM   artist a,
                                       songartist sa,
                                       song s
                                WHERE  s.songid = sa.songid
                                       AND sa.artistid = a.artistid";
            cmd.CommandType = CommandType.Text;

            OracleDataReader dr = cmd.ExecuteReader();

            dataGridView1.Columns.Add("song_id", "id");
            dataGridView1.Columns.Add("song_name", "name");
            dataGridView1.Columns.Add("song_date", "date");
            dataGridView1.Columns.Add("artist_id", "artist id");
            dataGridView1.Columns.Add("artist_name", "artist name");
            dataGridView1.Columns.Add("artist_gender", "artist gender");

            while (dr.Read())
            {
                int rowId = dataGridView1.Rows.Add();

                // Grab the new row!
                DataGridViewRow row = dataGridView1.Rows[rowId];

                // Add the data
                row.Cells["song_id"].Value = dr[0];
                row.Cells["song_name"].Value = dr[1];
                row.Cells["song_date"].Value = dr[2];
                row.Cells["artist_id"].Value = dr[3];
                row.Cells["artist_name"].Value = dr[4];
                row.Cells["artist_gender"].Value = dr[5];
            }

            dr.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => Application.Run(new mainForm()));
            thread.Start();
            this.Close();
        }
    }
}
