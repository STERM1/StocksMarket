using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace StocksMarket.Models
{
    public class ClsManager
    {
        public MySqlConnection objConnection = new MySqlConnection();
        public MySqlCommand objCmd = new MySqlCommand();
        public MySqlDataAdapter DataAdapter = new MySqlDataAdapter();

        public ClsManager()
        {
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringMySql"].ToString();
            objConnection.ConnectionString = conStr;
            objCmd.Connection = objConnection;
            DataAdapter.SelectCommand = objCmd;



        }
        public bool NonQuery(string Query)
        {
            objCmd.CommandText = Query;

            try
            {
                if (objConnection.State == ConnectionState.Closed) { objConnection.Open(); }
                int result = objCmd.ExecuteNonQuery();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {

                return false;
            }
            finally
            {
                if (objConnection.State != ConnectionState.Closed) { objConnection.Close(); }


            }
        }

        //public bool oraNonQuery(string Query)
        //{
        //    //oraCmd.CommandText = Query;

        //    //try
        //    //{
        //    //    if (oraConnection.State == ConnectionState.Closed) { oraConnection.Open(); }
        //    //    int result = oraCmd.ExecuteNonQuery();
        //    //    if (result > 0)
        //    //        return true;
        //    //    else
        //    //        return false;
        //    //}
        //    //catch (Exception ex)
        //    //{

        //    //    return false;
        //    //}
        //    //finally
        //    //{
        //    //    if (oraConnection.State != ConnectionState.Closed) { oraConnection.Close(); }


        //    //}
        //}

        public MySqlParameterCollection excProcedure(MySqlCommand Query)
        {
            MySqlParameterCollection col = Query.Parameters;
            try
            {
                if (objConnection.State == ConnectionState.Closed) { objConnection.Open(); }
                Query.Connection = objConnection;
                Query.ExecuteReader();

            }
            catch (Exception ex)
            {

                return null;
            }

            finally
            {
                if (objConnection.State != ConnectionState.Closed) { objConnection.Close(); }


            }
            return col;
        }


        public bool check_data(string Query)
        {
            objCmd.CommandText = Query;
            try
            {
                bool result = false;
                if (objConnection.State == ConnectionState.Closed) { objConnection.Open(); }
                MySqlDataReader dr = objCmd.ExecuteReader();

                if (dr.Read())
                {
                    result = true;
                    dr.Close();
                }
                else { result = false; }


                return result;
            }
            catch (Exception ee)
            {
                return false;
            }
            finally
            {
                if (objConnection.State != ConnectionState.Closed) { objConnection.Close(); }
            }
        }


        public DataSet GetDataSet(string Query, string TableAliasName)
        {
            DataSet dataSet = new DataSet();
            objCmd.CommandText = Query;
            try
            {
                if (objConnection.State == ConnectionState.Closed)
                {
                    objConnection.Open();
                }

                DataAdapter.Fill(dataSet);
                return dataSet;

            }
            catch (Exception ex)
            {
                return dataSet;

            }
            finally
            {
                if (objConnection.State != ConnectionState.Closed) { objConnection.Close(); }
            }
        }
        public DataTable GetDataTable(string Query)
        {
            DataTable dataTable = new DataTable();
            objCmd.CommandText = Query;
            try
            {
                if (objConnection.State == ConnectionState.Closed) { objConnection.Open(); }
                DataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return dataTable;

            }
            finally
            {
                if (objConnection.State != ConnectionState.Closed) { objConnection.Close(); }
            }
        }

        //public DataTable oraGetDataTable(string Query)
        //{
        //    DataTable dataTable = new DataTable();
        //    oraCmd.CommandText = Query;
        //    try
        //    {
        //        if (oraConnection.State == ConnectionState.Closed) { oraConnection.Open(); }
        //        oraAdapter.Fill(dataTable);
        //        return dataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ToString();
        //        return dataTable;

        //    }
        //    finally
        //    {
        //        if (oraConnection.State != ConnectionState.Closed) { oraConnection.Close(); }
        //    }
        //}
        public object GetScalar(string Query)
        {
            objCmd.CommandText = Query;
            try
            {
                if (objConnection.State == ConnectionState.Closed) { objConnection.Open(); }
                object result = objCmd.ExecuteScalar();
                return result;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (objConnection.State != ConnectionState.Closed) { objConnection.Close(); }
            }
        }

        public object GetReader(string Query)
        {
            objCmd.CommandText = Query;
            try
            {
                if (objConnection.State == ConnectionState.Closed) { objConnection.Open(); }
                MySqlDataReader dr = objCmd.ExecuteReader();
                object result = null;
                if (dr.Read())
                {
                    result = dr[0].ToString();
                    dr.Close();
                }
                else { result = null; }


                return result;
            }
            catch (Exception ee)
            {
                return null;
            }
            finally
            {
                if (objConnection.State != ConnectionState.Closed) { objConnection.Close(); }
            }
        }

        public ArrayList GetArrayList(string Query)
        {
            objCmd.CommandText = Query;
            DataTable dataTable = new DataTable();
            ArrayList _valueArray = new ArrayList();
            try
            {
                if (objConnection.State == ConnectionState.Closed) { objConnection.Open(); }

                DataAdapter.Fill(dataTable);
                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            _valueArray.Add(dataTable.Rows[i][0].ToString());
                        }
                    }

                }
                else
                {
                    dataTable = null;
                    _valueArray = null;
                }

                return _valueArray;
            }
            catch
            {
                _valueArray = null;
                return _valueArray;
            }
            finally
            {
                if (objConnection.State != ConnectionState.Closed) { objConnection.Close(); }
            }
        }
    }
}