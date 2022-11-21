using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.ComponentModel.Design;
using System.Configuration;

namespace AppForBD_Erdman
{
    public partial class Form1 : Form
    {

        //Перенос выбранной даты в textBox
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox4.Text = dateTimePicker1.Value.ToString("yyyy-MM-dd");
        }
        //поля заполнения новвых данных
        private void button1_Click(object sender, EventArgs e)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql1, connection);

                ds1 = new DataSet();
                SqlCommand command = new SqlCommand($"INSERT INTO [Students] (Name, Email, PhoneNumber, DataBirthday) VALUES (@Name, @Email, @PhoneNumber, @DataBirthday)", connection);



                if (textBox1.Text == "" || textBox1.Text == " " || textBox3.Text == "" || textBox3.Text == " " || textBox4.Text == "" || textBox4.Text == " ")
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
                    "SELECT * FROM Students ORDER BY ID DESC", connectionString);

                dataAdapter.Fill(ds1);

                dataGridView3.DataSource = ds1.Tables[0];
                dataGridView3.Columns["Id"].ReadOnly = true;
                dataGridView3.Columns[0].HeaderText = "Id";
                dataGridView3.Columns[1].HeaderText = "Имя";
                dataGridView3.Columns[2].HeaderText = "Почта";
                dataGridView3.Columns[3].HeaderText = "Телефон";
                dataGridView3.Columns[4].HeaderText = "Дата рождения";
            }

            

        }





        //кнопка показа таблицы студенты
        private void ViewStud_Click(object sender, EventArgs e)
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
            NameSearch.ReadOnly = false;
            EmailSearch.ReadOnly = false;
            PhoneNumberSearch.ReadOnly = false;

        }


        //кнопка удаления записи из dataGridView (из БД не удаляет)
        private void DeleteStud_Click(object sender, EventArgs e)
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

        }



        //кнопка сохранения изменений редактирования
        private void SaveStud_Click(object sender, EventArgs e)
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
        }





        //Фильтр по столбцам в данной таблице. Тестовая версия

        
        //блокировка кнопок при выборе вкладки с редактированием бд
        private void tabControl1_Click(object sender, EventArgs e)
        {
            tabPage1.Select();
            NameSearch.ReadOnly = true;
            EmailSearch.ReadOnly = true;
            PhoneNumberSearch.ReadOnly = true;

        }
        //фильтрация по столбцам
        private void NameSearch_TextChanged(object sender, EventArgs e)
        {
            (dataGridView3.DataSource as DataTable).DefaultView.RowFilter = $"Name LIKE '%{NameSearch.Text}%'";
        }
        private void EmailSearch_TextChanged(object sender, EventArgs e)
        {
            (dataGridView3.DataSource as DataTable).DefaultView.RowFilter = $"Email LIKE '%{EmailSearch.Text}%'";
        }
        private void PhoneNumberSearch_TextChanged(object sender, EventArgs e)
        {
            (dataGridView3.DataSource as DataTable).DefaultView.RowFilter = $"PhoneNumber LIKE '%{PhoneNumberSearch.Text}%'";
        }
        
        //Кнопка очистки полей
        private void CleanStudSearh_Click(object sender, EventArgs e)
        {
            NameSearch.Clear();
            EmailSearch.Clear();
            PhoneNumberSearch.Clear();
        }



    }

}
