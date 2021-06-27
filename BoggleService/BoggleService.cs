using System.Collections.Generic;
using System.Linq;

namespace BoggleService
{
    public class BoggleService
    {
        /// <summary>
        /// Represents the boggle board.
        /// </summary>
        private readonly char[][] board =
        {
           new []{'I','L','A','W'},
            new []{'B','N','G','E'},
            new []{'I','U','A','O'},
            new []{'A','S','R','L'}
       };

        /// <summary>
        /// Called recursively with each successive letter until we either make a word or run out of letters.
        /// </summary>
        /// <param name="guess">The guess word.</param>
        /// <param name="locations">List of locations to search through.</param>
        /// <returns></returns>
        private bool AttemptAnswer(string guess, List<Letter> locations)
        {
            foreach (var nextCell in locations)
            {
                if (nextCell.LetterIndex == guess.Length - 1 || AttemptAnswer(guess, this.AttemptNextLetter(nextCell.UsedLetterLocations, guess, nextCell.LetterIndex + 1, nextCell.LetterLocation.Column, nextCell.LetterLocation.Row)))
                {
                    return true;
                }
            }
            return false;
        }

        private Letter Check(List<LetterLocation> usedLetterLocations, int col, int row, int colCheck, int rowCheck, int index, char character)
        {
            // If the row/col check characters do not match then we dont need to go any further
            if (board[rowCheck][colCheck] != character)
            {
                return null;
            }

            // Checks to see if the next next cell has been previously used. If it has we cant use it again.
            if (usedLetterLocations.Any(ull => ull.Column == colCheck && ull.Row == rowCheck))
            {
                return null;
            }

            // If we get here we can return the next cell.
            usedLetterLocations.Add(new LetterLocation() { Row = row, Column = col });
            return new Letter(usedLetterLocations)
            {
                LetterIndex = index,
                Character = character,
                LetterLocation = new LetterLocation(){
                    Column = colCheck,
                    Row = rowCheck
                }
            };
        }

        private List<Letter> AttemptNextLetter(List<LetterLocation> usedLetterLocations, string guess, int index, int column, int row)
        {
            // The character we are checking.
            var character = guess[index];

            // Will contain a list of next locations to move to.
            var output = new List<Letter>() { };

            // Check left
            if (column > 0)
            {
                var checkLeft = this.Check(usedLetterLocations, column, row, column - 1, row, index, character);
                if (checkLeft != null)
                {
                    output.Add(checkLeft);
                }
            }

            // Check Right
            if (column < (board[row].Length - 1))
            {
                var checkRight = this.Check(usedLetterLocations, column, row, column + 1, row, index, character);
                if (checkRight != null)
                {
                    output.Add(checkRight);
                }
            }

            // Check Up
            if (row > 0)
            {
                var checkUp = this.Check(usedLetterLocations, column, row, column, row - 1, index, character);
                if (checkUp != null)
                {
                    output.Add(checkUp);
                }
            }

            // Check Down
            if (row < (board.Length - 1))
            {
                var checkDown = this.Check(usedLetterLocations, column, row, column, row + 1, index, character);
                if (checkDown != null)
                {
                    output.Add(checkDown);
                }
            }

            // Check diagonal top left
            if (row > 0 && column > 0)
            {
                var checkUpLeft = this.Check(usedLetterLocations, column, row, column - 1, row - 1, index, character);
                if (checkUpLeft != null)
                {
                    output.Add(checkUpLeft);
                }
            }

            // Check diagonal top right
            if (row > 0 && column < (board[row].Length - 1))
            {
                var checkUpRight = this.Check(usedLetterLocations, column, row, column + 1, row - 1, index, character);
                if (checkUpRight != null)
                {
                    output.Add(checkUpRight);
                }
            }

            // Check diagonal bottom left
            if (row < board.Length - 1 && column > 0)
            {
                var checkDownLeft = this.Check(usedLetterLocations, column, row, column - 1, row + 1, index, character);
                if (checkDownLeft != null)
                {
                    output.Add(checkDownLeft);
                }
            }

            // Check diagonal bottom right
            if (row < board.Length - 1 && column < (board[row].Length - 1))
            {
                var checkDownRight = this.Check(usedLetterLocations, column, row, column + 1, row + 1, index, character);
                if (checkDownRight != null)
                {
                    output.Add(checkDownRight);
                }
            }

            // Return a list of all possible moves from this one
            return output;
        }

        /// <summary>
        /// Allows the player to make a guess and see if the string would be valid on a game of boggle.
        /// </summary>
        /// <param name="guess">The guess the user has made.</param>
        /// <returns>If the guess is valid.</returns>
        public bool Guess(string guess)
        {
            // Output
            bool output = false;

            // We need to ensure we have a valid guess
            if (guess == null || guess.Length <= 2)
            {
                return false;
            }

            // All letters on boggle are uppercase so this will ensure we can match eaily without having to set the option to ignore casing in every check.
            var upperCaseGuess = guess.ToUpperInvariant();

            // The first letter of the guess
            var firstLetter = upperCaseGuess[0];

            // We now need to see if the first letter if on the board
            // If we cannot find a first letter than do not go any further
            if (!board.Any(row => row.Any(col => col == firstLetter)))
            {
                return false;
            }

            // finds a first letter
            var rowIndex = 0;

            // Loops through the rows
            foreach (var row in board)
            {
                // Loops through the columns in the rows
                var colIndex = 0;
                foreach (var col in row)
                {
                    // We have a matching character we can start to see if we can make the word.
                    if (col == firstLetter)
                    {
                        // Tells the application the first letter location has been used.
                        List<LetterLocation> usedLetterLocations = new List<LetterLocation>(){
                                new LetterLocation(){
                                    Column = colIndex,
                                    Row = rowIndex
                            }
                        };

                        // We have been able to make the word.
                        if (this.AttemptAnswer(guess, this.AttemptNextLetter(usedLetterLocations, guess, 1, colIndex, rowIndex)))
                        {
                            output = true;
                            break;
                        }
                    }

                    // If we have not found a match in this column, moves onto the next one.
                    colIndex++;
                }

                // If we have found an answer then we dont need to loop through any more rows.
                if (output)
                {
                    break;
                }

                // If we have not found a match on this row, moves onto the next one.
                rowIndex++;
            }

            // We have either found the aswer or have looped through all rows.
            return output;
        }
    }
}
