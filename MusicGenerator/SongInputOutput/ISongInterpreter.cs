using System;
using System.Collections.Generic;
using System.Text;

namespace MusicGenerator.SongInputOutput
{
    public interface ISongInterpreter
    {
		IList<Tuple<int, Note>> ConvertToNotes(string str);
		int SampleRate();
    }
}
