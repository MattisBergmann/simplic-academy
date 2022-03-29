using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace CodingChallenge
{
    public class PuzzleChallenge
    {
        [Fact]
        public void Puzzle_01()
        {
            // Task: Write an algorithm, that places all forms inside the board.
            // Rules: 1. Place all forms in the board
            //        2. The positions must be calculated by an algorithm, do not set statix/fixed positions!
            //        3. Different chars must not overlap
            //        4. Insert all forms into completedBoard
            //        5. Attach the generated (completed) board as string below in your explanation

            var board =
@"
xxxxxxxxxxxxxx
x    x       x
x            x
x       x    x
x  x         x
xxxxxxxxxxxxxx";

            var form0 =
@"
y
y
y
y";

            var form1 =
            @"
ww
ww";

            var form2 =
            @"
vvvv
   v";

            var form3 =
            @"
aaaa
a";

            var form4 =
            @"
 g 
ggg";


            board = Regex.Replace(board, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            form0 = Regex.Replace(form0, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            form1 = Regex.Replace(form1, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            form2 = Regex.Replace(form2, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            form3 = Regex.Replace(form3, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            form4 = Regex.Replace(form4, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);

            var completedBoard = board;

            // <---- ---- ---- add code here ---- ---- ---->

            var aboard = StringFormToArrayForm(completedBoard);
            var aform0 = StringFormToArrayForm(form0);
            var aform1 = StringFormToArrayForm(form1);
            var aform2 = StringFormToArrayForm(form2);
            var aform3 = StringFormToArrayForm(form3);
            var aform4 = StringFormToArrayForm(form4);

            var aforms = new char[][,] { aform0, aform1, aform2, aform3, aform4 };

            var compl = PlaceAll(aforms, aboard);
            completedBoard = ArrayFormToStringForm(compl);

            // @Tanyel Fixed 'Select' to be 'Where'
            Assert.Equal(board.Where(x => x == 'x').Count(), completedBoard.Where(x => x == 'x').Count());
            Assert.True(ContainsForm(form0, completedBoard));
            Assert.True(ContainsForm(form1, completedBoard));
            Assert.True(ContainsForm(form2, completedBoard));
            Assert.True(ContainsForm(form3, completedBoard));
            Assert.True(ContainsForm(form4, completedBoard));

            // Your explantation:
            // xxxxxxxxxxxxxx
            // xyww xvvvv g x
            // xywwaaaa vgggx
            // xy  a   x    x
            // xy x         x
            // xxxxxxxxxxxxxx
        }

        /// <summary>
        /// Returns all valid locations where form can be placed in board
        /// </summary>
        /// <param name="form"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        private int[,] GetPlacements(char[,] form, char[,] board)
        {
            var list = new LinkedList<int[]>();
            for (var i = 0; i < board.GetLength(0); i++)
            {
                for (var j = 0; j < board.GetLength(1); j++)
                {
                    if (Fits(form, board, j, i))
                    {
                        list.AddLast(new int[] { j, i });
                    }
                }
            }
            var placements = new int[list.Count, 2];
            foreach (var (value, i) in list.Select((value, i) => (value, i)))
            {
                placements[i, 0] = value[0];
                placements[i, 1] = value[1];
            }
            return placements;
        }

        /// <summary>
        /// Tries to place all forms inside of board
        /// </summary>
        /// <param name="forms"></param>
        /// <param name="placements"></param>
        /// <param name="board"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="FormPlacementException"></exception>
        private char[,] PlaceAll(char[][,] forms, char[,] board, int[][,] placements = null, int index = 0)
        {
            if (placements == null)
            {
                for (var i = 0; i < forms.Length; i++)
                    placements[i] = GetPlacements(forms[i], board);
            }
            if (index >= forms.Length) return board;
            var form = forms[index];
            var placement = placements[index];
            for (var i = 0; i < placement.GetLength(0); i++)
            {
                try
                {
                    char[,] board2 = Place(form, board, placement[i, 0], placement[i, 1]);
                    return PlaceAll(forms, board2, placements, index + 1);
                }
                catch
                {
                }
            }
            throw new FormPlacementException("Form could not be placed anywhere");
        }

        private char[,] Place(char[,] form, char[,] board, int x, int y)
        {
            var board2 = board.Clone() as char[,];
            for (var i = 0; i < form.GetLength(0); i++)
            {
                for (var j = 0; j < form.GetLength(1); j++)
                {
                    if (form[i, j] != ' ' && board2[y + i, x + j] != ' ')
                    {
                        throw new FormPlacementException($"Form does not fit.");
                    }
                    board2[y + i, x + j] = form[i, j];
                }
            }
            return board2;
        }

        /// <summary>
        /// Checks if the form fits into the board at the spicified location
        /// </summary>
        /// <param name="form"></param>
        /// <param name="board"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool Fits(char[,] form, char[,] board, int x, int y)
        {
            if (y + form.GetLength(0) > board.GetLength(0)) return false;
            if (x + form.GetLength(1) > board.GetLength(1)) return false;

            for (var i = 0; i < form.GetLength(0); i++)
            {
                for (var j = 0; j < form.GetLength(1); j++)
                {
                    if (form[i, j] != ' ' && board[y + i, x + j] != ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Converts a string to a form array
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        private char[,] StringFormToArrayForm(string form)
        {
            form = form.Replace("\r", "");
            var lines = form.Split('\n');
            var x = 0;
            var y = lines.Length;
            foreach (var line in lines)
            {
                if (line.Length > x)
                {

                    x = line.Length;
                }
            }
            var result = new char[y, x];
            for (var i = 0; i < y; i++)
            {
                var line = lines[i];
                for (var j = 0; j < x; j++)
                {
                    if (j < line.Length)
                    {
                        result[i, j] = line[j];
                    }
                    else
                    {
                        result[i, j] = ' ';
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Converts a form array into a string
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        private string ArrayFormToStringForm(char[,] form)
        {
            StringBuilder builder = new StringBuilder();
            for (var i = 0; i < form.GetLength(0); i++)
            {
                for (var j = 0; j < form.GetLength(1); j++)
                {
                    builder.Append(form[i, j]);
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }

        private bool ContainsForm(string form, string board)
        {
            form = form.Replace("\r", "");
            form = form.Replace("\n", "");
            form = new string(form.Where(c => !char.IsWhiteSpace(c)).ToArray());

            if (form.Any())
            {
                var letter = form[0];
                var letterCount = board.Where(x => x == letter).Count();
                return letterCount == form.Length;
            }

            return true;
        }

        /// <summary>
        /// Thrown when placing a form fails
        /// </summary>
        public class FormPlacementException : Exception
        {
            internal FormPlacementException(string msg) : base(msg)
            {
            }
        }
    }
}
