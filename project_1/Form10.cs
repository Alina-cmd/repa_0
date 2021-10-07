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
    public partial class Form10 : Form
    {
        public MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;pwd=Linux3004177;SslMode=none");
        public DataSet ds;
        public Form10()
        {
            InitializeComponent();
        }

        private MySqlCommand _getCommand(string sql)
        {
            return new MySqlCommand(sql, connection);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = " ";
            if (radioButton1.Checked)
            { // 1 day
                DateTime date_ = new DateTime();
                string d1 = Convert.ToString(DateTime.Now.AddDays(-1));
                string d2 = Convert.ToString(DateTime.Now);
                query = "SELECT (SELECT p_surname FROM alinacursach.customers WHERE ID_custom = b_id_custom ) AS Клиент, COUNT(ID_record) AS Проведённых_процедур FROM alinacursach.book WHERE b_date between '" + d1.Substring(6, 4) + "-" + d1.Substring(3, 2) + "-" + d1.Substring(0, 2) + "' and '" + d2.Substring(6, 4) + "-" + d2.Substring(3, 2) + "-" + d2.Substring(0, 2) + "' group by b_id_custom;";
            }
            
            if (radioButton2.Checked) { // 1 week
                string d1 = Convert.ToString(DateTime.Now.AddDays(-7));
                string d2 = Convert.ToString(DateTime.Now);
                query = "SELECT (SELECT p_surname FROM alinacursach.customers WHERE ID_custom = b_id_custom ) AS Клиент, COUNT(ID_record) AS Проведённых_процедур FROM alinacursach.book WHERE b_date between '" + d1.Substring(6, 4) + "-" + d1.Substring(3, 2) + "-" + d1.Substring(0, 2) + "' and '" + d2.Substring(6, 4) + "-" + d2.Substring(3, 2) + "-" + d2.Substring(0, 2) + "' group by b_id_custom;";
            }

            if (radioButton3.Checked) { // 1 mounth
                string d1 = Convert.ToString(DateTime.Now.AddDays(-31));
                string d2 = Convert.ToString(DateTime.Now);
                query = "SELECT (SELECT p_surname FROM alinacursach.customers WHERE ID_custom = b_id_custom ) AS Клиент, COUNT(ID_record) AS Проведённых_процедур FROM alinacursach.book WHERE b_date between '" + d1.Substring(6, 4) + "-" + d1.Substring(3, 2) + "-" + d1.Substring(0, 2) + "' and '" + d2.Substring(6, 4) + "-" + d2.Substring(3, 2) + "-" + d2.Substring(0, 2) + "' group by b_id_custom;";
            }
            if (radioButton4.Checked) {
                query = "SELECT (SELECT p_surname FROM alinacursach.customers WHERE ID_custom = b_id_custom ) AS Клиент, COUNT(ID_record) AS Проведённых_процедур FROM alinacursach.book WHERE b_date between '" + textBox1.Text + "-" + textBox2.Text.Substring(3, 2) + "-" + textBox2.Text.Substring(0, 2) + "' and '" + textBox4.Text + "-" + textBox3.Text.Substring(3, 2) + "-" + textBox3.Text.Substring(0, 2) + "' group by b_id_custom;";
                //query = "SELECT (SELECT m_surname from alinacursach.masters WHERE ID_master = b_id_master) AS Мастер, (SELECT p_name from alinacursach.procedures0 where ID_procedure = b_id_procedure) AS Процедура, b_date AS Время_проведения FROM alinacursach.book WHERE (b_date between '" + textBox1.Text + "-" + textBox2.Text.Substring(3, 2) + "-" + textBox2.Text.Substring(0, 2) + "' and '" + textBox4.Text + "-" + textBox3.Text.Substring(3, 2) + "-" + textBox3.Text.Substring(0, 2) + "')";
            }
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
    }
}
