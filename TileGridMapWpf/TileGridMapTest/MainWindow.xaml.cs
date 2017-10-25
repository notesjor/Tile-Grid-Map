using System;
using System.Collections.Generic;
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

namespace TileGridMapTest
{
  /// <summary>
  /// Interaktionslogik für MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      Map.SetCountryBorder(Map.GetAllCountriesOfRegion("Asia"), new SolidColorBrush(Color.FromRgb(255, 204, 0)));
      Map.SetCountryBorder(Map.GetAllCountriesOfRegion("Europe"), new SolidColorBrush(Color.FromRgb(0, 201, 255)));
      Map.SetCountryBorder(Map.GetAllCountriesOfRegion("Africa"), new SolidColorBrush(Color.FromRgb(0, 238, 153)));
      Map.SetCountryBorder(Map.GetAllCountriesOfRegion("Americas"), new SolidColorBrush(Color.FromRgb(241, 74, 155)));
      Map.SetCountryBorder(Map.GetAllCountriesOfRegion("Oceania"), new SolidColorBrush(Color.FromRgb(242, 88, 7)));

      Map.FixInnerBorder(2);

      Map.NoCountry = new SolidColorBrush(Color.FromRgb(200, 230, 255));

      Map.AddCountryAlpha3();
    }
  }
}
