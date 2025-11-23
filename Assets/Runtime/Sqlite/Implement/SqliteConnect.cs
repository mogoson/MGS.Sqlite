/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SqliteConnect.cs
 *  Description  :  Connect to sqlite data base.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/5/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

namespace MGS.Sqlite
{
    /// <summary>
    /// Connect to sqlite data base.
    /// </summary>
    public class SqliteConnect : ISqliteConnect
    {
        /// <summary>
        /// Connection string.
        /// </summary>
        protected string connectionString;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="uri">Uri of data source.</param>
        public SqliteConnect(string uri)
        {
            connectionString = string.Format(SqliteConst.CONNECTION_FORMAT, 3, uri);
        }

        /// <summary>
        /// Execute command with parameters.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual DataTable ExecuteQuery(string commandText, params SqliteParameter[] parameters)
        {
            try
            {
                using (var conn = new SqliteConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = commandText;
                        cmd.Parameters.AddRange(parameters);

                        using (var adapter = new SqliteDataAdapter(cmd))
                        {
                            var table = new DataTable();
                            adapter.FillSchema(table, SchemaType.Source);
                            adapter.Fill(table);
                            return table;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return null;
            }
        }

        /// <summary>
        /// Execute command with parameters.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual object ExecuteScalar(string commandText, params SqliteParameter[] parameters)
        {
            try
            {
                using (var conn = new SqliteConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = commandText;
                        cmd.Parameters.AddRange(parameters);

                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return null;
            }
        }

        /// <summary>
        /// Execute command with parameters.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns>Number of rows affected.</returns>
        public virtual int ExecuteNonQuery(string commandText, params SqliteParameter[] parameters)
        {
            try
            {
                using (var conn = new SqliteConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = commandText;
                        cmd.Parameters.AddRange(parameters);

                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return 0;
            }
        }

        /// <summary>
        /// Execute update table to data base by command with parameters.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns>Number of rows affected.</returns>
        public virtual int ExecuteNonQuery(DataTable dataTable, string commandText, params SqliteParameter[] parameters)
        {
            try
            {
                using (var conn = new SqliteConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = commandText;
                        cmd.Parameters.AddRange(parameters);

                        using (var adapter = new SqliteDataAdapter(cmd))
                        {
#if UNITY_STANDALONE
                            using (var builder = new SqliteCommandBuilder(adapter))
#else
                            using (var builder = new SqliteCmdBuilder(adapter))
#endif
                            {
                                adapter.InsertCommand = builder.GetInsertCommand();
                                adapter.UpdateCommand = builder.GetUpdateCommand();
                                adapter.DeleteCommand = builder.GetDeleteCommand();

                                return adapter.Update(dataTable);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return 0;
            }
        }
    }
}