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
using System.Xml;
using System.Xml.Serialization;
using LgsLib.Base.PermissionPolicy;
 

using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Editors;
using LgsLib.Base;

namespace dola.Module
{
    [DefaultClassOptions]
    [Table("StockControl")] 
    public class StockControl : BaseObjectWarehouseState
    {
        public StockControl()
        {
            Locations = new List<StockControlStep>();
        }
        public virtual IList<StockControlStep> Locations { get; set; }
        String _Name;
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        DateTime? _DateTime;
        public DateTime? DateTime
        {
            get { return _DateTime; }
            set { _DateTime = value; }
        }

        TaskTemplate _Template;
        public virtual TaskTemplate Template
        {
            get { return _Template; }
            set { _Template = value; }
        }

        TaskTemplate _TaskTemplate;
        public virtual TaskTemplate TaskTemplate
        {
            get { return _TaskTemplate; }
            set { _TaskTemplate = value; }
        }


    }
    [DefaultClassOptions]
    [Table("StockControlStep")] 
    public class StockControlStep : BaseObjectWarehouseState
    {

        StockControl _StockControl;
        public virtual StockControl StockControl
        {
            get { return _StockControl; }
            set { _StockControl = value; }
        }

        LocationWarehouse _Location;
        public virtual LocationWarehouse Location
        {
            get { return _Location; }
            set { _Location = value; }
        }


        Person _Person;
        public virtual Person Person
        {
            get { return _Person; }
            set { _Person = value; }
        } 

        int _Index;
        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        }

        TaskStep _TaskStep;
        public virtual TaskStep TaskStep
        {
            get { return _TaskStep; }
            set { _TaskStep = value; }
        }


    }

}
