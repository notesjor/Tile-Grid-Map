using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CorpusExplorer.Sdk.Extern.TileGridMap
{
  /// <summary>
  ///   Interaktionslogik für TileGridMap.xaml
  /// </summary>
  public partial class RealMap : UserControl
  {
    private readonly Dictionary<string, UIElement> _dic = new Dictionary<string, UIElement>();

    public RealMap()
    {
      InitializeComponent();
      BuildDictionary(g5838);
    }

    /// <summary>
    ///   Gets all country alpha2.
    /// </summary>
    /// <value>All country alpha2.</value>
    public IEnumerable<string> AllCountryAlpha2 => _dic.Keys;

    /// <summary>
    ///   Set the color for all NoCountry-cells
    /// </summary>
    public Brush NoCountry
    {
      get => worldmap.Background;
      set => worldmap.Background = value;
    }

    /// <summary>
    ///   Get the Grid-UserControl (for full ui customization)
    /// </summary>
    /// <param name="alpha2">alpha2</param>
    /// <returns>Grid</returns>
    public UIElement GetCountryGrid(string alpha2)
    {
      return _dic[alpha2.ToLower()];
    }

    /// <summary>
    ///   Set all country backgrounds at once
    /// </summary>
    /// <param name="brush">Brush</param>
    public void SetAllCountryBackground(Brush brush)
    {
      foreach (var c in _dic)
      {
        if (c.Value is Path)
        {
          var p = c.Value as Path;
          p.Fill = brush;
          p.StrokeThickness = 0.3;
          p.Stroke = new SolidColorBrush(Colors.Black);
        }

        if (c.Value is Canvas)
          SetCanvasFill((Canvas) c.Value, brush);
      }
    }

    /// <summary>
    ///   Set the background of a specific country
    /// </summary>
    /// <param name="alpha2">alpah2</param>
    /// <param name="brush">Brush</param>
    public void SetCountryBackground(string alpha2, Brush brush)
    {
      var key = alpha2.ToLower();
      if (!_dic.ContainsKey(key))
        return;

      var country = _dic[key];
      if (country is Path)
        ((Path) country).Fill = brush;
      if (country is Canvas)
        SetCanvasFill((Canvas) country, brush);
    }

    /// <summary>
    ///   Set the background of all specific countries
    /// </summary>
    /// <param name="alpha2">alpha2</param>
    /// <param name="brush">Brush</param>
    public void SetCountryBackground(IEnumerable<string> alpha2, Brush brush)
    {
      foreach (var x in alpha2)
        SetCountryBackground(x.ToLower(), brush);
    }

    /// <summary>
    ///   Set the value of a specific country
    /// </summary>
    /// <param name="alpha2">alpah3</param>
    /// <param name="value">Value</param>
    public void SetCountryValue(string alpha2, double value)
    {
      var key = alpha2.ToLower();
      if (!_dic.ContainsKey(key))
        return;

      var country = _dic[key];
      if (country is Path)
        ((MapToolTip) ((Path) country).ToolTip).Value = value.ToString();
      if (country is Canvas)
        ((MapToolTip) ((Canvas) country).ToolTip).Value = value.ToString();
    }

    private void BuildDictionary(Canvas canvas)
    {
      foreach (UIElement element in canvas.Children)
      {
        if (element is Canvas)
        {
          var c = element as Canvas;
          if (!string.IsNullOrEmpty(c.Name) && c.Name.Length == 2)
          {
            _dic.Add(c.Name, c);
            c.ToolTip = new MapToolTip {Label = c.Name, Value = ""};
          }

          BuildDictionary(c);
          continue;
        }

        // ReSharper disable once UseNullPropagation
        if (element is Path)
        {
          var p = element as Path;
          if (string.IsNullOrEmpty(p.Name) || p.Name.Length != 2)
            continue;
          p.ToolTip = new MapToolTip {Label = p.Name, Value = ""};
          p.StrokeThickness = 0.3;
          p.Stroke = new SolidColorBrush(Colors.Black);
          _dic.Add(p.Name, p);
        }
      }
    }

    private void SetCanvasFill(Canvas canvas, Brush brush)
    {
      foreach (var child in canvas.Children)
      {
        if (child is Canvas)
          SetCanvasFill((Canvas) child, brush);
        if (child is Path)
        {
          var p = child as Path;
          p.Fill = brush;
          p.StrokeThickness = 0.3;
          p.Stroke = new SolidColorBrush(Colors.Black);
        }
      }
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
  }
}