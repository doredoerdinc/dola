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
using DevExpress.ExpressApp.Editors;

namespace dola.Module
{ 

    [DefaultClassOptions]
    [Table("AddressType")]
    public class AddressType : BaseLookupC
    {
        public AddressType() { }
    }

    [DefaultClassOptions]
    [Table("Industry")]
    public class Industry : BaseLookupC
    {
        public Industry() { }
    }

    [Table("Address")]
    [DefaultClassOptions]
    [XafDefaultProperty(nameof(Name))] 
    public class Address : BaseLookupC, IMapPoint
    {
        public Address()
        {

        }


        double? _DistanceMaptoAddressQuantity;
        public double? DistanceMaptoAddressQuantity
        {
            get { return _DistanceMaptoAddressQuantity; }
            set { _DistanceMaptoAddressQuantity = value; }
        }



        WorkingArea _WorkingArea;
        public virtual WorkingArea WorkingArea
        {
            get { return _WorkingArea; }
            set { _WorkingArea = value; }
        }

        LocationGeo _LocationGeo;
        public virtual LocationGeo LocationGeo
        {
            get { return _LocationGeo; }
            set { _LocationGeo = value; }
        }

        AddressType _AddressType;
        public virtual AddressType AddressType
        {
            get { return _AddressType; }
            set { _AddressType = value; }
        }

        Owner _Owner;
        public virtual Owner Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }

        City _City;
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        public virtual City City
        {
            get { return _City; }
            set { _City = value; }
        }

        District _District;
        [DataSourceCriteria("City.ID = City.ID")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        public virtual District District
        {
            get { return _District; }
            set { _District = value; }
        }

        Neighborhood _Neighborhood;
        [DataSourceCriteria("City.ID = City.ID and District.Id=District.Id")]
        public virtual Neighborhood Neighborhood
        {
            get { return _Neighborhood; }
            set { _Neighborhood = value; }
        }

        Town _Town;
        public virtual Town Town
        {
            get { return _Town; }
            set { _Town = value; }
        }

        String _OpenAddress;
        public String OpenAddress
        {
            get { return _OpenAddress; }
            set { _OpenAddress = value; }
        }

        String _Description2;
        public String Description2
        {
            get { return _Description2; }
            set { _Description2 = value; }
        }

        String _ContactName;
        public String ContactName
        {
            get { return _ContactName; }
            set { _ContactName = value; }
        }

        String _ContactPhone;
        public String ContactPhone
        {
            get { return _ContactPhone; }
            set { _ContactPhone = value; }
        }

        String _IntegrationCode;
        public String IntegrationCode
        {
            get { return _IntegrationCode; }
            set { _IntegrationCode = value; }
        }


        public String Key
        {
            get
            {
                if (LocationGeo != null)
                {
                    return SysCode;
                }
                else
                {
                    return null;
                }
            }

        }


        public double Latitude
        {
            get
            {
                if (LocationGeo != null)
                {
                    return LocationGeo.Latitude;
                }
                else
                {
                    return 0;
                }
            }

        }

        public double Longitude
        {
            get
            {
                if (LocationGeo != null)
                {
                    return LocationGeo.Longitude;
                }
                else
                {
                    return 0;
                }
            }

        }

        public String Marker
        {
            get
            {
                if (LocationGeo != null)
                {
                    return LocationGeo.Marker;
                }
                else
                {
                    return null;
                }
            }

        }


        public String Title
        {
            get
            {
                if (LocationGeo != null)
                {
                    return this.Name;
                }
                else
                {
                    return null;
                }
            }

        }


    }

    [Table("WorkingTimes")]
    [DefaultClassOptions]
    public class WorkingTimes : BaseObjectI
    {
        public WorkingTimes()
        {

        }

        Address _Address;
        public virtual Address Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        int _DayOfWeek;
        public int DayOfWeek
        {
            get { return _DayOfWeek; }
            set { _DayOfWeek = value; }
        }

        DateTime? _StartTime;
        public DateTime? StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }

        DateTime? _EndTime;
        public DateTime? EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
    }
}

