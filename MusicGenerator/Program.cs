using System;
using System.IO;
using System.Threading;
using NAudio.Wave;

namespace MusicGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
			var guitarStrings = new GuitarString[58];
			InitializeGuitarStrings(guitarStrings);

			var filename = "AMovieScriptEnding";

			var song = new Song($@"C:\Users\jeremy.bradford\personal_workspace\MusicGenerator\MusicGenerator\Songs\{filename}.txt");
			var songNotes = song.Notes;

			var sampleRate = song.SampleRate;
			int lengthInSeconds = (int)Math.Ceiling(song.LengthInSeconds);

			byte[] raw = new byte[2 * sampleRate * lengthInSeconds];

			var noteIndex = 0;
			for (int sampleNum = 0; sampleNum < sampleRate * lengthInSeconds; ++sampleNum)
			{
				if (sampleNum % sampleRate == 0)
				{
					Console.WriteLine($"Processed {sampleNum/sampleRate} seconds");
				}
				// play notes if necessary
				while (noteIndex < songNotes.Count && songNotes[noteIndex].Item1 <= sampleNum)
				{
					guitarStrings[songNotes[noteIndex].Item2.ToPianoInt() - 16].Pluck();
					noteIndex++;
				}
				var sample = 0.0;
				for (int j= 0; j < guitarStrings.Length; ++j)
				{
					sample += guitarStrings[j].Sample();
				}
				var sampleShort = (short)(sample * Int16.MaxValue);
				var bytes = BitConverter.GetBytes(sampleShort);
				raw[sampleNum * 2] = bytes[0];
				raw[sampleNum * 2 + 1] = bytes[1];
				for (int j = 0; j < guitarStrings.Length; ++j)
				{
					guitarStrings[j].Tic();
				}
			}

			var ms = new MemoryStream(raw);
			var rs = new RawSourceWaveStream(ms, new WaveFormat(sampleRate, 16, 1));

			var outpath = $@"C:\Users\jeremy.bradford\personal_workspace\MusicGenerator\MusicGenerator\WavFiles\{filename}.wav";
			try
			{
				WaveFileWriter.CreateWaveFile(outpath, rs);
			} catch
			{
				Console.WriteLine("Please close .wav file. Press enter to continue.");
				var _ = Console.ReadLine();

				WaveFileWriter.CreateWaveFile(outpath, rs);
			}

			//var wo = new WaveOutEvent();
			//wo.Init(rs);
			//wo.Play();
			//while (wo.PlaybackState == PlaybackState.Playing)
			//{
			//	Thread.Sleep(500);
			//}
			//wo.Dispose();
		}

		public static void InitializeGuitarStrings(GuitarString[] strings)
		{
			for (int i = 0; i < strings.Length; ++i)
			{
				var frequency = Math.Pow(2, (i - strings.Length / 2) / 12.0) * 440.0; 
				strings[i] = new GuitarString(frequency);
			}
		}
    }
}
