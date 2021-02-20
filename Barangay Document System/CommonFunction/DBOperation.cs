using Barangay_Document_System.LogManager;
using Npgsql;
using System;
using System.Data;

namespace Barangay_Document_System
{
   public class DBOperation
   {
      public static string DBUserId { get; set; }
      public static string DBPass { get; set; }
      public static string DBConnectionString { get; set; }

      private static NpgsqlConnection dbConn;
      private static readonly string connString;
      private static SystemLogger logger = new SystemLogger();
      /// <summary>
      /// Connect to postgresDB
      /// </summary>
      /// <param name="user"></param>
      /// <param name="password"></param>
      /// <param name="connString"></param>
      /// <returns></returns>
      public static NpgsqlConnection Connect(string user, string password, string connString)
      {
         Disconnect();
         DBUserId = user;
         DBPass = password;
         DBConnectionString = connString;

         if (dbConn != null && dbConn.State == System.Data.ConnectionState.Open)
         {
            return dbConn;
         }

         string conStr = $"User Id = {user}; Password = {password}; {connString}";
         try
         {
            dbConn = new NpgsqlConnection(conStr);
            dbConn.Open();
            connString = conStr;
         }
         catch (System.Exception e)
         {
            logger.LogError(e.ToString());
            throw;
         }
         return dbConn;
      }

      private static void Disconnect()
      {
         if (dbConn != null)
         {
            dbConn.Close();
            dbConn = null;
         }
      }

      /// <summary>
      /// This method allows execution of an atomic statement into a DataTable (not thread safe)
      /// </summary>
      /// <param name="query"></param>
      /// <returns></returns>
      public static DataTable ExecuteToDataTable(string query)
      {
         try
         {
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, dbConn))
            {
               using (NpgsqlDataAdapter qAdapter = new NpgsqlDataAdapter(cmd))
               {
                  DataTable qResult = new DataTable();
                  qAdapter.Fill(qResult);
                  if(qResult != null && qResult.Rows.Count > 0)
                  {
                     return qResult;
                  }
                  return null;
               }
            }
         }
         catch (System.Exception e)
         {
            logger.LogError(e.ToString());
            return null;
         }
      }

      public static int ExecuteNonQuery(string stmt)
      {
         using (NpgsqlCommand cmd = new NpgsqlCommand(stmt, dbConn))
         {
            try
            {
               return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
               logger.LogError(e.ToString());
               return -1;
            }
         }
      }
   }
}
