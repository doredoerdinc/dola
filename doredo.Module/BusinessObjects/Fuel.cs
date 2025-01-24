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

namespace dola.Module
{ 

    public enum EnumFuelType
    {
        Diesel = 0,
        Gasoline = 1,
    }

    [DefaultClassOptions]
    [Table("FuelPriceChange")]
    public class FuelPriceChange : BaseObjectC
    {
        public FuelPriceChange() { }

        City _City;
        [RuleRequiredField(DefaultContexts.Save)]
        public virtual City City
        {
            get { return _City; }
            set { _City = value; }
        }

        District _District;
        [RuleRequiredField(DefaultContexts.Save)]
        public virtual District District
        {
            get { return _District; }
            set { _District = value; }
        }

        DateTime _ChangeDate;
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime ChangeDate
        {
            get { return _ChangeDate; }
            set { _ChangeDate = value; }
        }


        EnumFuelType _FuelType;
        [RuleRequiredField(DefaultContexts.Save)]
        public EnumFuelType FuelType
        {
            get { return _FuelType; }
            set { _FuelType = value; }
        }

        double? _FuelPrice;
        public double? FuelPrice
        {
            get { return _FuelPrice; }
            set { _FuelPrice = value; }
        }
        
        public override void OnSaving()
        {
            base.OnSaving();
            //var obj=ObjectSpace.FindObject(typeof(FuelAmount),)
        }

    }
}
