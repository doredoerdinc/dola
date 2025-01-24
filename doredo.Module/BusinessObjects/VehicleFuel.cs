using DevExpress.Persistent.Base;
using LgsLib.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dola.Module
{
    [Table("VehicleFuel")]
    [DefaultClassOptions]
    public class VehicleFuel : BaseObjectC
    {
        Vehicle _Vehicle;
        public virtual Vehicle Vehicle
        {
            get { return _Vehicle; }
            set { _Vehicle = value; }
        }

        double? _FuelQuantity;
        public double? FuelQuantity
        {
            get { return _FuelQuantity; }
            set { _FuelQuantity = value; }
        }

        double? _FuelAmount;
        public double? FuelAmount
        {
            get { return _FuelAmount; }
            set { _FuelAmount = value; }
        }

        double? _Odometer;
        public double? Odometer
        {
            get { return _Odometer; }
            set { _Odometer = value; }
        }


        Person _Employee;
        public virtual Person Employee
        {
            get { return _Employee; }
            set { _Employee = value; }
        }
    }
}
