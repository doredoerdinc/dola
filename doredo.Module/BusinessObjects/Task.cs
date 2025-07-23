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
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
 
using LgsLib.Base;
using Newtonsoft.Json;
using DevExpress.ExpressApp.Editors;

namespace dola.Module
{

    public enum EnumStockTransactionType : int
    {
        Pozitif = 1,
        Negatif = -1,
        Pozitif_Negatif=-2,
        None = 0
    }
    public enum EnumStepDoType : int
    {
        UnRegular = 0,
        Regular = 1, 
    } 
    public enum EnumOperationType : int
    { 
        None=0,
        GoodsIn = 1,
        GoodsOut = 2,
        Picking = 101,
        Packing=110,
        StockControl = 102,  
        PaletteTransfer=201, //Transaction not inserted
        Transport =300
    }

    public enum EnumOrderRequestType : int
    {
        None = 0,
        Planed=1,
        UnPlaned = 2,
      
    } 

    [Table("TaskOperationType")]
    [DefaultClassOptions]
    public class TaskOperationType : BaseObjectC
    {
       

    }


    [Table("TaskStepStatic")]
    [DefaultClassOptions]
    public class TaskStepStatic : BaseObjectC
    {
        public TaskStepStatic()
        {
            TemplateItems = new List<TaskTemplateItem>();
        }
         
        public virtual IList<TaskTemplateItem> TemplateItems { get; set; }


        TaskTemplate _TaskTemplate;
        public TaskTemplate TaskTemplate
        {
            get { return _TaskTemplate; }
            set { _TaskTemplate = value; }
        } 

        [FieldSize(FieldSizeAttribute.Unlimited)]
        String _JsonData;
        public String JsonData
        {
            get { return _JsonData; }
            set { _JsonData = value; }
        } 

        String _Location;
        public String Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        int _Index;
        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        }

        String _TaskStepIntegrationCode;
        public String TaskStepIntegrationCode
        {
            get { return _TaskStepIntegrationCode; }
            set { _TaskStepIntegrationCode = value; }
        }

