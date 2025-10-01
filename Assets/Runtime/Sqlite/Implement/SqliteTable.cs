/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SqliteTable.cs
 *  Description  :  Sqlite table.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/9/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Data;

namespace MGS.Sqlite
{
    /// <summary>
    /// Sqlite table.
    /// </summary>
    public class SqliteTable : SqliteView, ISqliteTable
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Name of table.</param>
        /// <param name="connect">Instance of sqlite connect.</param>
        public SqliteTable(string name, ISqliteConnect connect) : base(name, connect) { }

        /// <summary>
        /// Update rows modifications to source table.
        /// </summary>
        /// <param name="table"></param>
        /// <returns>Number of rows affected.</returns>
        public int Update(DataTable table)
        {
            var selectCmd = string.Format(SqliteConst.CMD_SELECT_FORMAT, "*", Name);
            var lines = connect.ExecuteNonQuery(table, selectCmd);
            if (lines > 0)
            {
                table.AcceptChanges();
            }
            return lines;
        }
    }
}