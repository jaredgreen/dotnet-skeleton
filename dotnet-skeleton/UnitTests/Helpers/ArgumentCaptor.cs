using NSubstitute;

namespace dotnet_skeleton.UnitTests.Helpers
{
    public class ArgumentCaptor<T>
    {
        public T Value { get; private set; }
        
        public T Capture()
        {
            return Arg.Is<T>(t => SaveValue(t));
        }

        private bool SaveValue(T t)
        {
            Value = t;
            return true;
        }
    }
}