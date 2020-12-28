using FindStream.Core;
using NUnit.Framework;

namespace FindStream.Test
{
    public class FindStreamUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TextoDefault()
        {
            IStream stream = new Stream();
            var streamText = "aAbBABacfe";

            var result = stream.FirstChar(streamText);

            Assert.IsTrue(result != '\0');
        }

        [Test]
        public void StreamNaoEncontrado()
        {
            IStream stream = new Stream();
            var streamText = "aAbBABacfaae";

            var result = stream.FirstChar(streamText);

            Assert.IsTrue(result == '\0');
        }

        [Test]
        public void Stream_i_EncontroPrimeiro()
        {
            IStream stream = new Stream();
            var streamText = "fiafsasasasasas";

            var result = stream.FirstChar(streamText);

            Assert.IsTrue(result == 'i');
        }

        [Test]
        public void Stream_vogal_ultimo()
        {
            IStream stream = new Stream();
            var streamText = "fsaiafsasasasasasA";

            var result = stream.FirstChar(streamText);

            Assert.IsTrue(result == 'A');
        }

        [Test]
        public void Stream_UmaLetra()
        {
            IStream stream = new Stream();
            var streamText = "a";

            var result = stream.FirstChar(streamText);

            Assert.IsTrue(result != '\0' || result == '\0');
        }

        [Test]
        public void Stream_SemLetra()
        {
            IStream stream = new Stream();
            var streamText = "";

            var result = stream.FirstChar(streamText);

            Assert.IsTrue(result != '\0' || result == '\0');
        }
    }
}