/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ISqliteConnect.cs
 *  Description  :  Interface for sqlite connect.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/5/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Data;
using Mono.Data.Sqlite;

namespace MGS.Sqlite
{
    /// <summary>
    /// Interface for sqlite connect.
    /// </summary>
    public interface ISqliteConnect
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