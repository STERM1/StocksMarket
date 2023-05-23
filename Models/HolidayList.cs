using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StocksMarket.Models
{
    public class HolidayList
    {
        public int H_ID;
        public string H_DATE;
        public string H_DSC;
        public List<Holiday> lstholiday = new List<Holiday>();

        public HolidayList(){
            GetHolidayList();
        }
        public void GetHolidayList()
        {
            using (MySqlConnection conn = new MySqlConnection(DBConnection.GetDBConnectionString()))
            {
                string strcmd = System.Configuration.ConfigurationManager.ConnectionStrings["HolidayTable"].ToString();
               
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(strcmd, conn);
                MySqlDataReader drStock = cmd.ExecuteReader();
                lstholiday.Clear();
                while (drStock.Read())
                {
                    Holiday st = new Holiday();
                    st.H_ID= Convert.ToInt32(drStock["H_ID"]);
                    st.H_DATE = String.Format("{0:dd-MM-yyyy}", drStock["H_DATE"]);
                   // st.H_DATE =  string.Format("{0:dd-MM-yyyy}", drStock["EMP_DOB"];
                    st.H_DSC = drStock["H_DSC"].ToString();
                    // st.Ipo = Convert.ToInt(drStock["IPO"]);
                    lstholiday.Add(st);
                }
                drStock.Close();
                conn.Close();
            }
        }

        public class Holiday {
            public int H_ID;
            public string H_DATE;
            public string H_DSC;
        }

    }
}