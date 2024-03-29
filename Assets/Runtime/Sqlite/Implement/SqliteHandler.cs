﻿/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SqliteHandler.cs
 *  Description  :  Handler for sqlite data base.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/5/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using Mono.Data.Sqlite;
using System;
using System.Data;
using System.IO;

namespace MGS.Sqlite
{
    /// <summary>
    /// Handler for sqlite data base.
    /// </summary>
    public class SqliteHandler : ISqliteHandler
    {
        /// <summary>
        /// Connection string.
        /// </summary>
        protected string connectionString;

        #region
        /// <summary>
        /// Constructor of SqliteHandler.
        /// </summary>
        /// <param name="uri">Uri of data source.</param>
        public SqliteHandler(string uri)
        {
            connectionString = string.Format(SqliteConst.CONNECTION_FORMAT, 3, uri);
        }

        /// <summary>
        /// Execute command with args.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual DataTable ExecuteQuery(string command, params SqliteParameter[] args)
        {
            try
            {
                using (var conn = new SqliteConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = command;
                        cmd.Parameters.AddRange(args);

                        using (var adapter = new SqliteDataAdapter(cmd))
                        {
                            var table = new DataTable();
                            adapter.Fill(table);
                            return table;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SqliteLogger.LogException(ex);
                return null;
            }
        }

        /// <summary>
        /// Execute command with args.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual object ExecuteScalar(string command, params SqliteParameter[] args)
        {
            try
            {
                using (var conn = new SqliteConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = command;
                        cmd.Parameters.AddRange(args);

                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                SqliteLogger.LogException(ex);
                return null;
            }
        }

        /// <summary>
        /// Execute command with args.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="args"></param>
        /// <returns>Number of rows affected.</returns>
        public virtual int ExecuteNonQuery(string command, params SqliteParameter[] args)
        {
            try
            {
                using (var conn = new SqliteConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = command;
                        cmd.Parameters.AddRange(args);

                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                SqliteLogger.LogException(ex);
                return 0;
            }
        }

        /// <summary>
        /// Execute update table to data base by command with args.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="command"></param>
        /// <param name="args"></param>
        /// <returns>Number of rows affected.</returns>
        public virtual int ExecuteNonQuery(DataTable table, string command, params SqliteParameter[] args)
        {
            try
            {
                using (var conn = new SqliteConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = command;
                        cmd.Parameters.AddRange(args);

                        using (var adapter = new SqliteDataAdapter(cmd))
                        {
                            using (var builder = new SqliteCommandBuilder(adapter))
                            {
                                adapter.InsertCommand = builder.GetInsertCommand();
                                adapter.UpdateCommand = builder.GetUpdateCommand();
                                adapter.DeleteCommand = builder.GetDeleteCommand();

                                return adapter.Update(table);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SqliteLogger.LogException(ex);
                return 0;
            }
        }
        #endregion

        #region
        /// <summary>
        /// Create data base file.
        /// </summary>
        /// <param name="file">Path of data base file.</param>
        public static void CreateFile(string file)
        {
            try
            {
                //Check create directory.
                var dir = Path.GetDirectoryName(file);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                SqliteConnection.CreateFile(file);
            }
            catch (Exception ex)
            {
                SqliteLogger.LogException(ex);
            }
        }
        #endregion
    }
}