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
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Data.Filtering;
using System.Collections.ObjectModel;
using DevExpress.ExpressApp;
using Newtonsoft.Json;
using LgsLib.Base.PermissionPolicy;

namespace dola.Module
{
    public class OrderItemCSV
    {
        String _Teslimat_Kod;
        public String Teslimat_Kod
        {
            get { return _Teslimat_Kod; }
            set { _Teslimat_Kod = value; }
        } 

        String _Bolge_Muduru;
        public String Bolge_Muduru
        {
            get { return _Bolge_Muduru; }
            set { _Bolge_Muduru = value; }
        }


        String _Gonderen_Magaza_Kod;
        public String Gonderen_Magaza_Kod
        {
            get { return _Gonderen_Magaza_Kod; }
            set { _Gonderen_Magaza_Kod = value; }
        }

        String _Gonderen_Magaza_Adi;
        public String Gonderen_Magaza_Adi
        {
            get { return _Gonderen_Magaza_Adi; }
            set { _Gonderen_Magaza_Adi = value; }
        }

        String _Gonderen_Magaza_iletisim_tel;
        public String Gonderen_Magaza_iletisim_tel
        {
            get { return _Gonderen_Magaza_iletisim_tel; }
            set { _Gonderen_Magaza_iletisim_tel = value; }
        }

        String _Teslim_Magaza_Kod;
        public String Teslim_Magaza_Kod
        {
            get { return _Teslim_Magaza_Kod; }
            set { _Teslim_Magaza_Kod = value; }
        }

        String _Teslim_Magaza_Adi;
        public String Teslim_Magaza_Adi
        {
            get { return _Teslim_Magaza_Adi; }
            set { _Teslim_Magaza_Adi = value; }
        }

        String _Teslim_Magaza_iletisim_tel;
        public String Teslim_Magaza_iletisim_tel
        {
            get { return _Teslim_Magaza_iletisim_tel; }
            set { _Teslim_Magaza_iletisim_tel = value; }
        }

        string _Urun_Kod;
        public string Urun_Kod
        {
            get { return _Urun_Kod; }
            set { _Urun_Kod = value; }
        }

        String _Urun_Adi;
        public String Urun_Adi
        {
            get { return _Urun_Adi; }
            set { _Urun_Adi = value; }
        }

        String _Tasima_Tipi;
        public String Tasima_Tipi
        {
            get { return _Tasima_Tipi; }
            set { _Tasima_Tipi = value; }
        }

        String _Miktar;
        public String Miktar
        {
            get { return _Miktar; }
            set { _Miktar = value; }
        } 
        String _Adet_GR;
        public String Adet_GR
        {
            get { return _Adet_GR; }
            set { _Adet_GR = value; }
        }

        String _Region;
        public String Region
        {
            get { return _Region; }
            set { _Region = value; }
        }

        String _State;
        public String State
        {
            get { return _State; }
            set { _State = value; }
        }

        String _Description;
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        String _LastLocation;
        public String LastLocation
        {
            get { return _LastLocation; }
            set { _LastLocation = value; }
        } 

    }
     
    [DefaultClassOptions]
    [Table("ExTernalDocument")]
    [XafDefaultProperty("Name")]
    public class ExTernalDocument:BaseObjectC
    {
        public ExTernalDocument()
        {
            Lines = new List<ExTernalOrderItem>();
        }

        public virtual IList<ExTernalOrderItem> Lines { get; set; }

        Owner _Owner;
        public virtual Owner Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }

        String _DocumentName;
        public String DocumentName
        {
            get { return _DocumentName; }
            set { _DocumentName = value; }
        }

        DateTime? _RequestPlanedDate;
        public DateTime? RequestPlanedDate
        {
            get { return _RequestPlanedDate; }
            set { _RequestPlanedDate = value; }
        } 

        DateTime? _ImportDate=DateTime.Now;
        public DateTime? ImportDate
        {
            get { return _ImportDate; }
            set { _ImportDate = value; }
        }
      
        int? _DocumentImportCount;
        public int? DocumentImportCount
        {
            get { return _DocumentImportCount; }
            set { _DocumentImportCount = value; }
        }

        int? _DocumentMapSuccessCount;
        public int? DocumentMapSuccessCount
        {
            get { return _DocumentMapSuccessCount; }
            set { _DocumentMapSuccessCount = value; }
        }

        int? _DocumentMapProcessCount;
        public int? DocumentMapProcessCount
        {
            get { return _DocumentMapProcessCount; }
            set { _DocumentMapProcessCount = value; }
        }

        int? _DocumentMapErrorCount;
        public int? DocumentMapErrorCount
        {
            get { return _DocumentMapErrorCount; }
            set { _DocumentMapErrorCount = value; }
        }


    } 
     
    [DefaultClassOptions]
    [Table("ExTernalOrderItem")]
    [XafDefaultProperty("Name")]
    public class ExTernalOrderItem : OrderItemCSV, IXafEntityObject, IObjectSpaceLink
    {
        [Key]
        [MaxLength(100)]
        public string SysCode { get; set; }  

        ExTernalDocument _Document;
        public virtual ExTernalDocument Document
        {
            get { return _Document; }
            set { _Document = value; }
        }

        State _StateIntegration;
        public virtual State StateIntegration
        {
            get { return _StateIntegration; }
            set { _StateIntegration = value; }
        } 

        bool _IsNewObject = false;
        [NotMapped]
        [ModelDefault("AllowEdit", "False")]
        [Browsable(false)]
        [JsonIgnore]
        public bool IsNewObject
        {
            get { return _IsNewObject; }
            set { _IsNewObject = value; }
        }

        DateTime? _CreateTime;
        [JsonIgnore]
        [ModelDefault("AllowEdit", "False")]
        public DateTime? CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }

        DateTime? _UpdateTime;
        [JsonIgnore]
        [ModelDefault("AllowEdit", "False"), ModelDefault("DisplayFormat", "G")]
        public DateTime? UpdateTime
        {
            get { return _UpdateTime; }
            set { _UpdateTime = value; }
        }

        User _CreatedBy;
        [ModelDefault("AllowEdit", "False")]
        [JsonIgnore]
        public User CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        User _UpdateBy;
        [JsonIgnore]
        [ModelDefault("AllowEdit", "False")]
        public User UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }

        //String _IntegrationCode;
        //[Key]
        //public String IntegrationCode
        //{
        //    get { return _IntegrationCode; }
        //    set { _IntegrationCode = value; }
        //}


        [NotMapped]
        [Browsable(false)]
        IObjectSpace _ObjectSpace;
        public IObjectSpace ObjectSpace
        {
            get { return _ObjectSpace; }
            set { _ObjectSpace = value; }
        }
        User GetCurrentUser()
        {
            return ObjectSpace.GetObjectByKey<User>(SecuritySystem.CurrentUserId);
        }

        String _Message;
        public String Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        Owner _Owner;
        public virtual Owner Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }


        public virtual void OnCreated()
        {
            CreatedBy = GetCurrentUser();
            CreateTime = DateTime.Now;
            IsNewObject = true;
        }
        public virtual void OnSaving()
        {
            if (_ObjectSpace != null)
            {
                UpdateBy = GetCurrentUser();
                UpdateTime = DateTime.Now;
            }
        } 
        public virtual void OnLoaded()
        {

        } 

    } 
     
}
      