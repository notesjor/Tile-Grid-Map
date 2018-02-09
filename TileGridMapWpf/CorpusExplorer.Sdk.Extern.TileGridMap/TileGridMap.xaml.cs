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

namespace CorpusExplorer.Sdk.Extern.TileGridMap
{
  /// <summary>
  /// Interaktionslogik für TileGridMapAlpha3.xaml
  /// </summary>
  public partial class TileGridMap : UserControl
  {
    private GridTileMap[] _data;

    public TileGridMap()
    {
      InitializeComponent();
      _data = StaticCountryData.Data;
    }

    /// <summary>
    /// Gets all country names.
    /// </summary>
    /// <value>All country names.</value>
    public IEnumerable<string> AllCountryNames => _data.Select(x => x.Name);

    /// <summary>
    /// Gets all country alpha2.
    /// </summary>
    /// <value>All country alpha2.</value>
    public IEnumerable<string> AllCountryAlpha2 => _data.Select(x => x.Alpha2);

    /// <summary>
    /// Gets all country alpha3.
    /// </summary>
    /// <value>All country alpha3.</value>
    public IEnumerable<string> AllCountryAlpha3 => _data.Select(x => x.Alpha3);

    /// <summary>
    /// Gets all country regions.
    /// </summary>
    /// <value>All country regions.</value>
    public IEnumerable<string> AllCountryRegions => new HashSet<string>(_data.Select(x => x.Region));

    /// <summary>
    /// Gets all country sub regions.
    /// </summary>
    /// <value>All country sub regions.</value>
    public IEnumerable<string> AllCountrySubRegions => new HashSet<string>(_data.Select(x => x.SubRegion));

    /// <summary>
    /// Gets all country region codes.
    /// </summary>
    /// <value>All country region codes.</value>
    public IEnumerable<int> AllCountryRegionCodes => new HashSet<int>(_data.Select(x => int.Parse(x.RegionCode)));

    /// <summary>
    /// Gets all country sub region codes.
    /// </summary>
    /// <value>All country sub region codes.</value>
    public IEnumerable<int> AllCountrySubRegionCodes => new HashSet<int>(_data.Select(x => int.Parse(x.SubRegionCode)));

    /// <summary>
    /// Gets all countries of region.
    /// </summary>
    /// <param name="region">The region.</param>
    /// <returns>IEnumerable&lt;System.String&gt;.</returns>
    public IEnumerable<string> GetAllCountriesOfRegion(string region) => _data.Where(x => x.Region == region).Select(x => x.Alpha3);

    /// <summary>
    /// Gets all countries of sub region.
    /// </summary>
    /// <param name="subRegion">The sub region.</param>
    /// <returns>IEnumerable&lt;System.String&gt;.</returns>
    public IEnumerable<string> GetAllCountriesOfSubRegion(string subRegion) => _data.Where(x => x.SubRegion == subRegion).Select(x => x.Alpha3);

    /// <summary>
    /// Gets all countries of region code.
    /// </summary>
    /// <param name="regionCode">The region code.</param>
    /// <returns>IEnumerable&lt;System.String&gt;.</returns>
    public IEnumerable<string> GetAllCountriesOfRegionCode(string regionCode) => _data.Where(x => x.RegionCode == regionCode).Select(x => x.Alpha3);

    /// <summary>
    /// Gets all countries of sub region code.
    /// </summary>
    /// <param name="subRegionCode">The sub region code.</param>
    /// <returns>IEnumerable&lt;System.String&gt;.</returns>
    public IEnumerable<string> GetAllCountriesOfSubRegionCode(string subRegionCode) => _data.Where(x => x.SubRegionCode == subRegionCode).Select(x => x.Alpha3);

    /// <summary>
    /// Gets all countries of region code.
    /// </summary>
    /// <param name="regionCode">The region code.</param>
    /// <returns>IEnumerable&lt;System.String&gt;.</returns>
    public IEnumerable<string> GetAllCountriesOfRegionCode(int regionCode) => GetAllCountriesOfRegionCode(regionCode.ToString());

    /// <summary>
    /// Gets all countries of sub region code.
    /// </summary>
    /// <param name="subRegionCode">The sub region code.</param>
    /// <returns>IEnumerable&lt;System.String&gt;.</returns>
    public IEnumerable<string> GetAllCountriesOfSubRegionCode(int subRegionCode) => GetAllCountriesOfSubRegionCode(subRegionCode.ToString());

    /// <summary>
    /// Set the color for all NoCountry-cells
    /// </summary>
    public Brush NoCountry
    {
      get => World.Background;
      set => World.Background = value;
    }

