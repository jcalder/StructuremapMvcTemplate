using System;

namespace BlankMvc.TestHelpers
{
    public class FakeDependency : IFakeDependency, IDisposable
    {
        private static int _count = 0;
        private readonly int _myCount;
        private string _myStory = "";
        private bool isDisposed = false;
        public FakeDependency()
        {
            _myCount = _count;
            _count++;
        }
        public string DescibeMyLifetime()
        {
            var story = string.Format("I am a {0} generation. I have had these things happen: \n", _myCount);

            return story + _myStory;
        }

        public void AddEvent(string lifeEvent)
        {
            _myStory += lifeEvent + '\n';
        }

        private Object _disposeLock;
        public void Dispose()
        {
            if (isDisposed) throw new Exception("already disposed");
            isDisposed = true;
            _myStory += "Then I was Disposed \n";

        }
    }
}