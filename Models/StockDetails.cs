using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace StocksMarket.Models
{
    public class StockDetails
    {
        // DBConnection DbConn = new DBConnection();
        //public string chName { get; set; }
        public int StockId { get; set; }
        public string StockName { get; set; }

        public int Group_Id { get; set; }
        public string Group_Name { get; set; }
        public double Open { get; set; }
        public double LastPrice { get; set; }
        public double Change { get; set; }
        public double PercentChange { get; set; }

        public double Close { get; set; }
        public string CodeN { get; set; }
        public string CodeR { get; set; }
        public string Graphic_Name { get; set; }
        public string Stack_Name { get; set; }
        public string Ticker_Name { get; set; }
        public int InstrumentId { get; set; }
        public int ExchangeId { get; set; }
        public string Active { get; set; }
        public int Ipo { get; set; }
        public bool IsActive { get; set; }

        [DefaultValue(2)]
        public string Decimal_Value { get; set; }

        public List<string> lstInstrumentId = new List<string> {"1", "2", "3","4","5", "6", "7", "8", "9", "10" };

        public List<string> lstExchangeId = new List<string> {"1", "2", "3", "4", "5", "6", "7", "8" };
        public List<string> lstActive = new List<string> {"1", "2", "3", "4", "5", "6", "7", "8" };
        public List<string> lstIpo = new List<string> { "0","1" };
        public string UpdateDateTime { get; set; }
        public List<string> lst_Group = new List<string>();
        public List<StockData> lstStockGrid = new List<StockData>();

        public StockDetails()
        {
            GetStockDetails("");
            PopulateGroup1();
        }


        public void PopulateGroup1()
        {
            lst_Group = new List<string>();
            DataTable dtDept = DBConnection.ReturnDataSet(System.Configuration.ConfigurationManager.ConnectionStrings["SQL_GROUPS"].ToString()).Tables[0];
            if (dtDept.Rows.Count > 0)
            {
                for (int i = 0; i < dtDept.Rows.Count; i++)
                {

                    lst_Group.Add(dtDept.Rows[i]["GROUP_NAME"].ToString());
                }
            }

        }
        public void GetStockDetails(string searchData)
        {
            using (MySqlConnection conn = new MySqlConnection(DBConnection.GetDBConnectionStringM()))
            {
                string strcmd = System.Configuration.ConfigurationManager.ConnectionStrings["StockGrid"].ToString();
                if (searchData != "")
                {
                    //  strcmd = strcmd + " where particulars like '%" + searchData + "%' or vendors_name like '%" + searchData + "%'";
                }
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(strcmd, conn);
                MySqlDataReader drStock = cmd.ExecuteReader();
                lstStockGrid.Clear();
                while (drStock.Read())
                {
                    StockData st = new StockData();
                  
                    st.StockId = Convert.ToInt32(drStock["STOCK_ID"]);
                    st.StockName = drStock["NAME"].ToString();
                    st.CodeR = drStock["CODE_R"].ToString();
                    st.CodeN = drStock["CODE_N"].ToString();
                    st.Open = Convert.ToDouble(String.Format("{0:0.00}", drStock["OPEN"]));
                    st.LastPrice = Convert.ToDouble(String.Format("{0:0.00}", drStock["LAST_PRICE"]));
                    st.Change = Convert.ToDouble(String.Format("{0:0.00}", drStock["CHANGE1"]));
                    st.PercentChange = Convert.ToDecimal(String.Format("{0:0.00}", drStock["PERCENT_CHANGE"]));
                    st.Close = Convert.ToDouble(String.Format("{0:0.00}", drStock["CLOSE"]));
                    st.ExchangeId = Convert.ToInt32(drStock["EXCHANGE_ID"]);
                    st.INSTRUMENT_ID = Convert.ToInt32(drStock["INSTRUMENT_ID"]);
                    st.ACTIVE = Convert.ToInt32(drStock["ACTIVE"]);
                    st.PercentChange = Convert.ToDecimal(String.Format("{0:0.00}", drStock["PERCENT_CHANGE"]));
                    st.Close = Convert.ToDouble(String.Format("{0:0.00}", drStock["CLOSE"]));
                    st.Graphic_Name = drStock["GRAPHIC_NAME"].ToString();
                    st.Ticker_Name = drStock["TICKER_NAME"].ToString();
                    st.Stack_Name = drStock["STACK_NAME"].ToString();
                   st.Ipo = Convert.ToInt32(drStock["IPO"].ToString());
                    lstStockGrid.Add(st);
                }
                drStock.Close();
                conn.Close();
            }
        }



        public string InsertToBothDB(StockDetails stockData)
        {
            try
            {
                string main = DBConnection.GetDBConnectionStringM();
                string backup = DBConnection.GetDBConnectionString();
                string[] values = { main, backup };
                foreach (string db in values)
                {
                    SaveStock(stockData, db);
                }


                return "true";
            }

            catch (Exception ex)
            {
                return "false";
            }

        }
        public string SaveStock(StockDetails stockData,string Database)
        {
            //string cmdInsert = "";
            
            int ipo;
           
            string stock_id_query;
            stock_id_query = "SELECT MAX(STOCK_ID)+1 FROM STOCK_TRANSITION";
            DataTable dt = DBConnection.GetDataTable(stock_id_query, "STOCK_TRANSITION", Database);
            //string stock_id= dt.Rows[0][0].ToString();
            int stock_id = Convert.ToInt32(dt.Rows[0][0].ToString());
            //if (stockData.Ipo == true)
            //{
            //    ipo = 1;
            //}
            //else
            //{
            //    ipo = 0;
            //}
            // (`STOCK_ID`, `EXCHANGE_ID`, `INSTRUMENT_ID`, `CODE_R`, `CODE_N`, `ACTIVE`, `OPEN`, `LAST_PRICE`, `CLOSE`, `TICKER_NAME`, `GRAPHIC_NAME`, `STACK_NAME`, `NAME`, `DECIMAL_VALUE`,`IPO`) VALUES('24441', '1', '5', 'ROUT.NS', 'ROUT.NS', '1', '350', '350', '350', 'Route Mobile Ltd', 'Route Mobile', 'Route Mobile', 'Route Mobile', '2', '1')
            string cmdInsert = "INSERT INTO STOCK_TRANSITION(STOCK_ID,EXCHANGE_ID,INSTRUMENT_ID,CODE_R,CODE_N,";

            cmdInsert = cmdInsert + "ACTIVE,OPEN,LAST_PRICE ,CLOSE,TICKER_NAME,GRAPHIC_NAME,";
            cmdInsert = cmdInsert + "STACK_NAME,NAME,DECIMAL_VALUE,IPO) values (";
            cmdInsert = cmdInsert + "'" + stock_id + "','" + stockData.ExchangeId + "','" + stockData.InstrumentId + "',";
            cmdInsert = cmdInsert + "'" + stockData.CodeR + "','" + stockData.CodeN + "',";
            cmdInsert = cmdInsert + "'" + stockData.Active + "','" + stockData.Open + "','" + stockData.LastPrice + "','" + stockData.Close + "',";
            cmdInsert = cmdInsert + "'" + stockData.Ticker_Name + "','" + stockData.Graphic_Name + "','" + stockData.Stack_Name + "',";
            cmdInsert = cmdInsert + "'" + stockData.StockName  + "','" + stockData.Decimal_Value + "','" + stockData.Ipo + "')";
           
            try
            {
                DBConnection.ExecuteQueryMySqlBothDb(cmdInsert,Database);
                AddToGrp(stock_id, stockData);


                return "true";
            }

            catch (Exception ex)
            {
                return "false";
            }
        }


        public void getStockList(string searchData)
        {
            using (MySqlConnection conn = new MySqlConnection(DBConnection.GetDBConnectionStringM()))
            {
                string strcmd = System.Configuration.ConfigurationManager.ConnectionStrings["StockGrid"].ToString();
                if (searchData != "")
                {
                    strcmd += " and  STOCK_ID = '" + searchData + "' ";

                    //strcmd += " ORDER BY id DESC";
                }
                else
                {
                    strcmd += " ORDER BY id DESC";
                }


                conn.Open();
                MySqlCommand cmdEmp = new MySqlCommand(strcmd, conn);
                MySqlDataReader drEmp = cmdEmp.ExecuteReader();
                lstStockGrid.Clear();

                while (drEmp.Read())
                {
                    StockData stckDetails = new StockData();
                    stckDetails.StockId = Convert.ToInt32(drEmp["STOCK_ID"].ToString());
                    stckDetails.StockName = drEmp["STACK_NAME"].ToString();
                    stckDetails.CodeR = drEmp["CODE_R"].ToString();
                    stckDetails.CodeN = drEmp["CODE_N"].ToString();
                    stckDetails.Open = Convert.ToDouble(drEmp["OPEN"].ToString());
                    stckDetails.LastPrice = Convert.ToDouble(drEmp["LASt_PRICE"].ToString());
                    stckDetails.Close = Convert.ToDouble(drEmp["CLOSE"].ToString());
                    stckDetails.Ticker_Name = drEmp["TICKER_NAME"].ToString();
                    stckDetails.Stack_Name = drEmp["STACK_NAME"].ToString();
                    stckDetails.Graphic_Name = drEmp["GRAPHIC_NAME"].ToString();
                    stckDetails.ExchangeId = Convert.ToInt32(drEmp["EXCHANGE_ID"].ToString());
                    stckDetails.INSTRUMENT_ID = Convert.ToInt32(drEmp["INSTRUMENT_ID"].ToString());
                    stckDetails.ACTIVE = Convert.ToInt32(drEmp["ACTIVE"].ToString());
                    stckDetails.Ipo = Convert.ToInt32(drEmp["IPO"]);
                    lstStockGrid.Add(stckDetails);
                }
                drEmp.Close();
                conn.Close();
            }
        }

        public string UpdateStock(StockDetails stckup)
        {
          
            //int up_ipo;
            //if (stckup.Ipo == true)
            //{
            //    up_ipo = 1;
            //}
            //else
            //{
            //    up_ipo = 0;
            //}
           
            string cmdInsert = "";
            cmdInsert = "Update STOCK_TRANSITION set STACK_NAME='" + stckup.StockName + "',";
            cmdInsert = cmdInsert + "Code_R='" + stckup.CodeR + "',";
            cmdInsert = cmdInsert + "Code_N='" + stckup.CodeN + "',";
            cmdInsert = cmdInsert + "OPEN='" + stckup.Open + "',";
            cmdInsert = cmdInsert + "LAST_PRICE='" + stckup.LastPrice + "',";
            cmdInsert = cmdInsert + "Close='" + stckup.Close + "',";
            cmdInsert = cmdInsert + "TICKER_NAME='" + stckup.Ticker_Name + "',";
            cmdInsert = cmdInsert + "STACK_NAME='" + stckup.Stack_Name + "',";
            cmdInsert = cmdInsert + "GRAPHIC_NAME='" + stckup.Graphic_Name + "',";
            cmdInsert = cmdInsert + "EXCHANGE_ID='" + stckup.ExchangeId + "',";
            cmdInsert = cmdInsert + "INSTRUMENT_ID='" + stckup.InstrumentId + "',";
            cmdInsert = cmdInsert + "ACTIVE='" + stckup.Active + "',";
            cmdInsert = cmdInsert + "IPO='" + stckup.Ipo + "',";
            cmdInsert = cmdInsert + "GRAPHIC_NAME='" + stckup.Graphic_Name + "' where STOCK_ID='" + stckup.StockId + "'";


            try
            {
                DBConnection.ExecuteQueryMySql(cmdInsert);
                return "true";
            }

            catch (Exception ex)
            {
                return "false";
            }
        }

        public string AddToGrp(int Stockid,StockDetails stockData)
        {
            StockGrpDetails stockGrp = new StockGrpDetails();
            int grp_id = stockGrp.getGroupId(stockData.Group_Name);
            string addStocks = "INSERT INTO STOCK_GROUP_DETAILS(GROUP_ID,STOCK_ID) values (";
            addStocks = addStocks + "'" + grp_id + "','" + Stockid + "')";

            try
            {
                DBConnection.ExecuteQueryMySql(addStocks);

                //DBConnection.ExecuteQueryMySql(addStocks);
                return "true";
            }

            catch (Exception ex)
            {
                return "false";
            }
        }

        public class StockData{
            public int StockId { get; set; }

            public int ExchangeId { get; set; }
            public int INSTRUMENT_ID { get; set; }
            public int ACTIVE { get; set; }
            public string StockName { get; set; }
            public double Open { get; set; }
            public double LastPrice { get; set; }
            public double Change { get; set; }
            public decimal PercentChange { get; set; }

            public double Close { get; set; }
            public string CodeN { get; set; }
            public string CodeR { get; set; }
            public string Graphic_Name { get; set; }
            public string Stack_Name { get; set; }
            public string Ticker_Name { get; set; }

            [DefaultValue(2)]
            public string Decimal_Value { get; set; }

            public int Ipo { get; set; }
            public bool IsActive { get; set; }
        }
       // public List<StockDetails> getStockDetails()
        //{
        //    DataTable dt = new DataTable();
        //    DataSet ds = new DataSet();
        //    List<StockDetails> lstStock = new List<StockDetails>();

        //    string stock = "SELECT STOCK_ID,CODE_R,CODE_N,NAME,OPEN,LAST_PRICE,CHANGE1,PERCENT_CHANGE,CLOSE,UPDATE_DATE_TIME,TICKER_NAME,GRAPHIC_NAME,STACK_NAME,IPO FROM STOCK_TRANSITION ORDER BY STOCK_ID DESC";
        //    ds = dbConn.ReturnDataSet(stock);
        //    dt = ds.Tables[0];


        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        StockDetails st = new StockDetails();
        //        st.StockId = Convert.ToInt32(dt.Rows[i]["STOCK_ID"]);
        //        st.StockName = dt.Rows[i]["NAME"].ToString();
        //        st.CodeR = dt.Rows[i]["CODE_R"].ToString();
        //        st.CodeN = dt.Rows[i]["CODE_N"].ToString();
        //        st.Open = Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows[i]["OPEN"]));
        //        st.LastPrice = Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows[i]["LAST_PRICE"]));
        //        st.Change = Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows[i]["CHANGE1"]));
        //        st.PercentChange = Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows[i]["PERCENT_CHANGE"]));
        //        st.Close = Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows[i]["CLOSE"]));
        //        st.Graphic_Name = dt.Rows[i]["GRAPHIC_NAME"].ToString();
        //        st.Ticker_Name = dt.Rows[i]["TICKER_NAME"].ToString();
        //        st.Stack_Name = dt.Rows[i]["STACK_NAME"].ToString();
        //        //st.Ipo = Convert.ToInt32(dt.Rows[i]["IPO"]);

        //        lstStock.Add(st);
        //    }
        //    return lstStock;
        //}
    }
}