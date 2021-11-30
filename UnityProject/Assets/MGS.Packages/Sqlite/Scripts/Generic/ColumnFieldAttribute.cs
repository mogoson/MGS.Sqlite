﻿/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ColumnFieldAttribute.cs
 *  Description  :  Attribute for column field.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/7/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using System;

namespace MGS.Sqlite
{
    /// <summary>
    /// Attribute for column field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class ColumnFieldAttribute : Attribute
    {
        #region
        /// <summary>
        /// The column is primary key?
        /// [Only one primary key is set for a table;
        /// Do not modify the value of primary key once it is set]
        /// </summary>
        public bool PrimaryKey { set; get; }

        /// <summary>
        /// The column is unique?
        /// </summary>
        public bool Unique { set; get; }

        /// <summary>
        /// The column is not null?
        /// </summary>
        public bool NotNull { set; get; }

        /// <summary>
        /// The default value of column.
        /// </summary>
        public object Default { set; get; }
        #endregion
    }
}