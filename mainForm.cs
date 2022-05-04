using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using System.Windows.Forms;

namespace spotify1
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
           
            Thread thread = new Thread(() => Application.Run(new SearchForm()));

            thread.Start();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => Application.Run(new publichSongForm()));
            thread.Start();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 
            // put the name of the form instade of searchForm
            Thread thread = new Thread(() => Application.Run(new displayAllSongs()));
            //
            thread.Start();
            this.Close();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => Application.Run(new ReportForm1()));
            thread.Start();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => Application.Run(new Form1()));
            thread.Start();
            this.Close();
        }
    }
}
