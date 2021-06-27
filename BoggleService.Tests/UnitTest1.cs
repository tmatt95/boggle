using Xunit;

namespace BoggleService.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void EmptyGuess()
        {
            Assert.False(new BoggleService().Guess(""));
        }

        [Fact]
        public void NullGuess()
        {
            Assert.False(new BoggleService().Guess(null));
        }

        [Fact]
        public void ValidGuess()
        {
            var testValidGuesses = new[] { "BINGO", "LINGO", "ILNBIA"  };
            foreach (var guess in testValidGuesses)
            {
                Assert.True(new BoggleService().Guess(guess));
            }
        }

        [Fact]
        public void InvalidGuess()
        {
            var testInvalidGuesses = new[] {"BUNGIE", "BINS", "SINUS" };
            foreach (var guess in testInvalidGuesses)
            {
                Assert.False(new BoggleService().Guess(guess));
            }
        }
    }
}
