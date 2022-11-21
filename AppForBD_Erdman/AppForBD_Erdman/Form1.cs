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

       


    }
    
}
