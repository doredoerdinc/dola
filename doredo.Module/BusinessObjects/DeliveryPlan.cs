//using DevExpress.ExpressApp.DC;
//using DevExpress.Persistent.Base;
//using LgsLib.Base;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//namespace dola.Module
//{
//    [DefaultClassOptions]
//    [Table("DeliveryPlan")]
//    [XafDefaultProperty("DeliveryPlan")]
//    public class DeliveryPlan : BaseObjectC
//    {
//        public DeliveryPlan()
//        {
//            OrderItems = new List<OrderItem>();
//        }
//        public virtual IList<OrderItem> OrderItems { get; set; } 

//        DateTime? _PlannedStartTime;
//        public DateTime? PlannedStartTime
//        {
//            get { return _PlannedStartTime; }
//            set { _PlannedStartTime = value; }
//        } 

//        DateTime? _StartTime;
//        public DateTime? StartTime
//        {
//            get { return _StartTime; }
//            set { _StartTime = value; }
//        }

//        DateTime? _FinishTime;
//        public DateTime? FinishTime
//        {
//            get { return _FinishTime; }
//            set { _FinishTime = value; }
//        }

//        WorkingArea _WorkingArea;
//        public virtual WorkingArea WorkingArea
//        {
//            get { return _WorkingArea; }
//            set { _WorkingArea = value; }
//        }

//        Address _FirstAddress;
//        public virtual Address FirstAddress
//        {
//            get { return _FirstAddress; }
//            set { _FirstAddress = value; }
//        }

//        State _State;
//        public virtual State State
//        {
//            get { return _State; }
//            set { _State = value; }
//        }


//    }
//}