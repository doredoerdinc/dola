//using DevExpress.ExpressApp.Model;
//using DevExpress.Persistent.Base;
//using DevExpress.Persistent.Validation;
//using LgsLib.Base;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;

//namespace dola.Module
//{
//    public enum EnumRoundQuantityType
//    {
//        None=0,
//        IsGreaterThan=1,
//        IsLessThan = 2, 
//    }

//    public enum EnumRouteType
//    {
//        None = 0,
//        FromAddress = 1,
//        ToAddress = 2,
//    }

//    [DefaultClassOptions]
//    [Table("Route")]
//    [DefaultProperty(nameof(Name))]
//    public class Route: BaseLookupC
//    {
//        public Route()
//        {
//            RouteAddresses = new List<RouteAddress>();
//        }

//        public virtual IList<RouteAddress> RouteAddresses { get; set; }

//        //IList<Owner> _ReceiverCompanies;
//        //[NotMapped]
//        //[ModelDefault("AllowEdit", "False")]
//        //public IList<Owner> ReceiverCompanies
//        //{
//        //    get
//        //    {
//        //        if (_ReceiverCompanies == null)
//        //        {
//        //            _ReceiverCompanies = Companies.Where(x => x.RouteType == EnumRouteType.ToAddress).Select(x => x.CompanyRoute).ToList();
//        //            return ReceiverCompanies;

//        //        }
//        //        return _ReceiverCompanies;
//        //    }

//        //}
//        //IList<Owner> _SenderCompanies;
//        //[NotMapped]
//        //[ModelDefault("AllowEdit", "False")]
//        //[Browsable(false)]
//        //public IList<Owner> SenderCompanies
//        //{
//        //    get
//        //    {
//        //        if (_SenderCompanies == null)
//        //        {
//        //            _SenderCompanies = _ReceiverCompanies = Companies.Where(x => x.RouteType == EnumRouteType.FromAddress).Select(x => x.CompanyRoute).ToList();
//        //            return SenderCompanies;

//        //        }
//        //        return _SenderCompanies;
//        //    }

//        //}

//        IList<Address> _SenderAddreses;
//        [NotMapped]
//        [ModelDefault("AllowEdit", "False")]
//        [Browsable(false)]
//        public IList<Address> SenderAddreses
//        {
//            get
//            {
//                if (_SenderAddreses == null)
//                {
//                    _SenderAddreses = RouteAddresses.Where(x => x.RouteType == EnumRouteType.FromAddress).Select(x => x.Address).ToList();
//                    return _SenderAddreses;

//                }
//                return _SenderAddreses;
//            } 
//        }

//        IList<Address> _ReceiverAddreses;
//        [NotMapped]
//        [ModelDefault("AllowEdit", "False")]
//        [Browsable(false)]
//        public IList<Address> ReceiverAddreses
//        {
//            get
//            {
//                if (_ReceiverAddreses == null)
//                {
//                    _ReceiverAddreses = RouteAddresses.Where(x => x.RouteType == EnumRouteType.ToAddress).Select(x => x.Address).ToList();
//                    return _ReceiverAddreses;

//                }
//                return _ReceiverAddreses;
//            }
//        }

//        public override void OnSaving()
//        {
//            base.OnSaving();
//            //if(ObjectSpace!=null)
//            //{
//            //    RouteDescription = null;
//            //    RouteDescription = String.Format(@"Gönderici_Firma:{0}, Gönderici_Address:{1}", Desc.NameAlias, SenderAddress.Name);
//            //    foreach (var itm in Receivers)
//            //    {
//            //        RouteDescription = RouteDescription + String.Format(@" Alıcı_Firma:{0}, Alıcı_Address:{1}  ", itm.DestinationCompany.NameAlias, itm.ReceiverAddress.Name); 

//            //    }
//            //}
         
//        } 

//    }

//    [DefaultClassOptions]
//    [Table("RouteAddress")]
//    public class RouteAddress : BaseObjectC
//    { 
//        public RouteAddress()
//        {
//            Routes = new List<Route>();
//        }

//        public virtual IList<Route> Routes { get; set; }
 

//        EnumRouteType _RouteType;
//        [RuleRequiredField(DefaultContexts.Save)]
//        public EnumRouteType RouteType
//        {
//            get { return _RouteType; }
//            set { _RouteType = value; }
//        }

//        Address _Address;
//        public virtual Address Address
//        {
//            get { return _Address; }
//            set { _Address = value; }
//        }


//    }

//}
