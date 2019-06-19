using System;
using System.Collections.Generic;
using System.Text;

namespace MusicGenerator
{
	public class RingBuffer
	{
		private double[] buffer;
		private int first;
		private int length;
		private int capacity;

		public RingBuffer(int cap)  // constructor to create an empty buffer, with given max capacity
		{
			buffer = new double[cap];
			capacity = cap;
			first = 0;
			length = 0;
		}

		public int Size()                    // return number of items currently in the buffer
		{
			return length;
		}

		public bool IsEmpty()                 // is the buffer empty (size equals zero)?
		{
			return length == 0;
		}
		public bool IsFull()                  // is the buffer full  (size equals capacity)?
		{                                      // how am i supposed to know
			return length == capacity;
		}
		public void Enqueue(double x)         // add item x to the end
		{                                     // rip item x
			if (!IsFull())
			{
				buffer[(first + length) % capacity] = x;
				length++;
			}
			else
			{
				throw new Exception("Buffer is already full");
			}                            // like my belly
		}
		public double Dequeue()                 // delete and return item from the front
		{                                       // item didn't stand a chance
			if (!IsEmpty())
			{
				var firstVal = buffer[first];
				first = (first + 1) % capacity;
				length--;
				return firstVal;
			}
			throw new Exception("Buffer is empty.");            // u r empty
		}
		public double Peek()                    // return (but do not delete) item from the front
		{                                       // the item is just trying to find her place in this world
			if (!IsEmpty())
			{
				return buffer[first];
			}
			throw new Exception("Buffer is empty");
		}

		public void Clear()
		{
			length = 0;
		}
	}
}