        string _TaskStepIntegrationObject;
        public string TaskStepIntegrationObject
        {
            get { return _TaskStepIntegrationObject; }
            set { _TaskStepIntegrationObject = value; }
        } 


    }



    [Table("TaskItemType")]
    [DefaultClassOptions]
    public class TaskItemType : BaseObjectC
    {
    } 
    [Table("TaskItemCriteriaType")]
    [DefaultClassOptions]
    public class TaskItemCriteriaType : BaseObjectC
    {
        String _MathematicalExpression;
        public String MathematicalExpression
        {
            get { return _MathematicalExpression; }
            set { _MathematicalExpression = value; }
        }

    } 
    [Table("TaskTemplate")]
    [DefaultClassOptions]
    public class TaskTemplate : BaseObjectC,INotifyPropertyChanged
    {
        public TaskTemplate()
        {
            TemplateItems = new List<TaskTemplateItem>();
        }  

        public virtual IList<TaskTemplateItem> TemplateItems { get; set; }
        String _IntegrationCode; 
        public String IntegrationCode
        {
            get { return _IntegrationCode; }
            set { _IntegrationCode = value; }
        }

        EnumOperationType _OperationType;
        public EnumOperationType OperationType
        {
            get { return _OperationType; }
            set { _OperationType = value; }
        } 

        EnumStepDoType _StepDoType;
        public EnumStepDoType StepDoType
        {
            get { return _StepDoType; }
            set { _StepDoType = value; }
        }

        String _Description;
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        String _DisplayNama;
        [RuleRequiredField(DefaultContexts.Save)]
        public String DisplayNama
        {
            get { return _DisplayNama; }
            set { _DisplayNama = value; }
        } 

        String _Title;
        [RuleRequiredField(DefaultContexts.Save)]
        public String Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        string _TitleImage;
        public string TitleImage
        {
            get { return _TitleImage; }
            set { _TitleImage = value; }
        }

        string _Subtitle;
        [RuleRequiredField(DefaultContexts.Save)]
        public string Subtitle
        {
            get { return _Subtitle; }
            set { _Subtitle = value; }
        }

        String _SubTitleReferansObject; 
        public String SubTitleReferansObject
        {
            get { return _SubTitleReferansObject; }
            set { _SubTitleReferansObject = value; }
        }

        String _TitleReferansObject; 
        public String TitleReferansObject
        {
            get { return _TitleReferansObject; }
            set { _TitleReferansObject = value; }
        } 

        State _StateTaskVisible;
        [RuleRequiredField(DefaultContexts.Save)]
        public virtual State StateTaskVisible
        {
            get { return _StateTaskVisible; }
            set { _StateTaskVisible = value; }
        } 

        State _StateTaskStarted;
        [RuleRequiredField(DefaultContexts.Save)]
        public virtual State StateTaskStarted
        {
            get { return _StateTaskStarted; }
            set { _StateTaskStarted = value; }
        }

        State _StateTaskFinished;
        [RuleRequiredField(DefaultContexts.Save)]
        public virtual State StateTaskFinished
        {
            get { return _StateTaskFinished; }
            set { _StateTaskFinished = value; }
        }

        State _StateTaskIssue;
        [RuleRequiredField(DefaultContexts.Save)]
        public virtual State StateTaskIssue
        {
            get { return _StateTaskIssue; }
            set { _StateTaskIssue = value; }
        }
 
        State _StateTaskStepVisible;
        [RuleRequiredField(DefaultContexts.Save)]
        public virtual State StateTaskStepVisible
        {
            get { return _StateTaskStepVisible; }
            set { _StateTaskStepVisible = value; }
        }

        State _StateTaskStepStarted;
        [RuleRequiredField(DefaultContexts.Save)]
        public virtual State StateTaskStepStarted
        {
            get { return _StateTaskStepStarted; }
            set { _StateTaskStepStarted = value; }
        }

        State _StateTaskStepFinished;
        [RuleRequiredField(DefaultContexts.Save)]
        public virtual State StateTaskStepFinished
        {
            get { return _StateTaskStepFinished; }
            set { _StateTaskStepFinished = value; }
        }

        State _StateTaskStepIssue;
        [RuleRequiredField(DefaultContexts.Save)]
        public virtual State StateTaskStepIssue
        {
            get { return _StateTaskStepIssue; }
            set { _StateTaskStepIssue = value; }
        } 

        String _SubtitleImage;
        [RuleRequiredField(DefaultContexts.Save)]
        public String SubtitleImage
        {
            get { return _SubtitleImage; }
            set { _SubtitleImage = value; }
        }

        EnumStockTransactionType _StockTransactionType;
        public EnumStockTransactionType StockTransactionType
        {
            get { return _StockTransactionType; }
            set { _StockTransactionType = value; }
        }

        string _BeforeCallAction;
        public string BeforeCallAction
        {
            get { return _BeforeCallAction; }
            set { _BeforeCallAction = value; }
        } 

        string _Smart;
        [FieldSize(FieldSizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.PopupCriteriaPropertyEditor)]
        [CriteriaOptions("ObjectType")]
        public string Smart
        {
            get { return _Smart; }
            set { _Smart = value; }
        }

        Type _ObjectType=typeof(OrderLine);
        [NotMapped]
        [ImmediatePostData]
        public Type ObjectType
        {
            get { return _ObjectType; }
            set
            {
                _ObjectType = value;
                OnPropertyChanged();
            }
        }

        string _ObjectTypeName;
        public string ObjectTypeName
        {
            get { return _ObjectTypeName; }
            set { _ObjectTypeName = value; }
        }

        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if (propertyName == "ObjectType")
            {                  
                ObjectTypeName = this.ObjectType.FullName;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

    }
    public interface ITaskTemplateItem
    {
        // Property declaration:
        int Index { get; set; }
        EnumVisible ListVisible { get; set; }
        EnumVisible FormVisible { get; set; }
        string Name{get;set;}
        string DisplayName { get; set; }
         
        string ClientValue { get; set; }
        string SysValue { get; set; }  
        string ValidationMessage { get; set; }
        int CharacterLength { get; set; }

    }
    public interface ITask
    {
        int? CompletedStepCount { get; set; } 
        int? WaitingStepCount { get; set; } 
        int? TodoStepCount { get; set; }
        int? Index { get; set; } 
        string Title { get; set; } 
        string  TitleImage { get; set; } 
        string SubTitle { get; set; } 
        string SubTitleImage { get; set; }
    }
    public class TaskStepMap
    {
        public int Index { get; set; }
        public string SysCode { get; set; }
        public string TaskSysCode { get; set; } 
        public string TaskTemplateCode { get; set; }
        public string Title { get; set; } 
        public string TitleImage { get; set; } 
        public string TaskStepIntegrationCode { get; set; }
        public string TaskStepIntegrationObject { get; set; } 
        public string Person { get; set; } 
        public string StepDoType { get; set; }   
        public List<TaskStepItemMap> items { get;set;} 

    }
    public enum EnumVisible
    {
        UnVisible = 0,
        Visible = 1,
        
    }
    public class TaskStepItemMap : ITaskTemplateItem
    {
        public int Index { get; set; }
        public EnumVisible ListVisible { get; set; }
        public EnumVisible FormVisible { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Type { get; set; }
        public string Criteria { get; set; }
        public string ValidationMessage { get; set; }
        public string ClientValue { get; set; }
        public string SysValue { get; set; }
        public int CharacterLength { get; set; }
        public int IsLoop { get; set; }

        public int IsEnableBarcodeScanner {get;set;}
        public int IsEnableMultipleBarcodeScanner { get; set; }
        
    }    
    public class SortByLocation
    { 
        public StockItem StockItem { get; set; }
        public int Index { get; set; }
    }
    public class TaskMap:ITask
    {
        int? _Index;
        public int? Index
        {
            get { return _Index; }
            set { _Index = value; }
        }

        String _TaskSysCode;
        public String TaskSysCode
        {
            get { return _TaskSysCode; }
            set { _TaskSysCode = value; }
        }

        String _TaskType;
        public String TaskType
        {
            get { return _TaskType; }
            set { _TaskType = value; }
        }

        String _Title;
        public String Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        String _TitleImage;
        public String TitleImage
        {
            get { return _TitleImage; }
            set { _TitleImage = value; }
        }

        String _SubTitle;
        public String SubTitle
        {
            get { return _SubTitle; }
            set { _SubTitle = value; }
        }

        String _SubTitleImage;
        public String SubTitleImage
        {
            get { return _SubTitleImage; }
            set { _SubTitleImage = value; }
        }

        String _TaskDescription;
        public String TaskDescription
        {
            get { return _TaskDescription; }
            set { _TaskDescription = value; }
        }

        String _FromLocation;
        public String FromLocation
        {
            get { return _FromLocation; }
            set { _FromLocation = value; }
        }

        String _ToLocation;
        public String ToLocation
        {
            get { return _ToLocation; }
            set { _ToLocation = value; }
        }

        int? _CompletedStepCount;
        public int? CompletedStepCount
        {
            get { return _CompletedStepCount; }
            set { _CompletedStepCount = value; }
        }

        int? _WaitingStepCount;
        public int? WaitingStepCount
        {
            get { return _WaitingStepCount; }
            set { _WaitingStepCount = value; }
        }

        int? _TodoStepCount;
        public int? TodoStepCount
        {
            get { return _TodoStepCount; }
            set { _TodoStepCount = value; }
        }
        string _TaskIntegrationCode;
        public string TaskIntegrationCode
        {
            get { return _TaskIntegrationCode; }
            set { _TaskIntegrationCode = value; }
        }

        string _TaskIntegrationObject;
        public string TaskIntegrationObject
        {
            get { return _TaskIntegrationObject; }
            set { _TaskIntegrationObject = value; }
        }

        string _IntegrationCode;
        public string IntegrationCode
        {
            get { return _IntegrationCode; }
            set { _IntegrationCode = value; }
        }

        EnumStepDoType _StepDoType;
        public EnumStepDoType StepDoType
        {
            get { return _StepDoType; }
            set { _StepDoType = value; }
        } 
    }

    [Table("TaskTemplateItem")]
    [DefaultClassOptions]
    public class TaskTemplateItem : BaseObjectI, ITaskTemplateItem,INotifyPropertyChanged
    {
        public TaskTemplateItem()
        {  
        }

        bool _IsTitle;
        public bool IsTitle
        {
            get { return _IsTitle; }
            set { _IsTitle = value; }
        }

        TaskStepStatic _TaskTemplateStatic;
        public TaskStepStatic TaskTemplateStatic
        {
            get { return _TaskTemplateStatic; }
            set { _TaskTemplateStatic = value; }
        }

        TaskTemplate _TaskTemplate;
        public virtual TaskTemplate TaskTemplate
        {
            get { return _TaskTemplate; }
            set { _TaskTemplate = value; }
        } 

        public int Index { get; set; }
        public EnumVisible ListVisible { get; set; }
        public EnumVisible FormVisible { get; set; }

        string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        int _IsLoop=0;
        public int IsLoop
        {
            get { return _IsLoop; }
            set { _IsLoop = value; }
        }  
        string _DisplayName;
        public string DisplayName
        {
            get { return _DisplayName; }
            set { _DisplayName = value; }
        }

        TaskItemType _Type;
        public virtual TaskItemType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        TaskItemCriteriaType _CriteriaType;
        public virtual TaskItemCriteriaType  CriteriaType
        {
            get { return _CriteriaType; }
            set { _CriteriaType = value; }
        }

        String _ClientValue;
        public String ClientValue
        {
            get { return _ClientValue; }
            set { _ClientValue = value; }
        }

        String _SysValue;
        public String SysValue
        {
            get { return _SysValue; }
            set { _SysValue = value; }
        }

        string _ValidationMessage;
        public string ValidationMessage
        {
            get { return _ValidationMessage; }
            set { _ValidationMessage = value; }
        }

        int _CharacterLength=0;
        public int CharacterLength
        {
            get { return _CharacterLength; }
            set { _CharacterLength = value; }
        }

        string _Smart;
        [FieldSize(FieldSizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.PopupCriteriaPropertyEditor)]
        [CriteriaOptions("ObjectType")]
        public string Smart
        {
            get { return _Smart; }
            set { _Smart = value; }
        }
          
        String _ObjectTypeName;
        public String ObjectTypeName
        {
            get { return _ObjectTypeName; }
            set { _ObjectTypeName = value; }
        }
         

        Type _ObjectType;
        [NotMapped]
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter))]
        [ImmediatePostData]
        public Type ObjectType
        {
            get { return _ObjectType; }
            set
            {
                _ObjectType = value;
                OnPropertyChanged();
            }
        }
        
        string _SysValueReferanceProperty;
        [JsonIgnore] 
        [EditorAlias(EditorAliases.PropertiesCollectionEditor)]
        public string SysValueReferanceProperty  
        {
            get { return _SysValueReferanceProperty; }
            set { _SysValueReferanceProperty = value; }
        }
        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if (propertyName == "ObjectType")
            {
                ObjectTypeName = this.ObjectType.FullName;
            }
            
        }
        public event PropertyChangedEventHandler PropertyChanged;

    } 

    [Table("Task")]
    [DefaultClassOptions]
    public class Task:BaseObjectWarehouseState, ITask
    {
        public Task() 
        {
            TaskSteps = new List<TaskStep>();   
        } 

        public virtual IList<TaskStep> TaskSteps { get; set; }

        String _Description;
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }


        TaskTemplate _Template;
        public virtual TaskTemplate Template
        {
            get { return _Template; }
            set { _Template = value; }
        }

        String _IntegrationCode;
        public String IntegrationCode
        {
            get { return _IntegrationCode; }
            set { _IntegrationCode = value; }
        } 

        int? _Index;
        public int? Index
        {
            get { return _Index; }
            set { _Index = value; }
        }   

        [FieldSize(FieldSizeAttribute.Unlimited)]
        String _JsonData;
        public String JsonData
        {
            get { return _JsonData; }
            set { _JsonData = value; }
        }  

        Trip _Trip;
        public virtual Trip Trip
        {
            get { return _Trip; }
            set { _Trip = value; }
        } 
          
        string _FromLocation;
        public string FromLocation
        {
            get { return _FromLocation; }
            set { _FromLocation = value; }
        } 

        string _ToLocation;
        public string ToLocation
        {
            get { return _ToLocation; }
            set { _ToLocation = value; }
        }

        int? _CompletedStepCount;
        public int? CompletedStepCount
        {
            get { return _CompletedStepCount; }
            set { _CompletedStepCount = value; }
        }

        int? _WaitingStepCount;
        public int? WaitingStepCount
        {
            get { return _WaitingStepCount; }
            set { _WaitingStepCount = value; }
        }

        int? _TodoStepCount;
        public int? TodoStepCount
        {
            get { return _TodoStepCount; }
            set { _TodoStepCount = value; }
        }

        String _Title;
        public String Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        string _TitleImage;
        public string TitleImage
        {
            get { return _TitleImage; }
            set { _TitleImage = value; }
        }

        string _SubTitle;
        public string SubTitle
        {
            get { return _SubTitle; }
            set { _SubTitle = value; }
        }

        string _SubTitleImage;
        public string SubTitleImage
        {
            get { return _SubTitleImage; }
            set { _SubTitleImage = value; }
        }

        string _TaskIntegrationCode;
        public string TaskIntegrationCode
        {
            get { return _TaskIntegrationCode; }
            set { _TaskIntegrationCode = value; }
        }

        string _TaskIntegrationObjectType;
        public string TaskIntegrationObject
        {
            get { return _TaskIntegrationObjectType; }
            set { _TaskIntegrationObjectType = value; }
        }

        String _DocumentCode;
        public String DocumentCode
        {
            get { return _DocumentCode; }
            set { _DocumentCode = value; }
        }

    } 

    [Table("TaskStep")]
    [DefaultClassOptions]
    public class TaskStep: BaseObjectWarehouseState
    {
        public TaskStep() {
            Transactions = new List<TaskTransactionTempory>();
        } 
        public virtual IList<TaskTransactionTempory> Transactions { get; set; } 

        Task _Task;
        public virtual Task Task
        {
            get { return _Task; }
            set { _Task = value; }
        } 

        [FieldSize(FieldSizeAttribute.Unlimited)]
        String _JsonData;
        public String JsonData
        {
            get { return _JsonData; }
            set { _JsonData = value; }
        }   

        String _Location;
        public String Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        int _Index;
        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        }

        String _TaskStepIntegrationCode;
        public String TaskStepIntegrationCode
        {
            get { return _TaskStepIntegrationCode; }
            set { _TaskStepIntegrationCode = value; }
        }

        string _TaskStepIntegrationObjecte;
        public string TaskStepIntegrationObject
        {
            get { return _TaskStepIntegrationObjecte; }
            set { _TaskStepIntegrationObjecte = value; }
        }

        Person _Assigned;
        public virtual Person Assigned
        {
            get { return _Assigned; }
            set { _Assigned = value; }
        }
          
    }

    [Table("TaskTransactionTempory")]
    [DefaultClassOptions]
    public class TaskTransactionTempory:BaseObjectWarehouseID, ITaskStepTransaction
    {

        Task _Task;
        public virtual Task Task
        {
            get { return _Task; }
            set { _Task = value; }
        } 

        TaskStep _TaskStep;
        public virtual TaskStep TaskStep
        {
            get { return _TaskStep; }
            set { _TaskStep = value; }
        }

        string _TaskTemplate_SysCode;
        public string TaskTemplate_SysCode
        {
            get { return _TaskTemplate_SysCode; }
            set { _TaskTemplate_SysCode = value; }
        }
        
        String _Type;
        public String Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        String _ClientValue;
        public String ClientValue
        {
            get { return _ClientValue; }
            set { _ClientValue = value; }
        } 


        String _TaskStepIntegrationCode;
        public String TaskStepIntegrationCode
        {
            get { return _TaskStepIntegrationCode; }
            set { _TaskStepIntegrationCode = value; }
        } 

        string _TaskStepIntegrationObject;
        public string TaskStepIntegrationObject
        {
            get { return _TaskStepIntegrationObject; }
            set { _TaskStepIntegrationObject = value; }
        }

        String _TaskIntegrationCode;
        public String TaskIntegrationCode
        {
            get { return _TaskIntegrationCode; }
            set { _TaskIntegrationCode = value; }
        }
        String _TaskIntegrationObject;
        public String TaskIntegrationObject
        {
            get { return _TaskIntegrationObject; }
            set { _TaskIntegrationObject = value; }
        }

        String _TaskStepState;
        public String TaskStepState
        {
            get { return _TaskStepState; }
            set { _TaskStepState = value; }
        }

        string _Client;
        public string Client
        {
            get { return _Client; }
            set { _Client = value; }
        }

        String _SqlError;
        public String SqlError
        {
            get { return _SqlError; }
            set { _SqlError = value; }
        }

        String _MobileError;
        public String MobileError
        {
            get { return _MobileError; }
            set { _MobileError = value; }
        }

        String _TransactionCode;
        public String TransactionCode
        {
            get { return _TransactionCode; }
            set { _TransactionCode = value; }
        } 

        public override void OnSaving()
        {
            base.OnSaving();
           
        }

    }
  
    [Table("TaskTransaction")]
    [DefaultClassOptions]
    public class TaskTransaction : BaseObjectWarehouseCode, ITaskStepTransaction
    {

        LocationWarehouse _LocationWarehouse;
        public virtual LocationWarehouse LocationWarehouse
        {
            get { return _LocationWarehouse; }
            set { _LocationWarehouse = value; }
        }


        //StockItem _Stock;
        //public virtual StockItem StockItem
        //{
        //    get { return _Stock; }
        //    set { _Stock = value; }
        //} 

        Task _Task;
        public virtual Task Task
        {
            get { return _Task; }
            set { _Task = value; }
        }

        TaskStep _TaskStep;
        public virtual TaskStep TaskStep
        {
            get { return _TaskStep; }
            set { _TaskStep = value; }
        }

        TaskTemplate _TaskTemplate;
        public TaskTemplate TaskTemplate
        {
            get { return _TaskTemplate; }
            set { _TaskTemplate = value; }
        }

        Item _Item;
        public virtual Item Item
        {
            get { return _Item; }
            set { _Item = value; }
        } 

        String _ContainerCode;
        public String ContainerCode
        {
            get { return _ContainerCode; }
            set { _ContainerCode = value; }
        }

        String _ExpireDate;
        public String ExpireDate
        {
            get { return _ExpireDate; }
            set { _ExpireDate = value; }
        }

        String _Quantity;
        public String Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        String _BatchCode;
        public String BatchCode
        {
            get { return _BatchCode; }
            set { _BatchCode = value; }
        }

        string _Temperature;
        public string Temperature
        {
            get { return _Temperature; }
            set { _Temperature = value; }
        } 

        string _TaskStepIntegrationCode;
        public string TaskStepIntegrationCode
        {
            get { return _TaskStepIntegrationCode; }
            set { _TaskStepIntegrationCode = value; }
        }

        string _TaskIntegrationObject;
        public string TaskIntegrationObject
        {
            get { return _TaskIntegrationObject; }
            set { _TaskIntegrationObject = value; }
        }

        string _TaskIntegrationCode;
        public string TaskIntegrationCode
        {
            get { return _TaskIntegrationCode; }
            set { _TaskIntegrationCode = value; }
        }

        string _TaskStepIntegrationObject;
        public string TaskStepIntegrationObject
        {
            get { return _TaskStepIntegrationObject; }
            set { _TaskStepIntegrationObject = value; }
        }

        string _TaskStepState;
        public string TaskStepState
        {
            get { return _TaskStepState; }
            set { _TaskStepState = value; }
        }

        String _Client;
        public String Client
        {
            get { return _Client; }
            set { _Client = value; }
        } 

        public override void OnSaving()
        {
            base.OnSaving(); 
        }

    }

    [Table("vw_TaskStepTransaction")]
    [DefaultClassOptions]
    public class TaskStepTransactionVW : IXafEntityObject, IObjectSpaceLink
    {
        Item _Item;
        [Column("Item_SysCode")]
        public virtual Item Item
        {
            get { return _Item; }
            set { _Item = value; }
        }


        String _ContainerCode;
        public String ContainerCode
        {
            get { return _ContainerCode; }
            set { _ContainerCode = value; }
        }

        LocationWarehouse _LocationWarehouse;
        [Column("LocationWarehouse_SysCode")] 
        public virtual LocationWarehouse LocationWarehouse
        {
            get { return _LocationWarehouse; }
            set { _LocationWarehouse= value; }
        }
         
        String _Batch;
        public String Batch
        {
            get { return _Batch; }
            set { _Batch = value; }
        }

        String _Quantity;
        public String Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        String _TransactionCode;
        [Key]
        public String TransactionCode
        {
            get { return _TransactionCode; }
            set { _TransactionCode = value; }
        }

        string _TaskStep_SysCode;
        public string TaskStep_SysCode
        {
            get { return _TaskStep_SysCode; }
            set { _TaskStep_SysCode = value; }
        }

        String _Task_SysCode;
        public String Task_SysCode
        {
            get { return _Task_SysCode; }
            set { _Task_SysCode = value; }
        }

        string _ExpireDate;
        public string ExpireDate
        {
            get { return _ExpireDate; }
            set { _ExpireDate = value; }
        }

        String _TaskTemplate_SysCode;
        public String TaskTemplate_SysCode
        {
            get { return _TaskTemplate_SysCode; }
            set { _TaskTemplate_SysCode = value; }
        }

        String _TaskStepIntegrationCode;
        public String TaskStepIntegrationCode
        {
            get { return _TaskStepIntegrationCode; }
            set { _TaskStepIntegrationCode = value; }
        }

        String _TaskIntegrationObject;
        public String TaskIntegrationObject
        {
            get { return _TaskIntegrationObject; }
            set { _TaskIntegrationObject = value; }
        }

        String _TaskIntegrationCode;
        public String TaskIntegrationCode
        {
            get { return _TaskIntegrationCode; }
            set { _TaskIntegrationCode = value; }
        }

        String _TaskStepIntegrationObject;
        public String TaskStepIntegrationObject
        {
            get { return _TaskStepIntegrationObject; }
            set { _TaskStepIntegrationObject = value; }
        }

        String _TaskStepState;
        public String TaskStepState
        {
            get { return _TaskStepState; }
            set { _TaskStepState = value; }
        }

        DateTime? _CreateTime;
        public DateTime? CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }

        String _Warehouse_SysCode;
        public String Warehouse_SysCode
        {
            get { return _Warehouse_SysCode; }
            set { _Warehouse_SysCode = value; }
        }

        String _Temperature;
        public String Temperature
        {
            get { return _Temperature; }
            set { _Temperature = value; }
        }

        String _Client;
        public String Client
        {
            get { return _Client; }
            set { _Client = value; }
        } 
        IObjectSpace _ObjectSpace;
        [NotMapped]
        [Browsable(false)]
        public IObjectSpace ObjectSpace
        {
            get { return _ObjectSpace; }
            set { _ObjectSpace = value; }
        }

        public virtual void OnCreated()
        {
        }

        public virtual void OnSaving()
        {

        }

        public virtual void OnLoaded()
        {

        }

    }
    public interface ITaskStepTransaction
    {
        
        TaskStep TaskStep { get; set; }
        Task Task { get; set; }
      
        Warehouse Warehouse { get; set; } 
        String TaskStepIntegrationCode { get; set; }
        String TaskIntegrationObject { get; set; }
        String TaskIntegrationCode { get; set; }
        String TaskStepIntegrationObject { get; set; }
        String TaskStepState { get; set; } 
        String Client { get; set; }

    }
}
 