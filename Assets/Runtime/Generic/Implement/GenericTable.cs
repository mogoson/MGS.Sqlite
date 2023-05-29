/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  GenericTable.cs
 *  Description  :  Generic sqlite table.
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
    /// Generic sqlite table.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericTable<T> : GenericView<T>, IGenericTable<T> where T : ITableRow, new()
    {
        /// <summary>
        /// Instance of sqlite source.
        /// </summary>
        protected new ISqliteTable source;

        /// <summary>
        /// DataTable of last selected results.
        /// </summary>
        protected DataTable dataTable;

        /// <summary>
        /// Constructor of GenericTable.
        /// </summary>
        /// <param name="table">Instance of sqlite table.</param>
        public GenericTable(ISqliteTable table) : base(table)
        {
            source = table;
            dataTable = table.Select();
        }

        /// <summary>
        /// Select rows from source.
        /// </summary>
        /// <param name="command">Select command [Select all if null].</param>
        /// <returns>Selected rows.</returns>
        public override ICollection<T> Select(string command = null)
        {
            var rows = base.Select(command);
            if (rows == null)
            {
                return null;
            }

            //Set the primary key for data table.
            var column = dataTable.Columns[new T().PrimaryKey];
            dataTable.PrimaryKey = new DataColumn[] { column };

            return rows;
        }

        /// <summary>
        /// Insert row to table.
        /// </summary>
        /// <param name="row"></param>
        /// <returns>Number of rows affected.</returns>
        public void Insert(T row)
        {
            var newRow = dataTable.NewRow();
            row.FillTo(newRow);
            dataTable.Rows.Add(newRow);
        }

        /// <summary>
        /// Update row to table.
        /// </summary>
        /// <param name="row"></param>
        /// <returns>Number of rows affected.</returns>
        public void Update(T row)
        {
            var key = row.PrimaryValue;
            if (!dataTable.Rows.Contains(key))
            {
                return;
            }

            var dataRow = dataTable.Rows.Find(key);
            dataRow.BeginEdit();
            row.FillTo(dataRow);
            dataRow.EndEdit();
        }

        /// <summary>
        /// Delete row from table.
        /// </summary>
        /// <param name="row"></param>
        /// <returns>Number of rows affected.</returns>
        public void Delete(T row)
        {
            Delete(row.PrimaryValue);
        }

        /// <summary>
        /// Delete row from table.
        /// </summary>
        /// <param name="key">Value of the primary key.</param>
        /// <returns>Number of rows affected.</returns>
        public void Delete(object key)
        {
            if (!dataTable.Rows.Contains(key))
            {
                return;
            }

            var dataRow = dataTable.Rows.Find(key);
            dataRow.Delete();
        }

        /// <summary>
        /// Commit modifications to data base.
        /// </summary>
        /// <returns>Number of rows affected.</returns>
        public int Commit()
        {
            var lines = source.Update(dataTable);
            if (lines > 0)
            {
                dataTable.AcceptChanges();
            }
            return lines;
        }
    }
}