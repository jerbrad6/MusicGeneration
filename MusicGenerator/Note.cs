using System;
using System.Collections.Generic;
using System.Text;

namespace MusicGenerator
{
    public class Note
    {
		private const int DistinctNotes = 12;
		private const char lowestNote = 'A';
		private const char highestNote = 'G';
		private static readonly Dictionary<int, int> WholeStepsToHalfSteps = new Dictionary<int, int>()
		{
			{ 0, 0 },
			{ 1, 2 },
			{ 2, 4 },
			{ 3, 5 },
			{ 4, 7 },
			{ 5, 9 },
			{ 6, 11 }
		};

		public NoteLetter Letter { get; }
		public short Octave { get; }
		public Note(NoteLetter letter, short octave)
		{
			Letter = letter;
			Octave = octave;
		}

		public static bool TryParse(string str, out Note note)
		{
			note = null;
			var letterParsed = Enum.TryParse(typeof(NoteLetter), str.Substring(0, str.Length - 1), out var letter);
			var	octave = str[str.Length - 1];
			if (letterParsed && octave >= '0' && octave <= '9')
			{
				note = new Note((NoteLetter)letter, (short)(octave - '0'));
				return true;
			}
			return false;
		}

		/// <summary>
		/// Returns this note as nth key on the piano starting from the lowest, A0
		/// </summary>
		/// <returns></returns>
		public int ToPianoInt()
		{
			return 12 * (Octave - 1) + (Letter - NoteLetter.C);
		}

		public static NoteLetter AddHalfSteps(NoteLetter note, int halfSteps)
		{
			return (NoteLetter)(((int)note + halfSteps) % DistinctNotes);
		}

		public static NoteLetter AddWholeSteps(NoteLetter note, int wholeSteps)
		{
			return AddHalfSteps(note, WholeStepsToHalfSteps[wholeSteps % 7]);
		}

		public enum NoteLetter
		{
			C = 0,
			Cs = 1,
			Db = 1,
			D = 2,
			Ds = 3,
			Eb = 3,
			E = 4,
			F = 5,
			Fs = 6,
			Gb = 6,
			G = 7,
			Gs = 8,
			Ab = 8,
			A = 9,
			As = 10,
			Bb = 10,
			B = 11,
		}
    }
}
