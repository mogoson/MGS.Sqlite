/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ISqliteTable.cs
 *  Description  :  Interface for sqlite table.
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
    /// Interface for sqlite table.
    /// </summary>
    public interface ISqliteTable : ISqliteView
    {
        /// <summary>
        /// Update data table modifications to source table.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns>Number of rows affected.</returns>
        int Update(DataTable dataTable);
    }
}