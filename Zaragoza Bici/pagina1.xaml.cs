using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Zaragoza_Bici
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class pagina1 : Page
    {
        List<Station> list;
        public pagina1()
        {

            this.InitializeComponent();
            BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = 41.654580, Longitude = -0.886506 };
            Geopoint cityCenter = new Geopoint(cityPosition);
            myMap.Center = cityCenter;
            myMap.ZoomLevel = 14;
            Task.Factory.StartNew(() =>
            {
                InternetStuff conn = new InternetStuff();
                list = conn.getData().Result;


            }
             ).ContinueWith(t => addPoi(DateTime.Now.ToString("HH:mm:ss")), TaskScheduler.FromCurrentSynchronizationContext());


            myMap.MapElementClick += MapControl_MapElementClick;
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.BackgroundColor = Colors.CadetBlue;
        }

        private async void addPoi(string time)
        {
            if (list == null)
            {
                MessageDialog mymsg = new MessageDialog("No pudimos obtener la lista de estaciones  ");
                mymsg.Title = "Error";
                await mymsg.ShowAsync();
            }
            else
            {


                int i = 0;
                myMap.MapElements.Clear();
                while (i < list.Count)
                {
                    MapIcon mapicon = new MapIcon();
                    int number = int.Parse(list[i].StationAvailableBikes);
                    int total = int.Parse(list[i].StationAvailableBikes) + int.Parse(list[i].StationFreeSlot);
                    mapicon.Title = list[i].StationID;
                    if (String.Compare(list[i].StationStatusCode, "OPN") != 0)
                    {
                        mapicon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/e_tachada.png"));

                    }
                    else if (number == 0)
                    {
                        mapicon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/e_negra.png"));

                    }
                    else if (number < 5)
                    {
                        mapicon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/e_amarilla.png"));

                    }
                    else
                    {
                        mapicon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/e_roja.png"));

                    }
                    
                    mapicon.NormalizedAnchorPoint = new Point(0.5, 0.5);
                    mapicon.CollisionBehaviorDesired = MapElementCollisionBehavior.RemainVisible;
                    BasicGeoposition snPosition = new BasicGeoposition() { Latitude = double.Parse(list[i].AddressGmapsLatitude), Longitude = double.Parse(list[i].AddressGmapsLongitude) };
                    Geopoint snPoint = new Geopoint(snPosition);
                    mapicon.Location = snPoint;
                    myMap.MapElements.Add(mapicon);
                    i++;
                }
            }
            textBlock5.Text = "Actualizado:   " + time;
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {

            Task.Factory.StartNew(() =>
            {
                InternetStuff conn = new InternetStuff();
                list = conn.getData().Result;

            }
              ).ContinueWith(t => addPoi(DateTime.Now.ToString("HH:mm:ss")), TaskScheduler.FromCurrentSynchronizationContext());
        }
        //event when click a station
        private void MapControl_MapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            if (args.MapElements.Count > 0 && args.MapElements[0] is MapIcon)
            {
                MapIcon mapIcon = (MapIcon)args.MapElements[0];
                int i = 0;
                while (i < list.Count)
                {
                    if (String.Compare(list[i].StationID, mapIcon.Title) == 0)
                    {
                        string estado = "Abierta";
                        if (String.Compare(list[i].StationStatusCode, "OPN") != 0)
                            estado = "Cerrada";
                        textBlock.Text = "Calle: " + list[i].AddressStreet1;
                        textBlock2.Text = "Bicis: " + list[i].StationAvailableBikes;
                        textBlock3.Text = "Huecos: " + list[i].StationFreeSlot;
                        textBlock4.Text = "Estado: " + estado;
                        textBlock6.Text = "Parada: "+ list[i].StationID;

                        textBlockParada.Text = list[i].StationID;
                        break;
                    }
                    i++;
                }
            }
        }

        private async void favoritos_Click(object sender, RoutedEventArgs ex)
        {
            string parada = textBlockParada.Text;
            if(string.IsNullOrEmpty(parada))
            {
                MessageDialog mymsg = new MessageDialog("No has seleccionado ninguna parada  ");
                mymsg.Title = "Error";
                await mymsg.ShowAsync();

            } else
            {
               
                try {
                    //create file
                   Windows.Storage.StorageFolder storageFolder =
                    Windows.Storage.ApplicationData.Current.LocalFolder;
                          Windows.Storage.StorageFile sampleFile =
                             await storageFolder.CreateFileAsync("Stations.txt",
                                 Windows.Storage.CreationCollisionOption.OpenIfExists);
                                 
                    //open file
                /*    Windows.Storage.StorageFolder storageFolder2 =
                    Windows.Storage.ApplicationData.Current.LocalFolder;
                    Windows.Storage.StorageFile sampleFile2 =
                        await storageFolder.GetFileAsync("Stations.txt");*/

                    ///
                   // await Windows.Storage.FileIO.WriteTextAsync(sampleFile, parada + " ");
                    await Windows.Storage.FileIO.AppendTextAsync(sampleFile,parada+"?");
                    Debug.WriteLine(parada + " ");
                    ////reading text
                    // string text = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
                    MessageDialog mymsg2 = new MessageDialog("seleccionada:  " + parada);
                    mymsg2.Title = "Parada añadida";
                    await mymsg2.ShowAsync();
                }
                catch (Exception e)
                {

                    MessageDialog mymsg3 = new MessageDialog("Error guardando datos  ");
                    mymsg3.Title = "Error";
                    await mymsg3.ShowAsync();
                }


            }

        }

        private async void button2_Click(object sender, RoutedEventArgs e)
        {
            //open file
            try
            {
                Windows.Storage.StorageFolder storageFolder2 =
          Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile sampleFile2 =
                    await storageFolder2.GetFileAsync("Stations.txt");
                string text = await Windows.Storage.FileIO.ReadTextAsync(sampleFile2);
                MessageDialog mymsg3 = new MessageDialog(text);
                mymsg3.Title = "lista";
                await mymsg3.ShowAsync();
            }
          catch
            {
                Windows.Storage.StorageFolder storageFolder =
            Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile sampleFile =
                   await storageFolder.CreateFileAsync("Stations.txt",
                       Windows.Storage.CreationCollisionOption.ReplaceExisting);
                MessageDialog mymsg3 = new MessageDialog("Lista vacía");
                mymsg3.Title = "lista";
                await mymsg3.ShowAsync();
            }
        }

        private async void button3_Click(object sender, RoutedEventArgs e)
        {
            //create file
            Windows.Storage.StorageFolder storageFolder =
             Windows.Storage.ApplicationData.Current.LocalFolder;
                  Windows.Storage.StorageFile sampleFile =
                     await storageFolder.CreateFileAsync("Stations.txt",
                         Windows.Storage.CreationCollisionOption.ReplaceExisting);
                         
       

        }
    }
}
