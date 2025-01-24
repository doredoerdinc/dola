using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
 
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Data.Filtering;

namespace LgsLib.Base
{
    [DefaultClassOptions]
    [Table("OwnerType")]
    public class OwnerType : BaseLookupC
    {
        public OwnerType() { }
    }

    [Table("Owner")]
    [DefaultClassOptions]
    [XafDefaultProperty(nameof(ShortName))]
    public class Owner : CompanyBase
    {
        public Owner()
        {

            
        }

      
        //public virtual IList<CompanyOperationField> Fields { get; set; } 
        String _IntegrationCode;
        //  [System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = true)]
        [MaxLength(100)]
        public String IntegrationCode
        {
            get { return _IntegrationCode; }
            set { _IntegrationCode = value; }
        }

        OwnerType _OwnerType;
        public virtual OwnerType OwnerType
        {
            get { return _OwnerType; }
            set { _OwnerType = value; }
        }

        String _Telephone;
        public String Telephone
        {
            get { return _Telephone; }
            set { _Telephone = value; }
        }

        String _Representative;
        public String Representative
        {
            get { return _Representative; }
            set { _Representative = value; }
        }

        string _Description;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        String _LegalOpenAddress;
        public String LegalOpenAddress
        {
            get { return _LegalOpenAddress; }
            set { _LegalOpenAddress = value; }
        }

        City _LegalCity;
        public virtual City LegalCity
        {
            get { return _LegalCity; }
            set { _LegalCity = value; }
        }

        District _LegalDistrict;
        public virtual District LegalDistrict
        {
            get { return _LegalDistrict; }
            set { _LegalDistrict = value; }
        }  


    }
}
