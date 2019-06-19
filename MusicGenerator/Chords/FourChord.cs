using System;
using System.Collections.Generic;
using System.Text;
using static MusicGenerator.Note;

namespace MusicGenerator.Chords
{
    public class FourChord : Chord
    {
		public FourChord(NoteLetter key, MajorMinor majorMinor) : base(key, majorMinor)
		{

			if (_majorMinor == MajorMinor.Major)
			{
				_baseNotes = new List<NoteLetter>(3) { AddHalfSteps(key, 5), AddHalfSteps(key, 9), AddHalfSteps(key, 12) };
			}
			else
			{
				_baseNotes = new List<NoteLetter>(3) { AddHalfSteps(key, 5), AddHalfSteps(key, 8), AddHalfSteps(key, 12) };
			}

		}
	}
}
