using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DevExpress.ExpressApp.DC;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp;

using LgsLib.Base.PermissionPolicy;
using DevExpress.Persistent.Validation;
using System.Xml.Serialization;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Drawing;
using LgsLib.Base;
using DevExpress.ExpressApp.Editors;

namespace dola.Module
{
    [DisplayName("User"), ImageName("BO_User")]
    [DefaultClassOptions]
    [Table("AddressRouteMatrix")]
    public class AddressRouteMatrix:BaseObjectI
    {  
            Address _FromAddress;
            [Column("FromAddress_Code")]
            public virtual Address FromAddress
            {
                get { return _FromAddress; }
                set { _FromAddress = value; }
            }

            Address _ToAddressCode;
            [Column("FromAddress_Code")]
            public virtual Address ToAddressCode
            {
                get { return _ToAddressCode; }
                set { _ToAddressCode = value; }
            }

            double? _DictanceMeters;
            public double? DictanceMeters
            {
                get { return _DictanceMeters; }
                set { _DictanceMeters = value; }
            }

            string _Duration;
            public string Duration
            {
                get { return _Duration; }
                set { _Duration = value; }
            }


            string _StaticDuration;
            public string StaticDuration
            {
                get { return _StaticDuration; }
                set { _StaticDuration = value; }
            }

        string _EncodedPolyline;
            [Column(TypeName = "varchar(MAX)")]
            public string EncodedPolyline
            {
                get { return _EncodedPolyline; }
                set { _EncodedPolyline = value; }
            } 
        }
    }
