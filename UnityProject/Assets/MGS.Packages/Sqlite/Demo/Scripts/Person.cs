/*************************************************************************
 *  Copyright (c) 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Person.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  7/11/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Data;

namespace MGS.Sqlite
{
    /// <summary>
    /// Implement the ITableRow interface, the simple way is to inherit from TableRow.
    /// </summary>
    public class Person : TableRow
    {
        //Must mark a PrimaryKey.
        [ColumnField(PrimaryKey = true)]
        public int id;

        [ColumnField]
        public string name;
    }

    /// <summary>
    /// TableRow is Implemented base on reflection,
    /// so a better way is implement the ITableRow interface by yourself.
    /// </summary>
    public class CustomPerson : ITableRow
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
}