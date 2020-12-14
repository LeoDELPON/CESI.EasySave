using System;
using System.Collections.Generic;
using System.Text;

namespace CESI.BS.EasySave.BS
{
    public interface ObserverFileSize
    {
        public void React(Save savetype);
        public void EndReaction(Save savetype);
    }
}
