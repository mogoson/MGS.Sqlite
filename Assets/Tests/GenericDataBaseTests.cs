/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  GenericDataBaseTests.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/9/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using System.IO;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace MGS.Sqlite.Tests
{
    public class GenericDataBaseTests
    {
        class TestTableRow
        {
            [SqliteField(PrimaryKey = true)]
            public int id;

            [SqliteField]
            public string name;
        }

        string dbFile;
        GenericDataBase dataBase;

        string tableName = "table_test";
        IGenericTable<TestTableRow> table;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            dbFile = $"{Application.temporaryCachePath}/DataBase/TestDB.db";
            dataBase = new GenericDataBase(dbFile);
        }

        [Test]
        public void CreateDataBaseTest()
        {
            var isCreate = GenericDataBase.CreateFile(dbFile);
            Assert.IsTrue(isCreate);
            Assert.IsTrue(File.Exists(dbFile));
            Debug.Log(dbFile);
        }

        [Test]
        public void CreateTableTest()
        {
            var lines = dataBase.CreateTable<TestTableRow>(tableName);
            Assert.GreaterOrEqual(lines, 0);
        }

        [Test]
        public void SelectTableTest()
        {
            table = dataBase.SelectTable<TestTableRow>(tableName);
            Assert.IsNotNull(table);
        }

        [Test]
        public void SelectRowTest()
        {
            InsertRowTest();

            var rows = table.Select();
            Assert.IsNotNull(rows);
            Assert.Greater(rows.Count, 0);
        }

        [Test]
        public void InsertRowTest()
        {
            SelectTableTest();
            table.Insert(new TestTableRow() { id = 0, name = $"Mogoson" });
            var lines = table.Commit();
            Assert.GreaterOrEqual(lines, 0);
        }

        [Test]
        public void UpdateRowTest()
        {
            InsertRowTest();

            table.Update(new TestTableRow() { id = 0, name = "Mogoson_Update" });
            table.Commit();

            var rows = table.Select();
            Assert.AreEqual("Mogoson_Update", rows.ToArray()[0].name);
        }

        [Test]
        public void DeleteRowTest()
        {
            InsertRowTest();

            table.Delete(0);
            var lines = table.Commit();
            Assert.Greater(lines, 0);
        }
    }
}