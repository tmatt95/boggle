namespace BoggleService
{

    /// <summary>
    /// Represents the location of the letter on the boggle board.
    /// </summary>
    public class LetterLocation
    {
        /// <summary>
        /// The row number of the letter (0 based).
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// The column number of the letter (0 based).
        /// </summary>
        public int Column { get; set; }
    }
}