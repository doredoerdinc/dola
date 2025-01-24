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

    [Table("RoutePlan")]
    [DefaultClassOptions]
    public class RoutePlan : BaseObjectState
    {
        public RoutePlan()
        {
            Trips = new List<Trip>();
            Orders = new List<Order>();
        }

        String _Description;
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        Address _StartAddress;
        public virtual Address StartAddress
        {
            get { return _StartAddress; }
            set { _StartAddress = value; }
        }

        DateTime? _PlannedStartTime;
        public DateTime? PlannedStartTime
        {
            get { return _PlannedStartTime; }
            set { _PlannedStartTime = value; }
        }

        DateTime? _PlannedFinishedTime;
        public DateTime? PlannedFinishedTime
        {
            get { return _PlannedFinishedTime; }
            set { _PlannedFinishedTime = value; }
        }

        public virtual IList<Trip> Trips { get; set; }
        public virtual IList<Order> Orders { get; set; }


    }
}
