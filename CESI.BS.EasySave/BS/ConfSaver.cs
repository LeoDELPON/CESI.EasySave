using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace CESI.BS.EasySave.BS.ConfSaver
{
    public class ConfSaver
    {
        public struct WorkVar
        {
            public string name;
            public string source;
            public string target;
            public int typeSave;
            public string key;
            public List<string> extension;
        }
        public FileStream file;
        public string savePath = Environment.CurrentDirectory + @"\saveConf\";
        public string extension = ".xml";
        
        public void SaveWork(WorkVar workvar)
        {
            byte[] header = new UTF8Encoding(true).GetBytes("<? xml version = \"1.0\" encoding = \"UTF-8\" ?>" + Environment.NewLine);

            MakeSurePathExist(workvar);
            WriteFile(workvar, header);

        }

        private void MakeSurePathExist(WorkVar workvar)
        {
            if (!FolderBuilder.CheckFolder(savePath))
            {
                FolderBuilder.CreateFolder(savePath);
            }
            if (!File.Exists(savePath + workvar.name + extension))
            {
               FileStream sr =  File.Create(savePath + workvar.name + extension);
                sr.Close();
            }
        }
        private void MakeSurePathExist()
        {
            if (!FolderBuilder.CheckFolder(savePath))
            {
                FolderBuilder.CreateFolder(savePath);
            }
        }
        public void ModifyFile(string name, int fieldChosen, string newString)
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
                    strFieldChoosenEnd = @"</name>";
                    break;
                case 2:
                    strFieldChoosen = "<source>";
                    strFieldChoosenEnd = "</source>";
                    break;
                case 3:
                    strFieldChoosen = "<target>";
                    strFieldChoosenEnd = "</target>";
                    break;
                case 4:
                    strFieldChoosen = "<typeSave>";
                    strFieldChoosenEnd = "</typeSave>";
                    break;
                case 5:
                    strFieldChoosen = "<key>";
                    strFieldChoosenEnd = "</key>";
                    break;
                case 6:
                    strFieldChoosen = "<extension>";
                    strFieldChoosenEnd = "</extension>";
                    break;
                default:
                    strFieldChoosen = "<name>";
                    strFieldChoosenEnd = "</name>";
                    break;
            }
            string text = File.ReadAllText(savePath + nameExt);
            string testMatch = strFieldChoosen + Environment.NewLine + "(.*)" + Environment.NewLine + strFieldChoosenEnd + ".*";
            

               Match mtch = Regex.Match(text, strFieldChoosen + Environment.NewLine + "(.*)" + Environment.NewLine + strFieldChoosenEnd + ".*");
            string value = mtch.Value;
            if (mtch.Success)
            {

                text=text.Replace(mtch.Value, strFieldChoosen + Environment.NewLine + newString + Environment.NewLine + strFieldChoosenEnd);
                File.WriteAllText(savePath + nameExt, text);
                if (fieldChosen == 1)
                {
                    FileSystem.Rename(savePath + nameExt, savePath + newString + extension);
                }
            }
        }

        public List<WorkVar> GetSavedWorks()
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
                        case "<key>":
                            workvar.key = sr.ReadLine();
                            break;
                        case "<extention>":
                            workvar.extension = sr.ReadLine();
                            break;
                        default:
                            Console.WriteLine(sr.ReadLine());
                            break;
                    }

                }
                listWorkVar.Add(workvar);
                sr.Close();
            }
            return listWorkVar;
        }

        public void DeleteFile(string name)
        {
            Console.WriteLine(savePath + name + extension);
            if (FileBuilder.CheckFile(savePath + name + extension))
            {
                FileBuilder.DeleteFile(savePath + name + extension);
            }
            else
            {
                Console.WriteLine("[-] There is no work with this name.");
            }
        }
        private void WriteFile(WorkVar workvar, byte[] header)
        {
            file = File.OpenWrite(savePath + workvar.name + extension);

            file.Write(header);
            file.Write(new UTF8Encoding(true).GetBytes("<name>" + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes(workvar.name + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes(@"</name>" + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes("<source>" + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes(workvar.source + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes(@"</source>" + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes("<target>" + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes(workvar.target + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes(@"</target>" + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes("<typeSave>" + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes(workvar.typeSave + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes(@"</typeSave>" + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes("<key>" + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes(workvar.key + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes(@"</key>" + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes("<extention>" + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes(workvar.extension + Environment.NewLine));
            file.Write(new UTF8Encoding(true).GetBytes(@"</extention>" + Environment.NewLine));
            file.Close();
        }

    }
}
