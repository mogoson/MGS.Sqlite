/*************************************************************************
 *  Copyright © 2025 Mogoson All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SqliteSchemaBuilder.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  11/22/2025
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Reflection;

namespace MGS.Sqlite
{
    /// <summary>
    /// Builder for sqlite schema.
    /// </summary>
    public sealed class SqliteSchemaBuilder
    {
        /// <summary>
        /// Build sqlite table columns schema for store data row that type is T.
        /// </summary>
        /// <typeparam name="T">Type of table row.</typeparam>
        /// <param name="tableName">Name of table.</param>
        /// <returns></returns>
        public static string BuildTableSchema<T>(string tableName)
        {
            var type = typeof(T);
            var fields = type.GetFields();
            if (fields.Length == 0)
            {
                var message = $"Can not find any field in Type {type}";
                throw new NullReferenceException(message);
            }

            var columns = new List<string>();
            FieldInfo primaryKeyField = null;
            foreach (var field in fields)
            {
                if (!field.IsDefined(typeof(SqliteFieldAttribute)))
                {
                    continue;
                }

                var columnSchema = new List<string>
                {
                    $"{field.Name} {field.FieldType.Name.ToUpper()}"
                };
                var atrbt = field.GetCustomAttribute<SqliteFieldAttribute>();
                if (atrbt.PrimaryKey)
                {
                    primaryKeyField = field;
                    columnSchema.Add($" {SqliteConst.PRIMARY_KEY}");
                }
                if (atrbt.Unique)
                {
                    columnSchema.Add($" {SqliteConst.UNIQUE}");
                }
                if (atrbt.NotNull)
                {
                    columnSchema.Add($" {SqliteConst.NOT_NULL}");
                }
                if (atrbt.Default != null)
                {
                    columnSchema.Add($" {SqliteConst.DEFAULT} {atrbt.Default}");
                }
                columns.Add(string.Join(" ", columnSchema));
            }

            if (columns.Count == 0)
            {
                var message = $"Can not find any column field in Type {type}";
                message += $", you can use {nameof(SqliteFieldAttribute)} to mark a field as column field.";
                throw new NullReferenceException(message);
            }

            if (primaryKeyField == null)
            {
                var message = $"Can not find the primary key field in Type {type}";
                message += $", you can use {nameof(SqliteFieldAttribute)} with 'PrimaryKey=true' to mark a field as primary key field.";
                throw new NullReferenceException(message);
            }

            return $"{tableName}({string.Join(", ", columns)})";
        }
    }
}