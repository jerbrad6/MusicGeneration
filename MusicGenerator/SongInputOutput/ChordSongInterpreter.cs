using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusicGenerator.Chords;
using static MusicGenerator.Note;

namespace MusicGenerator.SongInputOutput
{
	public class ChordSongInterpreter : ISongInterpreter
	{
		private ISongInterpreter _fallbackInterpreter;
		private NoteLetter _key;
		private MajorMinor _majorMinor;
		private int _sampleRate;
		private int _tempo;
		private int _timeSignature;
		private Dictionary<string, IChord> _chordLookup;

		public ChordSongInterpreter(NoteLetter key, MajorMinor majorMinor, int sampleRate, int beatsPerMinute, int beatsPerMeasure)
		{
			_key = key;
			_majorMinor = majorMinor;
			_sampleRate = sampleRate;
			_tempo = beatsPerMinute;
			_timeSignature = beatsPerMeasure;
			_fallbackInterpreter = new DefaultSongInterpreter(sampleRate, beatsPerMinute, beatsPerMeasure);
			SetupChordLookup();
		}
		public ChordSongInterpreter(NoteLetter key, int sampleRate, ISongInterpreter fallbackInterpreter)
		{
			_key = key;
			_sampleRate = sampleRate;
			_fallbackInterpreter = fallbackInterpreter;
		}

		private void SetupChordLookup()
		{
			_chordLookup= new Dictionary<string, IChord>()
			{
				{ "I", new OneChord(_key, _majorMinor)},
				{ "II", new TwoChord(_key, _majorMinor)},
				{ "III", new ThreeChord(_key, _majorMinor)},
				{ "IV", new FourChord(_key, _majorMinor)},
				{ "V", new FiveChord(_key, _majorMinor)},
				{ "VI", new SixChord(_key, _majorMinor)},
				{ "VII", new SevenChord(_key, _majorMinor)},
				{ "C", new OneChord(NoteLetter.C, MajorMinor.Major)},
				{ "CM", new OneChord(NoteLetter.C, MajorMinor.Minor)},
				{ "D", new OneChord(NoteLetter.D, MajorMinor.Major)},
				{ "DM", new OneChord(NoteLetter.D, MajorMinor.Minor)},
				{ "E", new OneChord(NoteLetter.E, MajorMinor.Major)},
				{ "EM", new OneChord(NoteLetter.E, MajorMinor.Minor)},
				{ "F", new OneChord(NoteLetter.F, MajorMinor.Major)},
				{ "FM", new OneChord(NoteLetter.F, MajorMinor.Minor)},
				{ "G", new OneChord(NoteLetter.G, MajorMinor.Major)},
				{ "GM", new OneChord(NoteLetter.G, MajorMinor.Minor)},
				{ "A", new OneChord(NoteLetter.A, MajorMinor.Major)},
				{ "AM", new OneChord(NoteLetter.A, MajorMinor.Minor)},
				{ "B", new OneChord(NoteLetter.B, MajorMinor.Major)},
				{ "BM", new OneChord(NoteLetter.B, MajorMinor.Minor)},
			};
		}

		public IList<Tuple<int, Note>> ConvertToNotes(string str)
		{
			var parts = str.Split(' ');
			var measureParsed = Int32.TryParse(parts[0], out var measureNumber);
			var beatsParsed = double.TryParse(parts[1], out var beatsNumber);
			var chordParsed = _chordLookup.TryGetValue(parts[2].ToUpperInvariant(), out var chord);
			//TODO parse the inversion and octave

			if (measureParsed && beatsParsed && chordParsed)
			{
				if (parts.Length > 3)
				{
					chord = ModifyChord(chord, parts.Skip(3).ToArray());
				}
				var sampleNum = (int)(_sampleRate * ((measureNumber * _timeSignature + beatsNumber) * 60) / (double)_tempo);
				var specifiedOctave = parts.FirstOrDefault(s => s.StartsWith("octave=")) ?? "";
				var inversion = parts.FirstOrDefault(s => s.StartsWith("inversion=")) ?? "";
				var notes = chord.GetNotes(inversion == "" ? Inversion.Base : (Inversion)(inversion.Last() - '0'), specifiedOctave == "" ? (short)3 : (short)(specifiedOctave.Last() - '0'));
				if (parts.Any(s => s == "strup"))
				{
					var noteTimes = new List<Tuple<int, Note>>();
					for (int i = notes.Count - 1; i >= 0; --i)
					{
						noteTimes.Add(new Tuple<int, Note>(sampleNum + (int)((notes.Count - (i+1)) * _sampleRate / 100.0), notes[i]));
					}
					return noteTimes;
				}
				if (parts.Any(s => s == "strdown"))
				{
					var noteTimes = new List<Tuple<int, Note>>();
					for (int i = 0; i < notes.Count; ++i)
					{
						noteTimes.Add(new Tuple<int, Note>(sampleNum + (int)(i * _sampleRate / 100.0), notes[i]));
					}
					return noteTimes;
				}
				return notes.Select(n => new Tuple<int, Note>(sampleNum, n)).ToList();
			}
			return _fallbackInterpreter.ConvertToNotes(str);
		}

		private IChord ModifyChord(IChord chord, params string[] modifications)
		{
			bool prevArgModifiedChord = false;
			int index = 0;
			do
			{
				var argument = modifications[index].ToLowerInvariant();
				if (argument.StartsWith("sus"))
				{
					chord.Suspend(Int32.Parse(argument.Substring(3)));
					prevArgModifiedChord = true;
				}
				if (argument.StartsWith("add"))
				{
					chord.Add(Int32.Parse(argument.Substring(3)));
					prevArgModifiedChord = true;
				}
				index++;
			} while (prevArgModifiedChord && index < modifications.Length);
			return chord;
		}

		public int SampleRate()
		{
			return _sampleRate;
		}
	}
}
