using System.Collections.Generic;

namespace BoggleService
{
    public class Answer
    {
        public Answer(List<LetterLocation> usedLetterLocations)
        {
            this.UsedLetterLocations = usedLetterLocations;
        }

        /// <summary>
        /// The letter the row and column are linked to.
        /// </summary>
        public char Letter { get; set; }

        /// <summary>
        /// Index of letter in the word (0 based).
        /// </summary>
        public int LetterIndex { get; set; }

        public LetterLocation LetterLocation { get; set; }

        /// <summary>
        /// Used to keep track of the locations we have used on the board.
        /// </summary>
        public List<LetterLocation> UsedLetterLocations { get; set; }
    }
}