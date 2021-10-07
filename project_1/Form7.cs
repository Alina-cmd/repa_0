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
    public partial class Form7 : Form
    {
        public MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;pwd=Linux3004177;SslMode=none");
        public DataSet ds;
        public string name_table;
        public string name_ID_master;
        public string name_ID_procesure;
        public string name_ID_custom;
        public string value_ID_master;
        public string value_ID_procedure;
        public string value_ID_custom;
        string _id;
        Form3 form;
        bool exist;
        public Form7(Form3 f)
        {
            InitializeComponent();
            form = f;
            exist = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private MySqlCommand _getCommand(string sql)
        {
            return new MySqlCommand(sql, connection);
        }

        public Form7(Form3 f, string id_)
        {
            InitializeComponent();
            form = f;
            exist = true;
            _id = id_;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            textBox4.Text = id_;
            //MessageBox.Show(id_);
            try
            {
                connection.Open();
                textBox1.Text = Convert.ToString(_getCommand("SELECT (SELECT m_surname FROM alinacursach.masters WHERE ID_master = b_id_master) FROM alinacursach.book WHERE ID_record = " + id_).ExecuteScalar());
                textBox2.Text = Convert.ToString(_getCommand("SELECT (SELECT p_name FROM alinacursach.procedures0 WHERE ID_procedure = b_id_procedure) FROM alinacursach.book WHERE ID_record = " + id_).ExecuteScalar());
                textBox3.Text = Convert.ToString(_getCommand("SELECT (SELECT p_surname FROM alinacursach.customers WHERE ID_custom = b_id_custom) FROM alinacursach.book WHERE ID_record = " + id_).ExecuteScalar());
                string time_  = Convert.ToString(_getCommand("SELECT b_date FROM alinacursach.book WHERE ID_record = " + id_).ExecuteScalar());
                string _time = time_;
                textBox5.Text = time_.Substring(6,4);// year
                time_ = _time;
                textBox6.Text = time_.Substring(3,2);// mounth
                time_ = _time;
                textBox7.Text = time_.Substring(0,2);// day
                time_ = _time;
                textBox8.Text = time_.Substring(11,5);// time
                value_ID_master = Convert.ToString(_getCommand("SELECT b_id_master FROM alinacursach.book WHERE ID_record = " + _id).ExecuteScalar());
                value_ID_procedure = Convert.ToString(_getCommand("SELECT b_id_procedure FROM alinacursach.book WHERE ID_record = " + _id).ExecuteScalar());
                value_ID_custom = Convert.ToString(_getCommand("SELECT b_id_custom FROM alinacursach.book WHERE ID_record = " + _id).ExecuteScalar());
                //MessageBox.Show("all completed");
                connection.Close();
            }catch (Exception ex) { MessageBox.Show(ex.Message); connection.Close(); }
            
            

        }

        private void button7_Click(object sender, EventArgs e) // master
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM alinacursach.masters ORDER BY ID_master ASC", this.connection);
            connection.Open();
            ds = new DataSet();
            adapter.Fill(ds, "masters");
            name_table = "masters";
            dataGridView1.DataSource = ds.Tables["masters"];
            int _count = dataGridView1.Rows.Count;
            for (int i = 0; i < _count; i++)
            {
                dataGridView1.Rows[i].ReadOnly = true;
            }

            name_ID_master = dataGridView1.Columns[0].Name;
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e) // procedure
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM alinacursach.procedures0 ORDER BY ID_procedure ASC", this.connection);
            connection.Open();
            ds = new DataSet();
            adapter.Fill(ds, "procedures0");
            name_table = "procedures0";
            dataGridView1.DataSource = ds.Tables["procedures0"];
            int _count = dataGridView1.Rows.Count;
            for (int i = 0; i < _count; i++)
            {
                dataGridView1.Rows[i].ReadOnly = true;
            }
            name_ID_procesure = dataGridView1.Columns[0].Name;
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e) // custom
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM alinacursach.customers ORDER BY ID_custom ASC", this.connection);
            connection.Open();
            ds = new DataSet();
            adapter.Fill(ds, "customers");
            name_table = "customers";
            dataGridView1.DataSource = ds.Tables["customers"];
            int _count = dataGridView1.Rows.Count;
            for (int i = 0; i < _count; i++)
            {
                dataGridView1.Rows[i].ReadOnly = true;
            }
            name_ID_custom = dataGridView1.Columns[0].Name;
            connection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e) // Choose
        {
            
            switch (name_table) {
                case "masters":
                    value_ID_master = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    try
                    {
                        connection.Open();
                        string prom = Convert.ToString(_getCommand("SELECT m_surname FROM alinacursach.masters WHERE ID_master = " + value_ID_master).ExecuteScalar());
                        if (prom.Length > 13)
                        {
                            textBox1.Text = prom.Substring(0, 12) + "..";
                        }
                        else { textBox1.Text = prom; }
                        
                        connection.Close();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); connection.Close(); }
                    break;
                case "procedures0":
                    value_ID_procedure = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    try
                    {
                        connection.Open();
                        string prom = Convert.ToString(_getCommand("SELECT p_name FROM alinacursach.procedures0 WHERE ID_procedure = " + value_ID_procedure).ExecuteScalar());
                        if (prom.Length > 13)
                        {
                            textBox2.Text = prom.Substring(0, 12) + "..";
                        }
                        else { textBox2.Text = prom; }
                        connection.Close();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); connection.Close(); }
                    break;
                case "customers":
                    value_ID_custom = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    try
                    {
                        connection.Open();
                        string prom = Convert.ToString(_getCommand("SELECT p_surname FROM alinacursach.customers WHERE ID_custom = " + value_ID_custom).ExecuteScalar());
                        if (prom.Length > 13)
                        {
                            textBox3.Text = prom.Substring(0, 12) + "..";
                        }
                        else { textBox3.Text = prom; }
                        connection.Close();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); connection.Close(); }
                    break;
                                                           
            }
            
        }

        private void button3_Click(object sender, EventArgs e) // Save
        {
            if (exist)
            {
                //MessageBox.Show(value_ID_master + " " + value_ID_procedure + " " + value_ID_custom);
                try
                {
                    connection.Open();
                    _getCommand("update alinacursach.book set b_id_master = '" + value_ID_master + "', b_id_procedure = '" + value_ID_procedure + "', b_id_custom = '" + value_ID_custom + "', b_date = '" + textBox5.Text + "-" + textBox6.Text + "-" + textBox7.Text + " " + textBox8.Text + "' Where ID_record = " + Convert.ToInt32(textBox4.Text)).ExecuteScalar();
                    connection.Close();
                    form._updateDataGrid();
                    this.Close();

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); connection.Close(); }

            }
            else
            {
                try
                {
                    connection.Open();
                    //MessageBox.Show("INSERT INTO alinacursach.book VALUES(" + textBox4.Text + ", " + value_ID_master + ", " + value_ID_procedure + ", " + value_ID_custom + ", '2020-01-01 10-00')");
                    _getCommand("INSERT INTO alinacursach.book VALUES(" + textBox4.Text + ", " + value_ID_master + ", " + value_ID_procedure + ", " + value_ID_custom + ", '" + textBox5.Text + "-" + textBox6.Text + "-" + textBox7.Text + " " + textBox8.Text + "')").ExecuteScalar();
                    connection.Close();
                    form._updateDataGrid();
                    this.Close();

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); connection.Close(); }
            }
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            textBox5.Text = "";
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            textBox6.Text = "";
        }

        private void textBox7_Click(object sender, EventArgs e)
        {
            textBox7.Text = "";
        }

        private void textBox8_Click(object sender, EventArgs e)
        {
            textBox8.Text = "";
        }
    }
}