    /// <summary>
    /// Adds the country names.
    /// </summary>
    /// <param name="fontSize">Size of the font.</param>
    /// <param name="foreground">The foreground.</param>
    /// <param name="background">The background.</param>
    /// <returns>IEnumerable&lt;TextBlock&gt;.</returns>
    public IEnumerable<TextBlock> AddCountryNames(double fontSize, Brush foreground, Brush background) => GenerateLabel(x => x.Name, fontSize, foreground, background);

    /// <summary>
    /// Adds the country alpha2.
    /// </summary>
    /// <param name="fontSize">Size of the font.</param>
    /// <param name="foreground">The foreground.</param>
    /// <param name="background">The background.</param>
    /// <returns>IEnumerable&lt;TextBlock&gt;.</returns>
    public IEnumerable<TextBlock> AddCountryAlpha2(double fontSize, Brush foreground, Brush background) => GenerateLabel(x => x.Alpha2, fontSize, foreground, background);

    /// <summary>
    /// Adds the country alpha3.
    /// </summary>
    /// <param name="fontSize">Size of the font.</param>
    /// <param name="foreground">The foreground.</param>
    /// <param name="background">The background.</param>
    /// <returns>IEnumerable&lt;TextBlock&gt;.</returns>
    public IEnumerable<TextBlock> AddCountryAlpha3(double fontSize, Brush foreground, Brush background) => GenerateLabel(x => x.Alpha3, fontSize, foreground, background);

    /// <summary>
    /// Adds the country region.
    /// </summary>
    /// <param name="fontSize">Size of the font.</param>
    /// <param name="foreground">The foreground.</param>
    /// <param name="background">The background.</param>
    /// <returns>IEnumerable&lt;TextBlock&gt;.</returns>
    public IEnumerable<TextBlock> AddCountryRegion(double fontSize, Brush foreground, Brush background) => GenerateLabel(x => x.Region, fontSize, foreground, background);

    /// <summary>
    /// Adds the country subregion.
    /// </summary>
    /// <param name="fontSize">Size of the font.</param>
    /// <param name="foreground">The foreground.</param>
    /// <param name="background">The background.</param>
    /// <returns>IEnumerable&lt;TextBlock&gt;.</returns>
    public IEnumerable<TextBlock> AddCountrySubRegion(double fontSize, Brush foreground, Brush background) => GenerateLabel(x => x.SubRegion, fontSize, foreground, background);

    private delegate string AddLabelDelegate(GridTileMap item);

