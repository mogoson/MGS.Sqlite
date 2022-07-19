/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SqliteConst.cs
 *  Description  :  Constant for sqlite.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/7/2020
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Sqlite
{
    /// <summary>
    /// Constant for sqlite.
    /// </summary>
    public sealed class SqliteConst
    {
        #region
        /// <summary>
        /// TABLE
        /// </summary>
        public const string TABLE = "TABLE";

        /// <summary>
        /// VIEW
        /// </summary>
        public const string VIEW = "VIEW";

        /// <summary>
        /// TRIGGER
        /// </summary>
        public const string TRIGGER = "TRIGGER";
        #endregion

        #region
        /// <summary>
        /// PRIMARY KEY
        /// </summary>
        public const string PRIMARY_KEY = "PRIMARY KEY";

        /// <summary>
        /// UNIQUE
        /// </summary>
        public const string UNIQUE = "UNIQUE";

        /// <summary>
        /// NOT NULL
        /// </summary>
        public const string NOT_NULL = "NOT NULL";

        /// <summary>
        /// DEFAULT
        /// </summary>
        public const string DEFAULT = "DEFAULT";
        #endregion

        #region
        /// <summary>
        /// BEFORE
        /// </summary>
        public const string BEFORE = "BEFORE";

        /// <summary>
        /// AFTER
        /// </summary>
        public const string AFTER = "AFTER";

        /// <summary>
        /// INSERT
        /// </summary>
        public const string INSERT = "INSERT";

        /// <summary>
        /// UPDATE
        /// </summary>
        public const string UPDATE = "UPDATE";

        /// <summary>
        /// UPDATE OF
        /// </summary>
        public const string UPDATE_OF = "UPDATE OF";

        /// <summary>
        /// DELETE
        /// </summary>
        public const string DELETE = "DELETE";

        /// <summary>
        /// FOR EACH ROW
        /// </summary>
        public const string FOR_EACH_ROW = "FOR EACH ROW";
        #endregion

        #region
        /// <summary>
        /// Sqlite main table sqlite_master.
        /// </summary>
        public const string SQLITE_MASTER = "sqlite_master";
        #endregion

        #region
        /// <summary>
        /// Format of file uri string [file].
        /// </summary>
        public const string URI_FILE_FORMAT = "file:{0}";

        /// <summary>
        /// Format of connection string [version, uri].
        /// </summary>
        public const string CONNECTION_FORMAT = "version={0},uri={1}";
        #endregion

        #region
        /// <summary>
        /// Format of create if not exists command [type, statement].
        /// </summary>
        public const string CMD_CREATE_IF_FORMAT = "CREATE {0} IF NOT EXISTS {1}";

        /// <summary>
        /// Format of select command [columns, name].
        /// </summary>
        public const string CMD_SELECT_FORMAT = "SELECT {0} FROM {1}";

        /// <summary>
        /// Format of select sqlite_master by type and name command [columns, type, name].
        /// </summary>
        public const string CMD_SELECT_MASTER_TYPE_NAME_FORMAT = "select {0} from sqlite_master where type='{1}' and name='{2}'";

        /// <summary>
        /// Format of delete command [type, name].
        /// </summary>
        public const string CMD_DROP_FORMAT = "DROP {0} {1}";
        #endregion

        #region
        /// <summary>
        /// Format of trigger statement [name, when, action, table, scope, where, code].
        /// </summary>
        public const string STMT_TRIGGER_FORMAT = "{0} {1} {2} ON {3} {4} {5} BEGIN {6}; END";
        #endregion

        #region
        /// <summary>
        /// Format of group by expression [column1, column2....columnN].
        /// </summary>
        public const string EXPR_GROUP_BY_FORMAT = "GROUP BY {0}";

        /// <summary>
        /// Format of order by expression [column1, column2....columnN].
        /// </summary>
        public const string EXPR_ORDER_BY_FORMAT = "ORDER BY {0}";

        /// <summary>
        /// Format of join using expression [table, column1, column2....columnN].
        /// </summary>
        public const string EXPR_JOIN_USING_FORMAT = "JOIN {0} USING({1})";
        #endregion
    }
}