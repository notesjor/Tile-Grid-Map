using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace CorpusExplorer.Sdk.Extern.TileGridMap
{
  public class MapToolTip : ToolTip
  {
    private TextBlock _label;
    private TextBlock _value;

    public MapToolTip()
    {
      Background = new SolidColorBrush(Colors.White);
      Width = 120;
      Height = 48;

      var stack = new StackPanel
      {
        Background = new SolidColorBrush(Colors.White),
        VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
        HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch
      };

      _label = new TextBlock { Width = 110, Height = 24, Text = "", TextAlignment = System.Windows.TextAlignment.Center, Margin = new System.Windows.Thickness(0, 0, 10, 0) };
      stack.Children.Add(_label);
      _value = new TextBlock { Width = 110, Height = 24, Text = "", TextAlignment = System.Windows.TextAlignment.Center, Margin = new System.Windows.Thickness(0, 0, 10, 0) };
      stack.Children.Add(_value);

      Content = stack;
    }

    public string Label
    {
      get => _label.Text;
      set => _label.Text = value;
    }

    public string Value
    {
      get => _value.Text;
      set => _value.Text = value;
    }
  }
}
