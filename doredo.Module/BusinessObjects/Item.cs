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
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;

namespace dola.Module
{ 

    [DefaultClassOptions]
    [Table("ItemType")]
    public class ItemType : BaseObjectC
    {
        public ItemType()
        {
            ParentGroups = new List<ItemType>();
            SubGroups = new List<ItemType>();
        } 

        String _Name;
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }


        ItemType _ParentGroup;
        public virtual ItemType ParentGroup
        {
            get { return _ParentGroup; }
            set { _ParentGroup = value; }
        }


        ItemType _SubGroup;
        public virtual ItemType SubGroup
        {
            get { return _SubGroup; }
            set { _SubGroup = value; }
        }

        String _Description;
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }




        [InverseProperty("SubGroup")]
        public virtual IList<ItemType> ParentGroups { get; set; } 

        [InverseProperty("ParentGroup")]
        public virtual IList<ItemType> SubGroups { get; set; }
        public virtual IList<Item> Items { get; set; }  

    }

    [DefaultClassOptions]
    [Table("Item")]
    [XafDefaultProperty("Name")]
    public class Item : BaseObjectC
    {
        public Item() {
             
            UnitConverts = new List<UnitConvert>();
        }


        String _ShortName;
        public String ShortName
        {
            get { return _ShortName; }
            set { _ShortName = value; }
        }

        EnumItemTrackingType _TrackingType;
        public EnumItemTrackingType TrackingType
        {
            get { return _TrackingType; }
            set { _TrackingType = value; }
        }

        public virtual IList<UnitConvert> UnitConverts { get; set; } 

       
        String _Name;
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        } 

        String _Gtin;
        public String Gtin
        {
            get { return _Gtin; }
            set { _Gtin = value; }
        }
        double? _RemaningShipmentLife;
        public double? RemaningShipmentLife
        {
            get { return _RemaningShipmentLife; }
            set { _RemaningShipmentLife = value; }
        }
         

        string _Barcode;
        public string Barcode
        {
            get { return _Barcode; }
            set { _Barcode = value; }
        } 

        double? _Sim;
        public double? Sim
        {
            get { return _Sim; }
            set { _Sim = value; }
        }

        string _Description;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        ItemType _ItemType;
        [RuleRequiredField(DefaultContexts.Save)]
        public virtual ItemType ItemType
        {
            get { return _ItemType; }
            set { _ItemType = value; }
        }

        UnitType _StockUnitType;
        public virtual UnitType StockUnitType
        {
            get { return _StockUnitType; }
            set { _StockUnitType = value; }
        } 

        double? _ShelfLife;
        public double? ShelfLife
        {
            get { return _ShelfLife; }
            set { _ShelfLife = value; }
        }

        bool? _PickReplacement;
        public bool? PickReplacement
        {
            get { return _PickReplacement; }
            set { _PickReplacement = value; }
        }

        Owner _Owner;
        public virtual Owner Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }

        EnumRotationType _RotationType;
        public EnumRotationType RotationType
        {
            get { return _RotationType; }
            set { _RotationType = value; }
        }

        State _State;
        public virtual State State
        {
            get { return _State; }
            set { _State = value; }
        }

        String _TransportType;
        public String TransportType
        {
            get { return _TransportType; }
            set { _TransportType = value; }
        } 

    }
    
    [DefaultClassOptions]
    [Table("UnitType")]
    public class UnitType : BaseLookupC
    {
        String _UETDSCode;
        public String UETDSCode
        {
            get { return _UETDSCode; }
            set { _UETDSCode = value; }
        }

        EnumDivionRate _DivisionRate;
        public EnumDivionRate DivisionRate
        {
            get { return _DivisionRate; }
            set { _DivisionRate = value; }
        }

        UnitType _ConvertType;
        public virtual UnitType ConvertType
        {
            get { return _ConvertType; }
            set { _ConvertType = value; }
        } 

        string _Barcode;
        public string Barcode
        {
            get { return _Barcode; }
            set { _Barcode = value; }
        }

        double? _Length;
        public double? Length
        {
            get { return _Length; }
            set { _Length = value; }
        }

        double? _Width;
        public double? Width
        {
            get { return _Width; }
            set { _Width = value; }
        }

        double? _GrossWeight;
        public double? GrossWeight
        {
            get { return _GrossWeight; }
            set { _GrossWeight = value; }
        }

        double? _NetWeight;
        public double? NetWeight
        {
            get { return _NetWeight; }
            set { _NetWeight = value; }
        }

        double? _Height;
        public double? Height
        {
            get { return _Height; }
            set { _Height = value; }
        }

        double? _Volume;
        public double? Volume
        {
            get { return _Volume; }
            set { _Volume = value; }
        }


    }
} 

