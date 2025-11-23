/*************************************************************************
 *  Copyright © 2025 Mogoson All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  DataRowAdapter.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  11/23/2025
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;

namespace MGS.Sqlite
{
    /// <summary>
    /// Adapter for data row.
    /// </summary>
    public sealed class DataRowAdapter
    {
        /// <summary>
        /// Create instance of type T and fill fields from DataRow columns for each row.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rowCollection"></param>
        /// <returns></returns>
        public static ICollection<T> FillFrom<T>(DataRowCollection rowCollection)
        {
            var instances = new List<T>();
            foreach (DataRow row in rowCollection)
            {
                instances.Add(FillFrom<T>(row));
            }
            return instances;
        }

        /// <summary>
        /// Create instance of type T and fill fields from DataRow columns.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        public static T FillFrom<T>(DataRow dataRow)
        {
            var instance = Activator.CreateInstance<T>();
            FillFrom(instance, dataRow);
            return instance;
        }

        /// <summary>
        /// Fill DataRow columns from object fields.
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="obj"></param>
        public static void FillFrom(DataRow dataRow, object obj)
        {
            var fields = obj.GetType().GetFields();
            foreach (var field in fields)
            {
                dataRow[field.Name] = field.GetValue(obj);
            }
        }

        /// <summary>
        /// Fill object fields from DataRow columns.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dataRow"></param>
        public static void FillFrom(object obj, DataRow dataRow)
        {
            var fields = obj.GetType().GetFields();
            foreach (var field in fields)
            {
                var value = dataRow[field.Name];
                if (value != null)
                {
                    field.SetValue(obj, value);
                }
            }
        }
    }
}