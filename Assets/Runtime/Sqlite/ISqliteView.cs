/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ISqliteView.cs
 *  Description  :  Interface for sqlite view.
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
    /// Interface for sqlite view.
    /// </summary>
    public interface ISqliteView
    {
        /// <summary>
        /// Name of source table.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Select rows from source table.
        /// </summary>
        /// <param name="command">Select command [Select all if null].</param>
        /// <returns></returns>
        DataTable Select(string command = null);
    }
}