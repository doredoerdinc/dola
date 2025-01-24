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

namespace dola.Module
{ 

    [DefaultClassOptions]
    [Table("LabelStatic")]
    [XafDefaultProperty("Name")]
    public class LabelStatic : BaseObjectC
    {
        String _IntegrationCode;
        public String IntegrationCode
        {
            get { return _IntegrationCode; }
            set { _IntegrationCode = value; }
        }


        String _Container;
        public String Container
        {
            get { return _Container; }
            set { _Container = value; }
        }

        String _ItemBarcode;
        public String ItemBarcode
        {
            get { return _ItemBarcode; }
            set { _ItemBarcode = value; }
        }

        String _Location;
        public String Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        string _Batch;
        public string Batch
        {
            get { return _Batch; }
            set { _Batch = value; }
        }

        string _ExpireDate;
        public string ExpireDate
        {
            get { return _ExpireDate; }
            set { _ExpireDate = value; }
        }

        string _Quantity;
        public string Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

    }
}
