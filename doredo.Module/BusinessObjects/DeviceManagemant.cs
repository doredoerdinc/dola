using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using LgsLib.Base;
using LgsLib.Base.PermissionPolicy;
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
    [Table("DeviceInfo")]
    [XafDefaultProperty("SysCode")]
    public class DeviceInfo:BaseObjectWarehouseCode
    {
        public DeviceInfo() { }


        State _State;
        public virtual State State
        {
            get { return _State; }
            set { _State = value; }
        }


        String _DeviceType;
        public String DeviceType
        {
            get { return _DeviceType; }
            set { _DeviceType = value; }
        }


        String _GSMNumber;
        public String GSMNumber
        {
            get { return _GSMNumber; }
            set { _GSMNumber = value; }
        }

        User _User;
        public virtual User User
        {
            get { return _User; }
            set { _User = value; }
        }

        String _AppVersion;
        public String AppVersion
        {
            get { return _AppVersion; }
            set { _AppVersion = value; }
        }

        String _Model;
        public String Model
        {
            get { return _Model; }
            set { _Model = value; }
        }

        string _Manufacture;
        public string Manufacture
        {
            get { return _Manufacture; }
            set { _Manufacture = value; }
        }

        string _OsVersion;
        public string OsVersion
        {
            get { return _OsVersion; }
            set { _OsVersion = value; }
        }		 
    }
}
