using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.BS.EasySave.DAL
{

    public class Work
    {
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
            get { return RemainingSize; }
            set
            {
                RemainingSize = value;
                Progress = Size == 0 ? 100 : Convert.ToInt64(100 - (Convert.ToDouble(value) / Convert.ToDouble(Size) * 100));
            }
        }
    }