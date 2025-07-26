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

    [Table("RoutePlanTransport")]
    [DefaultClassOptions]
    public class RoutePlanTransport : BaseObjectState
    {
        public RoutePlanTransport()
        {
            WorkingTimes = new List<WorkingTime>(); 
        }

        WorkingTime _WorkingTime;
        public virtual WorkingTime WorkingTime
        {
            get { return _WorkingTime; }
            set { _WorkingTime = value; }
        }


        Owner _Owner;
        public virtual Owner Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }

        String _Description;
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        int? _TotalStation;
        public int? TotalStation
        {
            get { return _TotalStation; }
            set { _TotalStation = value; }
        }

        int? _TotalDuration;
        public int? TotalDuration
        {
            get { return _TotalDuration; }
            set { _TotalDuration = value; }
        }

        int? _TotalKm;
        public int? TotalKm
        {
            get { return _TotalKm; }
            set { _TotalKm = value; }
        } 

        public override void OnSaving()
        {
            base.OnSaving();
            if(WorkingTimes.Count>0)
            {
                TotalStation = WorkingTimes.Count();

            }
        }


        public virtual IList<WorkingTime> WorkingTimes { get; set; }


    }
}
