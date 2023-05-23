using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace StocksMarket
{
    public class DBConnection
    {
        public static MySqlConnection mySQLConn;
        public static OleDbConnection myCon;

        public static void ExecuteQueryMySql(string Query)
        {
            MySqlConnection mySQLConn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnStringMain"].ConnectionString);
            try
            {

                if (mySQLConn.State != ConnectionState.Open)
                {
                    mySQLConn.Open();
                }
                MySqlCommand mySQLcmd = new MySqlCommand(Query, mySQLConn);
                mySQLcmd.CommandText = Query;
                mySQLcmd.ExecuteNonQuery();
                mySQLcmd.Dispose();

            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                throw new ApplicationException(ex.Message);
            }
            finally
            {
                if (mySQLConn.State == ConnectionState.Open)
                {

                    mySQLConn.Close();
                }
            }
        }

        public static void ExecuteQueryMySqlBothDb(string Query,String Conn)
        {
            MySqlConnection mySQLConn = new MySqlConnection(Conn);
            try
            {

                if (mySQLConn.State != ConnectionState.Open)
                {
                    mySQLConn.Open();
                }
                MySqlCommand mySQLcmd = new MySqlCommand(Query, mySQLConn);
                mySQLcmd.CommandText = Query;
                mySQLcmd.ExecuteNonQuery();
                mySQLcmd.Dispose();

            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                throw new ApplicationException(ex.Message);
            }
            finally
            {
                if (mySQLConn.State == ConnectionState.Open)
                {

                    mySQLConn.Close();
                }
            }
        }

        public static DataSet ReturnDataSet(string Query)
        {
            try
            {

                mySQLConn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnStringMain"].ConnectionString);
                mySQLConn.Open();
                MySqlCommand cmd = new MySqlCommand(Query, mySQLConn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                cmd.Dispose();
                adp.Dispose();
                return ds;

            }
            catch (Exception ex)
            {
                string xyz = ex.Message;
                return null;
                //throw (ex.Message);
            }
            finally
            {
                mySQLConn.Close();
            }
        }
        public static DataTable ReturnSQLData(string sql)
        {

            DataSet ds = ReturnDataSet(sql);
            return ds.Tables[0];
        }

        public static bool SuperUserSapIds(string SapId)
        {
            //return false;
            string[] SuperUserArr = System.Configuration.ConfigurationManager.ConnectionStrings["SuperUser"].ConnectionString.ToString().Split(',');
            string Value = "No";
            for (int i = 0; i < SuperUserArr.Length; i++)
            {
                if (SapId == SuperUserArr[i].ToString())
                    Value = "Yes";
            }
            if (Value == "Yes")
                return true;
            else
                return false;

        }


        public static string SendMail(string sessionid, string username, string sessionname)
        {
            string statusMain = "";
            string statusBackup = "";
            //string email = "ttnapp@timesgroup.com";
            string email = "etnowtest@gmail.com";
            //DataTable dtemail = DbConn.ReturnSQLData((System.Configuration.ConfigurationManager.ConnectionStrings["SQLEmail"].ConnectionString).Replace("[]", "'" + username.ToLower() + "'"));
            //if (dtemail.Rows.Count > 0 )
            //{
            //    email = dtemail.Rows[0][0].ToString();
            //}
            SmtpClient smtpClient = new SmtpClient();
            NetworkCredential basicCredential = new NetworkCredential(email, "times123");
            //new NetworkCredential(email, "ttnapp@123");
            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress(email, username);

            //smtpClient.Host = "bulksmtp.timesgroup.com";
            //smtpClient.Host = "smtp.timesgroup.com";
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = basicCredential;
            smtpClient.EnableSsl = true;
            smtpClient.Port = 587;

            message.From = fromAddress;
            message.Subject = "CHECKLIST REPORT for " + sessionname.ToUpper();
            message.IsBodyHtml = true;
            String html = "<table width=100% border=1 cellspacing=1 cellpadding=1>";
            DataTable dtJobsDone = DBConnection.ReturnSQLData((System.Configuration.ConfigurationManager.ConnectionStrings["SQLMail"].ConnectionString).Replace("[]", "'" + sessionid + "'"));


            html += ("<tr bgcolor=#80B668>");
            html += ("<th align=center colspan=2 color=#FFF>Session</th>");
            html += ("<th align=center  colspan=2 colspan=2 color=#FFF>Shift Developer</th>");
            html += ("</tr>");
            html += ("<tr>");
            html += ("<td align=center  colspan=2 colspan=2 color=#FFF>" + "Daily Check list for " + sessionname + "</td>");
            html += ("<td align=center  colspan=2 colspan=2 color=#FFF>" + username + "</td>");
            html += ("</tr>");




            html += ("<tr bgcolor=#CCCCCC>");
            html += ("<th align=center>Job Description</th>");
            html += ("<th align=center>Main System Activity</th>");
            html += ("<th align=center>Backup System Activity</th>");
            html += ("<th align=center>Remarks</th>");
            html += ("</tr>");

            foreach (DataRow rowJobsDone in dtJobsDone.Rows)
            {
                html += ("<tr>");
                html += ("<td align=center>" + rowJobsDone["JobDescription"].ToString() + "</td>");

                if (rowJobsDone["BackupSystem"].ToString() == "1")
                    statusBackup = "JOBS DONE";
                else
                    statusBackup = "JOBS NOT DONE";

                if (rowJobsDone["MainSystem"].ToString() == "1")
                    statusMain = "JOBS DONE";
                else
                    statusMain = "JOBS NOT DONE";

                html += ("<td align=center>" + statusMain + "</td>");
                html += ("<td align=center>" + statusBackup + "</td>");
                html += ("<td align=center>" + rowJobsDone["Remarks"].ToString() + "</td>");
                html += ("</tr>");
            }

            html += ("</tr>");
            html += ("</table>");
            message.Body = message.Body + html + "<br><br>  Regards <br> " + username;


            // message.To.Add("mohit.bhan2006@gmail.com");
            message.To.Add("dev@etnow.tv");
            try
            {
                smtpClient.Send(message);


            }
            catch (Exception ex)
            {
                //Error, could not send the message
                //Response.Write(ex.Message);
            }

            return "";
        }

        public static int ExecuteQuery(string SQuery, string strConnection = "ConnStringMain", MySqlConnection DBConn = null, MySqlTransaction DBTrans = null)
        {
            MySqlConnection cn = new MySqlConnection();
            int intRecs = 0;

            if (DBConn == null)
            {
                cn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[strConnection].ConnectionString);
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = SQuery;
                cmd.Connection = cn;
                cn.Open();
                cmd.CommandTimeout = 0;
                intRecs = cmd.ExecuteNonQuery();
                cn.Close();

            }
            else
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = SQuery;
                cmd.Connection = DBConn;
                cmd.Transaction = DBTrans;
                cmd.CommandTimeout = 0;
                intRecs = cmd.ExecuteNonQuery();

            }
            return intRecs;

        }

        public static MySqlConnection GetDBConnect(string strConnection = "ConnectionStringMySql")
        {
            MySqlConnection cn = new MySqlConnection(GetDBConnectionString(strConnection));
            return cn;
        }

        public static string GetDBConnectionString(string strConnection = "ConnStringBackup")
        {
            string strConn = System.Configuration.ConfigurationManager.ConnectionStrings[strConnection].ConnectionString;
            return strConn;
        }

        public static string GetDBConnectionStringM(string strConnection2 = "ConnStringMain")
        {
            string strConn = System.Configuration.ConfigurationManager.ConnectionStrings[strConnection2].ConnectionString;
            return strConn;
        }

        public static DataTable GetDataTable(string sQuery, string TableName, string strDBConnection, MySqlConnection DBConn = null, MySqlTransaction DBTrans = null)
        {
            DataSet ds = new DataSet();
            if ((DBConn != null))
            {
                MySqlDataAdapter da = new MySqlDataAdapter(sQuery, DBConn);
                da.SelectCommand.CommandTimeout = 0;
                if ((DBTrans != null))
                    da.SelectCommand.Transaction = DBTrans;

                da.Fill(ds, TableName);

            }
            else
            {
                MySqlDataAdapter da = new MySqlDataAdapter(sQuery, strDBConnection);
                da.SelectCommand.CommandTimeout = 0;
                da.Fill(ds, TableName);
            }
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;

            }
        }

    }
}
