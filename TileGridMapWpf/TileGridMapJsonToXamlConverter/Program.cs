using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorpusExplorer.Sdk.Extern.TileGridMap;
using Newtonsoft.Json;

namespace TileGridMapJsonToXamlConverter
{
  class Program
  {
    static void Main(string[] args)
    {
      try
      {
        ToXaml();
        ToCSharpCode();
      }
      catch(Exception ex)
      {
        Console.WriteLine(ex.Message);
        Console.WriteLine(ex.StackTrace);
      }
      Console.WriteLine("!END!");
      Console.ReadLine();
    }

    private static void ToCSharpCode()
    {
      var arr = JsonConvert.DeserializeObject<GridTileMap[]>(File.ReadAllText(@"TileGridMap.json"));
      arr = (from x in arr where x.Coordinates != null && x.Coordinates.Length == 2 select x).ToArray();
      
      var stb = new StringBuilder();
      stb.Append("private GridTileMap[] _data = new []{");

      foreach (var x in arr)
      {
        stb.Append("new GridTileMap{");
        stb.Append($"Name = \"{x.Name}\",");
        stb.Append($"Alpha2 = \"{x.Alpha2}\",");
        stb.Append($"Alpha3 = \"{x.Alpha3}\",");
        stb.Append($"Coordinates = new[] {{ {x.Coordinates[0]}, {x.Coordinates[1]} }},");
        stb.Append($"CountryCode = \"{x.CountryCode}\",");
        stb.Append($"Iso31662 = \"{x.Iso31662}\",");
        stb.Append($"Region = \"{x.Region}\",");
        stb.Append($"RegionCode = \"{x.RegionCode}\",");
        stb.Append($"SubRegion = \"{x.SubRegion}\",");
        stb.Append($"SubRegionCode = \"{x.SubRegionCode}\",");
        stb.Append("},");
      }

      stb.Append("};");

      File.WriteAllText("output.cs", stb.ToString(), Encoding.UTF8);
    }

    private static void ToXaml()
    {
      var arr = StaticCountryData.Data;
      var xm = arr.Select(x => x.Coordinates[0]).Concat(new[] { 0 }).Max();
      var ym = arr.Select(x => x.Coordinates[1]).Concat(new[] { 0 }).Max();

      var stb = new StringBuilder();
      stb.AppendLine("<Grid x:Name=\"World\">");
      stb.AppendLine("<Grid.ColumnDefinitions>");
      for (var i = 0; i < xm; i++)
        stb.AppendLine("<ColumnDefinition></ColumnDefinition>");
      stb.AppendLine("</Grid.ColumnDefinitions>");
      stb.AppendLine("<Grid.RowDefinitions>");
      for (var i = 0; i < ym; i++)
        stb.AppendLine("<RowDefinition></RowDefinition>");
      stb.AppendLine("</Grid.RowDefinitions>");
      for (var i = 0; i < xm; i++)
      {
        for (var j = 0; j < ym; j++)
        {
          var x = i + 1;
          var y = j + 1;

          var country = (from c in arr where c.Coordinates[0] == x && c.Coordinates[1] == y select c).FirstOrDefault();
          if (country == null)
          {
            // stb.AppendLine($"<Rectangle Grid.Column=\"{i}\" Grid.Row=\"{j}\" Fill=\"White\"/>");
          }
          else
          {
            stb.AppendLine(
              $"<Border x:Name=\"{country.Alpha2}\" BorderThickness=\"0\" BorderBrush=\"White\" Grid.Column=\"{i}\" Grid.Row=\"{j}\"><Grid Background=\"White\"/></Border>");
          }
        }
      }
      stb.AppendLine("</Grid>");

      File.WriteAllText("output.xaml", stb.ToString(), Encoding.UTF8);
    }
  }
}
