using System;
using System.IO;
using System.Linq;
using MGS.Sqlite;
using NUnit.Framework;

namespace Tests
{
    public class SqliteTests
    {
        class Person : TableRow
        {
            //Must mark a PrimaryKey.
            [ColumnField(PrimaryKey = true)]
            public int id;

            [ColumnField]
            public string name;
        }

        string dbFile;
        GenericDataBase dataBase;
        IGenericTable<Person> table;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            dbFile = $"{Environment.CurrentDirectory}/DataBase/TestDB.db";
            dataBase = new GenericDataBase(dbFile);
        }

        [Test]
        public void CreateDataBaseTest()
        {
            Assert.IsTrue(File.Exists(dbFile));
        }

        [Test]
        public void CreateTableTest()
        {
            var lines = dataBase.CreateTable<Person>("table_person");
            Assert.GreaterOrEqual(lines, 0);
        }

        [Test]
        public void SelectTableTest()
        {
            CreateTableTest();

            table = dataBase.SelectTable<Person>("table_person");
            Assert.IsNotNull(table);
        }

        [Test]
        public void SelectRowTest()
        {
            InsertRowTest();

            var persons = table.Select();
            Assert.IsNotNull(persons);
            Assert.Greater(persons.Count, 0);
        }

        [Test]
        public void InsertRowTest()
        {
            SelectTableTest();

            var pID = 0;
            var persons = table.Select();
            var count = persons == null ? 0 : persons.Count;
            if (persons != null)
            {
                pID = persons.Count;
            }

            table.Insert(new Person() { id = pID, name = $"Mogoson_{pID}" });
            var lines = table.Commit();
            Assert.Greater(lines, 0);

            persons = table.Select();
            var newCount = persons == null ? 0 : persons.Count;

            Assert.Greater(newCount, count);
        }

        [Test]
        public void UpdateRowTest()
        {
            InsertRowTest();

            var persons = table.Select();
            var id = persons.Count - 1;
            table.Update(new Person() { id = id, name = "Mogoson_Update" });
            table.Commit();

            persons = table.Select();
            Assert.AreEqual("Mogoson_Update", persons.ToArray()[persons.Count - 1].name);
        }

        [Test]
        public void DeleteRowTest()
        {
            InsertRowTest();

            var persons = table.Select();
            var count = persons.Count;
            var id = persons.Count - 1;
            table.Delete(id);
            var lines = table.Commit();
            Assert.Greater(lines, 0);

            persons = table.Select();
            var newCount = persons == null ? 0 : persons.Count;
            Assert.AreEqual(newCount, count - 1);
        }
    }
}