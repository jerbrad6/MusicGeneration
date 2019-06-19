using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Adapted from http://www.cs.princeton.edu/courses/archive/fall07/cos126/assignments/guitar.html
/// </summary>
namespace MusicGenerator
{
    public class GuitarString
    {
		private readonly double _decayFactor;
		private double _frequency;
		private RingBuffer _buffer;
		private Random _random;
		private int _tics;

		public GuitarString(double frequency, int samplingRate = 44100, double decayFactor = .99)
		{
			_frequency = frequency;
			_decayFactor = decayFactor;
			_buffer = new RingBuffer(samplingRate / (int)_frequency);
			_random = new Random();
			_tics = 0;
			while (!_buffer.IsFull())
			{
				_buffer.Enqueue(0);
			}
		}

		public void Pluck()                         // set the buffer to white noise
		{
			_buffer.Clear();
			while (!_buffer.IsFull())
			{
				_buffer.Enqueue(_random.NextDouble() - .5);
			}
		}
		public void Tic()                           // advance the simulation one time step
		{
			var top = _buffer.Dequeue();
			var next = _buffer.Peek();
			_buffer.Enqueue((top + next) * _decayFactor / 2);
			_tics++;
		}
		public double Sample()                        // return the current sample
		{
			return _buffer.Peek();
		}
		public int Time()                          // return number of tics
		{
			return _tics;
		}

	}
}
