using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Workstation_Simulation
{
    public partial class Form1 : Form
    {
        private string ConnectionString;
        public Form1()
        {
            InitializeComponent();
            ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"]?.ConnectionString;

            LoadEmployeeAndWorkstations();
        }

        private void LoadEmployeeAndWorkstations()
        {
            //employeeCombo.Items =
            //workstationCombo.Items = 
        }

        private void RunBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            testRetrievingData();
        }

        private void testRetrievingData()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("GetAvailableEmployee", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;

                var returnParameter = sql_cmnd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                /*               sql_cmnd.Parameters.AddWithValue("@FIRST_NAME", SqlDbType.NVarChar).Value = firstName;
                               sql_cmnd.Parameters.AddWithValue("@LAST_NAME", SqlDbType.NVarChar).Value = lastName;
                               sql_cmnd.Parameters.AddWithValue("@AGE", SqlDbType.Int).Value = age;*/
                sql_cmnd.ExecuteNonQuery();
                var result = returnParameter.Value;
                connection.Close();
            }
        }
    }
}
