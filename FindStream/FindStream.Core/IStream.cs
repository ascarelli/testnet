using System.Collections.Generic;

namespace FindStream.Core
{
    public interface IStream
    {
        public char GetNext();
        public bool HasNext();
        char FirstChar(string streamText);
    }
}
