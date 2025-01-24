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

namespace Lys.Module
{

    [DefaultClassOptions]
    [Table("UEDTSItemType")]
    public class UEDTSItemType : BaseLookupC
    {
        public UEDTSItemType()
        {
            
        }

        UnitType _UnitType;
        public virtual UnitType UnitType
        {
            get { return _UnitType; }
            set { _UnitType = value; }
        } 

        bool _Specified;
        public bool Specified
        {
            get { return _Specified; }
            set { _Specified = value; }
        }
    }
}
