/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ISqliteDataBase.cs
 *  Description  :  Interface for sqlite data base.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/5/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Data;

namespace MGS.Sqlite
{
    /// <summary>
    /// Interface for sqlite data base.
    /// </summary>
    public interface ISqliteDataBase
    {
        #region Data
        /// <summary>
        /// Select data rows from data base.
        /// </summary>
        /// <param name="command">Select command.</param>
        /// <returns></returns>
        DataTable Select(string command);
        #endregion

        #region View
        /// <summary>
        /// Create sqlite view if not exists.
        /// </summary>
        /// <param name="statement">Statement sql for view.</param>
        /// <returns>Number of rows affected.</returns>
        int CreateView(string statement);

        /// <summary>
        /// Select view from data base.
        /// </summary>
        /// <param name="name">Name of view.</param>
        /// <returns></returns>
        ISqliteView SelectView(string name);

        /// <summary>
        /// Delete view from data base.
        /// </summary>
        /// <param name="name">Name of view.</param>
        /// <returns>Number of rows affected.</returns>
        int DeleteView(string name);
        #endregion

        #region Table
        /// <summary>
        /// Create sqlite table if not exists.
        /// </summary>
        /// <param name="statement">Statement sql for table.</param>
        /// <returns>Number of rows affected.</returns>
        int CreateTable(string statement);

        /// <summary>
        /// Select table from data base.
        /// </summary>
        /// <param name="name">Name of table.</param>
        /// <returns></returns>
        ISqliteTable SelectTable(string name);

        /// <summary>
        /// Delete the table from data base.
        /// </summary>
        /// <param name="name">The name of table.</param>
        /// <returns>Number of rows affected.</returns>
        int DeleteTable(string name);
        #endregion

        #region Trigger
        /// <summary>
        /// Create sqlite trigger if not exists.
        /// </summary>
        /// <param name="statement">Statement sql for trigger.</param>
        /// <returns></returns>
        int CreateTrigger(string statement);

        /// <summary>
        /// Create sqlite trigger if not exists.
        /// </summary>
        /// <param name="name">The name of trigger.</param>
        /// <param name="when">[BEFORE/AFTER]</param>
        /// <param name="action">[INSERT/UPDATE/UPDATE OF/DELETE/]</param>
        /// <param name="table">Name of target table.</param>
        /// <param name="scope">[null/FOR EACH ROW]</param>
        /// <param name="condition">Condition statement.</param>
        /// <param name="code">Code of trigger.</param>
        /// <returns>Number of rows affected.</returns>
        int CreateTrigger(string name, string when, string action,
            string table, string scope, string condition, string code);

        /// <summary>
        /// Delete the trigger from data base.
        /// </summary>
        /// <param name="name">The name of trigger.</param>
        /// <returns>Number of rows affected.</returns>
        int DeleteTrigger(string name);
        #endregion
    }
}