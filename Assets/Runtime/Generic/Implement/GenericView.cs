/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  GenericView.cs
 *  Description  :  Generic sqlite view.
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
    /// Generic sqlite view.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericView<T> : IGenericView<T>
    {
        /// <summary>
        /// Instance of sqlite view.
        /// </summary>
        protected ISqliteView source;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="view">Instance of sqlite view.</param>
        public GenericView(ISqliteView view)
        {
            source = view;
        }

        /// <summary>
        /// Select rows from source table.
        /// </summary>
        /// <param name="commandText">Select command text (Select all if null).</param>
        /// <returns>Selected rows.</returns>
        public virtual ICollection<T> Select(string commandText = null)
        {
            var dataTable = source.Select(commandText);
            if (dataTable == null || dataTable.Rows == null || dataTable.Rows.Count == 0)
            {
                return null;
            }
            return DataRowAdapter.FillFrom<T>(dataTable.Rows);
        }
    }
}