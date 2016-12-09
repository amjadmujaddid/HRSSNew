using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HRSS.Utility
{
    public class Library
    {
        #region Initialize Column Excel

        public static string GetColumnExcel(int character)
        {
            int div = character;
            string column = String.Empty;
            int mod;

            while (div > 0)
            {
                mod = (div - 1) % 26;
                column = Convert.ToChar(65 + mod).ToString() + column;
                div = (int)((div - mod) / 26);
            }

            return column;
        }

        public static List<Dictionary<string, string>> GetFieldAndType(Type myClass, bool withAdditionalField = true)
        {           
            var result = new List<Dictionary<string, string>>();
            List<Type> propertyTypeNames = new List<Type>();

            foreach (PropertyInfo propertyInfo in myClass.GetProperties())
            {
                if (withAdditionalField)
                {
                    var tmpData = new Dictionary<string, string>();
                    tmpData.Add("fldName", propertyInfo.Name);
                    tmpData.Add("fldType", propertyInfo.PropertyType.ToString());
                    result.Add(tmpData);
                }
                else
                {
                    if ((propertyInfo.Name != "ValidationResult") && (propertyInfo.Name != "IsValid") && (propertyInfo.Name != "CreateDate") && (propertyInfo.Name != "CreateBy") && (propertyInfo.Name != "EditDate") && (propertyInfo.Name != "EditBy"))
                    {
                        var tmpData = new Dictionary<string, string>();
                        tmpData.Add("fldName", propertyInfo.Name);
                        tmpData.Add("fldType", propertyInfo.PropertyType.ToString());
                        result.Add(tmpData);
                    }
                }

            }
            // write property names

            return result;
        }


        #endregion

        #region DBConnection
        public static string ConDb(string con = "")
        {
            string ret = "";
            switch (con)
            {
                case "0":
                default: ret = ConfigurationManager.ConnectionStrings["HRSSWeb"].ConnectionString; break;
            }
            return ret;
        }

        public static SqlConnection DbConString(string con)
        {
            SqlConnection ret = new SqlConnection(ConDb(con));
            return ret;
        }

        #endregion

        #region Store Procedure Function

        #region SP Get Multi Data Function Repository

        public static DataSet ExecGetMultiDataBySP(string SpName, Dictionary<string, string> spParams, SqlConnection dbConString = null)
        {
            if (dbConString == null)
            {
                dbConString = DbConString("0");
            }

            DataSet dData;
            SqlDataAdapter sAdapter = new SqlDataAdapter();
            SqlCommand sCommand = new SqlCommand();
            DataSet sDataSet = new DataSet();

            try
            {
                dbConString.Close();
                dbConString.Open();

                sCommand = new SqlCommand(SpName, dbConString);
                foreach (var item in spParams)
                {
                    if (item.Value != "")
                    {
                        sCommand.Parameters.AddWithValue("@" + item.Key, item.Value);
                    }
                    else
                    {
                        sCommand.Parameters.AddWithValue("@" + item.Key, DBNull.Value);
                    }
                }

                sCommand.CommandType = CommandType.StoredProcedure;
                sAdapter.SelectCommand = sCommand;
                sAdapter.Fill(sDataSet);
                dData = sDataSet;
            }

            catch (Exception ex)
            {
                dData = null;
            }

            finally
            {
                dbConString.Close();
                dbConString.Dispose();
                sCommand.Dispose();
                sAdapter.Dispose();
                sDataSet = null;
            }

            return dData;
        }

        public static DataTable ExecGetMultiDataBySPtoDatatable(string SpName, Dictionary<string, string> spParams, SqlConnection dbConString = null)
        {
            if (dbConString == null)
            {
                dbConString = DbConString("0");
            }

            DataTable dData;
            SqlDataAdapter sAdapter = new SqlDataAdapter();
            SqlCommand sCommand = new SqlCommand();
            DataTable sDataSet = new DataTable();

            try
            {
                dbConString.Close();
                dbConString.Open();

                sCommand = new SqlCommand(SpName, dbConString);
                foreach (var item in spParams)
                {
                    if (item.Value != "")
                    {
                        sCommand.Parameters.AddWithValue("@" + item.Key, item.Value);
                    }
                    else
                    {
                        sCommand.Parameters.AddWithValue("@" + item.Key, DBNull.Value);
                    }
                }

                sCommand.CommandType = CommandType.StoredProcedure;
                sAdapter.SelectCommand = sCommand;
                sAdapter.Fill(sDataSet);
                dData = sDataSet;
            }

            catch (Exception ex)
            {
                dData = null;
            }

            finally
            {
                dbConString.Close();
                dbConString.Dispose();
                sCommand.Dispose();
                sAdapter.Dispose();
                sDataSet = null;
            }

            return dData;
        }
        public static DataTable QueryToDataTable(string sql, SqlConnection dbConString = null)
        {
            if (dbConString == null)
            {
                dbConString = DbConString("0"); ;
            }
            DataTable data;
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            DataTable dataTable = new DataTable();

            try
            {
                dbConString.Close();
                dbConString.Open();

                command = new SqlCommand(sql, dbConString);
                adapter.SelectCommand = command;
                adapter.Fill(dataTable);
                data = dataTable;
            }

            catch (Exception)
            {
                data = null;
            }

            finally
            {
                dbConString.Close();
                dbConString.Dispose();
                command.Dispose();
                adapter.Dispose();
                dataTable = null;
            }

            return data;
        }
        #endregion

        #region SP IDAL Function Repository

        public static bool ExecSP(string SpName, string spType, Dictionary<string, string> spParams, SqlConnection dbConString = null)
        {
            bool res = true;

            if (dbConString == null)
            {
                dbConString = DbConString("0");
            }

            SqlDataAdapter sAdapter = new SqlDataAdapter();
            SqlCommand sCommand = new SqlCommand();

            try
            {
                sCommand = new SqlCommand(SpName, dbConString);
                foreach (var item in spParams)
                {
                    if (item.Value != "")
                    {
                        sCommand.Parameters.AddWithValue("@" + item.Key, item.Value);
                    }
                    else
                    {
                        sCommand.Parameters.AddWithValue("@" + item.Key, DBNull.Value);
                    }
                }

                sCommand.CommandType = CommandType.StoredProcedure;

                dbConString.Close();
                dbConString.Open();

                switch (spType)
                {
                    case "add":
                        sAdapter.InsertCommand = sCommand;
                        sAdapter.InsertCommand.ExecuteNonQuery();
                        break;
                    case "edit":
                        sAdapter.UpdateCommand = sCommand;
                        sAdapter.UpdateCommand.ExecuteNonQuery();
                        break;
                    case "delete":
                        sAdapter.DeleteCommand = sCommand;
                        sAdapter.DeleteCommand.ExecuteNonQuery();
                        break;
                    default:
                        break;
                }
                res = true;
            }

            catch (Exception ex)
            {
                res = false;
            }

            finally
            {
                dbConString.Close();
                dbConString.Dispose();
                sCommand.Dispose();
                sAdapter.Dispose();
                sCommand.Dispose();
            }

            return res;
        }

        #endregion

        #endregion
    }
}
