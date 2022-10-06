using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CorpusExplorer.Sdk.Extern.TileGridMap
{
  public class EasyCountryCodeMapper
  {
    private readonly Dictionary<string, string> _dic;

    /// <summary>
    ///   Initializes a new instance of the <see cref="EasyCountryCodeMapper" /> class.
    ///   The csv-file must contains 2 column (original -> alpha3 code)
    /// </summary>
    /// <param name="path">Path to mapping csv data.</param>
    /// <param name="encoding">The encoding.</param>
    /// <param name="valueSeparator">The value separator.</param>
    /// <param name="rowSeparator">The row separator.</param>
    public EasyCountryCodeMapper(string path, Encoding encoding = null, string valueSeparator = ";",
                                 string rowSeparator = "\r\n")
    {
      _dic = new Dictionary<string, string>();
      var seperator = new[] { rowSeparator };
      var lines = File.ReadAllText(path, encoding ?? Encoding.UTF8)
                      .Split(seperator, StringSplitOptions.RemoveEmptyEntries);
      var separatorValue = new[] { valueSeparator };

      foreach (var line in lines)
      {
        var cells = line.Split(separatorValue, StringSplitOptions.RemoveEmptyEntries);
        if (cells.Length != 2)
          continue;
        if (_dic.ContainsKey(cells[0]))
          continue;
        _dic.Add(cells[0], cells[1]);
      }
    }

    public string GetCountryCode(string key)
    {
      return _dic.ContainsKey(key) ? _dic[key] : null;
    }
  }
}