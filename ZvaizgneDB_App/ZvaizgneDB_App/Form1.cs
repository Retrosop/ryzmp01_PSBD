using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZvaizgneDB_App
{
    public partial class Form1 : Form
    {
        DataSet ds1;
        DataSet ds2;
        DataSet ds3;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Catlink\source\repos\ZvaizgneDB_App\ZvaizgneDB_App\Database2.mdf;Integrated Security=True";
        string sql1 = "SELECT * FROM Book";
        string sql2 = "SELECT * FROM Clients";
        string sql3 = "SELECT * FROM Orders";


        public Form1()
        {
            InitializeComponent();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql1, connection);

                ds1 = new DataSet();
                adapter.Fill(ds1);
                dataGridView1.DataSource = ds1.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView1.Columns["Id"].ReadOnly = true;
                dataGridView1.Columns[0].HeaderText = "Код";
                dataGridView1.Columns[1].HeaderText = "Название книги";
                dataGridView1.Columns[2].HeaderText = "Дата публикации";
                dataGridView1.Columns[3].HeaderText = "Дата заказа";
                dataGridView1.Columns[4].HeaderText = "Количество";
            }

            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql2, connection);

                ds2 = new DataSet();
                adapter.Fill(ds2);
                dataGridView2.DataSource = ds2.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView2.Columns["Id"].ReadOnly = true;
                dataGridView2.Columns[0].HeaderText = "Код";
                dataGridView2.Columns[1].HeaderText = "Имя";
                dataGridView2.Columns[2].HeaderText = "Email";
                dataGridView2.Columns[3].HeaderText = "Номер телефона";
                dataGridView2.Columns[4].HeaderText = "Адрес";
            }

            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql3, connection);

                ds3 = new DataSet();
                adapter.Fill(ds3);
                dataGridView3.DataSource = ds3.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView3.Columns["Id"].ReadOnly = true;
                dataGridView3.Columns[0].HeaderText = "Код";
                dataGridView3.Columns[1].HeaderText = "Дата заказа";
                dataGridView3.Columns[2].HeaderText = "Клиент";
                dataGridView3.Columns[3].HeaderText = "Книга";
                dataGridView3.Columns[4].HeaderText = "Количество";
            }
        }




        private void button1_Click(object sender, EventArgs e)
        {
            DataRow row = ds1.Tables[0].NewRow(); // добавляем новую строку в DataTable
            ds1.Tables[0].Rows.Add(row);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql1, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("sp_CreateBook", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Book_Name", SqlDbType.NVarChar, 50, "Book_Name"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Date_Pub", SqlDbType.Date, 10, "Date_Pub"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Date_Order", SqlDbType.Date, 10, "Date_Order"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Kolvo", SqlDbType.Int, 50, "Kolvo"));

                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                parameter.Direction = ParameterDirection.Output;

                adapter.Update(ds1);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataRow row = ds2.Tables[0].NewRow(); // добавляем новую строку в DataTable
            ds2.Tables[0].Rows.Add(row);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            {
                dataGridView2.Rows.Remove(row);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql2, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("sp_CreateClients", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Name_Clients", SqlDbType.NVarChar, 50, "Name_Clients"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 50, "Email"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 50, "PhoneNumber"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Adress", SqlDbType.NVarChar, 50, "Adress"));

                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                parameter.Direction = ParameterDirection.Output;

                adapter.Update(ds2);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DataRow row = ds3.Tables[0].NewRow(); // добавляем новую строку в DataTable
            ds3.Tables[0].Rows.Add(row);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView3.SelectedRows)
            {
                dataGridView3.Rows.Remove(row);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql3, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("sp_CreateOreders", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@DateOrder", SqlDbType.Date, 10, "DateOrder"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Client", SqlDbType.NVarChar, 50, "Client"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Book", SqlDbType.NVarChar, 50, "Book"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Kolvo", SqlDbType.Int, 50, "Kolvo"));

                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                parameter.Direction = ParameterDirection.Output;

                adapter.Update(ds3);
            }
        }
    }
}






