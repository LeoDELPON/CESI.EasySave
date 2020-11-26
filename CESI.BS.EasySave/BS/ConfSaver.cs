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
        public static string extention = ".xml";
        
        public static void SaveWork(WorkVar workvar)
        {
            byte[] header = new UTF8Encoding(true).GetBytes("<? xml version = \"1.0\" encoding = \"UTF-8\" ?>" + Environment.NewLine);

            MakeSurePathExist();
            WriteFile(workvar, header);

        }

        private static void MakeSurePathExist(WorkVar workvar)
        {
            if (!FolderBuilder.CheckFolder(savePath))
            {
                FolderBuilder.CreateFolder(savePath);
            }
            if (!File.Exists(savePath + workvar.name + extention))
            {
                FileStream sr = File.Create(savePath + workvar.name + extention);
                sr.Close();               

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
            string nameExt = name + extention;
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
                File.WriteAllText(savePath + nameExt, text);
            }
        }

        public static List<WorkVar> GetSavedWorks()
        {
            MakeSurePathExist();
            string[] files = Directory.GetFiles(savePath);
            List<WorkVar> listWorkVar = new List<WorkVar>();
            StreamReader sr;
            string s = "";
            foreach (string fileStr in files)
            {
                WorkVar workvar = new WorkVar();
                sr  = File.OpenText(fileStr);
                while ((s = sr.ReadLine()) != null){
                    switch (s)
                    {
                        case "<name>":
                            s = sr.ReadLine();
                            workvar.name = s;
                            break;
                        case "<source>":
                            s = sr.ReadLine();
                            workvar.source = s;
                            break;
                        case "<target>":
                            s = sr.ReadLine();
                            workvar.target = s;
                            break;
                        case "<typeSave>":
                            s = sr.ReadLine();
                            workvar.typeSave = int.Parse(s);
                            break;
                        default:
                            break;
                    }

                }
                listWorkVar.Add(workvar);
                sr.Close();

            }
            
            return listWorkVar;
        }
        private static void WriteFile(WorkVar workvar, byte[] header)
        {

            try
            {
                
                file = File.Create(savePath + workvar.name + extention);
                
            }
            catch (Exception e)
            {
                Console.WriteLine("impossible d'ouvrir le fichier \n"+e.Message);
            
            }
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
