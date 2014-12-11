using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data.SqlTypes;

namespace MindGame.Utils
{
    /// <summary>
    /// Provides helper routines for working with SQL server
    /// </summary>
    public sealed class SqlDataHelper
    {
        #region Fields

        /// <summary>
        /// The number of seconds before the command is timed out.
        /// </summary>
        private const int DEFAULT_COMMAND_TIMEOUT = 30;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Private constructor
        /// </summary>
        private SqlDataHelper()
        {
        }

        #endregion Constructors

        #region SqlConnection methods
        /// <summary>
        /// Creates a Sql Server connection string.
        /// </summary>
        /// <param name="server">The name or IP address of the SQL Server to connect to.</param>
        /// <param name="userId">The login name used to connect to the SQL server.</param>
        /// <param name="pwd">The password used to connect to the SQL server.</param>
        /// <param name="database">The name of the database to connect to.</param>
        /// <param name="options">Additional connection string options in the form of arg=value.</param>
        /// <returns>Returns a Sql Server connection string</returns>
        public static string BuildSqlConnectionString(string server, string userId, string pwd, string database, params string[] options)
        {
            StringBuilder conStr;

            conStr = new StringBuilder();
            conStr.Append("Server=");
            conStr.Append(server.Trim());
            conStr.Append(";User Id=");
            conStr.Append(userId.Trim());
            conStr.Append(";Pwd=");
            conStr.Append(pwd);
            conStr.Append(";Database=");
            conStr.Append(database.Trim());
            conStr.Append(";");

            if (options != null)
            {

                for (int i = 0; i < options.Length; i++)
                {
                    conStr.Append(options[i]);
                    conStr.Append(";");
                }

            }

            return conStr.ToString();
        }

        /// <summary>
        /// Creates a connection to a SQL Server.
        /// </summary>
        /// <param name="connectionString">The string used to connect to a SQL Server.</param>
        /// <returns>Returns an instance of System.Data.SqlClient.SqlConnection.</returns>
        public static SqlConnection GetConnection(string connectionString)
        {
            SqlConnection con;
            SqlTransaction trans;

            con = new SqlConnection(connectionString);

            //set the isolation level to read uncommitted
            try
            {
                con.Open();
                trans = con.BeginTransaction(IsolationLevel.ReadUncommitted);
                trans.Commit();
                trans.Dispose();
                con.Close();
            }
            catch
            {

                if (con != null && con.State != ConnectionState.Closed)
                {
                    con.Close();
                }

            }

            return con;
        }

        #endregion

        #region DataSet Methods
        /// <summary>
        /// Gets a dataset from a SQL server.
        /// </summary>
        /// <param name="connectionString">The string used to connect to a SQL Server.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="tableName">The name of the table in the dataset.</param>
        /// <returns>Returns a data set with the results of the SQL statement.</returns>
        public static DataSet GetDataSet(string connectionString, string sql, string tableName)
        {
            return GetDataSet(connectionString, sql, CommandType.Text, tableName, null);
        }

        /// <summary>
        /// Gets a dataset from a SQL server.
        /// </summary>
        /// <param name="conToDb">An instance of SqlConnection that is configured for the data source.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="tableName">The name of the table in the dataset.</param>
        /// <returns>Returns a data set with the results of the SQL statement.</returns>
        public static DataSet GetDataSet(SqlConnection conToDb, string sql, string tableName)
        {
            return GetDataSet(conToDb, sql, CommandType.Text, tableName, null);
        }

        /// <summary>
        /// Gets a dataset from a SQL server.
        /// </summary>
        /// <param name="connectionString">The string used to connect to a SQL Server.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandType">The type of SQL statement to execute.</param>
        /// <param name="tableName">The name of the table in the dataset.</param>
        /// <param name="parameters">A collection of SqlParameters objects that will be added to the underlyng SqlCommand object.</param>
        /// <returns>Returns a data set with the results of the SQL statement.</returns>
        public static DataSet GetDataSet(string connectionString, string sql, CommandType commandType, string tableName, params SqlParameter[] parameters)
        {
            return GetDataSet(connectionString, sql, commandType, DEFAULT_COMMAND_TIMEOUT, tableName, parameters);
        }

