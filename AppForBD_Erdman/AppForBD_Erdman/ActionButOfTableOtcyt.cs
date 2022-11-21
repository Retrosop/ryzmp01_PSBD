using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppForBD_Erdman
{
    public partial class Form1 : Form
    {
        //перенос выбранной даты в поле
        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            textBox10.Text = dateTimePicker1.Value.ToString("yyyy-MM-dd");
        }

        //добавление новой записи в список
        private void button5_Click(object sender, EventArgs e)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql3, connection);

                ds3 = new DataSet();
            }


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

            

            dataAdapter.Fill(ds3);

            dataGridView3.DataSource = ds3.Tables[0];
            dataGridView3.Columns["Id"].ReadOnly = true;
            dataGridView3.Columns[0].HeaderText = "Id";
            dataGridView3.Columns[1].HeaderText = "Дата лекции";
            dataGridView3.Columns[2].HeaderText = "Название лекции";
            dataGridView3.Columns[3].HeaderText = "Время лекции";
            dataGridView3.Columns[4].HeaderText = "Имя отсутсвующего";

        }



        //Кнопка показа таблиц
        private void ViewOtcyt_Click(object sender, EventArgs e)
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
        }


        //кнопка удаления из списка
        private void DeleteOtcyt_Click(object sender, EventArgs e)
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



        //кнопка сохранения изменений в бд
        private void SaveOtcyt_Click(object sender, EventArgs e)
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
        }



    }
}
