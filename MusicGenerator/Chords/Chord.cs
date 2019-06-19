using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static MusicGenerator.Note;

namespace MusicGenerator.Chords
{
	public abstract class Chord : IChord
	{
		protected NoteLetter _key;
		protected MajorMinor _majorMinor;
		protected IList<NoteLetter> _baseNotes;
		protected IList<NoteLetter> _addedNotes;

		public Chord()
		{
			_baseNotes = new List<NoteLetter>();
			_addedNotes = new List<NoteLetter>();
		}

		public Chord(NoteLetter key, MajorMinor majorMinor)
		{
			_key = key;
			_majorMinor = majorMinor;
			_baseNotes = new List<NoteLetter>();
			_addedNotes = new List<NoteLetter>();
		}

		public IList<NoteLetter> GetNotes()
		{
			return _baseNotes.Union(_addedNotes).ToList();
		}

		public IList<Note> GetNotes(Inversion inversion, short lowestOctave)
		{
			var notes = new List<Note>(_baseNotes.Count);
			NoteLetter prevNote = NoteLetter.C; // Set to lowest note so first note in chord gets lowest octave
			short currentOctave = lowestOctave;
			for (int i = 0; i < _baseNotes.Count; ++i)
			{
				var noteLetter = _baseNotes[((int)inversion + i) % _baseNotes.Count];
				if (prevNote > noteLetter)
				{
					currentOctave++;
				}
				notes.Add(new Note(noteLetter, currentOctave));
				prevNote = noteLetter;
			}
			return notes;
		}

		public void Add(params int[] adds)
		{
			//TODO fix this for whole steps not half steps
			foreach (var addNote in adds) {
				_addedNotes.Add(AddWholeSteps(_baseNotes[0], addNote));
			}
		}

		public void Suspend(int sus)
		{
			switch(sus)
			{
				case 2:
					_baseNotes[1] = AddHalfSteps(_baseNotes[0], 2);
					break;
				case 4:
					_baseNotes[1] = AddHalfSteps(_baseNotes[0], 5);
					break;
				default:
					throw new ArgumentException($"Unsupported sus chord: {sus}");
			}
		}
	}

	public enum MajorMinor
	{
		Major = 0,
		Minor = 1
	}
}
