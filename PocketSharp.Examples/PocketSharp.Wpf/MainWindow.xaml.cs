using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Collections.Generic;
using System;

namespace PocketSharp.Wpf
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    ObservableCollection<Site> _Sites = new ObservableCollection<Site>();

    public ObservableCollection<Site> Sites { get { return _Sites; } }

    public class Site
    {
      public string Url { get; set; }
      public string Content { get; set; }
    }


    public MainWindow()
    {
      InitializeComponent();
    }


    private async void Button_Click(object sender, RoutedEventArgs e)
    {
      // User: pocketsharp-tests
      // PW:   pocketsharp
      // !! please don't misuse this account !!
      PocketClient client = new PocketClient(
        consumerKey: "15396-f6f92101d72c8e270a6c9bb3",
        callbackUri: "http://frontendplay.com",
        accessCode: "2c62cd50-b78a-5558-918b-65adae"
      );

      List<PocketSharp.Models.PocketItem> items = null;

      try
      {
        items = await client.Retrieve();

        items.ForEach(item =>
        {
          Sites.Add(new Site()
          {
            Content = item.Title,
            Url = item.Uri.ToString()
          });
        });
      }
      catch (PocketException ex)
      {
        Debug.Write(ex.Message);
      }
    }
  }
}
