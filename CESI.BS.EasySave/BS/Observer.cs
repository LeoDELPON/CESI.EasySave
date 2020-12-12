using CESI.BS.EasySave.DTO;


namespace CESI.BS.EasySave.BS
{
    public interface Observer
    {

   
            public void reactProgression(double progress);
            public void reactDataLogServ(DTOLogger dto);
    }

}

