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
using System.Drawing;

namespace dola.Module
{ 
    public enum EnumClock : long
    {
        [Display(Name = "00")]
        C00 = 00,
        [Display(Name = "01")]
        C01 = 01,
        [Display(Name = "02")]
        C02 = 02,
        [Display(Name = "03")]
        C03 = 03,
        [Display(Name = "04")]
        C04 = 04,
        [Display(Name = "04")]
        C05 = 05,
        [Display(Name = "05")]
        C06 = 06,
        C07 = 07,
        C08 = 08,
        C09 = 09,
        C10 = 10,
        C11 = 11,
        C12 = 12,
        C13 = 13,
        C14 = 14,
        C15 = 15,
        C16 = 16,
        C17 = 17,
        C18 = 18,
        C19 = 19,
        C20 = 20,
        C21 = 21,
        C22 = 22,
        C23 = 23,
        C24 = 24
    }

    [Table("ShiftType")]
    [DefaultClassOptions]
    public class ShiftType : BaseObjectC
    {
        public ShiftType(){}

        String _Name;
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        EnumClock _StartClock;
        [Column("shift_start_clock")]
        public EnumClock StartClock
        {
            get { return _StartClock; }
            set { _StartClock = value; }
        }

        EnumClock _FinishDate;
        [Column("shift_finish_clock")]
        public EnumClock FinishDate
        {
            get { return _FinishDate; }
            set { _FinishDate = value; }
        }

    } 

    //[Table("ShiftOperationVehicle")]
    //[DefaultClassOptions]
    //public class ShiftOperationVehicle : BaseObjectC
    //{
    //    public ShiftOperationVehicle() 
    //    {
    //        Vehicles = new List<VehicleDailyData>(); 
    //    }

    //    ShiftType _ShiftType;
    //    public ShiftType ShiftType
    //    {
    //        get { return _ShiftType; }
    //        set { _ShiftType = value; }
    //    }

    //    Person _Person;
    //    public virtual Person Person
    //    {
    //        get { return _Person; }
    //        set { _Person = value; }
    //    }

    //    DateTime? _ShiftDay;
    //    public DateTime? ShiftDay
    //    {
    //        get { return _ShiftDay; }
    //        set { _ShiftDay = value; }
    //    } 

    //    public virtual List<VehicleDailyData> Vehicles { get; set; }

    //}
}
