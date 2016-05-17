using System;

namespace BlankMvc.TestHelpers
{
    public class FakeDependency : IFakeDependency, IDisposable
    {
        private readonly IMobileDeterminer _mobileDeterminer;
        private static int _count = 0;
        private readonly int _myCount;
        private string _myStory = "";
        private bool _isDisposed;
        public FakeDependency(IMobileDeterminer mobileDeterminer)
        {
            _mobileDeterminer = mobileDeterminer;
            _myCount = _count;
            _count++;
        }
        public string DescibeMyLifetime()
        {
            var story = string.Format("I am a {0} generation. I am {1} mobile I have had these things happen: \n", _myCount, IsMobileString());

            return story + _myStory;
        }

        private string IsMobileString()
        {
            return _mobileDeterminer.IsMobile ? "" : "not";
        }

        public void AddEvent(string lifeEvent)
        {
            _myStory += lifeEvent + '\n';
        }

        public void Dispose()
        {
            if (_isDisposed) throw new Exception("already disposed");
            _isDisposed = true;
            _myStory += "Then I was Disposed \n";

        }
    }
}