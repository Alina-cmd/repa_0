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
    public partial class Form9 : Form
    {
        public MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;pwd=Linux3004177;SslMode=none");
        public DataSet ds;
        string value_ID_custom;
        public Form9()
        {
            InitializeComponent();
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private MySqlCommand _getCommand(string sql)
        {
            return new MySqlCommand(sql, connection);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
                 
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e) // SELECT custom
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM alinacursach.customers ORDER BY ID_custom ASC", this.connection);
            connection.Open();
            ds = new DataSet();
            adapter.Fill(ds, "customers");
            dataGridView1.DataSource = ds.Tables["customers"];
            int _count = dataGridView1.Rows.Count;
            for (int i = 0; i < _count; i++)
            {
                dataGridView1.Rows[i].ReadOnly = true;
            }
            connection.Close();
        }

        private void button5_Click(object sender, EventArgs e) // choose custom
        {
            value_ID_custom = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            try
            {
                connection.Open();
                string prom = Convert.ToString(_getCommand("SELECT p_surname FROM alinacursach.customers WHERE ID_custom = " + value_ID_custom).ExecuteScalar());
                if (prom.Length > 13)
                {
                    label6.Text = prom.Substring(0, 12) + "..";
                }
                else { label6.Text = prom; }
                connection.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); connection.Close(); }
        }

        private void button1_Click(object sender, EventArgs e) // to form
        {
            string query;
            if (radioButton1.Checked)
            {

                query = "SELECT (SELECT m_surname from alinacursach.masters WHERE ID_master = b_id_master) AS Мастер, (SELECT p_name from alinacursach.procedures0 where ID_procedure = b_id_procedure) AS Процедура, b_date AS Время_проведения FROM alinacursach.book WHERE (b_date between '" + textBox1.Text + "-" + textBox2.Text.Substring(3, 2) + "-" + textBox2.Text.Substring(0, 2) + "' and '" + textBox4.Text + "-" + textBox3.Text.Substring(3, 2) + "-" + textBox3.Text.Substring(0, 2) + "') AND b_id_custom = " + value_ID_custom;
            }
            else {
                DateTime date_ = new DateTime();
                //string now = Convert.ToString(DateTime.Now);
                //int day_of_week = (int)date_.DayOfWeek;
                string d1 = Convert.ToString(DateTime.Now.AddDays(-7));
                string d2 = Convert.ToString(DateTime.Now);
                //label8.Text = d1 + " and " + d2;
                //label8.Text = Convert.ToString(day_of_week);
                query = "SELECT (SELECT m_surname from alinacursach.masters WHERE ID_master = b_id_master) AS Мастер, (SELECT p_name from alinacursach.procedures0 where ID_procedure = b_id_procedure) AS Процедура, b_date AS Время_проведения FROM alinacursach.book WHERE (b_date between '" + d1.Substring(6,4) + "-" + d1.Substring(3,2) + "-" + d1.Substring(0,2) + "' and '" + d2.Substring(6, 4) + "-" + d2.Substring(3, 2) + "-" + d2.Substring(0, 2) + "') AND b_id_custom = " + value_ID_custom; // тут будет запрос записей за неделю
            }
            //MessageBox.Show(query);
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, this.connection);
            connection.Open();
            ds = new DataSet();
            adapter.Fill(ds, "statistic1");
            dataGridView2.DataSource = ds.Tables["statistic1"];
            int _count = dataGridView2.Rows.Count;
            for (int i = 0; i < _count; i++)
            {
                dataGridView2.Rows[i].ReadOnly = true;
            }
            connection.Close();
        }

       /* private void button2_Click(object sender, EventArgs e)
        {
            DateTime date_ = new DateTime();
            //string now = Convert.ToString(DateTime.Now);
            //int day_of_week = (int)date_.DayOfWeek;
            string d1 = Convert.ToString(DateTime.Now.AddDays(-7));
            string d2 = Convert.ToString(DateTime.Now);
            label8.Text = d1 + " and " + d2;
            //label8.Text = Convert.ToString(day_of_week);
        }*/
    }
}
