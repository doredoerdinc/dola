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
namespace LgsLib.Base
{

    public abstract class CompanyBase : BaseObjectC
    {
        string _LegalName;
        [RuleRequiredField(DefaultContexts.Save)]
        public string LegalName
        {
            get { return _LegalName; }
            set { _LegalName = value; }
        }

        string _ShortName;
        public string ShortName
        {
            get { return _ShortName; }
            set { _ShortName = value; }
        }


        String _TaxCode;
      //  [System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = true)] 
        [MaxLength(100)]
        public String TaxCode
        {
            get { return _TaxCode; }
            set { _TaxCode = value; }
        }

        string _TaxOfficeName;
      //  [RuleRequiredField(DefaultContexts.Save)]
        public string TaxOfficeName
        {
            get { return _TaxOfficeName; }
            set { _TaxOfficeName = value; }
        }

        string _IBAN;
     //   [RuleRequiredField(DefaultContexts.Save)]
        public string IBAN
        {
            get { return _IBAN; }
            set { _IBAN = value; }
        }

        string _DomainName;
        public string DomainName
        {
            get { return _DomainName; }
            set { _DomainName = value; }
        }

        public override void OnSaving()
        {
            base.OnSaving();
            if(IsNewObject && TaxCode!=null)
            {
                TaxCode = TaxCode.Replace(" ", "");
                SysCode = TaxCode;
            }
        }

    }

    [Table("Warehouse")]
    [DefaultClassOptions]
    public class Warehouse : CompanyBase
    {
        public Warehouse()
        {
            
            //   DocumentDesings = new List<ReportDataV2>();
        } 

    }

    [Table("Country")]
    [DefaultClassOptions]
    [XafDefaultProperty(nameof(Name))]
    public class Country : BaseLookupC
    {
        public Country() { }

    }

    [Table("City")]
    [DefaultClassOptions]
    [XafDefaultProperty(nameof(Name))]
    public class City : BaseObjectI
    {
        public City() { }


        String _Name;
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }



        Country _Country;
        public virtual Country Country
        {
            get { return _Country; }
            set { _Country = value; }
        }


    }
    [Table("District")]
    [DefaultClassOptions]
    [XafDefaultProperty(nameof(Name))] 
    public class District : BaseObjectI
    {
        public District() { }

        String _Name;
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        
        City _City;
        public virtual City City
        {
            get { return _City; }
            set { _City = value; }
        }
         

    }  

    [Table("Neighborhood")]
    [DefaultClassOptions]
    [XafDefaultProperty(nameof(Name))]
    public class Neighborhood : BaseObjectI
    {
        public Neighborhood() { }

        String _Name;
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        } 

        District _District;
        public virtual District District
        {
            get { return _District; }
            set { _District = value; }
        }

        Town _Town;
        public virtual Town Town
        {
            get { return _Town; }
            set { _Town = value; }
        }

        City _City;
        public virtual City City
        {
            get { return _City; }
            set { _City = value; }
        }

        double? _PostalCode;
        public double? PostalCode
        {
            get { return _PostalCode; }
            set { _PostalCode = value; }
        } 


    }

    [DefaultClassOptions]
    [Table("Town")]
    [XafDefaultProperty(nameof(Name))]
    public class Town : BaseObjectI
    {
        public Town() { }

        String _Name;
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        District _District;
        public virtual District District
        {
            get { return _District; }
            set { _District = value; }
        }

        City _City;
        public virtual City City
        {
            get { return _City; }
            set { _City = value; }
        }

    }


}
 