using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MFComputer.Views;
public sealed partial class LEDRow : UserControl {
    public string TopTitle { get; set; } = "Title"; //can be set from XAML, but no hot-reload available without observation?
    public string ButtonLabels { get; set; } = "A7,A6,A5,A4,A3,A2,A1,A0"; //can be set from XAML, but no hot-reload available without observation?
    public int LEDCount { get; set; } = 8;
    public LEDRow() {
        this.InitializeComponent();

    }

    private void Grid_Loaded(object sender, RoutedEventArgs e) {
        var labels = ButtonLabels.Split(',');
        var i = 0;
        foreach (var tblock in Grid.Children.Where(e => Grid.GetRow((FrameworkElement)e) == 2).OfType<TextBlock>()) {
            tblock.Text = labels[i++];
        }

        //remove extra LEDs if needed
        if (LEDCount < 8) { 
            var topLabels = Grid.Children.Where(e => Grid.GetRow((FrameworkElement)e) == 0).OfType<TextBlock>().ToArray();
            var LEDs =  Grid.Children.Where(e => Grid.GetRow((FrameworkElement)e) == 1).OfType<Ellipse>().ToArray();
            var bottomLabels = Grid.Children.Where(e => Grid.GetRow((FrameworkElement)e) == 2).OfType<TextBlock>().ToArray();
            for (i = 7; i >= LEDCount; i--) {
                Grid.Children.Remove(LEDs[i]);
                Grid.Children.Remove(bottomLabels[i]);
                Grid.ColumnDefinitions.RemoveAt(i);
            }
            Grid.Width = 600 * LEDCount / 8;
            Grid.SetColumnSpan(topLabels[0], LEDCount);

        }
    }
}
