using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CorpusExplorer.Sdk.Extern.TileGridMap
{
  public class MapToolTip : ToolTip
  {
    private readonly TextBlock _label;
    private readonly TextBlock _value;

    public MapToolTip()
    {
      Background = new SolidColorBrush(Colors.White);
      Width = 120;
      Height = 48;

      var stack = new StackPanel
      {
        Background = new SolidColorBrush(Colors.White),
        VerticalAlignment = VerticalAlignment.Stretch,
        HorizontalAlignment = HorizontalAlignment.Stretch
      };

      _label = new TextBlock
      {
        Width = 110,
        Height = 24,
        Text = "",
        TextAlignment = TextAlignment.Center,
        Margin = new Thickness(0, 0, 10, 0)
      };
      stack.Children.Add(_label);
      _value = new TextBlock
      {
        Width = 110,
        Height = 24,
        Text = "",
        TextAlignment = TextAlignment.Center,
        Margin = new Thickness(0, 0, 10, 0)
      };
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