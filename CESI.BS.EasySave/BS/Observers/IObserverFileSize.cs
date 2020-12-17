namespace CESI.BS.EasySave.BS
{
    public interface IObserverFileSize
    {
        public void React(Save savetype);
        public void EndReaction(Save savetype);
    }
}
