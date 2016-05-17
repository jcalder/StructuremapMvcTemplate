namespace BlankMvc.TestHelpers
{
    public interface IFakeDependency
    {
        string DescibeMyLifetime();
        void AddEvent(string lifeEvent);
    }
}