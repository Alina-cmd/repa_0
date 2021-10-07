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

namespace project_1
{
    public partial class Form4 : Form
    {
        bool exist_;
        Form2 for2;
        public MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;pwd=Linux3004177;SslMode=none");
        public Form4(bool exi, Form2 f2) {
            InitializeComponent();
            exist_ = exi;
            for2 = f2;
            
        }

        public Form4(bool exi, Form2 f2, string _id, string[] v) { // конструктор вызываемый "изменить"
            InitializeComponent();
            exist_ = exi;
            for2 = f2;
            if (exist_) // может и не нужно?
            { // если запись существует и мы её изменяем
                getValues(_id, v);
            }
        }

        private MySqlCommand _getCommand(string sql)
        {
            return new MySqlCommand(sql, connection);
        }

        private void getValues( string _id, string[] val) {
            textBox1.Text = _id;
            textBox1.Enabled = false;
            textBox2.Text = val[0];
            textBox3.Text = val[1];
            textBox4.Text = val[2];
            textBox5.Text = val[3];
            // update masters set m_surname = 'простФами', m_patron = 'простОтчест' Where ID_master = '13';
                                

        }

        private void button6_Click(object sender, EventArgs e) // Save
        {
            if (exist_ == false) // Create
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("INSERT INTO alinacursach.masters VALUES(" + textBox1.Text + ", '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', " + textBox5.Text + ")");
                    _getCommand("INSERT INTO alinacursach.masters VALUES(" + textBox1.Text + ", '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', " + textBox5.Text + ")").ExecuteScalar();
                    connection.Close();
                    for2._updateDataGrid();
                    this.Close();
                        
                }
                catch(Exception ex) { MessageBox.Show(ex.Message); }
            }
            else
            { // Change
                try
                {
                    connection.Open();
                    _getCommand("update alinacursach.masters set alinacursach.masters.m_name = '" + textBox2.Text + "', m_surname = '"+textBox3.Text+"', m_patron = '"+textBox4.Text+"', m_salary = "+Convert.ToInt32(textBox5.Text)+" Where ID_master = "+ Convert.ToInt32(textBox1.Text)).ExecuteScalar();
                    connection.Close();
                    for2._updateDataGrid();
                    this.Close();

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); connection.Close(); }
            } 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
