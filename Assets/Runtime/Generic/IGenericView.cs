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
    public interface IGenericView<T>
    {
        /// <summary>
        /// Select rows from source table.
        /// </summary>
        /// <param name="commandText">Select command text (Select all if null).</param>
        /// <returns>Selected rows.</returns>
        ICollection<T> Select(string commandText = null);
    }
}