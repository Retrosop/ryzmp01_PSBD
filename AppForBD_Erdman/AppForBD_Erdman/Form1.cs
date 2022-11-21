using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.Design;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.OleDb;

namespace AppForBD_Erdman
{
    public partial class Form1 : Form
    {   
        
        private SqlConnection sqlConnection = null;
        DataSet ds1;
        DataSet ds2;
        DataSet ds3;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        //объявление строки подключения
        string connectionString = ConfigurationManager.ConnectionStrings["Jurnal"].ConnectionString;
        //Команды показа таблиц
        string sql1 = "SELECT * FROM Students";
        string sql2 = "SELECT * FROM Pocechenie_Pricyt";
        string sql3 = "SELECT * FROM Pocechenie_Otcytctvoval";
       
        public Form1()
        {
            InitializeComponent();

        }

       





        /*private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Jurnal"].ConnectionString);

            sqlConnection.Open();

            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Students", sqlConnection);

            DataSet db = new DataSet();
            dataAdapter.Fill(db);
            dataGridView2.DataSource = db.Tables[0];
           
        }*/



















        /*private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox4.Text = dateTimePicker1.Value.ToString("yyyy-MM-dd");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlCommand command = new SqlCommand($"INSERT INTO [Students] (Name, Email, PhoneNumber, DataBirthday) VALUES (@Name, @Email, @PhoneNumber, @DataBirthday)", sqlConnection);

            if (textBox1.Text == "" || textBox1.Text == " " ||  textBox3.Text == "" || textBox3.Text == " " || textBox4.Text == "" || textBox4.Text == " ")
            {
                MessageBox.Show("Обязательное поле не заполнено!");
            }
            else
            {
                command.Parameters.AddWithValue("Name", textBox1.Text);
                command.Parameters.AddWithValue("Email", textBox2.Text);
                command.Parameters.AddWithValue("PhoneNumber", textBox3.Text);
                command.Parameters.AddWithValue("DataBirthday", textBox4.Text);
                command.ExecuteNonQuery().ToString();
                MessageBox.Show("Данные внесены в таблицу 'Студенты'");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }
            
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
                "SELECT * FROM Students ORDER BY ID DESC", sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView3.DataSource = dataSet.Tables[0];
        }*/

        /*private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            textBox6.Text = dateTimePicker1.Value.ToString("yyyy-MM-dd");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand($"INSERT INTO [Pocechenie_Pricyt] (dateOfLecii, nameOfLecii, TimeOfLecii, Pricytcvoval_Name) VALUES (@dateOfLecii, @nameOfLecii, @TimeOfLecii, @Pricytcvoval_Name)", sqlConnection);

            if (textBox6.Text == "" || textBox6.Text == " " || textBox7.Text == "" || textBox7.Text == " " || textBox8.Text == "" || textBox8.Text == " " || textBox9.Text == "" || textBox9.Text == " ")
            {
                MessageBox.Show("Обязательное поле не заполнено!");
            }
            else
            {
                command.Parameters.AddWithValue("dateOfLecii", textBox6.Text);
                command.Parameters.AddWithValue("nameOfLecii", textBox7.Text);
                command.Parameters.AddWithValue("TimeOfLecii", textBox8.Text);
                command.Parameters.AddWithValue("Pricytcvoval_Name", textBox9.Text);
                command.ExecuteNonQuery().ToString();
                MessageBox.Show("Данные внесены в таблицу 'Присутсвующие на лекции'!");
                textBox6.Clear();
                textBox7.Clear();
                textBox8.Clear();
                textBox9.Clear();
            }
           
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
                "SELECT * FROM Pocechenie_Pricyt ORDER BY ID DESC", sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView3.DataSource = dataSet.Tables[0];


        }*/

        /*private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            textBox10.Text = dateTimePicker1.Value.ToString("yyyy-MM-dd");
        }


        private void button5_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand($"INSERT INTO [Pocechenie_Otcytctvoval] (dateOfLecii, nameOfLecii, TimeOfLecii, Oncytctvoval_Name) VALUES (@dateOfLecii, @nameOfLecii, @TimeOfLecii, @Oncytctvoval_Name)", sqlConnection);


            if (textBox10.Text == "" || textBox10.Text == " " || textBox11.Text == "" || textBox11.Text == " " || textBox12.Text == "" || textBox12.Text == " " || textBox13.Text == "" || textBox13.Text == " ")
            {
                MessageBox.Show("Обязательное поле не заполнено!");
            }
            else
            {
                command.Parameters.AddWithValue("dateOfLecii", textBox10.Text);
                command.Parameters.AddWithValue("nameOfLecii", textBox11.Text);
                command.Parameters.AddWithValue("TimeOfLecii", textBox12.Text);
                command.Parameters.AddWithValue("Oncytctvoval_Name", textBox13.Text);
                command.ExecuteNonQuery().ToString();
                MessageBox.Show("Данные внесены в таблицу 'Отсуствующие на лекции'!");
                textBox10.Clear();
                textBox11.Clear();
                textBox12.Clear();
                textBox13.Clear();
            }

            SqlDataAdapter dataAdapter = new SqlDataAdapter(
                "SELECT * FROM Pocechenie_Otcytctvoval ORDER BY ID DESC", sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView3.DataSource = dataSet.Tables[0];

        }*/


        /*private void button2_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
                textBox5.Text, sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0];
        }*/



        /*private void textBox14_TextChanged(object sender, EventArgs e)
        {
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Name LIKE '%{textBox14.Text}%'";
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
              "SELECT * FROM Students", sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView4.DataSource = dataSet.Tables[0];
        }*/

