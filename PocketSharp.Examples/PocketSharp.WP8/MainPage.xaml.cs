using Microsoft.Phone.Controls;
using PocketSharp.WP8.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace PocketSharp.WP8
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

    // Load data for the ViewModel Items
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {

    }

    // Handle selection changed on LongListSelector
    private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      // If selected item is null (no selection) do nothing
      if (MainLongListSelector.SelectedItem == null)
        return;

      // Navigate to the new page
      NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as ItemViewModel).ID, UriKind.Relative));

      // Reset selected item to null (no selection)
      MainLongListSelector.SelectedItem = null;
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
      await App.ViewModel.LoadData();
    }
  }
}