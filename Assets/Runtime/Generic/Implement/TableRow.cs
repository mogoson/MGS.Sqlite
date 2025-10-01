/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  TableRow.cs
 *  Description  :  Generic sqlite table row base on reflection.
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
    /// Generic sqlite table row base on reflection.
    /// [A better way is implement the interface ITableRow by yourself]
    /// </summary>
    public abstract class TableRow : ViewRow, ITableRow
    {
        #region
        /// <summary>
        /// Statement of the columns.
        /// </summary>
        public string Statement { protected set; get; }

        /// <summary>
        /// Name of primary key.
        /// </summary>
        public virtual string PrimaryKey
        {
            get { return primaryField.Name; }
        }

        /// <summary>
        /// Value of primary key.
        /// </summary>
        public virtual object PrimaryValue
        {
            get { return primaryField.GetValue(this); }
        }

        /// <summary>
        /// Field info of primary field.
        /// </summary>
        protected static FieldInfo primaryField;
        #endregion

        /// <summary>
        /// Fill this table to data row.
        /// </summary>
        /// <param name="row"></param>
        public virtual void FillTo(DataRow row)
        {
            try
            {
                foreach (var field in columnFields)
                {
                    row[field.Name] = field.GetValue(this);
                }
            }
            catch (Exception ex)
            {
                SqliteLogger.LogException(ex);
            }
        }

        /// <summary>
        /// Initialize the follow infos
        /// [sqliteFields, primaryField, Statement]
        /// </summary>
        protected override void Initialize()
        {
            //Initialize the sqliteFields.
            base.Initialize();

            var columns = new List<string>();
            foreach (var field in columnFields)
            {
                var column = $"{field.Name} {field.FieldType.Name.ToUpper()}";
                var atrbt = field.GetCustomAttribute<ColumnFieldAttribute>(false);
                if (atrbt.PrimaryKey)
                {
                    primaryField = field;
                    column += $" {SqliteConst.PRIMARY_KEY}";
                }
                if (atrbt.Unique)
                {
                    column += $" {SqliteConst.UNIQUE}";
                }
                if (atrbt.NotNull)
                {
                    column += $" {SqliteConst.NOT_NULL}";
                }
                if (atrbt.Default != null)
                {
                    column += $" {SqliteConst.DEFAULT} {atrbt.Default}";
                }
                columns.Add(column);
            }
            Statement = $"({string.Join(", ", columns.ToArray())})";

            if (primaryField == null)
            {
                var message = $"Can not find the primary field in class {this}";
                message += $", you can use {nameof(ColumnFieldAttribute)} with 'PrimaryKey=true' to mark e field as primary field.";
                throw new NullReferenceException(message);
            }
        }
    }
}