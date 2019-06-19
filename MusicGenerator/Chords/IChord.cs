using System;
using System.Collections.Generic;
using System.Text;
using static MusicGenerator.Note;

namespace MusicGenerator.Chords
{
    public interface IChord
    {
		string ToString();
		IList<NoteLetter> GetNotes();
		IList<Note> GetNotes(Inversion inversion, short lowestOctave);
		void Suspend(int sus);
		void Add(params int[] adds);
    }
}
