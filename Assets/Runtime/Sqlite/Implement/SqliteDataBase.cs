﻿/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SqliteDataBase.cs
 *  Description  :  Sqlite data base.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/5/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;

namespace MGS.Sqlite
{
    /// <summary>
    /// Sqlite data base.
    /// </summary>
    public class SqliteDataBase : ISqliteDataBase
    {
        #region Constructor
        /// <summary>
        /// Sqlite connect of data base.
        /// </summary>
        protected ISqliteConnect connect;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="file">Data base file.</param>
        public SqliteDataBase(string file)
        {
            if (!File.Exists(file))
            {
                CreateFile(file);
            }

            var uri = string.Format(SqliteConst.URI_FILE_FORMAT, file);
            connect = new SqliteConnect(uri);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="connect">Sqlite connect of data base.</param>
        public SqliteDataBase(ISqliteConnect connect)
        {
            this.connect = connect;
        }
        #endregion

        #region File
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

        #region Data
        /// <summary>
        /// Select data rows from data base.
        /// </summary>
        /// <param name="command">Select command.</param>
        /// <returns></returns>
        public DataTable Select(string command)
        {
            return connect.ExecuteQuery(command);
        }
        #endregion

        #region View
        /// <summary>
        /// Create sqlite view if not exists.
        /// </summary>
        /// <param name="statement">Statement sql for view.</param>
        /// <returns>Number of rows affected.</returns>
        public int CreateView(string statement)
        {
            var createCmd = string.Format(SqliteConst.CMD_CREATE_IF_FORMAT, SqliteConst.VIEW, statement);
            return connect.ExecuteNonQuery(createCmd);
        }

        /// <summary>
        /// Select view from data base.
        /// </summary>
        /// <param name="name">Name of view.</param>
        /// <returns></returns>
        public ISqliteView SelectView(string name)
        {
            var selectCmd = string.Format(SqliteConst.CMD_SELECT_MASTER_TYPE_NAME_FORMAT, "name", "view", name);
            var result = connect.ExecuteScalar(selectCmd);
            if (result == null)
            {
                return null;
            }
            return new SqliteView(name, connect);
        }

        /// <summary>
        /// Delete view from data base.
        /// </summary>
        /// <param name="name">Name of view.</param>
        /// <returns>Number of rows affected.</returns>
        public int DeleteView(string name)
        {
            var deleteCmd = string.Format(SqliteConst.CMD_DROP_FORMAT, SqliteConst.VIEW, name);
            return connect.ExecuteNonQuery(deleteCmd);
        }
        #endregion

        #region Table
        /// <summary>
        /// Create sqlite table if not exists.
        /// </summary>
        /// <param name="statement">Statement sql for table.</param>
        /// <returns>Number of rows affected.</returns>
        public int CreateTable(string statement)
        {
            var createCmd = string.Format(SqliteConst.CMD_CREATE_IF_FORMAT, SqliteConst.TABLE, statement);
            return connect.ExecuteNonQuery(createCmd);
        }

        /// <summary>
        /// Select table from data base.
        /// </summary>
        /// <param name="name">Name of table.</param>
        /// <returns></returns>
        public ISqliteTable SelectTable(string name)
        {
            var selectCmd = string.Format(SqliteConst.CMD_SELECT_MASTER_TYPE_NAME_FORMAT, "name", "table", name);
            var result = connect.ExecuteScalar(selectCmd);
            if (result == null)
            {
                return null;
            }
            return new SqliteTable(name, connect);
        }

        /// <summary>
        /// Delete the table from data base.
        /// </summary>
        /// <param name="name">The name of table.</param>
        /// <returns>Number of rows affected.</returns>
        public int DeleteTable(string name)
        {
            var deleteCmd = string.Format(SqliteConst.CMD_DROP_FORMAT, SqliteConst.TABLE, name);
            return connect.ExecuteNonQuery(deleteCmd);
        }
        #endregion

        #region Trigger
        /// <summary>
        /// Create sqlite trigger if not exists.
        /// </summary>
        /// <param name="statement">Statement sql for trigger.</param>
        /// <returns></returns>
        public int CreateTrigger(string statement)
        {
            var createCmd = string.Format(SqliteConst.CMD_CREATE_IF_FORMAT, SqliteConst.TRIGGER, statement);
            return connect.ExecuteNonQuery(createCmd);
        }

        /// <summary>
        /// Create sqlite trigger if not exists.
        /// </summary>
        /// <param name="name">The name of trigger.</param>
        /// <param name="when">[BEFORE/AFTER]</param>
        /// <param name="action">[INSERT/UPDATE/UPDATE OF/DELETE/]</param>
        /// <param name="table">Name of target table.</param>
        /// <param name="scope">[null/FOR EACH ROW]</param>
        /// <param name="where">Condition statement.</param>
        /// <param name="code">Code of trigger (Without the ending symbol ';').</param>
        /// <returns>Number of rows affected.</returns>
        public int CreateTrigger(string name, string when, string action,
            string table, string scope, string where, string code)
        {
            var statement = string.Format(SqliteConst.STMT_TRIGGER_FORMAT, name, when, action, table, scope, where, code);
            var createCmd = string.Format(SqliteConst.CMD_CREATE_IF_FORMAT, SqliteConst.TRIGGER, statement);
            return connect.ExecuteNonQuery(createCmd);
        }

        /// <summary>
        /// Delete the trigger from data base.
        /// </summary>
        /// <param name="name">The name of trigger.</param>
        /// <returns>Number of rows affected.</returns>
        public int DeleteTrigger(string name)
        {
            var deleteCmd = string.Format(SqliteConst.CMD_DROP_FORMAT, SqliteConst.TRIGGER, name);
            return connect.ExecuteNonQuery(deleteCmd);
        }
        #endregion
    }
}