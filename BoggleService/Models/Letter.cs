using System.Collections.Generic;

namespace BoggleService
{
    /// <summary>
    /// Represents a letter on the board.
    /// </summary>
    public class Letter
    {
        public Letter(List<LetterLocation> usedLetterLocations)
        {
            this.UsedLetterLocations = usedLetterLocations;
        }

        /// <summary>
        /// The letter the row and column are linked to.
        /// </summary>
        public char Character { get; set; }

        /// <summary>
        /// Index of letter in the word (0 based).
        /// </summary>
        public int LetterIndex { get; set; }

        /// <summary>
        /// Location of the letter on the boggle board.
        /// </summary>
        public LetterLocation LetterLocation { get; set; }

        /// <summary>
        /// Used to keep track of the locations we have used on the board.
        /// </summary>
        public List<LetterLocation> UsedLetterLocations { get; set; }
    }
}