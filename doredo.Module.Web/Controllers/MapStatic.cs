using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LgsLib.Base;
using System.Text.RegularExpressions;
using DevExpress.ExpressApp;

namespace dola.Module.Web
{
    public static class MapStatic
    {
        public static string CallMapView(string path)
        {
            return @"
let params = 'scrollbars=no,resizable=no,status=no,location=no,toolbar=no,menubar=no,width=600,height=300,left=100,top=100';
let popupName = 'doredo';
let popupRef = null;
const POPUP_KEY = 'popup_window_open';
const POPUP_URL = 'popup_window_url';
const bc = new BroadcastChannel('popup_channel');

// Sayfa reload olursa popup'a tekrar ulaş
window.addEventListener('load', () => {
    if (sessionStorage.getItem(POPUP_KEY) === 'true') {
        try {
            popupRef = window.open('', popupName);
            if (popupRef && !popupRef.closed) {
                const savedUrl = sessionStorage.getItem(POPUP_URL);
                if (savedUrl && popupRef.location.href !== savedUrl) {
                    popupRef.location.href = savedUrl;
                } else {
                    popupRef.location.reload();
                }
            }
        } catch (e) {
            console.warn('Popup geri yüklenemedi:', e);
        }
    }
});

bc.onmessage = function(event) {
    if (event.data === 'focus') {
        if (popupRef && !popupRef.closed) popupRef.focus();
    }
};

function openOnce(url) {
    const absoluteUrl = new URL(url, window.location.origin).href;

    try {
        // Popup zaten açık mı kontrolü
        if (sessionStorage.getItem(POPUP_KEY) === 'true') {
            popupRef = window.open('', popupName);
            if (popupRef && !popupRef.closed) {
                if (popupRef.location.href === absoluteUrl) {
                    popupRef.location.reload();
                } else {
                    popupRef.location.href = absoluteUrl;
                }
                popupRef.focus();
                bc.postMessage('focus');
                return;
            }
        }
    } catch (e) {
        console.error('Popup kontrol hatası:', e);
    }

    // Yeni popup aç
    popupRef = window.open(absoluteUrl, popupName, params);

    if (popupRef) {
        sessionStorage.setItem(POPUP_KEY, 'true');
        sessionStorage.setItem(POPUP_URL, absoluteUrl);

        const popupWatcher = setInterval(() => {
            if (popupRef.closed) {
                clearInterval(popupWatcher);
                sessionStorage.setItem(POPUP_KEY, 'false');
                sessionStorage.removeItem(POPUP_URL);
                bc.postMessage('closed');

                // ASPX'e kapanma bildirimi
                fetch('/PopupClosed.aspx', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ message: 'popup_closed' })
                });
            }
        }, 500);
    }

    popupRef.focus();
    bc.postMessage('focus');
}

// Başlat
var url = '" + path + @"';
openOnce(url);";
        }


        static List<MapPointLGS> _StaticPointList;
        public static List<MapPointLGS> StaticPointList
        {
            get { return _StaticPointList; }
            set { _StaticPointList = value; }
        }

        static List<MapPointOrderTransfer> _StaticMapPointOrderTransferList;
        public static List<MapPointOrderTransfer> StaticMapPointOrderTransferList
        {
            get { return _StaticMapPointOrderTransferList; }
            set { _StaticMapPointOrderTransferList = value; }
        }

        public static string RemoveIllegalCharacters(string text) // method alınacak
        {
            string cleanText = null;
            if (!string.IsNullOrEmpty(text))
            {
                cleanText = Regex.Replace(text, @"[^a-zA-Z0-9ğüşöçıİĞÜŞÖÇ\s_]", "");
            }
            return cleanText;
            // Harf, rakam, boşluk ve alt çizgi dışındaki her şeyi sil

        }

        static IObjectSpace _MapObjectSpace;
        public static IObjectSpace MapObjectSpace
        {
            get { return _MapObjectSpace; }
            set { _MapObjectSpace = value; }
        }




        static List<GoogleRouteDraw> _StaticRouteDrawList;
        static public List<GoogleRouteDraw> StaticRouteDrawList
        {
            get { return _StaticRouteDrawList; }
            set { _StaticRouteDrawList = value; }
        }

        public static List<PolyLatLng> DecodePolylinePoints(string encodedPoints)
        {
            if (encodedPoints == null || encodedPoints == "") return null;
            List<PolyLatLng> poly = new List<PolyLatLng>();
            char[] polylinechars = encodedPoints.ToCharArray();
            int index = 0;

            int currentLat = 0;
            int currentLng = 0;
            int next5bits;
            int sum;
            int shifter;

            try
            {
                while (index < polylinechars.Length)
                {
                    // calculate next latitude
                    sum = 0;
                    shifter = 0;
                    do
                    {
                        next5bits = (int)polylinechars[index++] - 63;
                        sum |= (next5bits & 31) << shifter;
                        shifter += 5;
                    } while (next5bits >= 32 && index < polylinechars.Length);

                    if (index >= polylinechars.Length)
                        break;

                    currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                    //calculate next longitude
                    sum = 0;
                    shifter = 0;
                    do
                    {
                        next5bits = (int)polylinechars[index++] - 63;
                        sum |= (next5bits & 31) << shifter;
                        shifter += 5;
                    } while (next5bits >= 32 && index < polylinechars.Length);

                    if (index >= polylinechars.Length && next5bits >= 32)
                        break;

                    currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);
                    PolyLatLng p = new PolyLatLng();
                    p.lat = Convert.ToDouble(currentLat) / 100000.0;
                    p.lng = Convert.ToDouble(currentLng) / 100000.0;
                    poly.Add(p);
                }
            }
            catch (Exception ex)
            {
                // logo it
            }
            return poly;
        } 

    }
    public class GoogleRouteDraw
    {

        String _SysCode;
        public String SysCode
        {
            get { return _SysCode; }
            set { _SysCode = value; }
        }



        double? _DictanceMeters;
        public double? DictanceMeters
        {
            get { return _DictanceMeters; }
            set { _DictanceMeters = value; }
        }

        string _Duration;
        public string Duration
        {
            get { return _Duration; }
            set { _Duration = value; }
        }


        string _StaticDuration;
        public string StaticDuration
        {
            get { return _StaticDuration; }
            set { _StaticDuration = value; }
        }

        string _EncodedPolyline;
        public string EncodedPolyline
        {
            get { return _EncodedPolyline; }
            set { _EncodedPolyline = value; }
        }

        String _Object;
        public String Object
        {
            get { return _Object; }
            set { _Object = value; }
        }


    }

    public class JSRouteSettings
    {
        public string mode;
        public double opacity;
        public int weight;
        public string color;
        public List<MapPointLGS> locations;
    }



}