        /*private void ViewStud_Click(object sender, EventArgs e)
        {
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql1, connection);

                ds1 = new DataSet();
                adapter.Fill(ds1);
                dataGridView3.DataSource = ds1.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView3.Columns["Id"].ReadOnly = true;
                dataGridView3.Columns[0].HeaderText = "Id";
                dataGridView3.Columns[1].HeaderText = "Имя";
                dataGridView3.Columns[2].HeaderText = "Почта";
                dataGridView3.Columns[3].HeaderText = "Телефон";
                dataGridView3.Columns[4].HeaderText = "Дата рождения";
            }
        }*/

        /*private void ViewPricyt_Click(object sender, EventArgs e)
        {
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql2, connection);

                ds2 = new DataSet();
                adapter.Fill(ds2);
                dataGridView3.DataSource = ds2.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView3.Columns["Id"].ReadOnly = true;
                dataGridView3.Columns[0].HeaderText = "Id";
                dataGridView3.Columns[1].HeaderText = "Дата лекции";
                dataGridView3.Columns[2].HeaderText = "Название лекции";
                dataGridView3.Columns[3].HeaderText = "Время лекции";
                dataGridView3.Columns[4].HeaderText = "Имя присутсвующего";
            }
        }*/

        /*private void ViewOtcyt_Click(object sender, EventArgs e)
        {
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
                dataGridView3.Columns[0].HeaderText = "Id";
                dataGridView3.Columns[1].HeaderText = "Дата лекции";
                dataGridView3.Columns[2].HeaderText = "Название лекции";
                dataGridView3.Columns[3].HeaderText = "Время лекции";
                dataGridView3.Columns[4].HeaderText = "Имя отсутсвующего";
            }
        }*/



        /*private void DeleteStud_Click(object sender, EventArgs e)
        {


            DialogResult dialogResult = MessageBox.Show("Вы уверены, что хотите удалить выбранную строку/строки?", "Some Title", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridView3.SelectedRows)
                {
                    dataGridView3.Rows.Remove(row);
                }
                MessageBox.Show("Удаление из списка выполено, сохраните обязательно изменения");
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Удаление отменено");
            }

        }*/


        /*private void SaveStud_Click(object sender, EventArgs e)
        {
            
            DialogResult dialogResult = MessageBox.Show("Вы уверены, что хотите внести изменения?", "Some Title", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    adapter = new SqlDataAdapter(sql1, connection);
                    commandBuilder = new SqlCommandBuilder(adapter);
                    adapter.InsertCommand = new SqlCommand("sp_CreateUser", connection);
                    adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50, "Name"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 50, "Email"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 50, "PhoneNumber"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@DataBirthday", SqlDbType.Date, 10, "DataBirthday"));

                    SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                    parameter.Direction = ParameterDirection.Output;

                    adapter.Update(ds1);

                }
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Изменения НЕ внесены");
            }
        }*/
        /*private void DeletePricyt_Click(object sender, EventArgs e)
               {
                   DialogResult dialogResult = MessageBox.Show("Вы уверены, что хотите удалить выбранную строку/строки?", "Some Title", MessageBoxButtons.YesNo);
                   if (dialogResult == DialogResult.Yes)
                   {
                       foreach (DataGridViewRow row in dataGridView3.SelectedRows)
                       {
                           dataGridView3.Rows.Remove(row);
                       }
                       MessageBox.Show("Удаление из списка выполено, сохраните обязательно изменения");
                   }
                   else if (dialogResult == DialogResult.No)
                   {
                       MessageBox.Show("Удаление отменено");
                   }
               }*/
        /*private void SavePricyt_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы уверены, что хотите внести изменения?", "Some Title", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    adapter = new SqlDataAdapter(sql2, connection);
                    commandBuilder = new SqlCommandBuilder(adapter);
                    adapter.InsertCommand = new SqlCommand("sp_CreatePricyt", connection);
                    adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@dateOfLecii", SqlDbType.Date, 10, "dateOfLecii"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@nameOfLecii", SqlDbType.NVarChar, 50, "nameOfLecii"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@TimeOfLecii", SqlDbType.NVarChar, 50, "TimeOfLecii"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@Pricytcvoval_Name", SqlDbType.NVarChar, 50, "Pricytcvoval_Name"));

                    SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                    parameter.Direction = ParameterDirection.Output;

                    adapter.Update(ds2);

                }
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Изменения НЕ внесены");
            }
        }*/

        /*private void DeleteOtcyt_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы уверены, что хотите удалить выбранную строку/строки?", "Some Title", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridView3.SelectedRows)
                {
                    dataGridView3.Rows.Remove(row);
                }
                MessageBox.Show("Удаление из списка выполено, сохраните обязательно изменения");
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Удаление отменено");
            }
        }*/

        /*private void SaveOtcyt_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Вы уверены, что хотите внести изменения?", "Some Title", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    adapter = new SqlDataAdapter(sql3, connection);
                    commandBuilder = new SqlCommandBuilder(adapter);
                    adapter.InsertCommand = new SqlCommand("sp_CreateOtcyt", connection);
                    adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@dateOfLecii", SqlDbType.Date, 10, "dateOfLecii"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@nameOfLecii", SqlDbType.NVarChar, 50, "nameOfLecii"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@TimeOfLecii", SqlDbType.NVarChar, 50, "TimeOfLecii"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@Oncytctvoval_Name", SqlDbType.NVarChar, 50, "Oncytctvoval_Name"));

                    SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                    parameter.Direction = ParameterDirection.Output;

                    adapter.Update(ds3);

                }
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Изменения НЕ внесены");
            }
        }*/
    }
    
}
