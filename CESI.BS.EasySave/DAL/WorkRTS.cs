using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.BS.EasySave.DAL
{
    public class WorkRTS
    {
        public string Id { get; private set; }
        public string Name { get; set; }
        public DateTime TimeStamp { get; set; }
        public string CurrentFile { get; set; }
        public uint EligibleFiles { get; private set; }
        public uint RemainingFiles { get; private set; }
        public long Progress { get; private set; }
        public long Size { get; set; }
        public long RemainingSize { get; set; }

        public WorkRTS(Work work)
        {
            Id = work.Id.ToString();
            Name = work.Name;
            CurrentFile = work.CurrentFile;
            TimeStamp = DateTime.Now;
            EligibleFiles = work.EligibleFiles;
            RemainingFiles = work.RemainingFiles;
            Progress = work.Progress;
            Size = work.Size;
            RemainingSize = work.RemainingSize;
        }
    }
}
