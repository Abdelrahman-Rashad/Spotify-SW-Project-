using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace spotify1
{
    public partial class publichSongForm : Form
    {
        string ordb = "Data Source=orcl;User Id=hr;Password=Administrator1;";
        OracleConnection conn;

        public publichSongForm()
        {
            InitializeComponent();
        }

        private void publichSongForm_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int maxID, newID;
            OracleCommand c = new OracleCommand();
            c.Connection = conn;



            c.CommandText = "GetSongID";
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("id", OracleDbType.Int32, ParameterDirection.Output);
            c.ExecuteNonQuery();
            try
            {
                maxID = Convert.ToInt32(c.Parameters["id"].Value.ToString());
                newID = maxID + 1;
            }
            catch { newID = 1; }
     











            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into SONG values (:songID,:songName,:datePublished,:duration)";
            //cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("songID", newID);
            cmd.Parameters.Add("songName", textBox1.Text);
            cmd.Parameters.Add(new OracleParameter("dateParam", OracleDbType.Date)).Value = DateTime.Now;
            cmd.Parameters.Add("duration", textBox2.Text);
            int r = cmd.ExecuteNonQuery();


            OracleCommand cmd1 = new OracleCommand();
            cmd1.Connection = conn;
            cmd1.CommandText = "insert into SONGARTIST values (:songID,:artistID)";
            // cmd1.CommandType = CommandType.Text;
            cmd1.Parameters.Add("songID", newID);
            cmd1.Parameters.Add("artistID", textBox4.Text);
            int r1 = cmd1.ExecuteNonQuery();

            if (r != -1 && r1 != -1)
            {
                MessageBox.Show("Song Added Successfully");
            }
            else
            {
                MessageBox.Show("Couldn't Add Song");
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => Application.Run(new mainForm()));
            thread.Start();
            this.Close();
        }



        private void button2_Click_1(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "update SONG set SONGTITLE=:songName, DURATION=:duration "
                + "where SONGID=:songID";
            //cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("songName", textBox1.Text);
            cmd.Parameters.Add("duration", textBox2.Text);
            cmd.Parameters.Add("songID", textBox5.Text);
            int r = cmd.ExecuteNonQuery();
            if (r != -1)
            {
                MessageBox.Show("Song Modified");
            }
            else
            {
                MessageBox.Show("Couldn't Modify Song");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
