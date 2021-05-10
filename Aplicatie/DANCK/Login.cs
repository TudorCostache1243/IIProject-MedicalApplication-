using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace DANCK
{
    public partial class Login : Form
    {
        string type;
        bool admin;
        public Login(string type)
        {
            InitializeComponent();
            this.Size = new Size(1200, 700);
            this.type = type;

        }

        private void Pacient_Load(object sender, EventArgs e)
        {
            ControlBox = false;
            this.CenterToScreen();
            if (type == "Administrator")
                admin = true;
            else
                admin = false;
            label1.BackColor = System.Drawing.Color.Transparent;
            label2.BackColor = System.Drawing.Color.Transparent;
            label3.BackColor = System.Drawing.Color.Transparent;
            label4.BackColor = System.Drawing.Color.Transparent;
            label5.BackColor = System.Drawing.Color.Transparent;

            label1.Font = new System.Drawing.Font("Times New Roman", 15);
            label2.Font = new System.Drawing.Font("Times New Roman", 15);
            label3.Font = new System.Drawing.Font("Times New Roman", 35);
            label4.Font = new System.Drawing.Font("Times New Roman", 15);
            label5.Font = new System.Drawing.Font("Times New Roman", 15);

            label1.Text ="Username: ";
            label2.Text ="Password: ";
            label3.Text = "Authentification";
            label4.Text = "Log in into your account or create a new account using the New Account button";
            label5.Text = "Account type: "+type;

            if (type == "Patient")
                button2.Show();
            else
                button2.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection("server=127.0.0.1;port=3306;user id=admin;password=industrialInformaticsProjectPassword;persistsecurityinfo=True;database=industrialinformaticsproject;SslMode=none");
            string query = "SELECT * FROM users WHERE username=" + '"' + textBox1.Text + '"' + " AND password=" + '"' + textBox2.Text + '"';
           // MessageBox.Show(query);
            MySqlCommand command = new MySqlCommand(query, connection);
            command.CommandTimeout = 60;
            MySqlDataReader reader;
            
            
            if (type == "Patient")
            {
                string type2 = "Patient";
                connection.Open();
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    int id = -1;
                    string row = "";
                    while (reader.Read())
                    {
                        row = reader.GetString("type"); // "type" is the column's name in the data table 'users'
                        id = Convert.ToInt32(reader.GetString("id"));
                    }
                   
                    if (type2 == row)
                    {

                        Form profil = new PacientProfil(id,admin);
                        this.Hide();
                        profil.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid type", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Wrong username or password!", "Error");

                }
                connection.Close();
            }
            if(type=="Doctor")
            {
                connection.Open();
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    int doctorId = -1;
                    string row = "";
                    while (reader.Read())
                    {
                        row = reader.GetString("type"); // "type" is the column's name in the data table 'users'
                        doctorId = Convert.ToInt32(reader.GetString("id"));
                    }
                    if (this.type == row)
                    {
                        Form profil = new MedicProfile(doctorId, admin, null);
                        this.Hide();
                        profil.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid type", "Error");
                    }
                    
                }
                else
                {
                    MessageBox.Show("Wrong username or password!", "Error");

                }
                connection.Close();

            }
            if(type=="Specialist Doctor")
            {
                connection.Open();
                reader = command.ExecuteReader();
                if(reader.HasRows)
                {   int specialistId = -1;
                    string row = "";
                    while (reader.Read())
                    {
                        row = reader.GetString("type");
                        specialistId = Convert.ToInt32(reader.GetString("id"));
                    }
                    if(this.type==row)
                    {
                        Form profil = new SpecialistProfil(specialistId, admin,null);
                        this.Hide();
                        profil.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid type", "Error");
                    }
                }else
                {
                    MessageBox.Show("Wrong username or password!", "Error");
                }
                
            }
            if(type=="Administrator")
            {
                connection.Open();
                reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    string row = "";
                    while (reader.Read())
                        row = reader.GetString("type");
                    if(this.type==row)
                    {
                        Form profil = new Administrator(type);
                        profil.Show();
                        this.Hide();
                    }else
                    {
                        MessageBox.Show("Invalid type", "Error");
                    }
                }else
                {
                    MessageBox.Show("Wrong username or password!", "Error");
                }
               
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form newaccount = new NewAccount(type);
            newaccount.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            this.Close();
            Form1 frm = new Form1();
            frm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
