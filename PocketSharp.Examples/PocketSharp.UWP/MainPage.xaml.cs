using PocketSharp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PocketSharp.UWP
{
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  
  public sealed partial class MainPage : Page
  {
    public ObservableCollection<PocketItem> Items { get; set; }


    public MainPage()
    {
      InitializeComponent();
      Items = new ObservableCollection<PocketItem>();
    }

    public async Task LoadData()
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
        items = (await client.Get()).ToList();
        foreach (var item in items)
        {
          Items.Add(item);
        }
      }
      catch (PocketException ex)
      {
        Debug.WriteLine(ex.Message);
      }
    }

    private async void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
    {
      await LoadData();
    }
  }
}
