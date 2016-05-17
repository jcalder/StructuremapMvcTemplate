namespace BlankMvc.TestHelpers
{
    public interface IMobileDeterminer
    {
        bool IsMobile { get; }
    }


    public class MobileDeterminer : IMobileDeterminer
    {
        public bool IsMobile
        {
            get { return true; }
        }
    }

    public class NotMobileDeterminer : IMobileDeterminer
    {
        public bool IsMobile
        {
            get { return false; }
        }
    }
}