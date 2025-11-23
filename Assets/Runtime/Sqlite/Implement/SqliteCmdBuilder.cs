/*************************************************************************
 *  Copyright © 2025 Mogoson All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SqliteCmdBuilder.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  11/18/2025
 *  Description  :  Initial development version.
 *************************************************************************/

#if !UNITY_STANDALONE

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Mono.Data.Sqlite;

namespace MGS.Sqlite
{
    public class SqliteCmdBuilder : ISqliteCmdBuilder
    {
        protected SqliteDataAdapter adapter;
        protected DataTable dataTable = new();

        protected SqliteCommand insertCmd;
        protected SqliteCommand updateCmd;
        protected SqliteCommand deleteCmd;

        public SqliteCmdBuilder(SqliteDataAdapter adapter)
        {
            this.adapter = adapter;
            adapter.FillSchema(dataTable, SchemaType.Source);
        }

        public SqliteCommand GetInsertCommand()
        {
            insertCmd ??= BuildInsertCommand();
            return insertCmd;
        }

        public SqliteCommand GetUpdateCommand()
        {
            updateCmd ??= BuildUpdateCommand();
            return updateCmd;
        }

        public SqliteCommand GetDeleteCommand()
        {
            deleteCmd ??= BuildDeleteCommand();
            return deleteCmd;
        }

        protected virtual SqliteCommand BuildInsertCommand()
        {
            var insertColumns = new List<DataColumn>();
            foreach (DataColumn column in dataTable.Columns)
            {
                if (column.AutoIncrement)
                {
                    continue;
                }
                insertColumns.Add(column);
            }

            var columnNames = new List<string>();
            var parameterNames = new List<string>();
            foreach (var column in insertColumns)
            {
                columnNames.Add($"`{column.ColumnName}`");
                parameterNames.Add($"@{column.ColumnName}");
            }
            var tableName = dataTable.TableName;
            var columns = string.Join(", ", columnNames);
            var values = string.Join(", ", parameterNames);
            var cmdTex = string.Format(SqliteConst.CMD_INSERT_FORMAT, tableName, columns, values);

            return BuildCommand(cmdTex, insertColumns);
        }

        protected virtual SqliteCommand BuildUpdateCommand()
        {
            var primaryKeys = GetPrimaryKeys();
            var allColumns = dataTable.Columns.Cast<DataColumn>();
            var nonKeyColumns = allColumns.Where(column => !primaryKeys.Contains(column));

            var setClauses = new List<string>();
            foreach (var column in nonKeyColumns)
            {
                setClauses.Add($"`{column.ColumnName}` = @{column.ColumnName}");
            }

            var whereClauses = new List<string>();
            foreach (var primaryKey in primaryKeys)
            {
                whereClauses.Add($"`{primaryKey}` = @{primaryKey}");
            }

            var tableName = dataTable.TableName;
            var setTex = string.Join(", ", setClauses);
            var whereTex = string.Join(" AND ", whereClauses);
            var cmdTex = string.Format(SqliteConst.CMD_UPDATE_FORMAT, tableName, setTex, whereTex);

            return BuildCommand(cmdTex, allColumns);
        }

        protected virtual SqliteCommand BuildDeleteCommand()
        {
            var primaryKeys = GetPrimaryKeys();
            var whereClauses = new List<string>();
            foreach (var primaryKey in primaryKeys)
            {
                whereClauses.Add($"`{primaryKey}` = @{primaryKey}");
            }

            var tableName = dataTable.TableName;
            var whereTex = string.Join(" AND ", whereClauses);
            var cmdTex = string.Format(SqliteConst.CMD_DELETE_FORMAT, tableName, whereTex);

            return BuildCommand(cmdTex, primaryKeys);
        }

        protected SqliteCommand BuildCommand(string commandText, IEnumerable<DataColumn> columns)
        {
            var command = BuildCommand(commandText);
            var parameter = BuildParameter(columns);
            command.Parameters.AddRange(parameter.ToArray());
            return command;
        }

        protected SqliteCommand BuildCommand(string commandText)
        {
            return new SqliteCommand(commandText, adapter.SelectCommand.Connection);
        }

        protected DataColumn[] GetPrimaryKeys()
        {
            var primaryKeys = dataTable.PrimaryKey;
            if (primaryKeys == null || primaryKeys.Length == 0)
            {
                primaryKeys = new DataColumn[] { dataTable.Columns[0] };
            }
            return primaryKeys;
        }

        protected ICollection<SqliteParameter> BuildParameter(IEnumerable<DataColumn> columns)
        {
            var parameters = new List<SqliteParameter>();
            foreach (var column in columns)
            {
                var parameter = BuildParameter(column);
                parameters.Add(parameter);
            }
            return parameters;
        }

        protected SqliteParameter BuildParameter(DataColumn column)
        {
            var parameterName = $"@{column.ColumnName}";
            var parameter = new SqliteParameter(parameterName)
            {
                SourceColumn = column.ColumnName
            };
            return parameter;
        }

        public virtual void Dispose()
        {
            dataTable?.Dispose();
            insertCmd?.Dispose();
            updateCmd?.Dispose();
            deleteCmd?.Dispose();
        }
    }
}
#endif