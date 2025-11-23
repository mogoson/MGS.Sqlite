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
        /// Execute command with parameters.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        DataTable ExecuteQuery(string commandText, params SqliteParameter[] parameters);

        /// <summary>
        /// Execute command with parameters.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        object ExecuteScalar(string commandText, params SqliteParameter[] parameters);

        /// <summary>
        /// Execute command with parameters.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns>Number of rows affected.</returns>
        int ExecuteNonQuery(string commandText, params SqliteParameter[] parameters);

        /// <summary>
        /// Execute update table to data base by command with parameters.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns>Number of rows affected.</returns>
        int ExecuteNonQuery(DataTable dataTable, string commandText, params SqliteParameter[] parameters);
    }
}