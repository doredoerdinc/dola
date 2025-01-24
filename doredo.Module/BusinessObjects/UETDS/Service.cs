using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
//using gologdo.Module.tr.gov.turkiye.servis;
using gologdo.Module.tr.gov.turkiye.servisLive;
using DevExpress.ExpressApp;
using LgsLib.Base;
namespace Lys.Module.UETDS
{
    public static class ServiceUETDS
    {
        public static string UserName = "21203";
        public static string UserPassWord = "JEMMCGNY1O";
//        public static string url = "https://servis.turkiye.gov.tr/services/g2g/kdgm/test/uetdsesya?wsdl";
        public static string url = "https://servis.turkiye.gov.tr/services/g2g/kdgm/uetdsesya?wsdl";

        public static UdhbUetdsEsyaWsService UETDSService { get; set; }
        public static uetdsYtsUser LoginUetds { get; set; }
        private static NetworkCredential Credentials { get; set; }
        public static void UETDSServiceCh()
        {
            
        }

        public static void CheckLogin()
        {
            if(UETDSService == null)
            {
                UETDSService = new UdhbUetdsEsyaWsService();
                Credentials = new NetworkCredential(UserName, UserPassWord);
                UETDSService.Url = url;
                UETDSService.Credentials = Credentials;
            }

            if (LoginUetds == null) 
            {
                LoginUetds = new uetdsYtsUser();
                LoginUetds.kullaniciAdi = UserName;
                LoginUetds.sifre = UserPassWord;
            }
        } 


        public static paramEsyaTurListesi[] getYukTuru(String birim)
        {
            CheckLogin();
            var sonuc = new uetdsEsyaParamEsyaTurSonuc(); 
            sonuc=UETDSService.paramYukTuru(LoginUetds, birim);
            if(sonuc.sonucKodu==0)
            {
                return sonuc.yukTurListesi;
            }
            return null;

        }

        public static paramTasimaTuruListesi[] getTasimaTuru()
        {
            CheckLogin();
            var sonuc = new uetdsEsyaParamTasimaTuruSonuc();
            sonuc = UETDSService.paramTasimaTurleri(LoginUetds);
            if (sonuc.sonucKodu == 0)
            {
                return sonuc.tasimaTuruListesi;
            }
            return null;

        }

        public static uetdsGenelIslemSonuc yeniSeferEkle(Order order)
        {
            CheckLogin(); 
            var obj = new uetdsEsyaSeferBilgileriInputV3();
            obj.baslangicSaati = order.TripStartTimeUedts.Value.ToString("HH:ss", CultureInfo.InvariantCulture);
            obj.baslangicTarihi = order.TripStartTimeUedts.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            obj.bitisSaati = order.TripFinishTimeUedts.Value.ToString("HH:ss", CultureInfo.InvariantCulture);
            obj.bitisTarihi = order.TripFinishTimeUedts.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            obj.firmaSeferNo = order.SysCode;
            obj.plaka1 = order.Vehicle.VehicleIdentifier;
            obj.sofor1TCNo = order.Driver.IdentityCode;
            if (order.TripIntegrationCodeUedts < 1)
            {
                return UETDSService.yeniSeferEkleV3(LoginUetds, obj);
            }else
            {
                return UETDSService.seferDuzenleV3(LoginUetds,order.TripIntegrationCodeUedts,obj);
            }
             
        }

        public static uetdsGenelIslemSonuc yeniYükEkle(Order order , IObjectSpace newObjectSpace)
        {
            CheckLogin(); 
            var loads = new List<uetdsEsyaYukBilgileriInputV3>();
            //yükleme yeri ve bilgisi 
             
            var obj = new uetdsEsyaYukBilgileriInputV3();
            var item = newObjectSpace.GetObject<Item>(order.Item);
            var uetdsItemType = newObjectSpace.GetObject<UEDTSItemType>(item.UEDTSItemType);
            var unitType= newObjectSpace.GetObject<UnitType>(order.UnitType); 
            var senderAddress = newObjectSpace.GetObject<Address>(order.SenderAddress);
            var senderCompany = newObjectSpace.GetObject<Company>(senderAddress.CompanyLink);
            var receiverAddress = newObjectSpace.GetObject<Address>(order.ReceiverAddress);
            var receiverCompany = newObjectSpace.GetObject<Company>(receiverAddress.CompanyLink);

            if(senderCompany == null||receiverCompany==null|| string.IsNullOrEmpty(senderCompany.LegalName) || string.IsNullOrEmpty(receiverCompany.LegalName)|| unitType==null)
            {
                throw new ArgumentException(string.Format("Yükleme ve Boşaltma Firmalarının Yasal İsimleri boş olamaz, Sipariş Birim Boş Olamaz", order.SysCode));
            }

       //     obj.gonderenVergiNo = senderCompany.TaxCode;
            obj.gonderenUnvan = senderCompany.LegalName;
            obj.yuklemeIlMernisKodu = newObjectSpace.GetObject<City>(order.SenderAddress.City).PostCode;
 //         
            obj.yuklemeIlceMernisKodu = order.SenderAddress.District.SysCode;
            obj.yuklemeUlkeKodu = "TR";//sistemden alinacak;
            obj.yuklemeSaati = order.LoadTimeUedts.Value.ToString("HH:ss", CultureInfo.InvariantCulture);
            obj.yuklemeTarihi = order.LoadTimeUedts.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            obj.yukMiktari = order.FinishQuantity.ToString();
            obj.yukCinsId = uetdsItemType.SysCode; ;//sistemden alınacak.
            obj.yukMiktariBirimi = unitType.UETDSCode;//yukmiktari birimi.
            obj.yukMiktari = order.FinishQuantity.ToString();
            obj.yukCinsDigerAciklama = "";
            obj.tasimaTuruKodu = "2";

            obj.aliciUnvan = receiverCompany.LegalName;
        //    obj.aliciVergiNo = receiverCompany.TaxCode;
            obj.bosaltmaUlkeKodu = "TR";//sistemden alinacak;
            obj.bosaltmaIlMernisKodu = newObjectSpace.GetObject<City>(order.ReceiverAddress.City).PostCode;
            obj.bosaltmaIlceMernisKodu = order.ReceiverAddress.District.SysCode;
            obj.bosaltmaTarihi = order.UnLoadTimeUedts.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            obj.bosaltmaSaati = order.UnLoadTimeUedts.Value.ToString("HH:mm", CultureInfo.InvariantCulture);
            loads.Add(obj);

            if (order.LoadIntegrationCodeUedts < 1)
            {
                return UETDSService.sefereYukEkleV3(LoginUetds, order.TripIntegrationCodeUedts, loads.ToArray<uetdsEsyaYukBilgileriInputV3>());
            }else
            {
                return UETDSService.yukDuzenleV3(LoginUetds, order.LoadIntegrationCodeUedts, obj);

            }
        }

        public static void getYukCinsi()
        {
            CheckLogin();
            var yuktur=UETDSService.paramYukBirimi(LoginUetds,"2"); 

        }

    }
}
