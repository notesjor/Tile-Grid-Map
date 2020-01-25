using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CorpusExplorer.Sdk.Extern.TileGridMap
{
  /// <summary>
  ///   Interaktionslogik für TileGridMap.xaml
  /// </summary>
  public partial class TileGridMapAlpha2 : UserControl
  {
    private readonly GridTileMap[] _data;

    public TileGridMapAlpha2()
    {
      InitializeComponent();
      _data = StaticCountryData.Data;
    }

    /// <summary>
    ///   Gets all country alpha2.
    /// </summary>
    /// <value>All country alpha2.</value>
    public IEnumerable<string> AllCountryAlpha2 => _data.Select(x => x.Alpha2);

    /// <summary>
    ///   Gets all country alpha3.
    /// </summary>
    /// <value>All country alpha3.</value>
    public IEnumerable<string> AllCountryAlpha3 => _data.Select(x => x.Alpha3);

    /// <summary>
    ///   Gets all country names.
    /// </summary>
    /// <value>All country names.</value>
    public IEnumerable<string> AllCountryNames => _data.Select(x => x.Name);

    /// <summary>
    ///   Gets all country region codes.
    /// </summary>
    /// <value>All country region codes.</value>
    public IEnumerable<int> AllCountryRegionCodes => new HashSet<int>(_data.Select(x => int.Parse(x.RegionCode)));

    /// <summary>
    ///   Gets all country regions.
    /// </summary>
    /// <value>All country regions.</value>
    public IEnumerable<string> AllCountryRegions => new HashSet<string>(_data.Select(x => x.Region));

    /// <summary>
    ///   Gets all country sub region codes.
    /// </summary>
    /// <value>All country sub region codes.</value>
    public IEnumerable<int> AllCountrySubRegionCodes => new HashSet<int>(_data.Select(x => int.Parse(x.SubRegionCode)));

    /// <summary>
    ///   Gets all country sub regions.
    /// </summary>
    /// <value>All country sub regions.</value>
    public IEnumerable<string> AllCountrySubRegions => new HashSet<string>(_data.Select(x => x.SubRegion));

    /// <summary>
    ///   Set the color for all NoCountry-cells
    /// </summary>
    public Brush NoCountry
    {
      get => World.Background;
      set => World.Background = value;
    }

    /// <summary>
    ///   Adds the country alpha2.
    /// </summary>
    /// <param name="fontSize">Size of the font.</param>
    /// <param name="foreground">The foreground.</param>
    /// <param name="background">The background.</param>
    /// <returns>IEnumerable&lt;TextBlock&gt;.</returns>
    public IEnumerable<TextBlock> AddCountryAlpha2(double fontSize, Brush foreground, Brush background)
    {
      return GenerateLabel(x => x.Alpha2, fontSize, foreground, background);
    }

    /// <summary>
    ///   Adds the country alpha3.
    /// </summary>
    /// <param name="fontSize">Size of the font.</param>
    /// <param name="foreground">The foreground.</param>
    /// <param name="background">The background.</param>
    /// <returns>IEnumerable&lt;TextBlock&gt;.</returns>
    public IEnumerable<TextBlock> AddCountryAlpha3(double fontSize, Brush foreground, Brush background)
    {
      return GenerateLabel(x => x.Alpha3, fontSize, foreground, background);
    }

    /// <summary>
    ///   Adds the country names.
    /// </summary>
    /// <param name="fontSize">Size of the font.</param>
    /// <param name="foreground">The foreground.</param>
    /// <param name="background">The background.</param>
    /// <returns>IEnumerable&lt;TextBlock&gt;.</returns>
    public IEnumerable<TextBlock> AddCountryNames(double fontSize, Brush foreground, Brush background)
    {
      return GenerateLabel(x => x.Name, fontSize, foreground, background);
    }

    /// <summary>
    ///   Adds the country region.
    /// </summary>
    /// <param name="fontSize">Size of the font.</param>
    /// <param name="foreground">The foreground.</param>
    /// <param name="background">The background.</param>
    /// <returns>IEnumerable&lt;TextBlock&gt;.</returns>
    public IEnumerable<TextBlock> AddCountryRegion(double fontSize, Brush foreground, Brush background)
    {
      return GenerateLabel(x => x.Region, fontSize, foreground, background);
    }

    /// <summary>
    ///   Adds the country subregion.
    /// </summary>
    /// <param name="fontSize">Size of the font.</param>
    /// <param name="foreground">The foreground.</param>
    /// <param name="background">The background.</param>
    /// <returns>IEnumerable&lt;TextBlock&gt;.</returns>
    public IEnumerable<TextBlock> AddCountrySubRegion(double fontSize, Brush foreground, Brush background)
    {
      return GenerateLabel(x => x.SubRegion, fontSize, foreground, background);
    }

    /// <summary>
    ///   Convert the alpha3 country code into alpha2
    /// </summary>
    /// <param name="alpha3">alpha3 code</param>
    /// <returns>alpha2</returns>
    public string ConvertAlpha3ToAlpha2(string alpha3)
    {
      return (from x in _data where x.Alpha3 == alpha3 select x.Alpha2).FirstOrDefault();
    }

    /// <summary>
    ///   Convert the tileGridMap-country-code to alpha2
    /// </summary>
    /// <param name="countryCode">tileGridMap-country-code</param>
    /// <returns>alpha2</returns>
    public string ConvertCountryCodeToAlpha2(string countryCode)
    {
      return (from x in _data where x.CountryCode == countryCode select x.Alpha2).FirstOrDefault();
    }

    /// <summary>
    ///   Convert the ISO 3166-2 country code to alpha2
    /// </summary>
    /// <param name="iso">ISO 3166-2 country code</param>
    /// <returns>alpha2</returns>
    public string ConvertIso31662ToAlpha2(string iso)
    {
      return (from x in _data where x.Iso31662 == iso || x.Iso31662 == $"ISO 3166-2:{iso}" select x.Alpha2)
       .FirstOrDefault();
    }

    /// <summary>
    ///   Convert the english country name into alpha2
    /// </summary>
    /// <param name="countryName">name of the country</param>
    /// <returns>alpha2</returns>
    public string ConvertNameToAlpha2(string countryName)
    {
      return (from x in _data where x.Name == countryName select x.Alpha2).FirstOrDefault();
    }

    /// <summary>
    ///   Fixes the inner border.
    /// </summary>
    /// <param name="thickness">The thickness.</param>
    public void FixInnerBorder(double thickness)
    {
      var items = World.Children.OfType<Border>()
                       .Select(b => new Tuple<int, int, Border>(Grid.GetColumn(b), Grid.GetRow(b), b)).ToArray();
      foreach (var item in items)
      {
        var lft = item.Item1 != 0 &&
                  (from x in items where x.Item1 == item.Item1 - 1 && x.Item2 == item.Item2 select x)
                 .FirstOrDefault() != null;
        var rht = item.Item1 < World.ColumnDefinitions.Count &&
                  (from x in items where x.Item1 == item.Item1 + 1 && x.Item2 == item.Item2 select x)
                 .FirstOrDefault() != null;
        var top = item.Item2 != 0 &&
                  (from x in items where x.Item1 == item.Item1 && x.Item2 == item.Item2 - 1 select x)
                 .FirstOrDefault() != null;
        var btm = item.Item2 < World.RowDefinitions.Count &&
                  (from x in items where x.Item1 == item.Item1 && x.Item2 == item.Item2 + 1 select x)
                 .FirstOrDefault() != null;

        item.Item3.BorderThickness = new Thickness(
                                                   thickness * (lft ? 0.5 : 1),
                                                   thickness * (top ? 0.5 : 1),
                                                   thickness * (rht ? 0.5 : 1),
                                                   thickness * (btm ? 0.5 : 1));
      }
    }

    /// <summary>
    ///   Gets all countries of region.
    /// </summary>
    /// <param name="region">The region.</param>
    /// <returns>IEnumerable&lt;System.String&gt;.</returns>
    public IEnumerable<string> GetAllCountriesOfRegion(string region)
    {
      return _data.Where(x => x.Region == region).Select(x => x.Alpha2);
    }

    /// <summary>
    ///   Gets all countries of region code.
    /// </summary>
    /// <param name="regionCode">The region code.</param>
    /// <returns>IEnumerable&lt;System.String&gt;.</returns>
    public IEnumerable<string> GetAllCountriesOfRegionCode(string regionCode)
    {
      return _data.Where(x => x.RegionCode == regionCode).Select(x => x.Alpha2);
    }

    /// <summary>
    ///   Gets all countries of region code.
    /// </summary>
    /// <param name="regionCode">The region code.</param>
    /// <returns>IEnumerable&lt;System.String&gt;.</returns>
    public IEnumerable<string> GetAllCountriesOfRegionCode(int regionCode)
    {
      return GetAllCountriesOfRegionCode(regionCode.ToString());
    }

    /// <summary>
    ///   Gets all countries of sub region.
    /// </summary>
    /// <param name="subRegion">The sub region.</param>
    /// <returns>IEnumerable&lt;System.String&gt;.</returns>
    public IEnumerable<string> GetAllCountriesOfSubRegion(string subRegion)
    {
      return _data.Where(x => x.SubRegion == subRegion).Select(x => x.Alpha2);
    }

    /// <summary>
    ///   Gets all countries of sub region code.
    /// </summary>
    /// <param name="subRegionCode">The sub region code.</param>
    /// <returns>IEnumerable&lt;System.String&gt;.</returns>
    public IEnumerable<string> GetAllCountriesOfSubRegionCode(string subRegionCode)
    {
      return _data.Where(x => x.SubRegionCode == subRegionCode).Select(x => x.Alpha2);
    }

    /// <summary>
    ///   Gets all countries of sub region code.
    /// </summary>
    /// <param name="subRegionCode">The sub region code.</param>
    /// <returns>IEnumerable&lt;System.String&gt;.</returns>
    public IEnumerable<string> GetAllCountriesOfSubRegionCode(int subRegionCode)
    {
      return GetAllCountriesOfSubRegionCode(subRegionCode.ToString());
    }

    /// <summary>
    ///   Get the Grid-UserControl (for full ui customization)
    /// </summary>
    /// <param name="alpha2">alpha2</param>
    /// <returns>Grid</returns>
    public Grid GetCountryGrid(string alpha2)
    {
      return World.Children.OfType<Border>().FirstOrDefault(b => b.Name == alpha2.ToUpper())?.Child as Grid;
    }

    /// <summary>
    ///   Get the region information
    /// </summary>
    /// <param name="alpha2">alpha2</param>
    /// <returns>region (engl. name)</returns>
    public string GetRegion(string alpha2)
    {
      return (from x in _data where x.Alpha2 == alpha2.ToUpper() select x.Region).FirstOrDefault();
    }

    /// <summary>
    ///   Get the region code
    /// </summary>
    /// <param name="alpha2">alpha2</param>
    /// <returns>region code</returns>
    public int GetRegionCode(string alpha2)
    {
      return (from x in _data where x.Alpha2 == alpha2.ToUpper() select int.Parse(x.RegionCode)).FirstOrDefault();
    }

    /// <summary>
    ///   Get the sub region information
    /// </summary>
    /// <param name="alpha2">alpha2</param>
    /// <returns>sub region (engl. name)</returns>
    public string GetSubRegion(string alpha2)
    {
      return (from x in _data where x.Alpha2 == alpha2.ToUpper() select x.SubRegion).FirstOrDefault();
    }

    /// <summary>
    ///   Get the sub region code
    /// </summary>
    /// <param name="alpha2">alpha2</param>
    /// <returns>sub region code</returns>
    public int GetSubRegionCode(string alpha2)
    {
      return (from x in _data where x.Alpha2 == alpha2.ToUpper() select int.Parse(x.SubRegionCode)).FirstOrDefault();
    }

    /// <summary>
    ///   Set all country backgrounds at once
    /// </summary>
    /// <param name="brush">Brush</param>
    public void SetAllCountryBackground(Brush brush)
    {
      foreach (var b in World.Children.OfType<Border>())
        b.Background = brush;
    }

    /// <summary>
    ///   Set all country borders at once
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
    ///   Set the background of a specific country
    /// </summary>
    /// <param name="alpha2">alpah2</param>
    /// <param name="brush">Brush</param>
    public void SetCountryBackground(string alpha2, Brush brush)
    {
      var border = World.Children.OfType<Border>().FirstOrDefault(b => b.Name == alpha2.ToUpper());
      if (border != null)
        ((Grid) border.Child).Background = brush;
    }

    /// <summary>
    ///   Set the background of all specific countries
    /// </summary>
    /// <param name="alpha2">alpha2</param>
    /// <param name="brush">Brush</param>
    public void SetCountryBackground(IEnumerable<string> alpha2, Brush brush)
    {
      foreach (var x in alpha2)
        SetCountryBackground(x, brush);
    }

    /// <summary>
    ///   Set the border of a specific country
    /// </summary>
    /// <param name="alpha2">alpah3</param>
    /// <param name="brush">Brush</param>
    /// <param name="thickness">border thickness</param>
    public void SetCountryBorder(string alpha2, Brush brush, double thickness = 1)
    {
      var border = World.Children.OfType<Border>().FirstOrDefault(b => b.Name == alpha2.ToUpper());
      if (border == null)
        return;
      border.BorderBrush = brush;
      border.BorderThickness = new Thickness(thickness);
    }

    /// <summary>
    ///   Set the border of all specific countries
    /// </summary>
    /// <param name="alpha2">alpah3</param>
    /// <param name="brush">Brush</param>
    /// <param name="thickness">border thickness</param>
    public void SetCountryBorder(IEnumerable<string> alpha2, Brush brush, double thickness = 1)
    {
      foreach (var x in alpha2)
        SetCountryBorder(x, brush, thickness);
    }

    /// <summary>
    ///   Set the value of a specific country
    /// </summary>
    /// <param name="alpha2">alpah3</param>
    /// <param name="value">Value</param>
    public void SetCountryValue(string alpha2, double value)
    {
      var grid = World.Children.OfType<Border>().FirstOrDefault(b => b.Name == alpha2.ToUpper())?.Child as Grid;
      if (grid == null)
        return;

      if (grid.ToolTip == null)
        grid.ToolTip = new MapToolTip {Label = alpha2.ToUpper(), Value = value.ToString()};
      else
        ((MapToolTip) grid.ToolTip).Value = value.ToString();
    }

    /// <summary>
    ///   Generates the label foreach country.
    /// </summary>
    /// <param name="func">The function.</param>
    /// <param name="fontSize">Size of the font.</param>
    /// <param name="foreground">The foreground.</param>
    /// <param name="background">The background.</param>
    /// <returns>IEnumerable&lt;TextBlock&gt;.</returns>
    private IEnumerable<TextBlock> GenerateLabel(AddLabelDelegate func, double fontSize, Brush foreground,
                                                 Brush background)
    {
      var res = new List<TextBlock>();
      foreach (var x in _data)
      {
        var grid = GetCountryGrid(x.Alpha2);
        if (grid == null)
          continue;
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

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
      ZoomSlider.Value = 0.8;
    }

    private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      var w = e.NewSize.Width  - e.PreviousSize.Width;
      var h = e.NewSize.Height - e.PreviousSize.Height;

      var factor = h < w ? h / e.PreviousSize.Height : w / e.PreviousSize.Width;
      if (double.IsInfinity(factor) || double.IsNaN(factor))
        return;

      ZoomSlider.Value += ZoomSlider.Value * factor;
    }

    private delegate string AddLabelDelegate(GridTileMap item);
  }
}