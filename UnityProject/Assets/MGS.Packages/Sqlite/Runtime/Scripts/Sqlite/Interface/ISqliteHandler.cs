/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ISqliteHandler.cs
 *  Description  :  Interface for sqlite handler.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/5/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using Mono.Data.Sqlite;
using System.Data;

namespace MGS.Sqlite
{
    /// <summary>
    /// Interface for sqlite handler.
    /// </summary>
    public interface ISqliteHandler
    {
        /// <summary>
        /// Execute command with args.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        DataTable ExecuteQuery(string command, params SqliteParameter[] args);

        /// <summary>
        /// Execute command with args.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        object ExecuteScalar(string command, params SqliteParameter[] args);

        /// <summary>
        /// Execute command with args.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="args"></param>
        /// <returns>Number of rows affected.</returns>
        int ExecuteNonQuery(string command, params SqliteParameter[] args);

        /// <summary>
        /// Execute update table to data base by command with args.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="command"></param>
        /// <param name="args"></param>
        /// <returns>Number of rows affected.</returns>
        int ExecuteNonQuery(DataTable table, string command, params SqliteParameter[] args);
    }
}