/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ITableRow.cs
 *  Description  :  Interface for generic sqlite table row.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/10/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Data;

namespace MGS.Sqlite
{
    /// <summary>
    /// Interface for generic sqlite table row.
    /// </summary>
    public interface ITableRow : IViewRow
    {
        /// <summary>
        /// Statement of the columns.
        /// </summary>
        string Statement { get; }

        /// <summary>
        /// Name of primary key.
        /// </summary>
        string PrimaryKey { get; }

        /// <summary>
        /// Value of primary key.
        /// </summary>
        object PrimaryValue { get; }

        /// <summary>
        /// Fill this table to data row.
        /// </summary>
        /// <param name="row"></param>
        void FillTo(DataRow row);
    }
}