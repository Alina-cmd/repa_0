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
    public partial class Form3 : Form { 

        public MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;pwd=Linux3004177;SslMode=none");
        public DataSet ds;
        Form1 fo1;

        private MySqlCommand _getCommand(string sql)
        {
            return new MySqlCommand(sql, connection);
        }

        public void _updateDataGrid()// а нужен ли connection.Open(); ?
        {
            //Адаптер необходим для вывода в DataGrid данных из таблицы(можно воспользоваться и обычным Mysqlcommand)
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT ID_record AS ID, (SELECT m_surname FROM alinacursach.masters WHERE ID_master=b_id_master) AS Мастер , (SELECT p_name FROM alinacursach.procedures0 WHERE ID_procedure=b_id_procedure) AS Процедура, (SELECT p_surname FROM alinacursach.customers WHERE ID_custom=b_id_custom) AS Клиент, b_date AS Дата FROM alinacursach.book ORDER BY ID_record ASC", connection);
            ds = new DataSet();
            adapter.Fill(ds, "records");
            dataGridView1.DataSource = ds.Tables["records"];
        }

        public Form3( Form1 f1)
        {
            InitializeComponent();
            fo1 = f1;
            MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;pwd=Linux3004177;SslMode=none");
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT ID_record AS ID, (SELECT m_surname FROM alinacursach.masters WHERE ID_master=b_id_master) AS Мастер , (SELECT p_name FROM alinacursach.procedures0 WHERE ID_procedure=b_id_procedure) AS Процедура, (SELECT p_surname FROM alinacursach.customers WHERE ID_custom=b_id_custom) AS Клиент, b_date AS Дата FROM alinacursach.book ORDER BY ID_record ASC", connection);
            connection.Open();
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ds =new DataSet();
            adapter.Fill(ds, "records");
            dataGridView1.DataSource = ds.Tables["records"];
            connection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            fo1.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e) // ADD
        {
            Form7 form7 = new Form7(this);
            form7.Show();
        }

        private void button5_Click(object sender, EventArgs e) //Delete
        {
            string st = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            try
            {
                connection.Open();
                _getCommand("DELETE FROM alinacursach.book WHERE alinacursach.book.ID_record=" + st).ExecuteScalar();
                connection.Close();
                _updateDataGrid();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); connection.Close(); }
        }

        private void button7_Click(object sender, EventArgs e) // Change
        {
            string id_ = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            Form7 form7 = new Form7(this, id_);
            form7.Show();
        }
    }
}
