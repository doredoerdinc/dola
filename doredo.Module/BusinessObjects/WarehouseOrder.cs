using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using LgsLib.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using LgsLib.Base.PermissionPolicy;
namespace dola.Module
{
    public enum LocationType : int
    {
        Vehicle = 0,
        Cell = 1,
        Transport = -1,
        Perron = 0
    }

   

    public enum EnumWarehouseOrderTypeReason : int
    {
        None = 0,
        /// <summary>
        /// Satış Alma Siparişi
        /// </summary>
        Purchase = 1,
        /// <summary>
        /// İade
        /// </summary>
        Return = 2,
        /// <summary>
        /// Genel
        /// </summary>
        General = 3,
        /// <summary>
        /// Numune
        /// </summary>
        Sampling = 4,
        /// <summary>
        /// İmha
        /// </summary>
        Destruction = 5,
        /// <summary>
        /// Depolararası transfer
        /// </summary>
        TransferBetweenDepots = 6,
        /// <summary>
        /// Hasarlı
        /// </summary>
        Damaged = 7,
        /// <summary>
        /// Mix Ürün Girişi
        /// </summary>
        GoodsInMix = 8,
        /// <summary>
        /// Müx Ürün Çıkışı
        /// </summary>
        GoodsOutMix = 9,
        /// <summary>
        /// Satış Siparişi
        /// </summary>
        SalesOrder = 10,
        /// <summary>
        /// Promosyon
        /// </summary>
        Promotion = 11,//promosyon
        /// <summary>
        /// Depolar arası transfer girişi
        /// </summary>
        TransferBetweenDepotsin = 12,
        /// <summary>
        /// Depolar arası transfer çıkışı
        /// </summary>
        TransferBetweenDepotsOut = 13,
        /// <summary>
        /// Sarf malzeme çıkış siparişi
        /// </summary>
        GoodsOutConsumable = 14,
        /// <summary>
        /// Sarf malzeme giriş siparişi
        /// </summary>
        GoodsInConsumable = 15,

        CrossDock = 16
    }

    [DefaultClassOptions]
    [Table("LocationWarehouse")]
    public class LocationWarehouse: BaseObjectC
    {
        public LocationWarehouse()
        {
            LocationWarehouses = new List<LocationWarehouse>();

        }
        public virtual IList<LocationWarehouse> LocationWarehouses { get; set; }

        String _Name;
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        int _Line;
        public int Line
        {
            get { return _Line; }
            set { _Line = value; }
        }

        string _Code;
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        double? _Width;
        public double? Width
        {
            get { return _Width; }
            set { _Width = value; }
        }

        double _Height;
        public double Height
        {
            get { return _Height; }
            set { _Height = value; }
        }

        double? _MaxWeight;
        public double? MaxWeight
        {
            get { return _MaxWeight; }
            set { _MaxWeight = value; }
        }

        double? _Temperature;
        public double? Temperature
        {
            get { return _HTemperature; }
            set { _Temperature = value; }
        }

        double? _MinQuantity;
        public double? MinQuantity
        {
            get { return _MinQuantity; }
            set { _MinQuantity = value; }
        }

        double _MaxQuantity;
        public double MaxQuantity
        {
            get { return _MaxQuantity; }
            set { _MaxQuantity = value; }
        }

        UnitType _TrackingUnitType;
        public UnitType TrackingUnitType
        {
            get { return _TrackingUnitType; }
            set { _TrackingUnitType = value; }
        }

        LocationWarehouse _ParentLocationWarehouse;
        public virtual LocationWarehouse ParentLocationWarehouse
        {
            get { return _ParentLocationWarehouse; }
            set { _ParentLocationWarehouse = value; }
        }


    }

    [DefaultClassOptions]
    [Table("OrderWarehouse")]
    public class OrderWarehouse : BaseObjectC
    {
        public OrderWarehouse()
        {
            OrderWarehouseLines = new List<OrderWarehouseLine>();
        }

        Company _InvoiceCompany;
        public virtual Company InvoiceCompany
        {
            get { return _InvoiceCompany; }
            set { _InvoiceCompany = value; }
        }


        public virtual IList<OrderWarehouseLine> OrderWarehouseLines { get; set; }

        EnumWarehouseOrderType _OrderType;
        public EnumWarehouseOrderType OrderType
        {
            get { return _OrderType; }
            set { _OrderType = value; }
        }

        EnumWarehouseOrderTypeReason _Reason;
        public EnumWarehouseOrderTypeReason Reason
        {
            get { return _Reason; }
            set { _Reason = value; }
        }

        DateTime? _RequestDeliveryTime;
        public DateTime? RequestDeliveryTime
        {
            get { return _RequestDeliveryTime; }
            set { _RequestDeliveryTime = value; }
        }

        DateTime? _RequestOrderTime;
        public DateTime? RequestOrderTime
        {
            get { return _RequestOrderTime; }
            set { _RequestOrderTime = value; }
        }

