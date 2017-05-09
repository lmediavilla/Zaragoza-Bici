using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Zaragoza_Bici
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class pagina3 : Page
    {
        public List<string> unique2;
        // 
        List<Station> list;
        public pagina3()
        {

            this.InitializeComponent();
            try
            {
                paint();
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                titleBar.BackgroundColor = Colors.CadetBlue;
            }
            catch (Exception e)
            {
           
                error();

            }



        }
        public async void error()
        {
            MessageDialog mymsg3 = new MessageDialog("No se pudo cargar la lista de favoritos");
            mymsg3.Title = "Favoritos";
            await mymsg3.ShowAsync();
        }
        public async void paint()
        {
           string text =null;
            Windows.Storage.StorageFolder storageFolder2 =
         Windows.Storage.ApplicationData.Current.LocalFolder;
            try
            {
            
            Windows.Storage.StorageFile sampleFile2 =
                await storageFolder2.GetFileAsync("Stations.txt");
            text = await Windows.Storage.FileIO.ReadTextAsync(sampleFile2);
         //   Debug.WriteLine("Cadena: " + text);
        }
        catch(Exception e)
            {
                Windows.Storage.StorageFolder storageFolder =
                    Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile sampleFile =
                   await storageFolder.CreateFileAsync("Stations.txt",
                       Windows.Storage.CreationCollisionOption.OpenIfExists);
                error();
            }

            if (!string.IsNullOrEmpty(text))
            {
                List<int> numbers = new List<int>();
                List<string> text2 = new List<string>();
                char[] text3 = text.ToCharArray();
                //     Debug.WriteLine("Cadena: " + text3);
                int i = 0;
                while (i < text3.Count())
                {
                    //        Debug.Write(text3[i]);
                    i++;
                }
                i = 0;
                //    Debug.WriteLine("");
                string tmp = null;
                while (i < text3.Length)
                {
                    if (text3[i] != '?')
                    {
                        if (tmp == null)
                        {
                            tmp = text3[i].ToString();
                            //       Debug.WriteLine("tmp: " + tmp);
                        }
                        else
                        {
                            tmp = string.Concat(tmp, text3[i]);
                            //        Debug.WriteLine("tmp: " + tmp);
                        }
                    }
                    else
                    {
                        //  Debug.WriteLine("tmp comnpleto: " + tmp);
                        text2.Add(tmp);
                        tmp = null;
                    }
                    i++;

                }
                i = 0;



           //     Debug.WriteLine("Array elements: ");
                foreach (string o in text2)
                {
                    numbers.Add(int.Parse(o));
                }
                numbers.Sort();
                foreach (int u in numbers)
                {
            //        Debug.WriteLine("numero: " + u);
                }
                List<int> unique = new List<int>();
                unique = numbers.Distinct().ToList();
                unique2 = new List<string>();
                foreach (int v in unique)
                {
                    unique2.Add(v.ToString());
              //      Debug.WriteLine("numero: " + v);
                }
  


                await Task.Factory.StartNew(() =>
                 {
                     InternetStuff conn = new InternetStuff();
                     list = conn.getData().Result;


                 }
                      ).ContinueWith(t => repaint(), TaskScheduler.FromCurrentSynchronizationContext());


            }
            else
            {
                error();

            }
        }
            public  void repaint()
              {
            //unique2 list
           // pg3.Children.Clear();
            int row = 1;
            TextBlock tb = new TextBlock();
            TextBlock tb2 = new TextBlock();
            foreach (string s in unique2)
            {
                int i = 0;
               
             
                while (i < list.Count)
                {

                   // pg3.Children.Clear();

                    if (String.Compare(list[i].StationID, s) == 0)
                    {
               //         Debug.WriteLine("Paradas finales: " + list[i].StationID);
                        tb = new TextBlock();
                        tb.Text = ""+list[i].AddressStreet1+"";
                        pg3.Children.Add(tb);
                        Grid.SetRow(tb, row);
                        Grid.SetColumn(tb, 0);
                        row++;
                        tb2 = new TextBlock();
                        tb2.Text = "Bicis: " + list[i].StationAvailableBikes + "  Huecos: " + list[i].StationFreeSlot ;
                        pg3.Children.Add(tb2);
                        Grid.SetRow(tb2, row);
                        Grid.SetColumn(tb2, 0);
                        //     Debug.WriteLine("row: " + row);
                        if (row > 10)
                            break;
                        row++;

                    }
                   
                    i++;
                }
                
            }
            Debug.WriteLine("fin");
        }

        private async void button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //create file
            Windows.Storage.StorageFolder storageFolder =
             Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile =
               await storageFolder.CreateFileAsync("Stations.txt",
                   Windows.Storage.CreationCollisionOption.ReplaceExisting);
            MessageDialog mymsg3 = new MessageDialog("Favoritos Borrados, recarga la pagina");
            mymsg3.Title = "Favoritos";
            await mymsg3.ShowAsync();
        }
    }

    }

