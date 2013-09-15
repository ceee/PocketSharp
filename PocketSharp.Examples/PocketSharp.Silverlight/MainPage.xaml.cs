using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PocketSharp.Silverlight
{
  public partial class MainPage : UserControl
  {
    ObservableCollection<Site> _Sites = new ObservableCollection<Site>();

    public ObservableCollection<Site> Sites { get { return _Sites; } }

    public class Site
    {
      public string Url { get; set; }
      public string Content { get; set; }
    }


    public MainPage()
    {
      InitializeComponent();
    }


    private async void Button_Click(object sender, RoutedEventArgs e)
    {
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

        Content.ItemsSource = Sites;
      }
      catch (PocketException ex)
      {
        Debug.WriteLine(ex.Message);
      }
    }
  }
}
