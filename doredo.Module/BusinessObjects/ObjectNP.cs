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
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Validation;
using LgsLib.Base.PermissionPolicy;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;

using System.IO;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;

namespace dola.Module
{ 
    [DomainComponent]
    public class VehicleDefineOrder : NonePercentObject
    {
        Vehicle _Vehicle;
        public Vehicle Vehicle
        {
            get { return _Vehicle; }
            set { _Vehicle = value; }
        } 
        String _UnDefinationVehicle;
        public String UnDefinationVehicle
        {
            get { return _UnDefinationVehicle; }
            set { _UnDefinationVehicle = value; }
        } 
    }

    [DomainComponent]
    public class LabelTemplateNP : NonePercentObject
    {
        ContainerTemplate _LabelTemplate;
        public ContainerTemplate LabelTemplate
        {
            get { return _LabelTemplate; }
            set { _LabelTemplate = value; }
        }
          
        int _Quantity;
        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
    }


    [DomainComponent]
    public class TaskAssignedNP : NonePercentObject
    {
        Person _Person;
        public Person Person
        {
            get { return _Person; }
            set { _Person = value; }
        }

    }

    [DomainComponent]
    public class OrderItemNP : NonePercentObject
    { 

    }

    [DomainComponent]
    public class GoodsInOut : NonePercentObject
    {
        LocationWarehouse _Ramp;
     //   [DataSourceCriteria("FunctionType=EnumLocationFunctionType.Ramp")]
        public LocationWarehouse Ramp
        {
            get { return _Ramp; }
            set { _Ramp = value; }
        }


    }

    [DomainComponent]
    public class OrderImportNP : NonePercentObject
    {

        FileData _File;
        public FileData File
        {
            get { return _File; }
            set { _File = value; }
        }

        Owner _Owner;
        public Owner Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }


        DateTime _RequestPlanedDate;
        public DateTime RequestPlanedDate
        {
            get { return _RequestPlanedDate; }
            set { _RequestPlanedDate = value; }
        }





        String _Delidelimeter = ";";
        public String Delidelimeter
        {
            get { return _Delidelimeter; }
            set { _Delidelimeter = value; }
        }

    }


    [DomainComponent]
    public class OrderLineMetNP : NonePercentObject
    {

        string _SysCode;
        public string SysCode
        {
            get { return _SysCode; }
            set { _SysCode = value; }
        }


        OrderLine _OrderLine;
        public OrderLine OrderLine
        {
            get { return _OrderLine; }
            set { _OrderLine = value; }
        }

        StockItem _StockItem;
        public StockItem StockItem
        {
            get { return _StockItem; }
            set { _StockItem = value; }
        }

        double _ReserVation;
        public double Reservation
        {
            get { return _ReserVation; }
            set { _ReserVation = value; }
        }

        double _NoneMet;
        public double NoneMet
        {
            get { return _NoneMet; }
            set { _NoneMet = value; }
        } 

        double _QuantityAvaibleStock;
        public double QuantityAvaibleStock
        {
            get { return _QuantityAvaibleStock; }
            set { _QuantityAvaibleStock = value; }
        }

        double _QuantityReservationStock;
        public double QuantityReservationStock
        {
            get { return _QuantityReservationStock; }
            set { _QuantityReservationStock = value; }
        }

        double _QuantityPick;
        public double QuantityPick
        {
            get { return _QuantityPick; }
            set { _QuantityPick = value; }
        }

        double _QuantityOnWay;
        public double QuantityOnWay
        {
            get { return _QuantityOnWay; }
            set { _QuantityOnWay = value; }
        }

        double _QuantityLastAvailable;
        public double QuantityLastAvailable
        {
            get { return _QuantityLastAvailable; }
            set { _QuantityLastAvailable = value; }
        } 

        int _Index;
        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        }


    } 

    [DomainComponent]
    public class StockCheckNP : NonePercentObject
    {  
        StockControl _StockControl;
        public StockControl StockControl
        {
            get { return _StockControl; }
            set { _StockControl = value; }
        }

        Person _Person;
        public Person Person
        {
            get { return _Person; }
            set { _Person = value; }
        }


    }
     

    [DomainComponent]
    public class VehiclePlanedNP
    {
        Person _Driver;
        public Person Driver
        {
            get { return _Driver; }
            set { _Driver = value; }
        }


        Vehicle _Vehicle;
        public Vehicle Vehicle
        {
            get { return _Vehicle; }
            set { _Vehicle = value; }
        }  
    }

    [DomainComponent]
    public class CreateLabelContainerNP
    {

        int _Quantity;
        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }


        String _BatchCode;
        public String BatchCode
        {
            get { return _BatchCode; }
            set { _BatchCode = value; }
        }

        String _ExpireDate;
        public String ExpireDate
        {
            get { return _ExpireDate; }
            set { _ExpireDate = value; }
        } 

    }

    [DomainComponent]
    public class LabelGenerateNpm:NonePercentObject
    {
        int _Quantity;
        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

    }
     


}

