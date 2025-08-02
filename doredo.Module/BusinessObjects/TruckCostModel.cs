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
    [DefaultClassOptions]
    [Table("TruckCostModel")]
    public class TruckCostModel:BaseObjectI
    {
        double _CurentFuelPrice;
        public double CurentFuelPrice
        {
            get { return _CurentFuelPrice; }
            set { _CurentFuelPrice = value; }
        } 
    }
}
