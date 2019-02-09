using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;

namespace EasyVerilog
{
    public static class FileHandler
    {
        private static string _projectPath;

        //public static List<string> OpenedFiles = new List<string>();

        public static void SetPath(string path)
        {
            _projectPath = path + @"\";
        }
        public static void CreateFile(string filename, string text)
        {
            if (string.IsNullOrEmpty(_projectPath))
            {
                throw new Exception("Empty or Null _projectPath");
            }
            try
            {
                if (File.Exists(_projectPath + filename))
                {
                    File.Delete(_projectPath + filename);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            //If file was deleted successfully or it wasn't found
            using (var fs = File.Create(_projectPath + filename))
            {
                Byte[] data = new UTF8Encoding(true).GetBytes(text);
                fs.Write(data, 0, data.Length);
            }
        }

        /// <summary>
        /// This deletes the text file if exists, and make a new one with the new text.
        /// Can be used for saving or creating new files.
        /// </summary>
        /// <param name="fullname">The full path to the file</param>
        /// <param name="text">The text inside</param>
        public static void CreateFileAbsolute(string fullname, string text)
        {
            try
            {
                if (File.Exists(fullname))
                {
                    File.Delete(fullname);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            //If file was deleted successfully or it wasn't found
            using (var fs = File.Create(fullname))
            {
                //Replace all { and } with begin and end
                //text = text.Replace("{", "begin");
                //text = text.Replace("}", "end"); 
                //---------
                Byte[] data = new UTF8Encoding(true).GetBytes(text);

                fs.Write(data, 0, data.Length);
            }
        }

        public static void OpenFile(string filename, out string text)
        {
            if (string.IsNullOrEmpty(_projectPath))
            {
                throw new Exception("Empty or Null _projectPath");
            }
            text = "";
            using (var sr = File.OpenText(_projectPath + filename))
            {
                text = sr.ReadToEnd();
            }
        }
        /// <summary>
        /// This takes in the full path to the file including its name.
        /// This also adds the fullname to the opened files.
        /// </summary>
        /// <param name="fullname">Full path to file including nam e</param>
        /// <param name="text">The data inside the text file</param>
        public static void OpenFileAbsolute(string fullname, out string text)
        {
            text = "";
            using (var sr = File.OpenText(fullname))
            {
                text = sr.ReadToEnd();
            }
           OpenedFilesHandles.AddOpenFile(fullname, text);
        }
    }
}