        /// <summary>
        /// Gets a dataset from a SQL server.
        /// </summary>
        /// <param name="connectionString">The string used to connect to a SQL Server.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandType">The type of SQL statement to execute.</param>
        /// <param name="commandTimeout">The number of seconds allowed before the SQL statement is timed out.</param>
        /// <param name="tableName">The name of the table in the dataset.</param>
        /// <param name="parameters">A collection of SqlParameters objects that will be added to the underlyng SqlCommand object.</param>
        /// <returns>Returns a data set with the results of the SQL statement.</returns>
        public static DataSet GetDataSet(string connectionString, string sql, CommandType commandType, int commandTimeout, string tableName, params SqlParameter[] parameters)
        {
            SqlConnection conToDb;
            DataSet data;

            conToDb = GetConnection(connectionString);

            try
            {
                data = GetDataSet(conToDb, sql, commandType, commandTimeout, tableName, parameters);
            }
            finally
            {

                if (conToDb != null)
                {
                    conToDb.Dispose();

                }

            }

            return data;
        }
        /// <summary>
        /// Gets a dataset from a SQL server.
        /// </summary>
        /// <param name="conToDb">An instance of SqlConnection that is configured for the data source.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandType">The type of SQL statement to execute.</param>
        /// <param name="tableName">The name of the table in the dataset.</param>
        /// <param name="parameters">A collection of SqlParameters objects that will be added to the underlyng SqlCommand object.</param>
        /// <returns>Returns a data set with the results of the SQL statement.</returns>
        public static DataSet GetDataSet(SqlConnection conToDb, string sql, CommandType commandType, string tableName, params SqlParameter[] parameters)
        {
            return GetDataSet(conToDb, sql, commandType, DEFAULT_COMMAND_TIMEOUT, tableName, parameters);
        }

        /// <summary>
        /// Gets a dataset from a SQL server.
        /// </summary>
        /// <param name="conToDb">An instance of SqlConnection that is configured for the data source.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandType">The type of SQL statement to execute.</param>
        /// <param name="commandTimeout">The number of seconds allowed before the SQL statement is timed out.</param>
        /// <param name="tableName">The name of the table in the dataset.</param>
        /// <param name="parameters">A collection of SqlParameters objects that will be added to the underlyng SqlCommand object.</param>
        /// <returns>Returns a data set with the results of the SQL statement.</returns>
        public static DataSet GetDataSet(SqlConnection conToDb, string sql, CommandType commandType, int commandTimeout, string tableName, params SqlParameter[] parameters)
        {
            SqlDataAdapter adapter;
            SqlCommand cmd;
            DataSet data;

            cmd = conToDb.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = commandType;
            cmd.Connection = conToDb;
            cmd.CommandTimeout = commandTimeout;

            if (parameters != null)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.Add(parameters[i]);
                }

            }

            adapter = new SqlDataAdapter(cmd);
            data = new DataSet();

            try
            {
                adapter.Fill(data, tableName);
            }
            catch (Exception p)
            {
                Console.WriteLine(p.ToString());
            }
            finally
            {
                cmd.Dispose();
                adapter.Dispose();
            }

