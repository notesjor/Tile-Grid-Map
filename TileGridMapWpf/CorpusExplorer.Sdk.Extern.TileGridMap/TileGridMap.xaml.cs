using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace CorpusExplorer.Sdk.Extern.TileGridMap
{
  /// <summary>
  /// Interaktionslogik für TileGridMap.xaml
  /// </summary>
  public partial class TileGridMap : UserControl
  {
    private SolidColorBrush _noCountry;
    private GridTileMap[] _data;

    public TileGridMap()
    {
      InitializeComponent();
      InitializeData();
    }

    public void InitializeData(string path = "TileGridMap.json")
    {
      _data = JsonConvert.DeserializeObject<GridTileMap[]>(File.ReadAllText(path));
    }

    public IEnumerable<string> AllCountryNames => _data.Select(x => x.Name);
    public IEnumerable<string> AllCountryAlpha2 => _data.Select(x => x.Alpha2);
    public IEnumerable<string> AllCountryAlpha3 => _data.Select(x => x.Alpha3);
    public IEnumerable<string> AllCountryRegions => new HashSet<string>(_data.Select(x => x.Region));
    public IEnumerable<string> AllCountrySubRegions => new HashSet<string>(_data.Select(x => x.SubRegion));
    public IEnumerable<int> AllCountryRegionCodes => new HashSet<int>(_data.Select(x => int.Parse(x.RegionCode)));
    public IEnumerable<int> AllCountrySubRegionCodes => new HashSet<int>(_data.Select(x => int.Parse(x.SubRegionCode)));

    public IEnumerable<string> GetAllCountriesOfRegion(string region) => _data.Where(x => x.Region == region).Select(x => x.Alpha3);
    public IEnumerable<string> GetAllCountriesOfSubRegion(string subRegion) => _data.Where(x => x.SubRegion == subRegion).Select(x => x.Alpha3);
    public IEnumerable<string> GetAllCountriesOfRegionCode(string regionCode) => _data.Where(x => x.RegionCode == regionCode).Select(x => x.Alpha3);
    public IEnumerable<string> GetAllCountriesOfSubRegionCode(string subRegionCode) => _data.Where(x => x.SubRegionCode == subRegionCode).Select(x => x.Alpha3);
    public IEnumerable<string> GetAllCountriesOfRegionCode(int regionCode) => GetAllCountriesOfRegionCode(regionCode.ToString());
    public IEnumerable<string> GetAllCountriesOfSubRegionCode(int subRegionCode) => GetAllCountriesOfSubRegionCode(subRegionCode.ToString());

    /// <summary>
    /// Set the color for all NoCountry-cells
    /// </summary>
    public SolidColorBrush NoCountry
    {
      get => _noCountry;
      set
      {
        _noCountry = value;
        foreach (var x in World.Children.OfType<Rectangle>())
          x.Fill = value;
      }
    }

    public IEnumerable<TextBlock> AddCountryNames() => GenerateLabel(x => x.Name);
    public IEnumerable<TextBlock> AddCountryAlpha2() => GenerateLabel(x => x.Alpha2);
    public IEnumerable<TextBlock> AddCountryAlpha3() => GenerateLabel(x => x.Alpha3);
    public IEnumerable<TextBlock> AddCountryRegion() => GenerateLabel(x => x.Region);
    public IEnumerable<TextBlock> AddCountrySubRegion() => GenerateLabel(x => x.SubRegion);

    private delegate string AddLabelDelegate(GridTileMap item);

    private IEnumerable<TextBlock> GenerateLabel(AddLabelDelegate func)
    {
      var res = new List<TextBlock>();
      foreach (var x in _data)
      {
        var grid = GetCountryGrid(x.Alpha3);
        if (grid == null)
          continue;
        var tb = new TextBlock
        {
          Text = func(x),
          FontSize = 8,
          VerticalAlignment = VerticalAlignment.Top,
          HorizontalAlignment = HorizontalAlignment.Stretch,
          TextAlignment = TextAlignment.Center
        };
        grid.Children.Add(tb);
        res.Add(tb);
      }
      return res;
    }

    /// <summary>
    /// Convert the english country name into alpha3
    /// </summary>
    /// <param name="countryName">name of the country</param>
    /// <returns>alpha3</returns>
    public string ConvertNameToAlpha3(string countryName) =>
      (from x in _data where x.Name == countryName select x.Alpha3).FirstOrDefault();

    /// <summary>
    /// Convert the alpha2 country code into alpha3
    /// </summary>
    /// <param name="alpha2">alpha2 code</param>
    /// <returns>alpha3</returns>
    public string ConvertAlpha2ToAlpha3(string alpha2) =>
      (from x in _data where x.Alpha2 == alpha2 select x.Alpha3).FirstOrDefault();

    /// <summary>
    /// Convert the tileGridMap-country-code to alpha3
    /// </summary>
    /// <param name="countryCode">tileGridMap-country-code</param>
    /// <returns>alpha3</returns>
    public string ConvertCountryCodeToAlpha3(string countryCode) =>
      (from x in _data where x.CountryCode == countryCode select x.Alpha3).FirstOrDefault();

    /// <summary>
    /// Convert the ISO 3166-2 country code to alpha3
    /// </summary>
    /// <param name="iso">ISO 3166-2 country code</param>
    /// <returns>alpha3</returns>
    public string ConvertIso31662ToAlpha3(string iso) =>
      (from x in _data where x.Iso31662 == iso || x.Iso31662 == $"ISO 3166-2:{iso}" select x.Alpha3).FirstOrDefault();

    /// <summary>
    /// Get the region information
    /// </summary>
    /// <param name="alpha3">alpha3</param>
    /// <returns>region (engl. name)</returns>
    public string GetRegion(string alpha3) =>
      (from x in _data where x.Alpha3 == alpha3 select x.Region).FirstOrDefault();

    /// <summary>
    /// Get the sub region information
    /// </summary>
    /// <param name="alpha3">alpha3</param>
    /// <returns>sub region (engl. name)</returns>
    public string GetSubRegion(string alpha3) =>
      (from x in _data where x.Alpha3 == alpha3 select x.SubRegion).FirstOrDefault();

    /// <summary>
    /// Get the region code
    /// </summary>
    /// <param name="alpha3">alpha3</param>
    /// <returns>region code</returns>
    public int GetRegionCode(string alpha3) =>
      (from x in _data where x.Alpha3 == alpha3 select int.Parse(x.RegionCode)).FirstOrDefault();

    /// <summary>
    /// Get the sub region code
    /// </summary>
    /// <param name="alpha3">alpha3</param>
    /// <returns>sub region code</returns>
    public int GetSubRegionCode(string alpha3) =>
      (from x in _data where x.Alpha3 == alpha3 select int.Parse(x.SubRegionCode)).FirstOrDefault();

    /// <summary>
    /// Get the Grid-UserControl (for full ui customization)
    /// </summary>
    /// <param name="alpha3">alpha3</param>
    /// <returns>Grid</returns>
    public Grid GetCountryGrid(string alpha3) => 
      World.Children.OfType<Border>().FirstOrDefault(b => b.Name == alpha3)?.Child as Grid;

    /// <summary>
    /// Set all country borders at once
    /// </summary>
    /// <param name="brush">Brush</param>
    public void SetAllCountryBorder(Brush brush)
    {
      foreach (var b in World.Children.OfType<Border>())
        b.BorderBrush = brush;
    }

    /// <summary>
    /// Set the border of a specific country
    /// </summary>
    /// <param name="alpha3">alpah3</param>
    /// <param name="brush">Brush</param>
    public void SetCountryBorder(string alpha3, Brush brush)
    {
      var border = World.Children.OfType<Border>().FirstOrDefault(b => b.Name == alpha3);
      if (border != null)
        border.BorderBrush = brush;
    }

    /// <summary>
    /// Set the border of all specific countries
    /// </summary>
    /// <param name="alpha3">alpah3</param>
    /// <param name="brush">Brush</param>
    public void SetCountryBorder(IEnumerable<string> alpha3, Brush brush)
    {
      foreach (var x in alpha3)
        SetCountryBorder(x, brush);
    }
  }
}
