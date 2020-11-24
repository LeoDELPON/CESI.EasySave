using CESI.BS.EasySave.BS;
using CESI.BS.EasySave.BS.Interface;
using CESI.BS.EasySave.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.BS.EasySave.BS
{

    public class Work : IWork
    {
        private Save _saveType;
        private long remainingSize;

        public Guid Id { get; private set; }
        public WorkState State { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }
        public string CurrentFile { get; set; }
        public uint EligibleFiles { get; private set; }
        public uint RemainingFiles { get; private set; }
        public long Progress { get; private set; }
        public long Size { get; set; }
        public long RemainingSize
        {
            get { return remainingSize; }
            set
            {
                remainingSize = value;
                Progress = Size == 0 ? 100 : Convert.ToInt64(100 - (Convert.ToDouble(value) / Convert.ToDouble(Size) * 100));
            }
        }
        public double Duration { get; set; }
        public string SaveType
        {
            get { return _saveType.TypeSave.ToString(); }
        }

        internal Work(string name, string source, string destination, Save saveType)
        {
            Id = Guid.NewGuid();
            State = WorkState.Exist;
            _saveType = saveType;
            Name = name;
            Source = source;
            Target = destination;
            CurrentFile = null;
            RemainingFiles = 0;
            EligibleFiles = 0;
            Progress = 0;
            Size = 0;
            RemainingSize = 0;
        }
    }
}