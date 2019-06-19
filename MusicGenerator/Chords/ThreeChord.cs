using System;
using System.Collections.Generic;
using System.Text;
using static MusicGenerator.Note;

namespace MusicGenerator.Chords
{
    public class ThreeChord : Chord
    {
		public ThreeChord(NoteLetter key, MajorMinor majorMinor) : base(key, majorMinor)
		{

			if (_majorMinor == MajorMinor.Major)
			{
				_baseNotes = new List<NoteLetter>(3) { AddHalfSteps(key, 4), AddHalfSteps(key, 7), AddHalfSteps(key, 11) };
			}
			else
			{
				_baseNotes = new List<NoteLetter>(3) { AddHalfSteps(key, 4), AddHalfSteps(key, 8), AddHalfSteps(key, 11) };
			}

		}
	}
}
