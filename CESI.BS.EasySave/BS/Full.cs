using EasySave.Business.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave.Business.BS
{
    public class Full : Save
    {
        public override void perform()
        {
            throw new NotImplementedException();
        }
        override
     public string getName()
        {
            return Language.getFullName();
        }
    }

}