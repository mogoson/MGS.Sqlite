/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IGenericTable.cs
 *  Description  :  Interface for generic sqlite table.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/9/2020
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Sqlite
{
    /// <summary>
    /// Interface for generic sqlite table.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericTable<T> : IGenericView<T>
    {
        /// <summary>
        /// Insert row to data table.
        /// </summary>
        /// <param name="row"></param>
        /// <returns>Number of rows affected.</returns>
        void Insert(T row);

        /// <summary>
        /// Update row to data table.
        /// </summary>
        /// <param name="row"></param>
        /// <returns>Number of rows affected.</returns>
        void Update(T row);

        /// <summary>
        /// Delete row from data table.
        /// </summary>
        /// <param name="row"></param>
        /// <returns>Number of rows affected.</returns>
        void Delete(T row);

        /// <summary>
        /// Delete row from data table.
        /// </summary>
        /// <param name="key">Value of the primary key.</param>
        /// <returns>Number of rows affected.</returns>
        void Delete(object key);

        /// <summary>
        /// Commit modifications to data base.
        /// </summary>
        /// <returns>Number of rows affected.</returns>
        int Commit();
    }
}