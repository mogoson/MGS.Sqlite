/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  GenericDataBase.cs
 *  Description  :  Generic sqlite data base.
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
    /// Generic sqlite data base.
    /// </summary>
    public class GenericDataBase : SqliteDataBase, IGenericDataBase
    {
        /// <summary>
        /// Constructor of GenericDataBase.
        /// </summary>
        /// <param name="file">Data base file.</param>
        public GenericDataBase(string file) : base(file) { }

        /// <summary>
        /// Select data rows as view rows from data base.
        /// </summary>
        /// <typeparam name="T">Type of view row.</typeparam>
        /// <param name="command">Select command.</param>
        /// <returns></returns>
        public ICollection<T> Select<T>(string command) where T : IViewRow, new()
        {
            return new GenericView<T>(new SqliteView(null, Handler)).Select(command);
        }

        /// <summary>
        /// Select sqlite view from data base as IGenericView.
        /// </summary>
        /// <typeparam name="T">Type of view row.</typeparam>
        /// <param name="name">Name of view.</param>
        /// <returns></returns>
        public IGenericView<T> SelectView<T>(string name) where T : IViewRow, new()
        {
            var view = SelectView(name);
            if (view == null)
            {
                return null;
            }

            return new GenericView<T>(view);
        }

        /// <summary>
        /// Create sqlite table for the type T row. 
        /// </summary>
        /// <typeparam name="T">Type of table row.</typeparam>
        /// <param name="name">Name of table.</param>
        /// <returns>Number of rows affected.</returns>
        public int CreateTable<T>(string name) where T : ITableRow, new()
        {
            var statement = string.Format("{0}{1}", name, new T().Statement);
            return CreateTable(statement);
        }

        /// <summary>
        /// Select sqlite table from data base as IGenericTable.
        /// </summary>
        /// <typeparam name="T">Type of table row.</typeparam>
        /// <param name="name">Name of table.</param>
        /// <returns></returns>
        public IGenericTable<T> SelectTable<T>(string name) where T : ITableRow, new()
        {
            var table = SelectTable(name);
            if (table == null)
            {
                return null;
            }

            return new GenericTable<T>(table);
        }
    }
}