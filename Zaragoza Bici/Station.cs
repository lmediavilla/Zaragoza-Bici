using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaragoza_Bici
{
    class Station
    {
        //thank you http://json2csharp.com/ http://www.json.net https://www.youtube.com/watch?v=DqjIQiZ_ql4 https://www.youtube.com/watch?v=Gg94sM7DkLs
        public string StationID { get; set; }
        public string DisctrictCode { get; set; }
        public string AddressGmapsLongitude { get; set; }
        public string AddressGmapsLatitude { get; set; }
        public string StationAvailableBikes { get; set; }
        public string StationFreeSlot { get; set; }
        public string AddressZipCode { get; set; }
        public string AddressStreet1 { get; set; }
        public string AddressNumber { get; set; }
        public string NearbyStationList { get; set; }
        public string StationStatusCode { get; set; }
        public string StationName { get; set; }

        public Station(string StationID, string DisctrictCode, string AddressGmapsLongitude, string AddressGmapsLatitude, string StationAvailableBikes, string StationFreeSlot, string AddressZipCode, string AddressStreet1, string AddressNumber, string NearbyStationList, string StationStatusCode, string StationName)
        {
            this.StationID = StationID;
            this.DisctrictCode = DisctrictCode;
            this.AddressGmapsLongitude = AddressGmapsLongitude;
            this.AddressGmapsLatitude = AddressGmapsLatitude;
            this.StationAvailableBikes = StationAvailableBikes;
            this.StationFreeSlot = StationFreeSlot;
            this.AddressZipCode = AddressZipCode;
            this.AddressStreet1 = AddressStreet1;
            this.AddressNumber = AddressNumber;
            this.NearbyStationList = NearbyStationList;
            this.StationStatusCode = StationStatusCode;
            this.StationName = StationName;
        }
        public void createDB()
        {

        }
    }
}
