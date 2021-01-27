using CESI.BS.EasySave.BS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace EasySave_1._2.MainMenuClasses
{
	class BlckngPrcssModifierUI : Printer, IMainMenuMethodNonVoid
	{
		Process[] processCollection;
		public BlckngPrcssModifierUI(BSEasySave bs, PrintManager pm) : base(bs, pm)
		{

		}
		public List<string> Perform()
		{
			PrintRunningProcesses();
			return SelectBlockingProcesses();
		}

		private void PrintRunningProcesses()
		{
			processCollection = Process.GetProcesses();
			int i = 0;
			foreach (Process p in processCollection)
			{
				i++;
				Console.WriteLine(i + ") " + p.ProcessName);
			}
		}

		private List<string> SelectBlockingProcesses()
		{
			List<int> intList = new List<int>();
			List<string> stringList = new List<string>();
			IntReturn selectedInt;
			selectedInt.correct = false;
			Console.WriteLine(pm.GetPrintable("BlockingProcessesMenu"));
			do
			{
				selectedInt = GetIntFromUser(0, processCollection.Length-1, "");
				intList.Add(selectedInt.value);
			} while (selectedInt.value!=0);
			int i = 0;
			foreach (Process p in processCollection)
			{
				i++;
                if (i == intList[0])
                {
					intList.RemoveRange(0,1);
					stringList.Add(p.ProcessName);
                }
			}

			return stringList;
		}
	}
}
