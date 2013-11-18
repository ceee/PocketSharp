using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

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
        accessCode: "80acf6c5-c198-03c0-b94c-e74402"
      );

      List<PocketSharp.Models.PocketItem> items = null;

      try
      {
        items = await client.Get();

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
