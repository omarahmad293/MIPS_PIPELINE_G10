using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyVerilog
{
    public static class OpenedFilesHandles
    {
        //Full path of files opened
        private static List<string> OpenedFiles = new List<string>();

        //Names only of the files opened
        public static List<string> OpenedFilesNames = new List<string>();

        //Text inside the files opened, not saved
        public static List<string> OpenedFilesText = new List<string>();

        /// <summary>
        /// Add the opened file to the opened files list
        /// </summary>
        /// <param name="fullname">Path included</param>
        /// <param name="text">Text inside</param>
        public static void AddOpenFile(string fullname, string text)
        {
            string[] splittedName = fullname.Split('\\');

            //All of them share the same index
            OpenedFiles.Add(fullname);
            OpenedFilesNames.Add(splittedName[splittedName.Count() - 1]);
            OpenedFilesText.Add(text);
        }

        public static string GetFullName(int index)
        {
            return OpenedFiles[index];
        }
        public static int OpenedFilesCount
        {
            get
            {
                return OpenedFiles.Count();
            }
        }
        public static string GetText(int index)
        {
            return OpenedFilesText[index];
        }

        public static void SaveText(int index, string text)
        {
            if (index == -1)
            {
                //OpenedFilesText[0] = text;
                return;
            }
            else
            {
                OpenedFilesText[index] = text;
            }
        }

        public static void CloseFile(int index)
        {
            OpenedFiles.Remove(OpenedFiles[index]);
            OpenedFilesNames.Remove(OpenedFilesNames[index]);
            OpenedFilesText.Remove(OpenedFilesText[index]);
        }
    }
}
