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
    public partial class Form2 : Form

    {
        public MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;pwd=Linux3004177;SslMode=none");
        public DataSet ds;
        public string name_table = "masters";
        public string name_column;
        bool exist_ = false;
        Form1 fo1;
        //private const string _Select = "SELECT * FROM /*место для вашей таблицы*/";
        public Form2( Form1 f1)
        {
            InitializeComponent();
            fo1 = f1;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void button1_Click(object sender, EventArgs e)// masters
        {
 /*           MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM alinacursach.masters ORDER BY ID_master ASC", this.connection);
 //           connection.Open();
 //           ds = new DataSet();
            adapter.Fill(ds, "masters");
            name_table = "masters";
            dataGridView1.DataSource = ds.Tables["masters"];
            int _count = dataGridView1.Rows.Count;
            for (int i = 0; i < _count; i++)
            {
                dataGridView1.Rows[i].ReadOnly = true;
            }
            
            name_column = dataGridView1.Columns[0].Name;
            connection.Close();
*/
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            fo1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)//procedures0
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
            name_column = dataGridView1.Columns[0].Name;
            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e)//customers
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
            name_column = dataGridView1.Columns[0].Name;
            connection.Close();
        }

        private void button5_Click(object sender, EventArgs e) //Delete 
        {
            string st = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            MessageBox.Show("DELETE FROM alinacursach." + name_table + " WHERE alinacursach." + name_table + "."+name_column+"=" + st);
            try
            {
                connection.Open();
                //_getCommand("USE alinacursach");
                _getCommand("DELETE FROM alinacursach."+name_table+ " WHERE alinacursach." + name_table + "." + name_column + "=" + st).ExecuteScalar();
                connection.Close();
                _updateDataGrid();
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); connection.Close(); }
        }

        public void _updateDataGrid()// а нужен ли connection.Open(); ?
        {
            //Адаптер необходим для вывода в DataGrid данных из таблицы(можно воспользоваться и обычным Mysqlcommand)
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM alinacursach." + name_table, connection);
            ds = new DataSet();
            adapter.Fill(ds, name_table);
            dataGridView1.DataSource = ds.Tables[name_table];
        }

        private MySqlCommand _getCommand(string sql)
        {
            return new MySqlCommand(sql, connection);
        }

        private void button6_Click(object sender, EventArgs e) // ADD
        {
            Form2 for2 = this;
            exist_ = false;
            switch (name_table) {
                case "masters":
                    Form4 form4 = new Form4(for2.exist_, for2);        // конструктор пустой формы 
                    form4.Show();
                    break;
                case "procedures0": 
                    break;
                case "customers":
                    Form6 form6 = new Form6(for2.exist_, for2);        // конструктор пустой формы
                    form6.Show();
                    break;
            }

        }

        private void button7_Click(object sender, EventArgs e) // Change
        {
            Form2 for2 = this;
            exist_ = true;
            string[] val = new string[5];
            string st = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            switch (name_table)
            {
                case "masters":
                    for (int i = 0; i < 4; i++)
                    {
                        val[i] = dataGridView1.SelectedRows[0].Cells[i + 1].Value.ToString();
                    }
                    Form4 form4 = new Form4(for2.exist_, for2,st, val);        // конструктор пустой формы 
                    form4.Show();
                    break;
                case "customers":
                    for (int i = 0; i < 3; i++)
                    {
                        val[i] = dataGridView1.SelectedRows[0].Cells[i + 1].Value.ToString();
                    }
                    Form6 form6 = new Form6(for2.exist_, for2, st, val);        // конструктор пустой формы
                    form6.Show();
                    break;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
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
            
            name_column = dataGridView1.Columns[0].Name;
            connection.Close();
        }
    }
}
