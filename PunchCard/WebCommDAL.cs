using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PunchCard
{
    public class WebCommDAL
    {
        public static int Add(SqlConnection connection, WebCommModel model, PunchCard type)
        {
            string strSql = @"
            INSERT INTO [dbo].[WEBCOMM]
                       ([WBC_SERNO]
                       ,[WBC_DATE]
                       ,[WBC_CLOCKIN]
                       ,[WBC_CLOCKOUT])
                 VALUES
                       (@WBC_SERNO
                        ,@WBC_DATE
                        ,@WBC_CLOCKIN
                        ,@WBC_CLOCKOUT)
            ";
            SqlCommand command = new SqlCommand(strSql, connection);
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            if (string.IsNullOrEmpty(model.WBC_SERNO))
                command.Parameters.AddWithValue("@WBC_SERNO", DBNull.Value);
            else
                command.Parameters.AddWithValue("@WBC_SERNO", model.WBC_SERNO);

            if (string.IsNullOrEmpty(model.WBC_DATE))
                command.Parameters.AddWithValue("@WBC_DATE", DBNull.Value);
            else
                command.Parameters.AddWithValue("@WBC_DATE", model.WBC_DATE);

            if (string.IsNullOrEmpty(model.WBC_CLOCKIN))
                command.Parameters.AddWithValue("@WBC_CLOCKIN", DBNull.Value);
            else
                command.Parameters.AddWithValue("@WBC_CLOCKIN", model.WBC_CLOCKIN);

            if (string.IsNullOrEmpty(model.WBC_CLOCKOUT))
                command.Parameters.AddWithValue("@WBC_CLOCKOUT", DBNull.Value);
            else
                command.Parameters.AddWithValue("@WBC_CLOCKOUT", model.WBC_CLOCKOUT);

            return command.ExecuteNonQuery();

        }

        public static string GetSernoByDate(SqlConnection connection,string date)
        {
            string strSql = @"
            SELECT [WBC_SERNO]
              FROM [NetFusion].[dbo].[WEBCOMM]
              WHERE WBC_DATE = @WBC_DATE
            ";
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();
            SqlCommand cmd = new SqlCommand(strSql, connection);
            cmd.Parameters.AddWithValue("@WBC_DATE", date);
            var a = cmd.ExecuteScalar();
            return (string)(cmd.ExecuteScalar() ?? string.Empty);
        }

        public static int Delete(SqlConnection connection, string date)
        {
            string strSql = @"
                DELETE FROM [dbo].[WEBCOMM]
                WHERE WBC_DATE = @WBC_DATE";
            SqlCommand command = new SqlCommand(strSql, connection);
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            if (string.IsNullOrEmpty(date))
                command.Parameters.AddWithValue("@WBC_DATE", DBNull.Value);
            else
                command.Parameters.AddWithValue("@WBC_DATE", date);

            return command.ExecuteNonQuery();

        }
    }
}
