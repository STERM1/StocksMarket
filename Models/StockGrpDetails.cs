using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StocksMarket.Models
{
    public class StockGrpDetails
    {
        // ClsManager mgr;
        // DBConnection dbConn = new DBConnection();
        public int Stock_Id { get; set; }
        public string Stock_Name { get; set; }

        public bool Main { get; set; }
        public bool Backup { get; set; }
        //public string SelectedDB { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public string CodeR { get; set; }
        public List<string> lstGroup = new List<string>();
        public List<StckDetails> lstStockGrpGrid = new List<StckDetails>();
        public List<StckDetails> lstStockTransGrid = new List<StckDetails>();

        public int[] SelectedDB { get; set; }
        public StockGrpDetails()
        {
            // mgr = new ClsManager();
            populateGroup();
            GetStockInGrp("");
            GetStockTransDetails("");



        }
        public void populateGroup()
        {
            lstGroup = new List<string>();
            DataTable dtDept = DBConnection.ReturnDataSet(System.Configuration.ConfigurationManager.ConnectionStrings["SQL_GROUPS"].ToString()).Tables[0];
            if (dtDept.Rows.Count > 0)
            {
                for (int i = 0; i < dtDept.Rows.Count; i++)
                {

                    lstGroup.Add(dtDept.Rows[i]["GROUP_NAME"].ToString());
                }
            }

        }

        public int getGroupId(string grp_name)
        {
            int dept_id = 0;
            //return dept_id;
            DataTable dtgrpId = DBConnection.ReturnDataSet("Select GROUP_ID from STOCK_GROUP_MASTER where GROUP_NAME='" + grp_name + "'").Tables[0];
            if (dtgrpId.Rows.Count > 0)
            {
                for (int i = 0; i < dtgrpId.Rows.Count; i++)
                {
                    dept_id = Convert.ToInt32(dtgrpId.Rows[0][0].ToString());
                }
            }
            return dept_id;
        }
        //public DataTable GroupStocks()
        //{

        //    DataTable dt = new DataTable();
        //    string qry = "SELECT STOCK_ID,`NAME`,CODE_R FROM STOCK_TRANSITION WHERE STOCK_ID IN(SELECT STOCK_ID FROM STOCK_GROUP_DETAILS WHERE GROUP_ID=146) ORDER BY `NAME` ASC";
        //    //dt = dbConn.GetDataTable(qry, DBConnection.GetDBConnectionString());
        //    dt = mgr.GetDataTable(qry);
        //    return dt;
        //}
        public void GetStockInGrp(string group_Name)
        {
            using (MySqlConnection conn = new MySqlConnection(DBConnection.GetDBConnectionStringM()))
            {
                string strcmd = "";
                if (group_Name != "")
                {
                    int grp_id;

                    grp_id = getGroupId(group_Name);

                    strcmd = strcmd + System.Configuration.ConfigurationManager.ConnectionStrings["SQL_GROUPSTOCK"].ToString().Replace("{0}", Convert.ToString(grp_id));
                }
                else
                {//if (groupId != "")

                    //{
                    strcmd = System.Configuration.ConfigurationManager.ConnectionStrings["SQL_GROUPSTOCK"].ToString().Replace("{0}", Convert.ToString(114));
                }
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(strcmd, conn);
                MySqlDataReader drStock = cmd.ExecuteReader();
                lstStockGrpGrid.Clear();
                while (drStock.Read())
                {
                    StckDetails st = new StckDetails();
                    //  vMaster.VendorId = Convert.ToInt32(drVendor["VENDOR_ID"]).ToString();
                    //  vMaster.VendorName = drVendor["VENDORS_NAME"].ToString();
                    st.STOCK_ID = Convert.ToInt32(drStock["STOCK_ID"]);
                    st.STOCK_NAME = drStock["NAME"].ToString();
                    st.CODE_R = drStock["CODE_R"].ToString();
                    lstStockGrpGrid.Add(st);
                }
                drStock.Close();
                conn.Close();
            }
        }


        public void GetStockTransDetails(string searchData)
        {
            using (MySqlConnection conn = new MySqlConnection(DBConnection.GetDBConnectionString()))
            {
                string strcmd = System.Configuration.ConfigurationManager.ConnectionStrings["StockGrid"].ToString();
                if (searchData != "")
                {
                    //  strcmd = strcmd + " where particulars like '%" + searchData + "%' or vendors_name like '%" + searchData + "%'";
                }
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(strcmd, conn);
                MySqlDataReader drStock = cmd.ExecuteReader();
                lstStockTransGrid.Clear();
                while (drStock.Read())
                {
                    StckDetails st = new StckDetails();
                    //  vMaster.VendorId = Convert.ToInt32(drVendor["VENDOR_ID"]).ToString();
                    //  vMaster.VendorName = drVendor["VENDORS_NAME"].ToString();
                    st.STOCK_ID = Convert.ToInt32(drStock["STOCK_ID"]);
                    st.STOCK_NAME = drStock["NAME"].ToString();
                    st.CODE_R = drStock["CODE_R"].ToString();

                    lstStockTransGrid.Add(st);
                }
                drStock.Close();
                conn.Close();
            }
        }

        public string AddToBothDB(int[] IDS, string grpname)
        {
            try
            {
                string main = DBConnection.GetDBConnectionStringM();
                string backup = DBConnection.GetDBConnectionString();
                string[] values = { main,backup};
                foreach (string db in values)
                {
                    AddGrpStocks(IDS, grpname, db);
                }


                return "true";
            }

            catch (Exception ex)
            {
                return "false";
            }

        }

        public string DelToBothDB(int[] IDS, string grpname)
        {
            try
            {
                string main = DBConnection.GetDBConnectionStringM();
                string backup = DBConnection.GetDBConnectionString();
                string[] values = { main, backup };
                foreach (string db in values)
                {
                    DelGrpStocks(IDS, grpname, db);
                }

            
                 return "true";
            }

            catch (Exception ex)
            {
                return "false";
            }

       }
        public string DelGrpStocks(int[] IDS,string grpname,string Database)
        {
            int grp_id = getGroupId(grpname);
            try
            {
                using (MySqlConnection con = new MySqlConnection(Database))
            {
                con.Open();
             
                foreach (int stockid in IDS)
                {
                        string DeleteStocks = "DELETE FROM STOCK_GROUP_DETAILS WHERE STOCK_ID="+ stockid + " and GROUP_ID="+ grp_id;
                        MySqlCommand cmd = new MySqlCommand(DeleteStocks, con);
                        // cmd.Parameters.AddWithValue("@StockId", IDS);
                        DBConnection.ExecuteQueryMySqlBothDb(DeleteStocks, Database);
                    }
             
            }
            
               
                return "true";
            }

            catch (Exception ex)
            {
                return "false";
            }
        }

      

        public string AddGrpStocks(int[] IDS, string grpname,string Database)
        {
            int grp_id = getGroupId(grpname);
            try
            {
                using (MySqlConnection con = new MySqlConnection(Database))
                {
                    con.Open();

                    foreach (int stockid in IDS)
                    {
                        //int Stock_id = Convert.ToInt32(stockid);
                        string addStocks = "INSERT INTO STOCK_GROUP_DETAILS(GROUP_ID,STOCK_ID) values (";
                        addStocks = addStocks + "'" + grp_id + "','" + stockid + "')";
                        MySqlCommand cmd = new MySqlCommand(addStocks, con);
                        // cmd.Parameters.AddWithValue("@StockId", IDS);
                        DBConnection.ExecuteQueryMySqlBothDb(addStocks, Database);
                    }

                }


                return "true";
            }

            catch (Exception ex)
            {
                return "false";
            }
        }

      
        public class StckDetails
        {
            public int STOCK_ID { get; set; }
            public string STOCK_NAME { get; set; }

            public string CODE_R { get; set; }



        }

    }
}