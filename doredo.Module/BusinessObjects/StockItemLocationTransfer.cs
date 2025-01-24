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
    [Table("StockItemLocationTransfer")]
    [XafDefaultProperty("Container")]
    public class StockItemLocationTransfer : BaseObjectWarehouseID
    {

        LocationWarehouse _FromLocation;
        public virtual LocationWarehouse FromLocation
        {
            get { return _FromLocation; }
            set { _FromLocation = value; }
        }

        LocationWarehouse _ToLocation;
        public virtual LocationWarehouse ToLocation
        {
            get { return _ToLocation; }
            set { _ToLocation = value; }
        }

        Container _Label;
        public virtual Container Label
        {
            get { return _Label; }
            set { _Label = value; }
        }

        double? _Quantity;
        public double? Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }


    }
}
