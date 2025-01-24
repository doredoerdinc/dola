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
using DevExpress.Data.Filtering;

namespace dola.Module
{
    [DefaultClassOptions]
    [Table("OperationStation")]
    public class OperationStation : BaseObjectC
    {
        public OperationStation() { }

        String _Description;
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        LocationWarehouse _LocationWarehouse;
        public virtual LocationWarehouse LocationWarehouse
        {
            get { return _LocationWarehouse; }
            set { _LocationWarehouse= value; }
        }

    }
    [DefaultClassOptions]
    [Table("DeviceType")]
    public class DeviceType : BaseObjectC
    {
        public DeviceType() { }

    
    }
    [DefaultClassOptions]
    [Table("Device")]
    public class Device : BaseObjectC
    {  
        public Device()
        {

        }

        OperationStation _Station;
        public virtual OperationStation Station
        {
            get { return _Station; }
            set { _Station = value; }
        }

        DeviceType _DeviceType;
        public virtual DeviceType DeviceType
        {
            get { return _DeviceType; }
            set { _DeviceType = value; }
        }


        String _Name;
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        String _IPAddress;
        public String IPAddress
        {
            get { return _IPAddress; }
            set { _IPAddress = value; }
        }

        ReportDataV2 _Label;
        public virtual ReportDataV2 Label
        {
            get { return _Label; }
            set { _Label = value; }
        } 

    }
}