        DateTime? _OperationStartTime;
        public DateTime? OperationStartTime
        {
            get { return _OperationStartTime; }
            set { _OperationStartTime = value; }
        }

        DateTime? _OperastionFinishTime;
        public DateTime? OperastionFinishTime
        {
            get { return _OperastionFinishTime; }
            set { _OperastionFinishTime = value; }
        }

        String _DocumentCode;
        public String DocumentCode
        {
            get { return _DocumentCode; }
            set { _DocumentCode = value; }
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

        double? _TotalPallete;
        public double? TotalPallete
        {
            get { return _TotalPallete; }
            set { _TotalPallete = value; }
        }

        double? _TotalPackage;
        public double? TotalPackage
        {
            get { return _TotalPackage; }
            set { _TotalPackage = value; }
        }

        double? _TotalWeight;
        public double? TotalWeight
        {
            get { return _TotalWeight; }
            set { _TotalWeight = value; }
        }


    }

    [DefaultClassOptions]
    [Table("OrderWarehouseLine")]
    public class OrderWarehouseLine : BaseObjectState
    {
        public OrderWarehouseLine()
        {
            OrderWarehousesRelations = new List<OrderWarehouse>();
            Carriers = new List<Carrier>();
        }

        public virtual IList<OrderWarehouse> OrderWarehousesRelations { get; set; }
        

        int _LineNumber;
        public int LineNumber
        {
            get { return _LineNumber; }
            set { _LineNumber = value; }
        }

        Item _Item;
        public virtual Item Item
        {
            get { return _Item; }
            set { _Item = value; }
        }

        double? _RequestQuantity;
        public double? RequestQuantity
        {
            get { return _RequestQuantity; }
            set { _RequestQuantity = value; }
        }

        int _RezervationQuantity;
        public int RezervationQuantity
        {
            get { return _RezervationQuantity; }
            set { _RezervationQuantity = value; }
        }

        double? _OperationQuantity;
        public double? OperationQuantity
        {
            get { return _OperationQuantity; }
            set { _OperationQuantity = value; }
        }

        int _ShipmentQuantity;
        public int ShipmentQuantity
        {
            get { return _ShipmentQuantity; }
            set { _ShipmentQuantity = value; }
        }

        String _BatchCode;
        public String BatchCode
        {
            get { return _BatchCode; }
            set { _BatchCode = value; }
        }

        string _ExpireDate;
        public string ExpireDate
        {
            get { return _ExpireDate; }
            set { _ExpireDate = value; }
        }


    }

    [DefaultClassOptions]
    [Table("WorkOrder")]
    public class WorkOrder : BaseObjectCompanyC
    {

        LocationType _FromLocationType;
        public LocationType FromLocationType
        {
            get { return _FromLocationType; }
            set { _FromLocationType = value; }
        }

        string _FromLocation;
        public string FromLocation
        {
            get { return _FromLocation; }
            set { _FromLocation = value; }
        }

        LocationType _ToLocationType;
        public LocationType ToLocationType
        {
            get { return _ToLocationType; }
            set { _ToLocationType = value; }
        }

        string _ToLocation;
        public string ToLocation
        {
            get { return _ToLocation; }
            set { _ToLocation = value; }
        }

        Item _Item;
        public Item Item
        {
            get { return _Item; }
            set { _Item = value; }
        }

        double? _ExecutedQuantity;
        public double? ExecutedQuantity
        {
            get { return _ExecutedQuantity; }
            set { _ExecutedQuantity = value; }
        }

        double? _RequestQuantity;
        public double? RequestQuantity
        {
            get { return _RequestQuantity; }
            set { _RequestQuantity = value; }
        }

        String _Criteria;
        public String Criteria
        {
            get { return _Criteria; }
            set { _Criteria = value; }
        }

        User _AssingedPerson;
        public virtual User AssingedPerson
        {
            get { return _AssingedPerson; }
            set { _AssingedPerson = value; }
        }

        User _ExecutedPerson;
        public virtual User ExecutedPerson
        {
            get { return _ExecutedPerson; }
            set { _ExecutedPerson = value; }
        }

    }


    [DefaultClassOptions]
    [Table("Stock")]
    public class Stock : BaseObjectC
    {
        Item _Item;
        public virtual Item Item
        {
            get { return _Item; }
            set { _Item = value; }
        }

        DateTime? _ProductionDate;
        public DateTime? ProductionDate
        {
            get { return _ProductionDate; }
            set { _ProductionDate = value; }
        }

        string _ExpireDate;
        public string ExpireDate
        {
            get { return _ExpireDate; }
            set { _ExpireDate = value; }
        }

        DateTime? _StockTime;
        public DateTime? StockTime
        {
            get { return _StockTime; }
            set { _StockTime = value; }
        }

        OrderWarehouse _Order;
        public virtual OrderWarehouse Order
        {
            get { return _Order; }
            set { _Order = value; }
        }

        string _Batch;
        public string Batch
        {
            get { return _Batch; }
            set { _Batch = value; }
        }
    }
 
}
