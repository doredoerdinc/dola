using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using LgsLib.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DevExpress.Persistent.Base;
using System.Drawing;

namespace dola.Module
{
    [Table("CurrentData")]
    [DefaultClassOptions]
    public class CurrentData : BaseObjectI
    {
        public CurrentData() { }

        DateTime? _Tarih;
        public DateTime? Tarih
        {
            get { return _Tarih; }
            set { _Tarih = value; }
        }

        String _OperasyonIsmi;
        public String OperasyonIsmi
        {
            get { return _OperasyonIsmi; }
            set { _OperasyonIsmi = value; }
        }

        String _Referans;
        public String Referans
        {
            get { return _Referans; }
            set { _Referans = value; }
        }

        String _YuklemeYeri;
        public String YuklemeYeri
        {
            get { return _YuklemeYeri; }
            set { _YuklemeYeri = value; }
        }

        String _TahliyeYeri;
        public String TahliyeYeri
        {
            get { return _TahliyeYeri; }
            set { _TahliyeYeri = value; }
        }

        String _Tahliye;
        public String Tahliye
        {
            get { return _Tahliye; }
            set { _Tahliye = value; }
        }

        String _YukCinsi;
        public String YukCinsi
        {
            get { return _YukCinsi; }
            set { _YukCinsi = value; }
        }

        String _Plaka;
        public String Plaka
        {
            get { return _Plaka; }
            set { _Plaka = value; }
        }

        String _Sahiplik;
        public String Sahiplik
        {
            get { return _Sahiplik; }
            set { _Sahiplik = value; }
        }

        String _FirmaIsmi;
        public String FirmaIsmi
        {
            get { return _FirmaIsmi; }
            set { _FirmaIsmi = value; }
        }

        float? _SeferTon;
        public float? SeferTon
        {
            get { return _SeferTon; }
            set { _SeferTon = value; }
        }

        String _Surucu;
        public String Surucu
        {
            get { return _Surucu; }
            set { _Surucu = value; }
        } 
    }

    [Table("VehicleReport")]
    [DefaultClassOptions]
    public class VehicleReport : BaseObjectI
    {
        public VehicleReport() { }

        String _AracGrubu;
        public String AracGrubu
        {
            get { return _AracGrubu; }
            set { _AracGrubu = value; }
        }

        String _Plaka;
        public String Plaka
        {
            get { return _Plaka; }
            set { _Plaka = value; }
        }

        String _Surucu;
        public String Surucu
        {
            get { return _Surucu; }
            set { _Surucu = value; }
        }

        String _Rolanti;
        public String Rolanti
        {
            get { return _Rolanti; }
            set { _Rolanti = value; }
        }

        String _Surus;
        public String Surus
        {
            get { return _Surus; }
            set { _Surus = value; }
        }

        String _Calisma;
        public String Calisma
        {
            get { return _Calisma; }
            set { _Calisma = value; }
        }
        
        String _Duraklama;
        public String Duraklama
        {
            get { return _Duraklama; }
            set { _Duraklama = value; }
        }

        float? _TuketimLT;
        public float? TuketimLT
        {
            get { return _TuketimLT; }
            set { _TuketimLT = value; }
        }

        String _KM;
        public String KM
        {
            get { return _KM; }
            set { _KM = value; }
        }

        float? _YakitOrt;
        public float? YakitOrt
        {
            get { return _YakitOrt; }
            set { _YakitOrt = value; }
        }

        String _AracKM;
        public String AracKM
        {
            get { return _AracKM; }
            set { _AracKM = value; }
        }

        float? _CalismaSaat;
        public float? CalismaSaat
        {
            get { return _CalismaSaat; }
            set { _CalismaSaat = value; }
        }

        float? _SurusSaat;
        public float? SurusSaat
        {
            get { return _SurusSaat; }
            set { _SurusSaat = value; }
        }

        float? _DuraklamaSaat;
        public float? DuraklamaSaat
        {
            get { return _DuraklamaSaat; }
            set { _DuraklamaSaat = value; }
        }

        float? _KmSaat;
        public float? KmSaat
        {
            get { return _KmSaat; }
            set { _KmSaat = value; }
        }

        float? _AracToplamKM;
        public float? AracToplamKM
        {
            get { return _AracToplamKM; }
            set { _AracToplamKM = value; }
        }

        float? _RolantiSaat;
        public float? RolantiSaat
        {
            get { return _RolantiSaat; }
            set { _RolantiSaat = value; }
        }


        DateTime? _Date;
        public DateTime? Date
        {
            get { return _Date; }
            set { _Date = value; }
        } 
    }
}
