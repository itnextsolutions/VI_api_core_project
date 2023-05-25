﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;


namespace VastraIndiaDAL
{
    public class SqlHelper
    {
        public static string sqlDataSource = "Data Source=DESKTOP-AQU9GKL;Initial Catalog=VastraIndia ; Integrated Security = True;";

        

        private Hashtable parmCache = Hashtable.Synchronized(new Hashtable());
        public SqlParameter[] cmdParameter;
        private SqlConnection conn;
        // public readonly string CON_STRING = Convert.ToString(ConfigurationManager.ConnectionStrings["ConnectionStrings:Database"]);

        public static string GetConnectionString()
        {
            return sqlDataSource;

            //  return Convert.ToString(ConfigurationManager.ConnectionStrings["ConnectionStrings:Database"]);

        }
        public SqlConnection GetConnection()
        {
            try
            {
                if (conn == null)
                {
                    conn = new SqlConnection();
                    conn.ConnectionString = sqlDataSource;
                    // conn.ConnectionString = DLLStringEncrypt.DecryptString(CON_STRING, "ART");
                    conn.Open();
                    return conn;
                }
                else
                {
                    if (conn.State == 0)
                    {
                        conn.Open();
                        return conn;
                    }
                    else
                    {
                        return conn;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == 0)
                {
                    conn.Open();
                }
                else
                {
                    conn.Close();
                }
            }

        }

        public void closeconnection()
        {
            try
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int ExecuteNonQuery(string connString, CommandType cmdType, string cmdText)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, null);
                    int val = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return val;
                }
            }
            catch
            {
                throw;
            }
        }


        private void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                cmd.Connection = conn;
                cmd.CommandText = cmdText;

                if (trans != null)
                    cmd.Transaction = trans;

                cmd.CommandType = cmdType;

                if (cmdParms != null)
                {
                    foreach (SqlParameter parm in cmdParms)
                        cmd.Parameters.Add(parm);
                }
            }
            catch
            {
                throw;
            }

        }

        #region Execution of DataTable
        public DataTable ExecuteDataTable(SqlConnection conn, CommandType cmdType, string cmdText)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da;
            //SqlDataReader dr;

            DataTable dt;
            try
            {

                //PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                dt = new DataTable();
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.CommandType = cmdType;
                //create the DataAdapter & DataSet
                da = new SqlDataAdapter(cmd);
                //fill the DataSet using default values for DataTable names, etc.
                try
                {
                    // dr = cmd.ExecuteReader();
                    //dt.Load(dr);
                    da.Fill(dt);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
                cmd.Parameters.Clear();

                //return the dataTable
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        public DataTable ExecuteDataTable(SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da;

            DataTable dt;

            try
            {
                //PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                dt = new DataTable();
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.CommandType = cmdType;


                if (cmdParms != null)
                {
                    foreach (SqlParameter parm in cmdParms)
                        cmd.Parameters.Add(parm);
                }

                //create the DataAdapter & DataTable
                da = new SqlDataAdapter(cmd);

                //fill the DataTable using default values for DataTable names, etc.
                try
                {
                    da.Fill(dt);
                }

                catch (Exception ex)
                {
                    throw;
                }
                cmd.Parameters.Clear();

                //return the dataTable
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        #endregion

        #region Execution of DataSet
        public DataSet ExecuteDataset(SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da;
            //cmd.CommandTimeout = 600;
            DataSet ds;

            try
            {

                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                ds = new DataSet();
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.CommandType = cmdType;


                //create the DataAdapter & DataSet
                da = new SqlDataAdapter(cmd);
                //fill the DataSet using default values for DataTable names, etc.
                da.Fill(ds);

                cmd.Parameters.Clear();

                //return the dataset
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }
        #endregion

        #region ExecuteNonQuery
        public int ExecuteNonQuery(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                cmdParameter = cmdParms;
                cmd.Parameters.Clear();
                return val;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        //static string ConvertToObjectToXML()
        // serialize
        public string ListStrAryToXML(int[] lToSerialize, string parentnode, string childnode, string nodename)
        {
            string xmlstring = null;
            StringBuilder objstringBuilder = new StringBuilder();
            objstringBuilder.Append("<" + parentnode + ">");
            foreach (var a in lToSerialize)
            {
                objstringBuilder.Append("<" + childnode + ">");

                objstringBuilder.Append("<" + nodename + ">" + a.ToString());


                objstringBuilder.Append("</" + nodename + ">");

                objstringBuilder.Append("</" + childnode + ">");
            }
            objstringBuilder.Append("</" + parentnode + ">");
            xmlstring = objstringBuilder.ToString();

            return xmlstring;
            //using (var strWritr = new StringWriter(new StringBuilder()))
            //{
            //    var serializer = new System.Xml.Serialization.XmlSerializer(typeof(int[]));
            //    serializer.Serialize(strWritr, lToSerialize);
            //    return strWritr.ToString();
            //}
        }

        public string ListStrAryToXMLImage(object[] imageNameWithId, string parentnode, string childnode)
        {


            string[] propertyNames = { "Id", "MenName", "WomenName" };

            StringBuilder xmlBuilder = new StringBuilder();

            xmlBuilder.AppendFormat("<{0}>", parentnode);
            foreach (var item in imageNameWithId)
            {
                xmlBuilder.AppendFormat("<{0}>", childnode);
                foreach (string propertyName in propertyNames)
                {
                    var property = item.GetType().GetProperty(propertyName);
                    var value = property.GetValue(item, null);
                    xmlBuilder.AppendFormat("<{0}>{1}</{0}>", propertyName, value);
                }
                xmlBuilder.AppendFormat("</{0}>", childnode);
            }
            xmlBuilder.AppendFormat("</{0}>", parentnode);
            string xml = xmlBuilder.ToString();
            return xml;
        }
        public string ListStrAryToXMLMen(IEnumerable<(string Id, string menFileName)> menarray, string v1, string v2)
        {
            string xmlstring = null;

            StringBuilder objstringBuilder = new StringBuilder();
            objstringBuilder.AppendFormat("<{0}>", v1);
            foreach ((string id, string menFileName) in menarray)
            {
                //xmlBuilder.AppendLine("  <v2>");
                objstringBuilder.Append("<" + v2 + ">");
                objstringBuilder.Append("<Id>"+id+"</Id>");
                objstringBuilder.Append("<Name>" +menFileName+ "</Name>");
                objstringBuilder.Append("</" + v2 + ">");
            }
            objstringBuilder.AppendFormat("</{0}>", v1);
            string xml = objstringBuilder.ToString();
            return xml;
        }

        public string ListStrAryToXMLWomen(IEnumerable<(string Id, string womenFileName)> womenarray, string v1, string v2)
        {
            string xmlstring = null;

            StringBuilder objstringBuilder = new StringBuilder();
            objstringBuilder.AppendFormat("<{0}>", v1);
            foreach ((string id, string womenFileName) in womenarray)
            {
                //xmlBuilder.AppendLine("  <v2>");
                objstringBuilder.Append("<" + v2 + ">");
                objstringBuilder.Append("<Id>" + id + "</Id>");
                objstringBuilder.Append("<Name>" + womenFileName + "</Name>");
                objstringBuilder.Append("</" + v2 + ">");
            }
            objstringBuilder.AppendFormat("</{0}>", v1);
            string xml = objstringBuilder.ToString();
            return xml;
            //string xmlstring = null;

            //StringBuilder objstringBuilder = new StringBuilder();
            //objstringBuilder.AppendFormat("<{0}>", v1);
            //foreach ((string id, string womenFileName) in womenarray)
            //{
            //    //xmlBuilder.AppendLine("  <v2>");
            //    objstringBuilder.Append("<" + v2 + ">");
            //    objstringBuilder.AppendLine($"    <Id>{id}</Id>");
            //    objstringBuilder.AppendLine($"    <Name>{womenFileName}</Name>");
            //    objstringBuilder.Append("</" + v2 + ">");
            //}
            //objstringBuilder.AppendFormat("<{0}>", v1);
            //string xml = objstringBuilder.ToString();
            //return xml;
        }


    }

    
}