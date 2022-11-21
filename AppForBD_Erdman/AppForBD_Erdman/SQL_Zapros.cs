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

namespace AppForBD_Erdman
{
    public partial class Form1 : Form
    {

        //Кнопка запуска SQL команды
        private void SQLZapros_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
                InsertSQLZapros.Text, connectionString);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0];
            
        }

        //Кнопка очистки поля ввода SQL запроса
        private void ClearSQL_Click(object sender, EventArgs e)
        {
            InsertSQLZapros.Clear();
        }

    }
}
