/*************************************************************************
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
using UnityEngine;

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
        /// <returns></returns>
        public static bool CreateFile(string file)
        {
            try
            {
                var dir = Path.GetDirectoryName(file);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                SqliteConnection.CreateFile(file);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return false;
            }
        }
        #endregion

        #region Data
        /// <summary>
        /// Select data rows from data base.
        /// </summary>
        /// <param name="commandText">Select command text.</param>
        /// <returns></returns>
        public DataTable Select(string commandText)
        {
            return connect.ExecuteQuery(commandText);
        }
        #endregion

        #region View
        /// <summary>
        /// Create sqlite view if not exists.
        /// </summary>
        /// <param name="schema">Schema of view.</param>
        /// <returns>Number of rows affected.</returns>
        public int CreateView(string schema)
        {
            var createCmd = string.Format(SqliteConst.CMD_CREATE_IF_FORMAT, SqliteConst.VIEW, schema);
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
        /// <param name="schema">Schema of table.</param>
        /// <returns>Number of rows affected.</returns>
        public int CreateTable(string schema)
        {
            var createCmd = string.Format(SqliteConst.CMD_CREATE_IF_FORMAT, SqliteConst.TABLE, schema);
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
        /// <param name="schema">Schema of trigger.</param>
        /// <returns></returns>
        public int CreateTrigger(string schema)
        {
            var createCmd = string.Format(SqliteConst.CMD_CREATE_IF_FORMAT, SqliteConst.TRIGGER, schema);
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
            var schema = string.Format(SqliteConst.SCHM_TRIGGER_FORMAT, name, when, action, table, scope, where, code);
            var createCmd = string.Format(SqliteConst.CMD_CREATE_IF_FORMAT, SqliteConst.TRIGGER, schema);
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