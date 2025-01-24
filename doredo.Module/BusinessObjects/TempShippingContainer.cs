using System;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.ComponentModel;
using DevExpress.ExpressApp.EF.Updating;
using LgsLib.Base;
using LgsLib.Base.PermissionPolicy;
using LgsLib.Base.StateLGS;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.Persistent.Base;

namespace dola.Module
{
    [Table("ContainerTemp")]
    [DefaultClassOptions]
    public class ContainerTemp:BaseObjectI
    {
        String _Containercoed;
        public String Containercoed
        {
            get { return _Containercoed; }
            set { _Containercoed = value; }
        }

        String _BatchCode;
        public String BatchCode
        {
            get { return _BatchCode; }
            set { _BatchCode = value; }
        }

        String _ItemCode;
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }

        String _ItemName;
        public String ItemName
        {
            get { return _ItemName; }
            set { _ItemName = value; }
        }

        String _Quantity;
        public String Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        String _ExpireDate;
        public String ExpireDate
        {
            get { return _ExpireDate; }
            set { _ExpireDate = value; }
        } 

        OrderLine _OrderLine;
        public OrderLine OrderLine
        {
            get { return _OrderLine; }
            set { _OrderLine = value; }
        }  

    }
}
