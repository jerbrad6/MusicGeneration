using System;
using System.Collections.Generic;
using System.Text;
using static MusicGenerator.Note;

namespace MusicGenerator.SongInputOutput
{
	public class DefaultSongInterpreter : ISongInterpreter
	{
		private int _sampleRate;
		private int _tempo;
		private int _timeSignature;

		public DefaultSongInterpreter(int sampleRate, int beatsPerMinute, int beatsPerMeasure)
		{
			_sampleRate = sampleRate;
			_tempo = beatsPerMinute;
			_timeSignature = beatsPerMeasure;
		}

		public IList<Tuple<int,Note>> ConvertToNotes(string str)
		{
			var parts = str.Split(' ');
			var measureParsed = Int32.TryParse(parts[0], out var measureNumber);
			var beatsParsed = double.TryParse(parts[1], out var beatsNumber);
			var noteParsed = Note.TryParse(parts[2], out var note);

			if (measureParsed && beatsParsed && noteParsed)
			{
				var sampleNum = (int)(_sampleRate * ((measureNumber * _timeSignature + beatsNumber) * 60) / (double)_tempo);
				return new List<Tuple<int, Note>>(1) { new Tuple<int, Note>(sampleNum, note)};
			}
			return null;
		}

		public int SampleRate()
		{
			return _sampleRate;
		}
	}
}
