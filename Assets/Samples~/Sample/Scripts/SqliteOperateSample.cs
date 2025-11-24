/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SqliteOperateSample.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  7/11/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace MGS.Sqlite.Sample
{
    public class DeveloperRowSample
    {
        //Must use 'SqliteField' to mark fields if you need create table for it runtime.

        [SqliteField(PrimaryKey = true)]
        public int id;

        [SqliteField]
        public string name;
    }

    public class SqliteOperateSample : MonoBehaviour
    {
        GenericDataBase dataBase;
        IGenericTable<DeveloperRowSample> table;
        ICollection<DeveloperRowSample> developers;

        string upd_ID = string.Empty;
        string upd_Name = string.Empty;
        string ins_ID = string.Empty;
        string ins_Name = string.Empty;
        string del_ID = string.Empty;
        Vector2 pos;

        private void Awake()
        {
            var dbFile = $"{Application.persistentDataPath}/DataBase/TestDB.db";
            if (!File.Exists(dbFile))
            {
                GenericDataBase.CreateFile(dbFile);
            }
            Debug.Log(dbFile);
            dataBase = new GenericDataBase(dbFile);

            var tableName = "table_developer";
            table = dataBase.SelectTable<DeveloperRowSample>(tableName);
            if (table == null)
            {
                dataBase.CreateTable<DeveloperRowSample>(tableName);
                table = dataBase.SelectTable<DeveloperRowSample>(tableName);
            }

            developers = table.Select();
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical(GUILayout.Width(Screen.width));

            #region
            GUILayout.BeginHorizontal();
            ins_ID = GUILayout.TextField(ins_ID);
            ins_Name = GUILayout.TextField(ins_Name);
            if (GUILayout.Button("Insert Row"))
            {
                InsertRow(ins_ID, ins_Name);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            upd_ID = GUILayout.TextField(upd_ID);
            upd_Name = GUILayout.TextField(upd_Name);
            if (GUILayout.Button("Update Row"))
            {
                UpdateRow(upd_ID, upd_Name);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            del_ID = GUILayout.TextField(del_ID);
            if (GUILayout.Button("Delete Row"))
            {
                DeleteRow(del_ID);
            }
            GUILayout.EndHorizontal();

            if (developers != null && developers.Count > 0)
            {
                pos = GUILayout.BeginScrollView(pos, "Box");
                foreach (var item in developers)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(item.id.ToString());
                    GUILayout.Label(item.name);
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndScrollView();
            }
            #endregion

            GUILayout.EndVertical();
        }

        void InsertRow(string pID, string pName)
        {
            table.Insert(new DeveloperRowSample { id = int.Parse(pID), name = pName });
            var lines = table.Commit();
            developers = table.Select();

            Debug.LogFormat("Insert lines {0}", lines);
        }

        void UpdateRow(string id, string name)
        {
            var developer = developers.First(dev => dev.id.ToString() == id);
            table.Update(developer);
            var lines = table.Commit();
            developers = table.Select();

            Debug.LogFormat("Update lines {0}", lines);
        }

        void DeleteRow(object key)
        {
            table.Delete(del_ID);
            var lines = table.Commit();
            developers = table.Select();

            Debug.LogFormat("Delete lines {0}", lines);
        }
    }
}