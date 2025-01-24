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
    [Table("UnitConvert")]
    [XafDefaultProperty("Name")]
    public class UnitConvert:BaseObjectC
    { 
        Item _Item;
        public Item Item
        {
            get { return _Item; }
            set { _Item = value; }
        } 

        UnitType _ToUnit;
        public virtual UnitType ToUnit
        {
            get { return _ToUnit; }
            set { _ToUnit = value; }
        }

        double _Quantity;
        public double Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        } 

    }

    public enum EnumCreatType 
    {
        None=1,
        Tariff=2,
        UnTariff=3
    }

    public enum EnumTransportUnitType
    {
        Pallete = 1,
        Package = 2,
    }

    [DefaultClassOptions]
    [Table("Attribute")]
    [XafDefaultProperty("Name")]
    public class Attribute : BaseObjectI
    {

        public Attribute()
        {
            
        }
       
        Order _Order;
        public virtual Order Order
        {
            get { return _Order; }
            set { _Order = value; }
        }

        OrderLine _OrderLine;
        public virtual OrderLine OrderLine
        {
            get { return _OrderLine; }
            set { _OrderLine = value; }
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

        String _SerialNumber;
        public String SerialNumber
        {
            get { return _SerialNumber; }
            set { _SerialNumber = value; }
        }

        String _ContainerCode;
        public String ContainerCode
        {
            get { return _ContainerCode; }
            set { _ContainerCode = value; }
        } 
    }

    [DefaultClassOptions]
    [Table("OrderType")]
    [XafDefaultProperty("Name")] 
    public class OrderType : BaseLookupC
    {
        public OrderType() 
        {
            ParentGroups = new List<OrderType>();
            AltGroups = new List<OrderType>();
        } 

        OrderType _ParentGroup;
        public virtual OrderType ParentGroup
        {
            get { return _ParentGroup; }
            set { _ParentGroup = value; }
        }

        OrderType _AltGroup;
        public virtual OrderType AltGroup
        {
            get { return _AltGroup; }
            set { _AltGroup = value; }
        }  
        public virtual IList<OrderType> ParentGroups { get; set; } 

        [InverseProperty("ParentGroup")]
        public virtual IList<OrderType> AltGroups { get; set; } 

    } 
    public abstract class OrderBase : BaseObjectWarehouseState
    {
        public OrderBase(){}   

        DateTime? _RequestTime;
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime? RequestTime
        {
            get { return _RequestTime; }
            set { _RequestTime = value; }
        }

        DateTime? _RequestFinishTime;
    //    [RuleRequiredField(DefaultContexts.Save)]
        public DateTime? RequestFinishTime
        {
            get { return _RequestFinishTime; }
            set { _RequestFinishTime = value; }
        } 

        DateTime? _StartTime;
        public DateTime? StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        } 
        DateTime? _FinishTime;
        public DateTime? FinishTime
        {
            get { return _FinishTime; }
            set { _FinishTime = value; }
        } 

        String _Description;
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        Owner _Owner; 
        public virtual Owner Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        } 

        Address _FromAddress;
        public virtual Address FromAddress
        {
            get { return _FromAddress; }
            set { _FromAddress = value; }
        }

        Address _ToAddress;
        public virtual Address ToAddress
        {
            get { return _ToAddress; }
            set { _ToAddress = value; }
        } 

        VehicleType _VehicleType;
        public virtual VehicleType VehicleType
        {
            get { return _VehicleType; }
            set { _VehicleType = value; }
        }

        double? _Gross;
        public double? Gross
        {
            get { return _Gross; }
            set { _Gross = value; }
        }


        double? _Volume;
        public double? Volume
        {
            get { return _Volume; }
            set { _Volume = value; }
        }

        double? _Quantity;
        public double? Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        UnitType _UnitType;
        public UnitType UnitType
        {
            get { return _UnitType; }
            set { _UnitType = value; }
        }

        double? _TransporterQuantity;
        public double? TransporterQuantity
        {
            get { return _TransporterQuantity; }
            set { _TransporterQuantity = value; }
        }


        public override void OnSaving()
        {
            base.OnSaving();

        } 
        public override void OnCreated()
        {
            base.OnCreated();
            RequestTime = DateTime.Now;
            State = ObjectSpace.GetObjectByKey<State>("Created");
        }
      
        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
        }   
    }

    //[DefaultClassOptions]
    //[Table("Container")]
    //public class Container : BaseObjectC
    //{
    //    public Container()
    //    {

    //    }
    //    string _Barcode;
    //    public string Barcode
    //    {
    //        get { return _Barcode; }
    //        set { _Barcode = value; }
    //    }

    //    LocationStock _Location;
    //    public virtual LocationStock LocationStock
    //    {
    //        get { return _Location; }
    //        set { _Location = value; }
    //    }


    //    ContainerTemplate _LabelTemplate;
    //    public virtual ContainerTemplate ContainerTemplate
    //    {
    //        get { return _LabelTemplate; }
    //        set { _LabelTemplate = value; }
    //    }

    //    int _Value;
    //    public int Value
    //    {
    //        get { return _Value; }
    //        set { _Value = value; }
    //    }

    //    String _ParentBarcode;
    //    public String ParentBarcode
    //    {
    //        get { return _ParentBarcode; }
    //        set { _ParentBarcode = value; }
    //    } 

    //    double? _AvailableQuantity;
    //    public double? AvailableQuantity
    //    {
    //        get { return _AvailableQuantity; }
    //        set { _AvailableQuantity = value; }
    //    }

    //    Order _Order;
    //    public virtual Order Order
    //    {
    //        get { return _Order; }
    //        set { _Order = value; }
    //    }

    //} 

    [DefaultClassOptions]
    [Table("OrderLine")]
    public class OrderLine : BaseObjectWarehouseState
    {
        public OrderLine()
        {
            Containeries = new List<Container>();
            Transactions = new List<TaskTransaction>();
            ItemReservations = new List<StockItemReservation>();
            Attributes = new List<Attribute>();
        }
        public virtual IList<Container> Containeries { get; set; }

        public virtual IList<Attribute> Attributes { get; set; }
        public virtual IList<TaskTransaction> Transactions { get; set; }
        public virtual IList<StockItemReservation> ItemReservations { get; set; } 

        double? _QuantityCheck;
        public double? QuantityCheck
        {
            get { return _QuantityCheck; }
            set { _QuantityCheck = value; }
        }

        Order _Order;
        public virtual Order Order
        {
            get { return _Order; }
            set { _Order = value; }
        }

        Owner _Owner;
        public virtual Owner Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }


        Item _Item;
       // [DataSourceCriteria("Owner.Syscode = Order.Owner.Syscode")]
        public virtual Item Item
        {
            get { return _Item; }
            set { _Item = value; }
        }

        TaskTemplate _TaskTemplate;
        public virtual TaskTemplate TaskTemplate
        {
            get { return _TaskTemplate; }
            set { _TaskTemplate = value; }
        } 

        UnitType _OperationUnitType;
        public virtual UnitType OperationUnitType
        {
            get { return _OperationUnitType; }
            set { _OperationUnitType = value; }
        }

        Double? _Groos;
        public Double? Groos
        {
            get { return _Groos; }
            set { _Groos = value; }
        }

        Double? _VehicleQuantity;
        public Double? VehicleQuantity
        {
            get { return _VehicleQuantity; }
            set { _VehicleQuantity = value; }
        }

        string _DocumentQuantity;
        public string DocumentQuantity
        {
            get { return _DocumentQuantity; }
            set { _DocumentQuantity = value; }
        }

        double? _RequestQuantity;
        public double? RequestQuantity
        {
            get { return _RequestQuantity; }
            set { _RequestQuantity = value; }
        }

        double? _OperationQuantity;
        public double? OperationQuantity
        {
            get { return _OperationQuantity; }
            set { _OperationQuantity = value; }
        }

        DateTime? _FinishTime;
        public DateTime? FinishTime
        {
            get { return _FinishTime; }
            set { _FinishTime = value; }
        }

        DateTime? _StartTime;
        public DateTime? StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        } 

        double? _FinishQuantity;
        public double? FinishQuantity
        {
            get { return _FinishQuantity; }
            set { _FinishQuantity = value; }
        }

        double? _ReservationQuantity;
        public double? ReservationQuantity
        {
            get { return _ReservationQuantity; }
            set { _ReservationQuantity = value; }
        } 

        double? _TransporterQuantity;
        public double? TransporterQuantity
        {
            get { return _TransporterQuantity; }
            set { _TransporterQuantity = value; }
        }

        string _IntegrationCode;
        public string IntegrationCode
        {
            get { return _IntegrationCode; }
            set { _IntegrationCode = value; }
        }


        public override void OnSaving()
        {
            base.OnSaving();
        }

    }
    [Table("FileType")]
    public class FileType : BaseLookupC
    {
    }

    [Table("OrderFile")]
    public class OrderFile:BaseObjectC
    {  
        String _Description;
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        Order _Order;
        public virtual Order Order
        {
            get { return _Order; }
            set { _Order = value; }
        } 
        FileData _File;
        public virtual FileData File
        {
            get { return _File; }
            set { _File = value; }
        }

        LocationWarehouse _Location;
        public virtual LocationWarehouse Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

    }

    [Table("Order")]
    [DefaultClassOptions]
    public class Order : OrderBase, IRoute
    { 
        public Order()
        {
            Items = new List<OrderLine>();
            //        Trips = new List<Trip>();
            Containeries = new List<Container>();
            Files = new List<OrderFile>();
            EntryLocations = new List<EntryLocation>();
            StockItemReservations = new List<StockItemReservation>();
            Attributes = new List<Attribute>();
             
        }
        public override void OnSaving()
        {
            base.OnSaving();
        }

        String _CancelReason;
        public String CancelReason
        {
            get { return _CancelReason; }
            set { _CancelReason = value; }
        }

        EntryLocation _EntryLocation;
        public virtual EntryLocation EntryLocation
        {
            get { return _EntryLocation; }
            set { _EntryLocation = value; }
        }

        TaskTemplate _LastTaskTemplate;
        public virtual TaskTemplate TaskTemplate
        {
            get { return _LastTaskTemplate; }
            set { _LastTaskTemplate = value; }
        } 
        string _IntegrationCode;
        [RuleUniqueValue]
        public string IntegrationCode
        {
            get { return _IntegrationCode; }
            set { _IntegrationCode = value; }
        }

        RoutePlan _RoutePlan;
        public virtual RoutePlan RoutePlan
        {
            get { return _RoutePlan; }
            set { _RoutePlan = value; }
        }

        EntryLocation _LastEntryLocation;
        public virtual EntryLocation LastEntryLocation
        {
            get { return _LastEntryLocation; }
            set { _LastEntryLocation = value; }
        }

        LocationWarehouse _LocationWarehouse;
        public virtual LocationWarehouse LocationWarehouse
        {
            get { return _LocationWarehouse; }
            set { _LocationWarehouse = value; }
        } 
        public virtual IList<Container> Containeries { get; set; }
        public virtual IList<OrderLine> Items { get; set; }

        public virtual IList<Attribute> Attributes { get; set; }
        public virtual IList<OrderFile> Files { get; set; }

        [Aggregated]
        [InverseProperty("Orders")]
        public virtual IList<EntryLocation> EntryLocations { get; set; }
        public virtual IList<StockItemReservation> StockItemReservations { get; set; }

        Trip _Trip;
        public virtual Trip Trip
        {
            get { return _Trip; }
            set { _Trip = value; }
        }

        OrderType _OrderType;
        [RuleRequiredField(DefaultContexts.Save)]
        public virtual OrderType OrderType
        {
            get { return _OrderType; }
            set { _OrderType = value; }
        }
         

    }

    [Table("StockItemReservation")]
    [DefaultClassOptions]
    public class StockItemReservation : BaseObjectWarehouseCode
    {

        Order _Order;
        public virtual Order Order
        {
            get { return _Order; }
            set { _Order = value; }
        } 

        OrderLine _OrderLine;
        public virtual OrderLine OrderLine
        {
            get { return _OrderLine; }
            set { _OrderLine = value; }
        }

        Container _Container;
        public virtual Container Container
        {
            get { return _Container; }
            set { _Container = value; }
        } 

        double? _ReservationQuantity;
        public double? ReservationQuantity
        {
            get { return _ReservationQuantity; }
            set { _ReservationQuantity = value; }
        }

        double? _OperationQuantity;
        public double? OperationQuantity
        {
            get { return _OperationQuantity; }
            set { _OperationQuantity = value; }
        }

        StockItem _StockItem;
        public virtual StockItem StockItem
        {
            get { return _StockItem; }
            set { _StockItem = value; }
        } 

        int _Index;
        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        } 

        public override void OnSaving()
        {
            base.OnSaving();
            if (ObjectSpace != null&&OrderLine!=null)
            {
                var stock = ObjectSpace.GetObject<OrderLine>(OrderLine);
                stock.ReservationQuantity=stock.ItemReservations.Sum(x => x.ReservationQuantity);
            }
        }
    }
}
