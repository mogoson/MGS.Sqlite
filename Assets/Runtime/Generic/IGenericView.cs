/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IGenericView.cs
 *  Description  :  Interface for generic sqlite view.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/9/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;

namespace MGS.Sqlite
{
    /// <summary>
    /// Interface for generic sqlite view.
    /// </summary>
    public interface IGenericView<T> where T : IViewRow, new()
    {
        /// <summary>
        /// Select rows from source table.
        /// </summary>
        /// <param name="command">Select command [Select all if null].</param>
        /// <returns>Selected rows.</returns>
        ICollection<T> Select(string command = null);
    }
}