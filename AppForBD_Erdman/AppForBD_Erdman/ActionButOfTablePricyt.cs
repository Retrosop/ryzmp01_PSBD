using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppForBD_Erdman
{
    public partial class Form1 : Form
    {
        //перенос выбранной даты в поле
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            textBox6.Text = dateTimePicker1.Value.ToString("yyyy-MM-dd");
        }

        //Добавление нвоой записи в таблицу
        private void button4_Click(object sender, EventArgs e)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql2, connection);

                ds2 = new DataSet();
            }



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

            

            dataAdapter.Fill(ds2);

           
            dataGridView3.DataSource = ds2.Tables[0];
            // делаем недоступным столбец id для изменения
            dataGridView3.Columns["Id"].ReadOnly = true;
            dataGridView3.Columns[0].HeaderText = "Id";
            dataGridView3.Columns[1].HeaderText = "Дата лекции";
            dataGridView3.Columns[2].HeaderText = "Название лекции";
            dataGridView3.Columns[3].HeaderText = "Время лекции";
            dataGridView3.Columns[4].HeaderText = "Имя присутсвующего";

            NameSearch.ReadOnly = true;
            EmailSearch.ReadOnly = true;
            PhoneNumberSearch.ReadOnly = true;


        }






        //Кнопка показа таблицы
        private void ViewPricyt_Click(object sender, EventArgs e)
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
        }



        //Кнопка удаление из списка
        private void DeletePricyt_Click(object sender, EventArgs e)
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



        //кнопка сохранения измнения в БД
        private void SavePricyt_Click(object sender, EventArgs e)
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
        }



    }

}
