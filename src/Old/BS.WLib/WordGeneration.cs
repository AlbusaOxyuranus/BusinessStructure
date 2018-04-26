using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WordTest;

namespace XCS.WGen
{
    public static class WordGeneration
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        static extern System.IntPtr FindWindow(string lpClassName, string lpWindowName);

        private static void ShowForeground(Microsoft.Office.Interop.Word._Application app)
        {
            IntPtr handler = FindWindow(null, app.Caption);
            SetForegroundWindow(handler);
        }

        public static void OpenDoc(string testDoc, object pathOutDocuments)
        {WordDocument wordDoc;
            try
            {
                wordDoc = new WordDocument(pathOutDocuments+testDoc,true);
            }
            catch (Exception error)
            {
                //MessageBox.Show("Ошибка при открытии шаблона Word. Подробности " + error.Message);
                return;
            }

            ShowForeground(wordDoc._application);
            //wordDoc.Visible = true;

        }
        public static void Create(object pathTemplates, string testDoc, object pathOutDocuments, string s,
            Dictionary<string, string> dict)
        {
            WordDocument wordDoc = null;
            try
            {
                wordDoc = new WordDocument(pathTemplates+testDoc);
                foreach (var d in dict)
                {
                    wordDoc.ReplaceAllStrings(d.Key, d.Value);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            wordDoc.Save(pathOutDocuments+s);
            wordDoc.Close();            
            
        }
    }
}
