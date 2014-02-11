using Microsoft.Phone.Controls;
using System.Windows;

namespace PocketSharp.WP7
{
  public partial class MainPage : PhoneApplicationPage
  {
    // Constructor
    public MainPage()
    {
      InitializeComponent();

      // Set the data context of the LongListSelector control to the sample data
      DataContext = App.ViewModel;
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
      await App.ViewModel.LoadData();
    }
  }
}