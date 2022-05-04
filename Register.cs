using Oracle.DataAccess.Client;
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

namespace spotify1
{
    public partial class Register : Form
    {
        string ordb = "Data Source=orcl;User Id=hr;Password=Administrator1;";
        OracleConnection conn;

        public Register()
        {
            InitializeComponent();
        }

        private void Register_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text != String.Empty && textBox2.Text != String.Empty && textBox3.Text != String.Empty && textBox4.Text != String.Empty && comboBox1.SelectedItem != null)
            {
                if (textBox3.Text == textBox4.Text)
                {
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "select Username from ARTIST where Username=:usrname";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("usrname", textBox2.Text);

                    OracleDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        dr.Close();
                        errorProvider1.SetError(textBox2, "Username already exist please enter another one");
                    }
                    else
                    {
                        dr.Close();
                        OracleCommand cn = new OracleCommand();
                        cn.Connection = conn;
                        cn.CommandType = CommandType.Text;
                        cn.CommandText = "select mySequence.nextval from dual";
                        string lnNextval = Convert.ToString(cn.ExecuteScalar());
                        cn.CommandText = "insert into ARTIST (artistid,artistname,username,password,gender) values (:id,:name,:usname,:password,:gender)";

                        cn.Parameters.Add("id", lnNextval);
                        cn.Parameters.Add("name", textBox1.Text);
                        cn.Parameters.Add("usname", textBox2.Text);
                        cn.Parameters.Add("password", textBox3.Text);
                        cn.Parameters.Add("gender", comboBox1.SelectedItem.ToString());
                        cn.ExecuteNonQuery();
                        MessageBox.Show("Your Account is created . Please login now.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Thread thread = new Thread(() => Application.Run(new mainForm()));
                        thread.Start();
                        this.Close();
                        //this.Hide();
                        //Login login = new Login();
                        //login.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Please enter both password same ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill all the required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
        }
    }
}
