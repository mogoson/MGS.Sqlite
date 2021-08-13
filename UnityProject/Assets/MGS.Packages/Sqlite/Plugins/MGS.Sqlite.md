[TOC]

﻿# MGS.Sqlite.dll

## Summary

- Generic sqlite data view and table for custom data class.

## Environment

- .Net Framework 3.5 or above.

## Dependence

- System.dll
- System.Data.dll
- Mono.Data.Sqlite.dll
- MGS.Logger.dll

## Demand

- Create data base file, view table runtime.
- Select rows from view as DataTable and parse to custom data structure.
- Select, Insert, Update, Delete table rows, use custom data structure.

## Implemented

- Sqlite.

  ```C#
  public sealed class SqliteConst{}
  
  public class SqliteDataBase : ISqliteDataBase{}
  
  public class SqliteHandler : ISqliteHandler{}
  
  public class SqliteTable : SqliteView, ISqliteTable{}
  
  public class SqliteView : ISqliteView{}
  ```
- Generic.

  ```C#
  [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
  public class ColumnFieldAttribute : Attribute{}
  
  public class GenericDataBase : SqliteDataBase, IGenericDataBase{}
  
  public class GenericTable<T> : GenericView<T>, IGenericTable<T> where T : ITableRow, new(){}
  
  public class GenericView<T> : IGenericView<T> where T : IViewRow, new(){}
  
  public abstract class TableRow : ViewRow, ITableRow{}
  
  public abstract class ViewRow : IViewRow{}
  ```

## Usage

### Native

- Relate to sqlite file.

  ```C#
  //Relate to data base file, this operate will create the file if it does not exists.
  var dbFile = string.Format("{0}/TestDB.db", Environment.CurrentDirectory);
  var dataBase = new GenericDataBase(dbFile);
  ```

- Operate sqlite table.

  ```c#
  //Create table if not exists, this operate will create data base file if not exists.
  var statement = "table_t0(id INT32 PRIMARY KEY UNIQUE NOT NULL,name STRING NOT NULL)"
  var lines = dataBase.CreateTable(statement);
  
  //Select table form data base.
  var table = dataBase.SelectTable("table_t0");
  
  //Select rows form table.
  var dataTable = table.Select();//Select * from table.
  //var dataTable = table.Select(cmd);//Use custom cmd to select.
  
  //Insert new row.
  dataTable.Rows.Add(0, "name");//Add new row.
  var lines = table.Update(dataTable);//Commit to sqlite table.
  
  //Update row.
  var row_0 = dataTable.Rows[0];
  row_0.BeginEdit();
  row_0["name"] = "test update name";
  row_0.EndEdit();
  var lines = table.Update(dataTable);//Commit to sqlite table.
  
  //Delete row.
  dataTable.Rows[0].Delete();
  var lines = table.Update(dataTable);//Commit to sqlite table.
  
  //Delete table.
  var lines = dataBase.DeleteTable("table_t0");
  ```

- Operate sqlite view.

  ```C#
  //Create view if not exists.
  var lines = dataBase.CreateView("view_v0 as select name from table_t0");
  
  //Select rows form view.
  var view = dataBase.SelectView("view_v0");
  var dataTable = view.Select();
  
  //Delete view.
  var lines = dataBase.DeleteView("view_v0");
  ```
  
- Operate sqlite trigger.

  ```C#
  //Create sqlite trigger if not exists.
  var statement = "trigger_t0 AFTER INSERT ON table_t0 BEGIN INSERT INTO table_t1(id, salary)VALUES(new.ID, 0); END";
  var lines = dataBase.CreateTrigger(statement);
  
  //Create sqlite trigger if not exists.
  var name = "trigger_t0";
  var when = SqliteConst.AFTER;
  var action = SqliteConst.INSERT;
  var table = "table_t0";
  string scope = null;//SqliteConst.FOR_EACH_ROW
  string where = null;//id>=0
  var code = "INSERT INTO table_t1(id, salary)VALUES(new.ID, 0)";
  var lines = dataBase.CreateTrigger(name, when, action, table, scope, where, code);
  
  //Delete the trigger from data base.
  var lines = dataBase.DeleteTrigger("trigger_t0");
  ```

### Expand

- Custom TableRow.

  ```C#
  //Implement the ITableRow interface, the simple way is to inherit from TableRow.
  public class PersonT : TableRow
  {
      //Must mark a PrimaryKey.
      [ColumnField(PrimaryKey = true)]
      public int id;
  
      [ColumnField]
      public string name;
  }
  
  //TableRow is Implemented base on reflection,
  //so a better way is implement the ITableRow interface by yourself.
  public class PersonT : ITableRow
  {
      public int id;
      public string name;
  
      public string Statement
      {
          get { return "(id INT32 PRIMARY KEY UNIQUE NOT NULL,name STRING NOT NULL)"; }
      }
  
      public string PrimaryKey
      {
          get { return "id"; }
      }
  
      public object PrimaryValue
      {
          get { return id; }
      }
  
      public void FillFrom(DataRow row)
      {
          id = (int)row["id"];
          name = row["name"].ToString();
      }
  
      public void FillTo(DataRow row)
      {
          row["id"] = id;
          row["name"] = name;
      }
  }
  ```
  
- Custom ViewRow.

  ```C#
  //Implement the IViewRow interface, the simple way is to inherit from ViewRow.
  public class PersonV : ViewRow
  {
      //Do not need mark PrimaryKey.
      [ColumnField]
      public int id;
  
      [ColumnField]
      public string name;
  }
  
  //ViewRow is Implemented base on reflection,
  //so a better way is implement the IViewRow interface by yourself.
  public class PersonV : IViewRow
  {
      public int id;
      public string name;
      
      public void FillFrom(DataRow row)
      {
          id = (int)row["id"];
          name = row["name"].ToString();
      }
  }
  ```

- Use GenericDataBase for custom TableRow.

  ```C#
  //Create table if not exists.
  var lines = dataBase.CreateTable<PersonT>("table_person");
  
  //Select table from data base.
  var table = dataBase.SelectTable<PersonT>("table_person");
  
  //Insert new row.
  table.Insert(new PersonT() { id = 0, name = "name" });
  var lines = table.Commit();//Commit to sqlite table.
  
  //Select rows from table as custom structure.
  var persons = table.Select();
  var enumer = persons.GetEnumerator();
  enumer.MoveNext();
  var person_0 = enumer.Current;
  
  //person_0.id = 100;//person_0.id is primary key, modify it's value not supported.
  person_0.name = "Test update name";
  table.Update(person_0);
  var lines = table.Commit();//Commit to sqlite table.
  
  //Delete row from table.
  table.Delete(person_0);
  //table.Delete(person_0.id);//Use the value if primary key to delete row is ok.
  var lines = table.Commit();//Commit to sqlite table.
  ```

- Use GenericDataBase for custom ViewRow.

  ```C#
  //Create view if not exists.
  var lines = dataBase.CreateView("view_person as select * from table_person");
  
  //Select view from data base.
  var view = dataBase.SelectView<PersonT>("view_person");
  
  //Select rows from view as custom structure.
  var persons = view.Select();
  ```

------

[Previous](../../README.md)

------

Copyright © 2021 Mogoson.	mogoson@outlook.com