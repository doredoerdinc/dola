using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DevExpress.ExpressApp.DC;

using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp;
using System.ComponentModel.DataAnnotations.Schema;
using LgsLib.Base.PermissionPolicy;
using DevExpress.Persistent.Validation;
using System.Xml.Serialization;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base; 
using System.Drawing;
using Newtonsoft.Json;

namespace LgsLib.Base
{

    [DefaultClassOptions]
    public abstract class ObjectBase : IXafEntityObject, IObjectSpaceLink
    {
        [Browsable(false)]
        public ObjectBase() 
        {
            
        } public Type findXafType(string typeFullName)
        {
            var typeInfo = XafTypesInfo.Instance.PersistentTypes.First(X => X.FullName == typeFullName).Type;
            if (typeInfo != null)
            {
                return typeInfo;
            }
            return null;
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

    [DefaultClassOptions]
    public abstract class StateHistoryAudit:BaseObjectI
    {
        StateHistory _StateHistory;
        public StateHistory StateHistory
        {
            get { return _StateHistory; }
            set { _StateHistory = value; }
        }

        String _PropertyName;
        public String PropertyName
        {
            get { return _PropertyName; }
            set { _PropertyName = value; }
        }

        String _OldValue;
        public String OldValue
        {
            get { return _OldValue; }
            set { _OldValue = value; }
        }

        String _NewValue;
        public String NewValue
        {
            get { return _NewValue; }
            set { _NewValue = value; }
        }

    }

    [DefaultClassOptions]
    public abstract class BaseObjectState : BaseObjectC
    {
        public BaseObjectState (){

        } 
        State _State;
       // [ModelDefault("AllowEdit", "False")]
        public virtual State State
        {
            get { return _State; }
            set { _State = value; }
        } 


        public override void OnCreated()
        {
            base.OnCreated();
            State = ObjectSpace.GetObjectByKey<State>("Olusruruldu");
        }

    }

    [DefaultClassOptions]
    public abstract class BaseObjectWarehouseState
        : BaseObjectState
    {
        public BaseObjectWarehouseState()
        {

        }

        Warehouse _Warehouse;
        public virtual Warehouse Warehouse
        {
            get { return _Warehouse; }
            set { _Warehouse = value; }
        }
         




    }
     
    [XafDefaultProperty(nameof(SysCode))]

    [DefaultClassOptions]
    public abstract class BaseLookupC: BaseObjectC
    {
        String _Name;
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        
        String _Description;
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

    }
    [DefaultProperty("SysCode")]

    [DefaultClassOptions]
    public abstract class BaseObjectC : ObjectBase
    {
        string _Code = string.Empty;
        [Key]
        [MaxLength(100)] 
      //  [RuleRequiredField(DefaultContexts.Save)]
        public string SysCode
        {
            get
            {
                return _Code;
            }
            set { _Code = value; }
        }

        public override void OnCreated()
        {
            base.OnCreated();
           
        }

        public override void OnSaving()
        {
            base.OnSaving();
            if (IsNewObject)
            {
                if (string.IsNullOrEmpty(SysCode))
                {
                    SysCode = SetNexCode();
                }
                else
                    SysCode = SysCode.Replace(" ", "");
            }

        } 

        
        private string SetNexCode()
        {
            string code = string.Empty;
            //var criteria = CriteriaOperator.Parse("ObjectType.TypeName=? and MainCompany.SysCode=?", ObjectSpace.GetObjectType(this).FullName, SecuritySystemBase.MainCompany);
            var criteria = CriteriaOperator.Parse("ObjectTypeName=?", ObjectSpace.GetObjectType(this).FullName);
            var obj = ObjectSpace.FindObject<ObjectCode>(criteria);
            if (obj != null)
            { 
                code = GlobalSys.getSysCode(obj, SecuritySystemBase.Warehouse); 
                obj.NextValue = obj.NextValue + obj.Increasing;
                return code;
            }
            throw new System.ArgumentException("Syscode Boş Olamaz");
        }
    }


    [DefaultClassOptions]
    public abstract class BaseObjectN : ObjectBase
    {


        double _Number;
        [Key]
        public double Number
        {
            get { return _Number; }
            set { _Number = value; }
        } 
         
        public override void OnCreated()
        {
            base.OnCreated();

        } 
       
    }


    [DefaultProperty("SysCode")]
    public abstract class BaseObjectWarehouseCode : BaseObjectC
    {

        Warehouse _Warehouse;
        public virtual Warehouse Warehouse
        {
            get { return _Warehouse; }
            set { _Warehouse = value; }
        } 

        public override void OnCreated()
        {
            base.OnCreated();

        }

       
        
    }
    [DefaultProperty("ID")]
    public abstract class BaseObjectI : ObjectBase { 

         public BaseObjectI()
            {

            } 
 
        int _ID;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
    }
    public abstract class BaseObjectWarehouseID : BaseObjectI
    {
        public BaseObjectWarehouseID()
        {

        }

        Warehouse _Warehouse;
        public virtual Warehouse Warehouse
        {
            get { return _Warehouse; }
            set { _Warehouse = value; }
        } 

        public override void OnCreated()
        {
            base.OnCreated();
           // MainCompany = ObjectSpace.GetObject<MainCompany>(SecuritySystemBase.MainCompany);
        }

    }  
   
    [DefaultClassOptions]
    [DefaultProperty("TranslateValue")]
    [Table("State")] 
    public class State :  BaseObjectC
    { 


        public State() {

            States = new List<State>();
        }
        public virtual IList<State> States { get; set; }

        State _ParentState;
        public virtual  State ParentState
        {
            get { return _ParentState; }
            set { _ParentState = value; }
        }
         

        [Browsable(false), Column("Color")]
        public int StateColor
        {
            get { return fColor.ToArgb(); }
            set { fColor = Color.FromArgb(value); }
        }
        private Color fColor;
        [NotMapped]
        public Color Color
        {
            get { return fColor; }
            set { fColor = value; }
        }

        String _TranslateValue;
        public String TranslateValue
        {
            get { return _TranslateValue; }
            set { _TranslateValue = value; }
        }
         
    } 
    public class StateHistory : BaseObjectI
    {
        public StateHistory() { }

        State _State;
        public State State
        {
            get { return _State; }
            set { _State = value; }
        }

        String _ObjectType;
        [Browsable(false)]
        public String ObjectType
        {
            get { return _ObjectType; }
            set { _ObjectType = value; }
        }

        String _ObjectKey;
        [Browsable(false)]
        public String ObjectKey
        {
            get { return _ObjectKey; }
            set { _ObjectKey = value; }
        }

        String _Description;
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        DateTime _StateTime;
        public DateTime StateTime
        {
            get { return _StateTime; }
            set { _StateTime = value; }
        }
    }

    [DomainComponent]
    public abstract class NonePercentObject: IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        public NonePercentObject() { }

        [DevExpress.ExpressApp.Data.Key]
        public int Oid { get; set; }

        IObjectSpace _ObjectSpace;
        [NotMapped]
        [Browsable(false)]
        public IObjectSpace ObjectSpace
        {
            get { return _ObjectSpace; }
            set { _ObjectSpace = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnCreated()
        { 
        }

        public void OnLoaded()
        { 
        }

        public void OnSaving()
        { 
        }
    }
}


