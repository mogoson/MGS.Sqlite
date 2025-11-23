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

using System.Data;
using System.Reflection;

namespace MGS.Sqlite
{
    /// <summary>
    /// Generic sqlite table.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericTable<T> : GenericView<T>, IGenericTable<T>
    {
        /// <summary>
        /// Instance of sqlite source.
        /// </summary>
        protected new ISqliteTable source;

        /// <summary>
        /// DataTable of initial selected results.
        /// </summary>
        protected DataTable dataTable;

        /// <summary>
        /// Field info of primaryKey.
        /// </summary>
        protected FieldInfo primaryField;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="table">Instance of sqlite table.</param>
        public GenericTable(ISqliteTable table) : base(table)
        {
            source = table;
            dataTable = table.Select();

            var primaryKey = dataTable.PrimaryKey[0].ColumnName;
            primaryField = typeof(T).GetField(primaryKey);
        }

        /// <summary>
        /// Insert row to data table.
        /// </summary>
        /// <param name="row"></param>
        /// <returns>Number of rows affected.</returns>
        public void Insert(T row)
        {
            var key = primaryField.GetValue(row);
            if (dataTable.Rows.Contains(key))
            {
                return;
            }
            var newRow = dataTable.NewRow();
            DataRowAdapter.FillFrom(newRow, row);
            dataTable.Rows.Add(newRow);
        }

        /// <summary>
        /// Update row to data table.
        /// </summary>
        /// <param name="row"></param>
        /// <returns>Number of rows affected.</returns>
        public void Update(T row)
        {
            var key = primaryField.GetValue(row);
            if (!dataTable.Rows.Contains(key))
            {
                return;
            }

            var dataRow = dataTable.Rows.Find(key);
            dataRow.BeginEdit();
            DataRowAdapter.FillFrom(dataRow, row);
            dataRow.EndEdit();
        }

        /// <summary>
        /// Delete row from data table.
        /// </summary>
        /// <param name="row"></param>
        /// <returns>Number of rows affected.</returns>
        public void Delete(T row)
        {
            Delete(primaryField.GetValue(row));
        }

        /// <summary>
        /// Delete row from data table.
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
            return source.Update(dataTable);
        }
    }
}