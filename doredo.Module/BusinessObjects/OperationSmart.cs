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

using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;

namespace dola.Module
{
    [DefaultClassOptions]
    [Table("LocationOperation")]
    public class LocationOperation : BaseObjectC
    {
        public LocationOperation(){
            OperationTariffs = new List<LocationOperationTariff>();
        }

        public virtual IList<LocationOperationTariff> OperationTariffs { get; set; }

        String _Name;
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }  

        int _Index;
        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        }

        EnumOperationType _OperationType;
        public EnumOperationType OperationType
        {
            get { return _OperationType; }
            set { _OperationType = value; }
        }

    }

    [DefaultClassOptions]
    [Table("LocationOperationTariff")]
    public class LocationOperationTariff : BaseObjectWarehouseID
    {
        public LocationOperationTariff()
        {

        }

        LocationWarehouse _LocationWarehouse;
        [RuleRequiredField(DefaultContexts.Save)]
        public LocationWarehouse LocationWarehouse
        {
            get { return _LocationWarehouse; }
            set { _LocationWarehouse= value; }
        }

        LocationOperation _OperationTariff;
        [RuleRequiredField(DefaultContexts.Save)]
        public virtual LocationOperation OperationTariff
        {
            get { return _OperationTariff; }
            set { _OperationTariff = value; }
        }

        
        Type _StockItemType = typeof(StockItem);
        [NotMapped]
        public Type StockItemType
        {
            get { return _StockItemType; }
            set { _StockItemType = value; }
        } 

        String _StockItemRule;
        [FieldSize(FieldSizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.PopupCriteriaPropertyEditor)]
        [CriteriaOptions("StockItemType")]
        public String StockItemRule
        {
            get { return _StockItemRule; }
            set { _StockItemRule = value; }
        } 

        int _Index;
        [RuleRequiredField(DefaultContexts.Save)]
        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        }

        double? _ReplenanchmentPercent;
        public double? ReplenanchmentPercent
        {
            get { return _ReplenanchmentPercent; }
            set { _ReplenanchmentPercent = value; }
        }


    }



    [DefaultClassOptions]
    [Table("RotaOperationSmart")]
    public class RotaOperationSmart : BaseObjectC
    { 
        VehicleType _VehicleType;
        public virtual VehicleType VehicleType
        {
            get { return _VehicleType; }
            set { _VehicleType = value; }
        }


        int _Index;
        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        }

        Type _LocationType = typeof(LocationWarehouse);
        [NotMapped]
        public Type LocationType
        {
            get { return _LocationType; }
            set { _LocationType = value; }
        }


        Type _VehicleTypeObject = typeof(VehicleType);
        [NotMapped]
        public Type VehicleTypeObject
        {
            get { return _VehicleTypeObject; }
            set { _VehicleTypeObject = value; }
        }

        Type _OrderType = typeof(Order);
        [NotMapped]
        public Type OrderType
        {
            get { return _OrderType; }
            set { _OrderType = value; }
        }

        Type _ItemType = typeof(Item);
        [NotMapped]
        public Type ItemType
        {
            get { return _ItemType; }
            set { _ItemType = value; }
        }

        String _ItemRule;
        [FieldSize(FieldSizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.PopupCriteriaPropertyEditor)]
        [CriteriaOptions("ItemType")]
        public String ItemRule
        {
            get { return _ItemRule; }
            set { _ItemRule = value; }
        }

        String _LocationRule;
        [FieldSize(FieldSizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.PopupCriteriaPropertyEditor)]
        [CriteriaOptions("LocationType")]
        public String LocationRule
        {
            get { return _LocationRule; }
            set { _LocationRule = value; }
        }

        string _OrderRule;
        [FieldSize(FieldSizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.PopupCriteriaPropertyEditor)]
        [CriteriaOptions("OrderType")]
        public string OrderRule
        {
            get { return _OrderRule; }
            set { _OrderRule = value; }
        }

        string _VehicleRule;
        [FieldSize(FieldSizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.PopupCriteriaPropertyEditor)]
        [CriteriaOptions("VehicleTypeObject")]
        public string VehicleRule
        {
            get { return _VehicleRule; }
            set { _VehicleRule = value; }
        }

    }
}
