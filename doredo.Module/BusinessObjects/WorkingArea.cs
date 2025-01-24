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
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.Data.Filtering;

namespace dola.Module
{
    [DefaultClassOptions]
    [Table("WorkingArea")]
    [XafDefaultProperty("SysCode")]
    public class WorkingArea:BaseObjectC
    {
        public WorkingArea()
        {
            Address = new List<Address>();
          
        }

        String _Description;
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        Person _ResponsiblePerson;
        public virtual Person ResponsiblePerson
        {
            get { return _ResponsiblePerson; }
            set { _ResponsiblePerson = value; }
        }

        Person _DeliveryPerson1;
        public virtual Person DeliveryPerson1
        {
            get { return _DeliveryPerson1; }
            set { _DeliveryPerson1 = value; }
        } 
        Person _DeliveryPerson2;
        public virtual Person DeliveryPerson2
        {
            get { return _DeliveryPerson2; }
            set { _DeliveryPerson2 = value; }
        } 

        Vehicle _Vehicle;
        public virtual Vehicle Vehicle
        {
            get { return _Vehicle; }
            set { _Vehicle = value; }
        }
         
        public virtual IList<Address> Address { get; set; } 

    }
}
