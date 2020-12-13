using CESI.BS.EasySave.DAL;
using CESI.BS.EasySave.DTO;
using System.Collections.Generic;

namespace CESI.BS.EasySave.BS
{
    public interface IObserver
    {
    
        public void ReactDataLogServ(DTODataServer dict);
    }

}

