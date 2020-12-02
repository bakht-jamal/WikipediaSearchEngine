using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WikiSearch.DAL
{
    public class ConnectionLogic
    {
        private readonly string connectionString;
        public ConnectionLogic()
        {
            connectionString = ConfigurationManager.ConnectionStrings["dbx"].ToString();
        }

        public async Task<DataTable> ExecuteQuery(string SpName, Hashtable Param)
        {
            try
            {
                DataTable dtResult = new DataTable("Result");
                using (SqlConnection Con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = Con.CreateCommand())
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            if (Con.State != ConnectionState.Open)
                            {
                                await Con.OpenAsync();
                            }
                            else if (Con.State == ConnectionState.Connecting)
                            {
                                Con.Dispose();
                                await Con.OpenAsync();
                            }

                            cmd.Connection = Con;
                            cmd.CommandText = SpName;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 60;

                            foreach (string para in Param.Keys)
                            {
                                cmd.Parameters.AddWithValue(para, Param[para]);
                            }
                            sda.SelectCommand = (cmd);
                            await Task.Run(() => sda.Fill(dtResult));
                            return dtResult;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}