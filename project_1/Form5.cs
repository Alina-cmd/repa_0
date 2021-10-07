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
    public partial class Form5 : Form
    {
        bool exist_;
        Form2 for2;
        public MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;pwd=Linux3004177;SslMode=none");
        public Form5(bool exi, Form2 f2)
        {
            InitializeComponent();
            exist_ = exi;
            for2 = f2;
        }

        public Form5(bool exi, Form2 f2, string _id, string[] v)
        { // конструктор вызываемый "изменить"
            InitializeComponent();
            exist_ = exi;
            for2 = f2;
            if (exist_)
            { // если запись существует и мы её изменяем
                getValues(_id, v);
            }
        }

        private MySqlCommand _getCommand(string sql)
        {
            return new MySqlCommand(sql, connection);
        }

        private void getValues(string _id, string[] val)
        {
            textBox1.Text = _id;
            textBox1.Enabled = false;
            textBox2.Text = val[0];
            textBox3.Text = val[1];
            textBox4.Text = val[2];
            //MessageBox.Show(_id);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e) // Save
        {
            if (exist_ == false) // Create
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("INSERT INTO alinacursach.procedures0 VALUES(" + textBox1.Text + ", '" + textBox2.Text + "', " + textBox3.Text + ", " + textBox4.Text + ")");
                    _getCommand("INSERT INTO alinacursach.procedures0 VALUES(" + textBox1.Text + ", '" + textBox2.Text + "', " + textBox3.Text + ", " + textBox4.Text + ")").ExecuteScalar();
                    connection.Close();
                    for2._updateDataGrid();
                    this.Close();

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else
            {// Change
                try
                {
                    connection.Open();
                    _getCommand("update alinacursach.procedures0 set p_name = '" + textBox2.Text + "', p_cost = " + Convert.ToInt32(textBox3.Text) + ", p_time_spending = " + Convert.ToInt32(textBox4.Text) + " Where ID_procedure = " + Convert.ToInt32(textBox1.Text)).ExecuteScalar();
                    connection.Close();
                    for2._updateDataGrid();
                    this.Close();

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); connection.Close(); }
            } 
        }
    }
}
