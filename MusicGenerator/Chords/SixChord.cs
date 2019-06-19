using System;
using System.Collections.Generic;
using System.Text;
using static MusicGenerator.Note;

namespace MusicGenerator.Chords
{
    public class SixChord : Chord
    {
		public SixChord(NoteLetter key, MajorMinor majorMinor) : base(key, majorMinor)
		{

			if (_majorMinor == MajorMinor.Major)
			{
				_baseNotes = new List<NoteLetter>(3) { AddHalfSteps(key, 9), AddHalfSteps(key, 12), AddHalfSteps(key, 16) };
			}
			else
			{
				_baseNotes = new List<NoteLetter>(3) { AddHalfSteps(key, 9), AddHalfSteps(key, 13), AddHalfSteps(key, 16) };
			}

		}
	}
}
