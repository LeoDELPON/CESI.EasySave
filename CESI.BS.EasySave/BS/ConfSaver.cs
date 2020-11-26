using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace CESI.BS.EasySave.BS.ConfSaver
{
    public static class ConfSaver
    {
        public struct WorkVar
        {
            public string name;
            public string source;
            public string target;
            public int typeSave;
        }
        public static FileStream file;
        public static string savePath = Environment.CurrentDirectory + @"\saveConf\";
        public static string extension = ".xml";
        
        public static void SaveWork(WorkVar workvar)
        {
            byte[] header = new UTF8Encoding(true).GetBytes("<? xml version = \"1.0\" encoding = \"UTF-8\" ?>" + Environment.NewLine);

            MakeSurePathExist(workvar);
            WriteFile(workvar, header);

        }

        private static void MakeSurePathExist(WorkVar workvar)
        {
            if (!FolderBuilder.CheckFolder(savePath))
            {
                FolderBuilder.CreateFolder(savePath);
            }
            if (!File.Exists(savePath + workvar.name + extension))
            {
                File.Create(savePath + workvar.name + extension);
            }
        }
        private static void MakeSurePathExist()
        {
            if (!FolderBuilder.CheckFolder(savePath))
            {
                FolderBuilder.CreateFolder(savePath);
            }
        }
        public static void ModifyFile(string name, int fieldChosen, string newString)
        {
            string nameExt = name + extension;
            if (!File.Exists(savePath + nameExt))
            {
                return;
            }
            string strFieldChoosen ;
            string strFieldChoosenEnd;
            switch (fieldChosen)
            {
                case 1:
                    strFieldChoosen = "<name>";
                    strFieldChoosenEnd = "<\\name>";
                    break;
                case 2:
                    strFieldChoosen = "<source>";
                    strFieldChoosenEnd = "<\\source>";
                    break;
                case 3:
                    strFieldChoosen = "<target>";
                    strFieldChoosenEnd = "<\\targer>";
                    break;
                case 4:
                    strFieldChoosen = "<typeSave>";
                    strFieldChoosenEnd = "<\\typesave>";
                    break;
                default:
                    strFieldChoosen = "<name>";
                    strFieldChoosenEnd = "<\\name>";
                    break;

            }
            string text = File.ReadAllText(savePath + nameExt);
            Match mtch = Regex.Match(text, strFieldChoosen + ".+?"+ strFieldChoosenEnd);
            if (mtch.Success)
            {

                text.Replace(mtch.Value, strFieldChoosen + Environment.NewLine + newString + Environment.NewLine + strFieldChoosenEnd + Environment.NewLine);
                File.WriteAllText("test.txt", text);
            }
        }

        public static List<WorkVar> GetSavedWorks()
        {
            MakeSurePathExist();
            string[] files = Directory.GetFiles(savePath);
            List<WorkVar> listWorkVar = new List<WorkVar>();
            StreamReader sr;
            foreach (string fileStr in files)
            {
                WorkVar workvar = new WorkVar();
                sr  = File.OpenText(fileStr);
                while ((sr.ReadLine()) != null){
                    switch (sr.ReadLine())
                    {
                        case "<name>":
                            
                            workvar.name = sr.ReadLine();
                            break;
                        case "<source>":
                            workvar.source = sr.ReadLine();
                            break;
                        case "<target>":
                            workvar.target = sr.ReadLine();
                            break;
                        case "<typeSave>":
                            workvar.typeSave = int.Parse(sr.ReadLine());
                            break;
                        default:
                            Console.WriteLine(sr.ReadLine());
                            break;
                    }

                }
                listWorkVar.Add(workvar);
                
            }

            return listWorkVar;
        }
        private static void WriteFile(WorkVar workvar, byte[] header)
        {
            file = File.OpenWrite(savePath + workvar.name + extension);

            file.Write(header);
            file.Write(new UTF8Encoding(true).GetBytes("<name>" + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes(workvar.name + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes(@"<\name>" + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes("<source>" + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes(workvar.source + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes(@"<\source>" + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes("<target>" + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes(workvar.target + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes(@"<\target>" + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes("<typeSave>" + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes(workvar.typeSave + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes(@"<\typeSave>" + Environment.NewLine));
            file.Close();
        }

    }
}
