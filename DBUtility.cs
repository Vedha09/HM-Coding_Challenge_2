using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM_CodingChallenge.Data
{
    internal class DBUtility
    {
        static readonly string connectionString = @"Server = VEDHA\SQLEXPRESS; Database = HMDB; Integrated Security = True; MultipleActiveResultSets = true";

        public static SqlConnection GetConnection()
        {
            SqlConnection ConnectionObject = new SqlConnection(connectionString);
            try
            {
                ConnectionObject.Open();
                return ConnectionObject;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Opening the Connection: {e.Message}");
                return null;
            }
        }
        
        public static void CloseDbConnection(SqlConnection connectionObject)
        {
            if (connectionObject != null)
            {
                try
                {
                    if (connectionObject.State != ConnectionState.Open)
                    {
                        connectionObject.Close();
                        connectionObject.Dispose();
                        Console.WriteLine("Connection Closed!!");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error closing connection {e.Message}");
                }
            }
            else
            {
                Console.WriteLine("Connection is already null!!");
            }
        }
    }
}