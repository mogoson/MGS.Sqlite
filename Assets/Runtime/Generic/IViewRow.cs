/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IViewRow.cs
 *  Description  :  Interface for generic sqlite view row.
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
    /// Interface for generic sqlite view row.
    /// </summary>
    public interface IViewRow
    {
        /// <summary>
        /// Fill this view from data row.
        /// </summary>
        /// <param name="row"></param>
        void FillFrom(DataRow row);
    }
}