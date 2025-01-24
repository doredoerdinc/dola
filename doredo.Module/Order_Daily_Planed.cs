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

namespace dola.Module
{
    //[Table("OrderDailyPlaned")]
    //[DefaultClassOptions]
  
    //public class OrderDailyPlaned : BaseObjectC
    //{
    //    public OrderDailyPlaned()
    //    {
    //        Lines = new List<OrderItem>();

    //    }

    //    Owner _Owner;
    //    public virtual Owner Owner
    //    {
    //        get { return _Owner; }
    //        set { _Owner = value; }
    //    }

    //    DateTime _PlanedStartTime;
    //    public DateTime PlanedStartTime
    //    {
    //        get { return _PlanedStartTime; }
    //        set { _PlanedStartTime = value; }
    //    }

    //    DateTime? _PlanedFinishTime;
    //    public DateTime? PlanedFinishTime
    //    {
    //        get { return _PlanedFinishTime; }
    //        set { _PlanedFinishTime = value; }
    //    }

    //    DateTime? _StartTime;
    //    public DateTime? StartTime
    //    {
    //        get { return _StartTime; }
    //        set { _StartTime = value; }
    //    }

    //    DateTime? _FinishTime;
    //    public DateTime? FinishTime
    //    {
    //        get { return _FinishTime; }
    //        set { _FinishTime = value; }
    //    }

    //    String _Description;
    //    public String Description
    //    {
    //        get { return _Description; }
    //        set { _Description = value; }
    //    }

    //    String _DocumentName;
    //    public String DocumentName
    //    {
    //        get { return _DocumentName; }
    //        set { _DocumentName = value; }
    //    }


    //    String _IntegrationCode;
    //    public String IntegrationCode
    //    {
    //        get { return _IntegrationCode; }
    //        set { _IntegrationCode = value; }
    //    } 

    //    public virtual IList<OrderItem> Lines { get; set; }
    //}
}
