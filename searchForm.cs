using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Threading;

namespace spotify1
{
    public partial class SearchForm : Form
    {
        OracleDataAdapter adapter;
        OracleCommandBuilder builder;
        DataSet ds;

        public SearchForm()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView1.ForeColor = Color.Black;
            dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(60)))), ((int)(((byte)(55)))));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connstr = "Data Source=orcl;User Id=hr;Password=Administrator1;";
            string cmdstr = "select * from SONG " +
                "where songTitle=:songName";

            adapter = new OracleDataAdapter(cmdstr,connstr);
            adapter.SelectCommand.Parameters.Add("songName", textBox1.Text.ToString());

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

            builder = new OracleCommandBuilder(adapter);
            adapter.Update(ds.Tables[0]);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connstr = "Data Source=orcl;User Id=hr;Password=Administrator1;";
            string cmdstr = "select * from ARTIST  " +
                "where ArtistName=:Name";

            adapter = new OracleDataAdapter(cmdstr, connstr);
            adapter.SelectCommand.Parameters.Add("Name", textBox1.Text.ToString());

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

            builder = new OracleCommandBuilder(adapter);
            adapter.Update(ds.Tables[0]);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Thread thread = new Thread(() => Application.Run(new mainForm()));
            thread.Start();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        
    }
}
