/*************************************************************************
 *  Copyright (c) 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SqliteOperateDemo.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  7/11/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace MGS.Sqlite
{
    [AddComponentMenu("MGS/Demo/SqliteOperateDemo")]
    public class SqliteOperateDemo : MonoBehaviour
    {
        GenericDataBase dataBase;
        IGenericTable<Person> table;
        ICollection<Person> persons;

        string upd_ID = string.Empty;
        string upd_Name = string.Empty;
        string ins_ID = string.Empty;
        string ins_Name = string.Empty;
        string del_ID = string.Empty;
        Vector2 pos;

        private void Awake()
        {
            var dbFile = string.Format("{0}/DataBase/TestDB.db", Environment.CurrentDirectory);
            dataBase = new GenericDataBase(dbFile);

            //Create table if not exists.
            var lines = dataBase.CreateTable<Person>("table_person");
            Debug.LogFormat("CreateTable lines {0}", lines);

            table = dataBase.SelectTable<Person>("table_person");
            persons = table.Select();
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

            if (persons != null && persons.Count > 0)
            {
                pos = GUILayout.BeginScrollView(pos, "Box");
                foreach (var item in persons)
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
            table.Insert(new Person { id = int.Parse(pID), name = pName });
            var lines = table.Commit();
            persons = table.Select();

            Debug.LogFormat("Insert lines {0}", lines);
        }

        void UpdateRow(string id, string name)
        {
            Person person = null;
            foreach (var item in persons)
            {
                if (item.id.ToString() == id)
                {
                    item.name = name;
                    person = item;
                }
            }

            table.Update(person);
            var lines = table.Commit();
            persons = table.Select();

            Debug.LogFormat("Update lines {0}", lines);
        }

        void DeleteRow(object key)
        {
            table.Delete(del_ID);
            var lines = table.Commit();
            persons = table.Select();

            Debug.LogFormat("Delete lines {0}", lines);
        }
    }
}