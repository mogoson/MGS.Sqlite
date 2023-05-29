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
        /// Fill this object to data row.
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
                var column = string.Format("{0} {1}", field.Name, field.FieldType.Name.ToUpper());
                var atrbt = field.GetCustomAttributes(typeof(ColumnFieldAttribute), false)[0] as ColumnFieldAttribute;
                if (atrbt.PrimaryKey)
                {
                    primaryField = field;
                    column += string.Format(" {0}", SqliteConst.PRIMARY_KEY);
                }
                if (atrbt.Unique)
                {
                    column += string.Format(" {0}", SqliteConst.UNIQUE);
                }
                if (atrbt.NotNull)
                {
                    column += string.Format(" {0}", SqliteConst.NOT_NULL);
                }
                if (atrbt.Default != null)
                {
                    column += string.Format(" {0} {1}", SqliteConst.DEFAULT, atrbt.Default);
                }
                columns.Add(column);
            }
            Statement = string.Format("({0})", string.Join(",", columns.ToArray()));

            if (primaryField == null)
            {
                var message = string.Format("Can not find the primary field in class {0}", this);
                message += string.Format(", you can use {0} with 'PrimaryKey=true' to mark e field as primary field.", typeof(ColumnFieldAttribute));
                throw new NullReferenceException(message);
            }
        }
    }
}