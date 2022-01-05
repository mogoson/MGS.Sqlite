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
using System.Data;

namespace MGS.Sqlite
{
    /// <summary>
    /// Generic sqlite view.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericView<T> : IGenericView<T> where T : IViewRow, new()
    {
        /// <summary>
        /// Instance of sqlite source.
        /// </summary>
        protected ISqliteView source;

        /// <summary>
        /// Constructor of GenericView.
        /// </summary>
        /// <param name="view">Instance of sqlite view.</param>
        public GenericView(ISqliteView view)
        {
            source = view;
        }

        /// <summary>
        /// Select rows from source.
        /// </summary>
        /// <param name="command">Select command [Select all if null].</param>
        /// <returns>Selected rows.</returns>
        public virtual ICollection<T> Select(string command = null)
        {
            var dataTable = source.Select(command);
            if (dataTable == null || dataTable.Rows == null || dataTable.Rows.Count == 0)
            {
                return null;
            }

            var rows = new List<T>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var row = new T();
                row.FillFrom(dataRow);
                rows.Add(row);
            }
            return rows;
        }
    }
}