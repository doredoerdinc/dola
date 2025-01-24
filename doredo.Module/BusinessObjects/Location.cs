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
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Data.Filtering;

namespace dola.Module
{ 
    [DefaultClassOptions]
    [Table("LocationFunction")]
    [XafDefaultProperty("SysCode")]
    public class LocationFunction : BaseObjectC
    {
    }

     
    [DefaultClassOptions]
    [Table("LocationZone")]
    [XafDefaultProperty("SysCode")]
    public class LocationZone : BaseObjectC
    {
        string _Code;
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

    }


    [DefaultClassOptions]
    [Table("LocationColumn")]
    [XafDefaultProperty("Number")]
    public class LocationColumn : BaseObjectN
    {

        string _Code;
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }


    }

    [DefaultClassOptions]
    [Table("LocationHoll")]
    [XafDefaultProperty("Number")]
    public class LocationHoll : BaseObjectN
    {
        string _Code;
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }



    }

    [DefaultClassOptions]
    [Table("LocationLevel")]
    [XafDefaultProperty("Number")]
    public class LocationLevel   : BaseObjectN
    {
        string _Code;
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }



    }

    [DefaultClassOptions]
    [Table("LocationWarehouse")]
    [XafDefaultProperty("Code")]
    public class LocationWarehouse : BaseObjectState
    {
        public LocationWarehouse()
        {
            Containeries = new List<Container>(); 
        }
         
        public virtual IList<Container> Containeries { get; set; }
      
        LocationZone _Zone;
        public virtual LocationZone Zone
        {
            get { return _Zone; }
            set { _Zone = value; }
        }

        LocationColumn _Column;
        public virtual LocationColumn Column
        {
            get { return _Column; }
            set { _Column = value; }
        }

        LocationHoll _Holl;
        public virtual LocationHoll Holl
        {
            get { return _Holl; }
            set { _Holl = value; }
        }


        int? _Side;
        public int? Side
        {
            get { return _Side; }
            set { _Side = value; }
        }

        LocationLevel _Level;
        public virtual LocationLevel Level
        {
            get { return _Level; }
            set { _Level = value; }
        }

        int? _OperationRow;
        public int? OperationRow
        {
            get { return _OperationRow; }
            set { _OperationRow = value; }
        }

        string _Code;
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }


        EnumLocationFunctionType _FunctionType = EnumLocationFunctionType.Stock;
        public EnumLocationFunctionType FunctionType
        {
            get { return _FunctionType; }
            set { _FunctionType = value; }
        } 

        double? _Width;
        public double? Width
        {
            get { return _Width; }
            set { _Width = value; }
        }

        double? _Height;
        public double? Height
        {
            get { return _Height; }
            set { _Height = value; }
        }

        double? _Length;
        public double? Length
        {
            get { return _Length; }
            set { _Length = value; }
        }

        double? _MaxLoadQuantityKg;
        public double? MaxLoadQuantityKg
        {
            get { return _MaxLoadQuantityKg; }
            set { _MaxLoadQuantityKg = value; }
        }

        double? _MinTemperature;
        public double? MinTemperature
        {
            get { return _MinTemperature; }
            set { _MinTemperature = value; }
        }

        double? _MaxTemperature;
        public double? MaxTemperature
        {
            get { return _MaxTemperature; }
            set { _MaxTemperature = value; }
        }

        double? _PalleteCapacity;
        public double? PalleteCapacity
        {
            get { return _PalleteCapacity; }
            set { _PalleteCapacity = value; }
        }  

    }
}

