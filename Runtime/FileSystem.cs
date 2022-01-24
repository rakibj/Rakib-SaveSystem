using System.IO;
using UnityEngine;

namespace SaveSystem.Core
{
    public class FileSystem
    {
        private string _savePath;
        private const string SAVE_FILE_APPEND = ".json";

        public FileSystem()
        {
            GetDefaultSavePath();
        }

        public void WriteToPath(string path, string json)
        {
            var streamWriter = new StreamWriter(path);
            streamWriter.Write(json);
            streamWriter.Close();
        }

        public string ReadFromPath(string path)
        {
            using var reader = new StreamReader(path);
            var json = reader.ReadToEnd();
            return json;
        }

        public string GetSavePathForID(string uniqueId)
        {
            var path = GenerateFilePath(uniqueId);
            var fileStream = new FileStream(path, FileMode.OpenOrCreate);
            fileStream.Close();
            return path;
        }

        public bool HasSavePathForID(string uniqueId)
        {
            var path = GenerateFilePath(uniqueId);
            var pathExists = File.Exists(path);
            Debug.Log(path + " exist state: " + pathExists);
            return pathExists;
        }

        private void GetDefaultSavePath()
        {
            var separator = Path.AltDirectorySeparatorChar;
            var devicePath = Application.persistentDataPath + separator;
            //CreateDirectory(devicePath);

            _savePath = devicePath;

#if UNITY_EDITOR
            var appendToPath = separator + Application.productName + separator;
            var editorPath = Application.dataPath + appendToPath;
            CreateDirectory(editorPath);
            _savePath = editorPath;
#endif

            Debug.Log("_savePath: " + _savePath);
        }

        private void CreateDirectory(string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }

        private string GenerateFilePath(string uniqueId)
        {
            return _savePath + uniqueId + SAVE_FILE_APPEND;
        }
    }
}