    /// <summary>
    /// Generates the label foreach country.
    /// </summary>
    /// <param name="func">The function.</param>
    /// <param name="fontSize">Size of the font.</param>
    /// <param name="foreground">The foreground.</param>
    /// <param name="background">The background.</param>
    /// <returns>IEnumerable&lt;TextBlock&gt;.</returns>
    private IEnumerable<TextBlock> GenerateLabel(AddLabelDelegate func, double fontSize, Brush foreground, Brush background)
    {
      var res = new List<TextBlock>();
      foreach (var x in _data)
      {
        var grid = GetCountryGrid(x.Alpha3);
        if (grid == null)
          continue;
        grid.ToolTip = new MapToolTip { Label = func(x) };
        var tb = new TextBlock
        {
          Text = func(x),
          FontSize = fontSize,
          VerticalAlignment = VerticalAlignment.Top,
          HorizontalAlignment = HorizontalAlignment.Stretch,
          TextAlignment = TextAlignment.Center,
          Foreground = foreground,
          Background = background
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
      (from x in _data where x.Alpha2 == alpha2.ToUpper() select x.Alpha3).FirstOrDefault();

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
      (from x in _data where x.Alpha3 == alpha3.ToUpper() select x.Region).FirstOrDefault();

    /// <summary>
    /// Get the sub region information
    /// </summary>
    /// <param name="alpha3">alpha3</param>
    /// <returns>sub region (engl. name)</returns>
    public string GetSubRegion(string alpha3) =>
      (from x in _data where x.Alpha3 == alpha3.ToUpper() select x.SubRegion).FirstOrDefault();

    /// <summary>
    /// Get the region code
    /// </summary>
    /// <param name="alpha3">alpha3</param>
    /// <returns>region code</returns>
    public int GetRegionCode(string alpha3) =>
      (from x in _data where x.Alpha3 == alpha3.ToUpper() select int.Parse(x.RegionCode)).FirstOrDefault();

    /// <summary>
    /// Get the sub region code
    /// </summary>
    /// <param name="alpha3">alpha3</param>
    /// <returns>sub region code</returns>
    public int GetSubRegionCode(string alpha3) =>
      (from x in _data where x.Alpha3 == alpha3.ToUpper() select int.Parse(x.SubRegionCode)).FirstOrDefault();

    /// <summary>
    /// Get the Grid-UserControl (for full ui customization)
    /// </summary>
    /// <param name="alpha3">alpha3</param>
    /// <returns>Grid</returns>
    public Grid GetCountryGrid(string alpha3) =>
      World.Children.OfType<Border>().FirstOrDefault(b => b.Name == alpha3.ToUpper())?.Child as Grid;

    /// <summary>
    /// Set all country borders at once
    /// </summary>
    /// <param name="brush">Brush</param>
    /// <param name="thickness">border thickness</param>
    public void SetAllCountryBorder(Brush brush, double thickness = 1)
    {
      foreach (var b in World.Children.OfType<Border>())
      {
        b.BorderBrush = brush;
        b.BorderThickness = new Thickness(thickness);
      }
    }

    /// <summary>
    /// Set the border of a specific country
    /// </summary>
    /// <param name="alpha3">alpah3</param>
    /// <param name="brush">Brush</param>
    /// <param name="thickness">border thickness</param>
    public void SetCountryBorder(string alpha3, Brush brush, double thickness = 1)
    {
      var border = World.Children.OfType<Border>().FirstOrDefault(b => b.Name == alpha3.ToUpper());
      if (border == null)
        return;
      border.BorderBrush = brush;
      border.BorderThickness = new Thickness(thickness);
    }

    /// <summary>
    /// Set the border of all specific countries
    /// </summary>
    /// <param name="alpha3">alpah3</param>
    /// <param name="brush">Brush</param>
    /// <param name="thickness">border thickness</param>
    public void SetCountryBorder(IEnumerable<string> alpha3, Brush brush, double thickness = 1)
    {
      foreach (var x in alpha3)
        SetCountryBorder(x, brush, thickness);
    }

    /// <summary>
    /// Set all country backgrounds at once
    /// </summary>
    /// <param name="brush">Brush</param>
    public void SetAllCountryBackground(Brush brush)
    {
      foreach (var b in World.Children.OfType<Border>())
        b.Background = brush;
    }

    /// <summary>
    /// Set the background of a specific country
    /// </summary>
    /// <param name="alpha3">alpah3</param>
    /// <param name="brush">Brush</param>
    public void SetCountryBackground(string alpha3, Brush brush)
    {
      var border = World.Children.OfType<Border>().FirstOrDefault(b => b.Name == alpha3.ToUpper());
      if (border != null)
        ((Grid)border.Child).Background = brush;
    }

    /// <summary>
    /// Set the background of all specific countries
    /// </summary>
    /// <param name="alpha3">alpah3</param>
    /// <param name="brush">Brush</param>
    public void SetCountryBackground(IEnumerable<string> alpha3, Brush brush)
    {
      foreach (var x in alpha3)
        SetCountryBackground(x, brush);
    }

    /// <summary>
    /// Set the value of a specific country
    /// </summary>
    /// <param name="alpha3">alpah3</param>
    /// <param name="value">Value</param>
    public void SetCountryValue(string alpha3, double value)
    {
      var grid = World.Children.OfType<Border>().FirstOrDefault(b => b.Name == alpha3.ToUpper())?.Child as Grid;
      if (grid == null) 
        return;
      
      if (grid.ToolTip == null)
        grid.ToolTip = new MapToolTip {Label = alpha3, Value = value.ToString()};
      else
        ((MapToolTip) grid.ToolTip).Value = value.ToString();
    }

    /// <summary>
    /// Fixes the inner border.
    /// </summary>
    /// <param name="thickness">The thickness.</param>
    public void FixInnerBorder(double thickness)
    {
      var items = World.Children.OfType<Border>().Select(b => new Tuple<int, int, Border>(Grid.GetColumn(b), Grid.GetRow(b), b)).ToArray();
      foreach (var item in items)
      {
        var lft = item.Item1 != 0 && (from x in items where x.Item1 == item.Item1 - 1 && x.Item2 == item.Item2 select x).FirstOrDefault() != null;
        var rht = item.Item1 < World.ColumnDefinitions.Count && (from x in items where x.Item1 == item.Item1 + 1 && x.Item2 == item.Item2 select x).FirstOrDefault() != null;
        var top = item.Item2 != 0 && (from x in items where x.Item1 == item.Item1 && x.Item2 == item.Item2 - 1 select x).FirstOrDefault() != null;
        var btm = item.Item2 < World.RowDefinitions.Count && (from x in items where x.Item1 == item.Item1 && x.Item2 == item.Item2 + 1 select x).FirstOrDefault() != null;

        item.Item3.BorderThickness = new Thickness(
          thickness * (lft ? 0.5 : 1),
          thickness * (top ? 0.5 : 1),
          thickness * (rht ? 0.5 : 1),
          thickness * (btm ? 0.5 : 1));
      }
    }
  }
}