            return data;
        }

        #endregion

        #region SqlDataReader Methods
        /// <summary>
        /// Gets a SqlDataReader object.
        /// </summary>
        /// <param name="connectionString">The string used to connect to a SQL Server.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <returns>Returns a SqlDataReader with the results of the SQL statement.</returns>
        public static SqlDataReader GetDataReader(string connectionString, string sql)
        {
            return GetDataReader(connectionString, sql, CommandType.Text, null);
        }

        /// <summary>
        /// Gets a SqlDataReader object.
        /// </summary>
        /// <param name="connectionString">The string used to connect to a SQL Server.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandTimeout">The number of seconds allowed before the SQL statement is timed out.</param>
        /// <returns>Returns a SqlDataReader with the results of the SQL statement.</returns>
        public static SqlDataReader GetDataReader(string connectionString, string sql, int commandTimeout)
        {
            return GetDataReader(connectionString, sql, CommandType.Text, commandTimeout, null);
        }

        /// <summary>
        /// Gets a SqlDataReader object.
        /// </summary>
        /// <param name="conToDb">An instance of SqlConnection that is configured for the data source.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <returns>Returns a SqlDataReader with the results of the SQL statement.</returns>
        public static SqlDataReader GetDataReader(SqlConnection conToDb, string sql)
        {
            return GetDataReader(conToDb, sql, CommandType.Text, null);
        }

        /// <summary>
        /// Gets a SqlDataReader object.
        /// </summary>
        /// <param name="conToDb">An instance of SqlConnection that is configured for the data source.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandTimeout">The number of seconds allowed before the SQL statement is timed out.</param>
        /// <returns>Returns a SqlDataReader with the results of the SQL statement.</returns>
        public static SqlDataReader GetDataReader(SqlConnection conToDb, string sql, int commandTimeout)
        {
            return GetDataReader(conToDb, sql, CommandType.Text, commandTimeout, null);
        }

        /// <summary>
        /// Gets a SqlDataReader object.
        /// </summary>
        /// <param name="connectionString">The string used to connect to a SQL Server.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandType">The type of SQL statement to execute.</param>
        /// <param name="parameters">A collection of SqlParameters objects that will be added to the underlyng SqlCommand object.</param>
        /// <returns>Returns a SqlDataReader with the results of the SQL statement.</returns>
        public static SqlDataReader GetDataReader(string connectionString, string sql, CommandType commandType, params SqlParameter[] parameters)
        {
            return GetDataReader(connectionString, sql, commandType, DEFAULT_COMMAND_TIMEOUT, parameters);
        }

        /// <summary>
        /// Gets a SqlDataReader object.
        /// </summary>
        /// <param name="connectionString">The string used to connect to a SQL Server.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandType">The type of SQL statement to execute.</param>
        /// <param name="commandTimeout">The number of seconds allowed before the SQL statement is timed out.</param>
        /// <param name="parameters">A collection of SqlParameters objects that will be added to the underlyng SqlCommand object.</param>
        /// <returns>Returns a SqlDataReader with the results of the SQL statement.</returns>
        public static SqlDataReader GetDataReader(string connectionString, string sql, CommandType commandType, int commandTimeout, params SqlParameter[] parameters)
        {
            return GetDataReader(connectionString, sql, commandType, commandTimeout, false, parameters);
        }

        /// <summary>
        /// Gets a SqlDataReader object.
        /// </summary>
        /// <param name="connectionString">The string used to connect to a SQL Server.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandType">The type of SQL statement to execute.</param>
        /// <param name="commandTimeout">The number of seconds allowed before the SQL statement is timed out.</param>
        /// <param name="parameters">A collection of SqlParameters objects that will be added to the underlyng SqlCommand object.</param>
        /// <returns>Returns a SqlDataReader with the results of the SQL statement.</returns>
        public static SqlDataReader GetDataReader(string connectionString, string sql, CommandType commandType, int commandTimeout, bool retry, params SqlParameter[] parameters)
        {
            SqlConnection conToDb;
            SqlDataReader reader = null;

            conToDb = null;

            try
            {
                conToDb = GetConnection(connectionString);
                conToDb.Open();
                reader = GetDataReader(conToDb, sql, commandType, commandTimeout, parameters);
            }
            catch (Exception Ex)
            {
                // Clear connection if needed
                ClearConnectionPool(Ex, conToDb);

                if (conToDb != null)
                {
                    conToDb.Dispose();
                }

                if (!retry && Ex.Message.ToLower().IndexOf("transaction") > -1)
                {
                    reader = GetDataReader(connectionString, sql, commandType, commandTimeout, true, parameters);
                }
                else
                {
                    throw Ex;
                }
            }

            return reader;

        }

        /// <summary>
        /// Gets a SqlDataReader object.
        /// </summary>
        /// <param name="conToDb">An instance of SqlConnection that is configured for the data source.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandType">The type of SQL statement to execute.</param>
        /// <param name="parameters">A collection of SqlParameters objects that will be added to the underlyng SqlCommand object.</param>
        /// <returns>Returns a SqlDataReader with the results of the SQL statement.</returns>
        public static SqlDataReader GetDataReader(SqlConnection conToDb, string sql, CommandType commandType, params SqlParameter[] parameters)
        {
            return GetDataReader(conToDb, sql, commandType, DEFAULT_COMMAND_TIMEOUT, parameters);
        }

        /// <summary>
        /// Gets a SqlDataReader object.
        /// </summary>
        /// <param name="conToDb">An instance of SqlConnection that is configured for the data source.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandType">The type of SQL statement to execute.</param>
        /// <param name="commandTimeout">The number of seconds allowed before the SQL statement is timed out.</param>
        /// <param name="parameters">A collection of SqlParameters objects that will be added to the underlyng SqlCommand object.</param>
        /// <returns>Returns a SqlDataReader with the results of the SQL statement.</returns>
        public static SqlDataReader GetDataReader(SqlConnection conToDb, string sql, CommandType commandType, int commandTimeout, params SqlParameter[] parameters)
        {
            return GetDataReader(conToDb, sql, commandType, commandTimeout, false, parameters);
        }


        /// <summary>
        /// Gets a SqlDataReader object.
        /// </summary>
        /// <param name="conToDb">An instance of SqlConnection that is configured for the data source.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandType">The type of SQL statement to execute.</param>
        /// <param name="commandTimeout">The number of seconds allowed before the SQL statement is timed out.</param>
        /// <param name="parameters">A collection of SqlParameters objects that will be added to the underlyng SqlCommand object.</param>
        /// <returns>Returns a SqlDataReader with the results of the SQL statement.</returns>
        public static SqlDataReader GetDataReader(SqlConnection conToDb, string sql, CommandType commandType, int commandTimeout, bool retry, params SqlParameter[] parameters)
        {
            SqlDataReader reader;
            SqlCommand cmd;

            cmd = null;

            try
            {

                cmd = GetCommand(conToDb, sql, commandType, commandTimeout, parameters);

                if (conToDb.State == ConnectionState.Closed)
                {
                    conToDb.Open();
                }

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }

            return reader;
        }
        #endregion

        #region SqlCommand Methods
        /// <summary>
        /// Gets a SqlCommand object.
        /// </summary>
        /// <param name="conToDb">An instance of SqlConnection that is configured for the data source.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandType">The type of sql statement to execute.</param>
        /// <param name="parameters">A collection of SqlParameters objects that will be added to the SqlCommand object.</param>
        /// <returns>Returns a SqlCommand object.</returns>
        public static SqlCommand GetCommand(SqlConnection conToDb, string sql, CommandType commandType, params SqlParameter[] parameters)
        {
            return GetCommand(conToDb, sql, commandType, DEFAULT_COMMAND_TIMEOUT, parameters);
        }

        /// <summary>
        /// Gets a SqlCommand object.
        /// </summary>
        /// <param name="conToDb">An instance of SqlConnection that is configured for the data source.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandType">The type of sql statement to execute.</param>
        /// <param name="commandTimeout">The number of seconds the command is allowed to run before timing out.</param>
        /// <param name="parameters">A collection of SqlParameters objects that will be added to the SqlCommand object.</param>
        /// <returns>Returns a SqlCommand object.</returns>
        public static SqlCommand GetCommand(SqlConnection conToDb, string sql, CommandType commandType, int commandTimeout, params SqlParameter[] parameters)
        {
            SqlCommand cmd;

            cmd = conToDb.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = commandType;
            cmd.CommandTimeout = commandTimeout;

            if (parameters != null)
            {

                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.Add(parameters[i]);
                }

            }

            return cmd;
        }
        #endregion

        #region ExecNonQuery Methods
        /// <summary>
        /// Executes a sql statement against a SQL server.
        /// </summary>
        /// <param name="connectionString">The string used to connect to a SQL Server.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <returns>Returns the number of records affected by the SQL statement.</returns>
        public static int ExecNonQuery(string connectionString, string sql)
        {
            return ExecNonQuery(connectionString, sql, CommandType.Text, null);
        }

        /// <summary>
        /// Executes a sql statement against a SQL server.
        /// </summary>
        /// <param name="connectionString">The string used to connect to a SQL Server.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandTimeout">The number of seconds allowed before the SQL statement is timed out.</param>
        /// <returns>Returns the number of records affected by the SQL statement.</returns>
        public static int ExecNonQuery(string connectionString, string sql, int commandTimeout)
        {
            return ExecNonQuery(connectionString, sql, CommandType.Text, commandTimeout, null);
        }

        /// <summary>
        /// Executes a sql statement against a SQL server.
        /// </summary>
        /// <param name="conToDb">An instance of SqlConnection that is configured for the data source.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <returns>Returns the number of records affected by the SQL statement.</returns>
        public static int ExecNonQuery(SqlConnection conToDb, string sql)
        {
            return ExecNonQuery(conToDb, sql, CommandType.Text, null);
        }

        /// <summary>
        /// Executes a sql statement against a SQL server.
        /// </summary>
        /// <param name="conToDb">An instance of SqlConnection that is configured for the data source.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandTimeout">The number of seconds allowed before the SQL statement is timed out.</param>
        /// <returns>Returns the number of records affected by the SQL statement.</returns>
        public static int ExecNonQuery(SqlConnection conToDb, string sql, int commandTimeout)
        {
            return ExecNonQuery(conToDb, sql, CommandType.Text, commandTimeout, null);
        }

        /// <summary>
        /// Executes a sql statement against a SQL server.
        /// </summary>
        /// <param name="connectionString">The string used to connect to a SQL Server.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandType">The type of SQL statement to execute.</param>
        /// <param name="parameters">A collection of SqlParameters objects that will be added to the underlyng SqlCommand object.</param>
        /// <returns>Returns the number of records affected by the SQL statement.</returns>
        public static int ExecNonQuery(string connectionString, string sql, CommandType commandType, params SqlParameter[] parameters)
        {
            return ExecNonQuery(connectionString, sql, commandType, DEFAULT_COMMAND_TIMEOUT, parameters);
        }

        /// <summary>
        /// Executes a sql statement against a SQL server.
        /// </summary>
        /// <param name="connectionString">The string used to connect to a SQL Server.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandType">The type of SQL statement to execute.</param>
        /// <param name="commandTimeout">The number of seconds allowed before the SQL statement is timed out.</param>
        /// <param name="parameters">A collection of SqlParameters objects that will be added to the underlyng SqlCommand object.</param>
        /// <returns>Returns the number of records affected by the SQL statement.</returns>
        public static int ExecNonQuery(string connectionString, string sql, CommandType commandType, int commandTimeout, params SqlParameter[] parameters)
        {
            return ExecNonQuery(connectionString, sql, commandType, commandTimeout, false, parameters);
        }


        /// <summary>
        /// Executes a sql statement against a SQL server.
        /// </summary>
        /// <param name="connectionString">The string used to connect to a SQL Server.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandType">The type of SQL statement to execute.</param>
        /// <param name="commandTimeout">The number of seconds allowed before the SQL statement is timed out.</param>
        /// <param name="parameters">A collection of SqlParameters objects that will be added to the underlyng SqlCommand object.</param>
        /// <returns>Returns the number of records affected by the SQL statement.</returns>
        public static int ExecNonQuery(string connectionString, string sql, CommandType commandType, int commandTimeout, bool retry, params SqlParameter[] parameters)
        {
            int recordsAffected;
            SqlConnection conToDb;

            conToDb = null;

            try
            {
                conToDb = GetConnection(connectionString);
                conToDb.Open();
                recordsAffected = ExecNonQuery(conToDb, sql, commandType, commandTimeout, parameters);
            }
            catch (Exception Ex)
            {
                ClearConnectionPool(Ex, conToDb);

                if (conToDb != null)
                {
                    conToDb.Dispose();
                }

                if (!retry && Ex.Message.ToLower().IndexOf("transaction") > -1)
                {
                    recordsAffected = ExecNonQuery(connectionString, sql, commandType, commandTimeout, true, parameters);
                }
                else
                {
                    throw Ex;
                }
            }
            finally
            {
                if (conToDb != null)
                {
                    conToDb.Dispose();
                }

            }

            return recordsAffected;
        }


        /// <summary>
        /// Executes a sql statement against a SQL server.
        /// </summary>
        /// <param name="conToDb">An instance of SqlConnection that is configured for the data source.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandType">The type of SQL statement to execute.</param>
        /// <param name="parameters">A collection of SqlParameters objects that will be added to the underlyng SqlCommand object.</param>
        /// <returns>Returns the number of records affected by the SQL statement.</returns>
        public static int ExecNonQuery(SqlConnection conToDb, string sql, CommandType commandType, params SqlParameter[] parameters)
        {
            return ExecNonQuery(conToDb, sql, commandType, DEFAULT_COMMAND_TIMEOUT, parameters);
        }


        /// <summary>
        /// Executes a sql statement against a SQL server.
        /// </summary>
        /// <param name="conToDb">An instance of SqlConnection that is configured for the data source.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandType">The type of SQL statement to execute.</param>
        /// <param name="commandTimeout">The number of seconds allowed before the SQL statement is timed out.</param>
        /// <param name="parameters">A collection of SqlParameters objects that will be added to the underlyng SqlCommand object.</param>
        /// <returns>Returns the number of records affected by the SQL statement.</returns>
        public static int ExecNonQuery(SqlConnection conToDb, string sql, CommandType commandType, int commandTimeout, params SqlParameter[] parameters)
        {
            return ExecNonQuery(conToDb, sql, commandType, commandTimeout, false, parameters);
        }


        /// <summary>
        /// Executes a sql statement against a SQL server.
        /// </summary>
        /// <param name="conToDb">An instance of SqlConnection that is configured for the data source.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandType">The type of SQL statement to execute.</param>
        /// <param name="commandTimeout">The number of seconds allowed before the SQL statement is timed out.</param>
        /// <param name="parameters">A collection of SqlParameters objects that will be added to the underlyng SqlCommand object.</param>
        /// <returns>Returns the number of records affected by the SQL statement.</returns>
        public static int ExecNonQuery(SqlConnection conToDb, string sql, CommandType commandType, int commandTimeout, bool retry, params SqlParameter[] parameters)
        {
            SqlCommand cmd;
            int recordsAffected;

            cmd = null;

            try
            {
                cmd = GetCommand(conToDb, sql, commandType, commandTimeout, parameters);
                recordsAffected = cmd.ExecuteNonQuery();
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }

            return recordsAffected;
        }

        /// <summary>
        /// Clears connection pool.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="conToDb"></param>
        private static void ClearConnectionPool(Exception ex, SqlConnection conToDb)
        {
            if (ex.Message.ToLower().IndexOf("transaction") > -1)
            {
                try
                {
                    System.Data.SqlClient.SqlConnection.ClearPool(conToDb);
                }
                catch
                {
                }
            }
        }
        #endregion

        #region ExecScalar Methods
        /// <summary>
        /// Executes a sql statement against a SQL server.
        /// </summary>
        /// <param name="connectionString">The string used to connect to a SQL Server.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <returns>Returns the value of the first column of the first row in the resulting data set.</returns>
        public static object ExecScalar(string connectionString, string sql)
        {
            return ExecScalar(connectionString, sql, CommandType.Text, null);
        }

        /// <summary>
        /// Executes a sql statement against a SQL server.
        /// </summary>
        /// <param name="connectionString">The string used to connect to a SQL Server.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandTimeout">The number of seconds allowed before the SQL statement is timed out.</param>
        /// <returns>Returns the value of the first column of the first row in the resulting data set.</returns>
        public static object ExecScalar(string connectionString, string sql, int commandTimeout)
        {
            return ExecScalar(connectionString, sql, CommandType.Text, commandTimeout, null);
        }

        /// <summary>
        /// Executes a sql statement against a SQL server.
        /// </summary>
        /// <param name="conToDb">An instance of SqlConnection that is configured for the data source.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <returns>Returns the value of the first column of the first row in the resulting data set.</returns>
        public static object ExecScalar(SqlConnection conToDb, string sql)
        {
            return ExecScalar(conToDb, sql, CommandType.Text, null);
        }

        /// <summary>
        /// Executes a sql statement against a SQL server.
        /// </summary>
        /// <param name="conToDb">An instance of SqlConnection that is configured for the data source.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandTimeout">The number of seconds allowed before the SQL statement is timed out.</param>
        /// <returns>Returns the value of the first column of the first row in the resulting data set.</returns>
        public static object ExecScalar(SqlConnection conToDb, string sql, int commandTimeout)
        {
            return ExecScalar(conToDb, sql, CommandType.Text, commandTimeout, null);
        }

        /// <summary>
        /// Executes a sql statement against a SQL server.
        /// </summary>
        /// <param name="connectionString">The string used to connect to a SQL Server.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandType">The type of SQL statement to execute.</param>
        /// <param name="parameters">A collection of SqlParameters objects that will be added to the underlyng SqlCommand object.</param>
        /// <returns>Returns the value of the first column of the first row in the resulting data set.</returns>
        public static object ExecScalar(string connectionString, string sql, CommandType commandType, params SqlParameter[] parameters)
        {
            return ExecScalar(connectionString, sql, commandType, DEFAULT_COMMAND_TIMEOUT, parameters);
        }

        /// <summary>
        /// Executes a sql statement against a SQL server.
        /// </summary>
        /// <param name="connectionString">The string used to connect to a SQL Server.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandType">The type of SQL statement to execute.</param>
        /// <param name="commandTimeout">The number of seconds allowed before the SQL statement is timed out.</param>
        /// <param name="parameters">A collection of SqlParameters objects that will be added to the underlyng SqlCommand object.</param>
        /// <returns>Returns the value of the first column of the first row in the resulting data set.</returns>
        public static object ExecScalar(string connectionString, string sql, CommandType commandType, int commandTimeout, params SqlParameter[] parameters)
        {
            object result;
            SqlConnection conToDb;

            conToDb = null;

            try
            {
                conToDb = GetConnection(connectionString);
                conToDb.Open();
                result = ExecScalar(conToDb, sql, commandType, commandTimeout, parameters);
            }
            finally
            {

                if (conToDb != null)
                {
                    conToDb.Dispose();
                }

            }

            return result;
        }


        /// <summary>
        /// Executes a sql statement against a SQL server.
        /// </summary>
        /// <param name="conToDb">An instance of SqlConnection that is configured for the data source.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandType">The type of SQL statement to execute.</param>
        /// <param name="parameters">A collection of SqlParameters objects that will be added to the underlyng SqlCommand object.</param>
        /// <returns>Returns the value of the first column of the first row in the resulting data set.</returns>
        public static object ExecScalar(SqlConnection conToDb, string sql, CommandType commandType, params SqlParameter[] parameters)
        {
            return ExecScalar(conToDb, sql, commandType, DEFAULT_COMMAND_TIMEOUT, parameters);
        }

        /// <summary>
        /// Executes a sql statement against a SQL server.
        /// </summary>
        /// <param name="conToDb">An instance of SqlConnection that is configured for the data source.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="commandType">The type of SQL statement to execute.</param>
        /// <param name="commandTimeout">The number of seconds allowed before the SQL statement is timed out.</param>
        /// <param name="parameters">A collection of SqlParameters objects that will be added to the underlyng SqlCommand object.</param>
        /// <returns>Returns the value of the first column of the first row in the resulting data set.</returns>
        public static object ExecScalar(SqlConnection conToDb, string sql, CommandType commandType, int commandTimeout, params SqlParameter[] parameters)
        {
            SqlCommand cmd;
            object result;

            cmd = null;
            result = null;

            try
            {
                cmd = GetCommand(conToDb, sql, commandType, commandTimeout, parameters);
                result = cmd.ExecuteScalar();
            }
            finally
            {

                if (cmd != null)
                {
                    cmd.Dispose();
                }

            }

            return result;
        }

        #endregion

        #region Properties
        public static bool SqlBinary2Boolean(object value)
        {

            if (value == DBNull.Value)
            {
                return false;
            }
            else
            {
                return Convert.ToChar(Convert.ToInt32(((Array)value).GetValue(0))) == 'T' ? true : false;
            }

        }
        #endregion Properties
    }
}
