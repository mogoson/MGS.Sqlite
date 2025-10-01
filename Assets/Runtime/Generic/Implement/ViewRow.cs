/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ViewRow.cs
 *  Description  :  Generic sqlite view row base on reflection.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/10/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace MGS.Sqlite
{
    /// <summary>
    /// Generic sqlite view row base on reflection.
    /// [A better way is implement the interface IViewRow by yourself]
    /// </summary>
    public abstract class ViewRow : IViewRow
    {
        /// <summary>
        /// Field info of column fields.
        /// </summary>
        protected static ICollection<FieldInfo> columnFields;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ViewRow()
        {
            Initialize();
        }

        /// <summary>
        /// Fill this view from data row.
        /// </summary>
        /// <param name="row"></param>
        public void FillFrom(DataRow row)
        {
            try
            {
                foreach (var field in columnFields)
                {
                    field.SetValue(this, row[field.Name]);
                }
            }
            catch (Exception ex)
            {
                SqliteLogger.LogException(ex);
            }
        }

        /// <summary>
        /// Initialize the columnFields.
        /// </summary>
        protected virtual void Initialize()
        {
            //The info of sqlite fields is generic, so just need init once.
            if (columnFields == null)
            {
                columnFields = new List<FieldInfo>();

                var fields = GetType().GetFields();
                foreach (var field in fields)
                {
                    if (field.IsDefined(typeof(ColumnFieldAttribute), false))
                    {
                        columnFields.Add(field);
                    }
                }

                if (columnFields.Count == 0)
                {
                    var message = $"Can not find any column field in class {this}";
                    message += $", you can use {nameof(ColumnFieldAttribute)} to mark e field as column field.";
                    throw new NullReferenceException(message);
                }
            }
        }
    }
}