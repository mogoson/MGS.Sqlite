/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SqliteView.cs
 *  Description  :  Sqlite view.
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
    /// Sqlite view.
    /// </summary>
    public class SqliteView : ISqliteView
    {
        /// <summary>
        /// Name of source table.
        /// </summary>
        public string Name { protected set; get; }

        /// <summary>
        /// Instance of sqlite connect.
        /// </summary>
        protected ISqliteConnect connect;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Name of view.</param>
        /// <param name="connect">Instance of sqlite connect.</param>
        public SqliteView(string name, ISqliteConnect connect)
        {
            Name = name;
            this.connect = connect;
        }

        /// <summary>
        /// Select rows from source table.
        /// </summary>
        /// <param name="commandText">Select command text (Select all if null).</param>
        /// <returns></returns>
        public DataTable Select(string commandText = null)
        {
            if (string.IsNullOrEmpty(commandText))
            {
                commandText = string.Format(SqliteConst.CMD_SELECT_FORMAT, "*", Name);
            }
            return connect.ExecuteQuery(commandText);
        }
    }
}