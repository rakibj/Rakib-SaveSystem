using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace SaveSystem.Core
{
    public class Saver : MonoBehaviour
    {
        private FileSystem _fileSystem;
        public Action OnInitialize;
        public Action OnSave;

        private void Awake()
        {
            _fileSystem = new FileSystem();
            OnInitialize?.Invoke();
        }
        public void SaveEntityDatabase<T>(EntityTable<T> table)
        {
            var path = _fileSystem.GetSavePathForID(table.uniqueId);
            var json = JsonUtility.ToJson(table);
            _fileSystem.WriteToPath(path, json);
        }

        public EntityTable<T> LoadEntityDatabase<T>(string uniqueId)
        {
            var path = _fileSystem.GetSavePathForID(uniqueId);
            var json = _fileSystem.ReadFromPath(path);
            var table = JsonUtility.FromJson<EntityTable<T>>(json);
            return table;
        }
        
        public EntityTable<T> LoadEntityDatabase<T>(string uniqueId, EntityTable<T> defaultTable)
        {
            var entityTable = defaultTable;

            if (_fileSystem.HasSavePathForID(uniqueId))
            {
                entityTable = LoadEntityDatabase<T>(uniqueId);
                Debug.Log("Path exists, loading value");
            }
            else
            {
                SaveEntityDatabase(entityTable);
                Debug.Log("Path does not exist, loading default");
            }

            return entityTable;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if(!hasFocus) OnSave?.Invoke();
        }
    }
}