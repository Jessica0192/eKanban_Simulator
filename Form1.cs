/*
* FILE          : Form1.cs
* PROJECT       : SENG3070 - Project Kanban
* PROGRAMMER    : Enes Demirsoz, Jessica Sim, Hoda Akrami
* FIRST VERSION : 2022-11-24
* DESCRIPTION:
*    This file is to allow user to run workstation simulator and selected employee and workstation
*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Workstation_Simulation
{
    public partial class Form1 : Form
    {
        private string ConnectionString;
        private long timeSec, timeMin, timeHours = 0;
        private bool isActive = false;
        private long count = 0;
        private int nextTimeSpan;
        private long previousTimeSpan = 0;
        private Employee employee;
        private WorkStation workstation;

        //config values
        private Dictionary<string, string> configurations;
        

        public Form1()
        {
            InitializeComponent();
            ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"]?.ConnectionString;

            RetrieveEmployeeWorkstationData();
            GetConfigurationValues();
        }

        /*
        * FUNCTION : GetConfigurationValues
        * DESCRIPTION : This method is to get configuration values from configuration table by calling GetDefaultTimeEfficiencyForWorkstation()
        *               procedure from eKanban database
        * PARAMETERS : no parameters
        * RETURNS : void
        */
        private void GetConfigurationValues()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("GetDefaultTimeEfficiencyForWorkstation", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;


                sql_cmnd.Parameters.Add("@TimeScale", SqlDbType.Int, 100);
                sql_cmnd.Parameters["@TimeScale"].Direction = ParameterDirection.Output;
                sql_cmnd.Parameters.Add("@Base", SqlDbType.Int, 100);
                sql_cmnd.Parameters["@Base"].Direction = ParameterDirection.Output;
                sql_cmnd.Parameters.Add("@BaseDifference", SqlDbType.Float, 100);
                sql_cmnd.Parameters["@BaseDifference"].Direction = ParameterDirection.Output;
                sql_cmnd.Parameters.Add("@NewEmployee_Efficiency", SqlDbType.NVarChar, 5);
                sql_cmnd.Parameters["@NewEmployee_Efficiency"].Direction = ParameterDirection.Output;
                sql_cmnd.Parameters.Add("@VeryExperienced_Efficiency", SqlDbType.NVarChar, 5);
                sql_cmnd.Parameters["@VeryExperienced_Efficiency"].Direction = ParameterDirection.Output;
                sql_cmnd.Parameters.Add("@New_Productivity", SqlDbType.NVarChar, 10);
                sql_cmnd.Parameters["@New_Productivity"].Direction = ParameterDirection.Output;
                sql_cmnd.Parameters.Add("@Experienced_Productivity", SqlDbType.NVarChar, 10);
                sql_cmnd.Parameters["@Experienced_Productivity"].Direction = ParameterDirection.Output;
                sql_cmnd.Parameters.Add("@VeryExperienced_Productivity", SqlDbType.NVarChar, 10);
                sql_cmnd.Parameters["@VeryExperienced_Productivity"].Direction = ParameterDirection.Output;

                sql_cmnd.ExecuteNonQuery();

                configurations = new Dictionary<string, string>();
                configurations.Add("TimeScale", sql_cmnd.Parameters["@TimeScale"].Value.ToString());
                configurations.Add("Base", sql_cmnd.Parameters["@Base"].Value.ToString());
                configurations.Add("BaseDifference",sql_cmnd.Parameters["@BaseDifference"].Value.ToString());
                configurations.Add("NewEmployee_Efficiency", sql_cmnd.Parameters["@NewEmployee_Efficiency"].Value.ToString());
                configurations.Add("VeryExperienced_Efficiency",sql_cmnd.Parameters["@VeryExperienced_Efficiency"].Value.ToString());
                configurations.Add("New/Rookie_Productivity", sql_cmnd.Parameters["@New_Productivity"].Value.ToString());
                configurations.Add("Experienced/Normal_Productivity", Convert.ToString(sql_cmnd.Parameters["@Experienced_Productivity"].Value));
                configurations.Add("VeryExperienced/Super_Productivity", Convert.ToString(sql_cmnd.Parameters["@VeryExperienced_Productivity"].Value));

                connection.Close();
            }
        }

        /*
        * FUNCTION : RunBtn_Click
        * DESCRIPTION : This method is being called when Run button is clicked from UI side and it calls 
        *               AssignEmployeeToWorkstation() procedure in eKanban database
        * PARAMETERS : object sender: the control that the event was fired from
        *              EventArgs e: represents the base class for classes that contain event data, 
        *                           and provides a value to use for events that do not include event data.
        * RETURNS : void
        */
        private void RunBtn_Click(object sender, EventArgs e)
        {
            employee = employeeCombo.SelectedItem as Employee;
            workstation = workstationCombo.SelectedItem as WorkStation;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("AssignEmployeeToWorkstation", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;

                sql_cmnd.Parameters.AddWithValue("@WorkstationID", SqlDbType.Int).Value = workstation.ID;
                sql_cmnd.Parameters.AddWithValue("@EmployeeID", SqlDbType.Int).Value = employee.ID;
                sql_cmnd.ExecuteNonQuery();

                connection.Close();
            }

            configurations.TryGetValue("TimeScale", out string timeScale);
            var interval = Convert.ToInt32(timeScale);

            isActive = true;
            timer1.Interval = 1000 / 1000;          //interval
            getTime();
            timer1.Start();
        }


        /*
        * FUNCTION : StopBtn_Click
        * DESCRIPTION : This method is being called when Stop button is clicked from UI side and it calls 
        *               ReleaseEmployeeAndWorkstation() procedure in eKanban database to release employee and workstation from list
        * PARAMETERS : object sender: the control that the event was fired from
        *              EventArgs e: represents the base class for classes that contain event data, 
        *                           and provides a value to use for events that do not include event data.
        * RETURNS : void
        */
        private void StopBtn_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("ReleaseEmployeeAndWorkstation", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;

                sql_cmnd.Parameters.AddWithValue("@WorkstationID", SqlDbType.Int).Value = workstation.ID;
                sql_cmnd.Parameters.AddWithValue("@EmployeeId", SqlDbType.Int).Value = employee.ID;
                sql_cmnd.ExecuteNonQuery();

                connection.Close();
            }

            timer1.Stop();

            employeeCombo.Items.Clear();
            workstationCombo.Items.Clear();
            RetrieveEmployeeWorkstationData();

            timeSec = 0;
            timeMin = 0;
            timeHours = 0;
            timeLabel.Text = timeHours + ":" + timeMin + ":" + timeSec;
        }

        /*
        * FUNCTION : RetrieveEmployeeWorkstationData
        * DESCRIPTION : This method is to retrive available employee and workstations by calling GetAvailableEmployee()
        *               procedure from eKanban database
        * PARAMETERS : void
        * RETURNS : void
        */
        private void RetrieveEmployeeWorkstationData()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = new SqlCommand("GetAvailableEmployee", connection);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;

                        DataSet ds = new DataSet();
                        da.Fill(ds, "result_name");

                        DataTable dt = ds.Tables["result_name"];

                        foreach (DataRow row in dt.Rows)
                        {
                            //manipulate your data
                            var emp = new Employee() { ID = Int32.Parse(row[0].ToString()), Name = row[1].ToString(), Level = row[2].ToString() };
                            employeeCombo.Items.Add(emp);
                        }
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = new SqlCommand("GetAvailableWorkstations", connection);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;

                        DataSet ds = new DataSet();
                        da.Fill(ds, "result_name");

                        DataTable dt = ds.Tables["result_name"];

                        foreach (DataRow row in dt.Rows)
                        {
                            //manipulate your data
                            var ws = new WorkStation() { ID = Int32.Parse(row[0].ToString()), Name = row[1].ToString() };
                            workstationCombo.Items.Add(ws);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
        }

        /*
        * FUNCTION : timer1_Tick
        * DESCRIPTION : This method is being called every second and it checks if the time has passed to create new product
        * PARAMETERS : object sender: the control that the event was fired from
        *              EventArgs e: represents the base class for classes that contain event data, 
        *                           and provides a value to use for events that do not include event data.
        * RETURNS : void
        */
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isActive)
            {
                timeSec++;
                count++;

                if (timeSec >= 60)
                {
                    timeMin++;
                    timeSec = 0;
                }

                if (timeMin >= 60)
                {
                    timeHours++;
                    timeMin = 0;
                }

                if (count - previousTimeSpan >= nextTimeSpan)
                {
                    previousTimeSpan = count;
                    //create product
                    CreateProduct();
                    getTime();
                }
            }
            
            timeLabel.Text = timeHours + ":" + timeMin + ":" + timeSec;
        }

        /*
        * FUNCTION : CreateProduct
        * DESCRIPTION : This method is to create new product(Fog Lamp) by calling CreateNewProduct()
        *               procedure from eKanban database
        * PARAMETERS : void
        * RETURNS : void
        */
        private void CreateProduct()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("CreateNewProduct", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;

                sql_cmnd.Parameters.AddWithValue("@WorkstationID", SqlDbType.Int).Value = workstation.ID;
                sql_cmnd.ExecuteNonQuery();

                connection.Close();
            }
        }

        /*
        * FUNCTION : getTime
        * DESCRIPTION : This method is to set the next time span for creating new product based on the employee's skill level
        * PARAMETERS : void
        * RETURNS : void
        */
        private void getTime()
        {
            configurations.TryGetValue("Base", out string baseNum);
            configurations.TryGetValue("BaseDifference", out string baseDifference);

            float min = Convert.ToInt32(baseNum) - Convert.ToInt32(baseNum) * float.Parse(baseDifference);
            float max = Convert.ToInt32(baseNum) + Convert.ToInt32(baseNum) * float.Parse(baseDifference);

            var random = new Random();
            var tmp = random.Next(Convert.ToInt32(min), Convert.ToInt32(max) + 1);

            switch(employee.Level)
            {
                case "New Employee":
                    configurations.TryGetValue("NewEmployee_Efficiency", out string efficiency);
                    var numPercentage = Convert.ToDouble(efficiency.Substring(1, efficiency.Length - 2));
                    numPercentage = numPercentage * 0.01;
                    tmp = Convert.ToInt32(tmp + (tmp * numPercentage));

                    break;
                case "Very Experienced":
                    configurations.TryGetValue("VeryExperienced_Efficiency", out string veryEfficiency);
                    var num = Convert.ToDouble(veryEfficiency.Substring(1, veryEfficiency.Length - 2));
                    num = num * 0.01;
                    tmp = Convert.ToInt32(tmp - (tmp * num));
                    break;
            }

            nextTimeSpan = tmp;
        }
    }
}
