using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using PocketSharp.WP8.Resources;
using PocketSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PocketSharp.WP8.ViewModels
{
  public class MainViewModel : INotifyPropertyChanged
  {
    public MainViewModel()
    {
      this.Items = new ObservableCollection<ItemViewModel>();
    }

    /// <summary>
    /// A collection for ItemViewModel objects.
    /// </summary>
    public ObservableCollection<ItemViewModel> Items { get; private set; }

    private string _sampleProperty = "Sample Runtime Property Value";
    /// <summary>
    /// Sample ViewModel property; this property is used in the view to display its value using a Binding
    /// </summary>
    /// <returns></returns>
    public string SampleProperty
    {
      get
      {
        return _sampleProperty;
      }
      set
      {
        if (value != _sampleProperty)
        {
          _sampleProperty = value;
          NotifyPropertyChanged("SampleProperty");
        }
      }
    }

    /// <summary>
    /// Sample property that returns a localized string
    /// </summary>
    public string LocalizedSampleProperty
    {
      get
      {
        return AppResources.SampleProperty;
      }
    }

    public bool IsDataLoaded
    {
      get;
      private set;
    }

    /// <summary>
    /// Creates and adds a few ItemViewModel objects into the Items collection.
    /// </summary>
    public async Task LoadData()
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
        items = await client.Retrieve().ConfigureAwait(false);

        items.ForEach(item =>
        {
          this.Items.Add(new ItemViewModel()
          {
            ID = item.ID.ToString(),
            LineOne = item.Title,
            LineTwo = item.Uri.ToString()
          });
        });
      }
      catch (PocketException ex)
      {
        Debug.WriteLine(ex.Message);
      }

      this.IsDataLoaded = true;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void NotifyPropertyChanged(String propertyName)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (null != handler)
      {
        handler(this, new PropertyChangedEventArgs(propertyName));
      }
    }
  }
}