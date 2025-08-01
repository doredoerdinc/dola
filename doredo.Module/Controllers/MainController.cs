using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using LgsLib.Base;
using LgsLib.Base.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CsvHelper;
using CsvHelper.Configuration;

using System.Reflection;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Data.SqlClient;
using DevExpress.XtraPrinting;
using DevExpress.ExpressApp.ReportsV2;
using Microsoft.Extensions.DependencyInjection;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;

//using System.Text.Json;
namespace dola.Module
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
  
    public partial class MainController : ViewController
    {
     //   public OrderDailyPlaned orderDailyPlanedStatic = null;
        public ExTernalDocument ExTernalDocumentImportStatic = null;
        private List<OrderItemCSV> OrderItemCSVes;
        public MainController()
        {
            InitializeComponent();
             OrderPlanedGoodsOutInTask.TargetObjectType = typeof(Order);
            LabelCreateFromOrderLine.TargetObjectType = typeof(OrderLine);
            TemplateLabelCreate.TargetObjectType = typeof(ContainerTemplate);
            OrderPickReservation.TargetObjectType = typeof(Order);
            GenerateTaskForItemPickProcess.TargetObjectType = typeof(Order);
            GenerateTaskForItemAcceptProcess.TargetObjectType = typeof(Order);
            GenerateTaskForItemPickProcess.TargetObjectType = typeof(Order);
            //GenerateTaskForVehicleLoadProcess.TargetObjectType = typeof(Trip);
            GenerateTaskForStockControlProcess.TargetObjectType = typeof(LocationWarehouse);
            GenerateTaskForLocationItemReplenanchmentProcess.TargetObjectType = typeof(LocationOperationTariff);
            ItemTempToSTock.TargetObjectType = typeof(TaskStepTransactionVW); 
            DeleteTaskStepTransactionTemplate.TargetObjectType = typeof(TaskStepTransactionVW);
            DeleteStockControlStep.TargetObjectType = typeof(StockControlStep);
            TaskAssignedOrderGoodsIn.TargetObjectType = typeof(Order);
            TaskAssignedStockControl.TargetObjectType = typeof(StockControlStep);
            
           // OrderItemPrint.TargetObjectType = typeof(OrderItem);
            OrderItemImport.TargetObjectType = typeof(ExTernalDocument);
            OrderItemExtoMap.TargetObjectType = typeof(ExTernalDocument);


        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            if (View is ListView)
            {

                var members = View.ObjectTypeInfo.Members;
                foreach (var member in members)
                {
                    if ((member.Name == "Owner" || member.Name == "Owner")&& SecuritySystem.CurrentUserName!="Admin")
                    {
                        var a = 1;

                        ((ListView)View).CollectionSource.Criteria["OwnerFilter"] =
                          new BinaryOperator("Owner.Syscode", GlobalConst.UserOwner.SysCode, BinaryOperatorType.Equal);
                    }
                }
            }
        }

        public IEnumerable<ActionBase> GetActions()
        {
            var controllers = Frame.Controllers.Values as System.Collections.Generic.List<DevExpress.ExpressApp.Controller>;
            return controllers.SelectMany(x => x.Actions.AsEnumerable());
        }
        protected override void OnDeactivated()
        {
            ApplicationDefination.PopupSettingsInitial();
            base.OnDeactivated();
        }
        private void GoodsInOut_FromOrder(IObjectSpace newObjectSpace, List<OrderLine> selectedLines)
        {
            (this).ExecutePopupSimple((clientModel, nonObjectSpace) =>
            {
                clientModel.Ramp = newObjectSpace.GetObjectByKey<LocationWarehouse>("R1");
            }, (Action<GoodsInOut>)((clientModel) =>
            {

                foreach (var order in getSelectedOrderFromOrderLine(newObjectSpace,selectedLines))
                {
                    var wlocation = newObjectSpace.GetObjectByKey<LocationWarehouse>(clientModel.Ramp.SysCode);
                    order.LocationWarehouse = wlocation;
                }
                foreach (var line in selectedLines)
                { 
                    var template = getTaskTemplate(newObjectSpace, typeof(OrderLine), line);
                    if (template == null)
                    {
                        throw new ArgumentException(string.Format("Siparişe ait satırların bazılarının görev şablonları bulunamadı", line.SysCode));
                    }
                    line.TaskTemplate = template;
                }
                newObjectSpace.CommitChanges();
                foreach (var line in selectedLines)
                {
                    if (line.TaskTemplate.BeforeCallAction != null)
                    {
                        var execAction = GetActions().SingleOrDefault(x => x.Model.Id == line.TaskTemplate.BeforeCallAction);
                        if (execAction != null)
                        {
                            var simpleAction = execAction as SimpleAction;
                            execAction.Active["ActionActivePassive"] = true;
                            simpleAction.DoExecute();
                            execAction.Active["ActionActivePassive"] = false;
                        }
                        else
                        {
                            throw new ArgumentException(string.Format("Seçili Before Action Bulunamadı ActionId={0}", line.TaskTemplate.BeforeCallAction));
                        }

                    }

                }
            }
          )); 
        }
        private void GenerateTaskForItemAccept(IObjectSpace newObjectSpace, List<OrderLine> selectLines)
        {
            var groupLines = selectLines.GroupBy(x => new { x.TaskTemplate, x.Order })
           .Select(group => new { groupOrder = group.Key, totalStep = group.Count() });
            var tasks = new List<Task>();
            foreach (var line in groupLines)
            {
                var order = newObjectSpace.GetObject<Order>(line.groupOrder.Order);
                order.State = newObjectSpace.GetObjectByKey<State>("OnRamp");
                string fromAddress = null;
                string toAddress = null;
                if (order.EntryLocation != null)
                {
                    fromAddress = order.EntryLocation.Vehicle.SysCode;
                }
                if (order.LocationWarehouse != null)
                {
                    toAddress = order.LocationWarehouse.SysCode;
                }  
                var task = GetTask(newObjectSpace, line.groupOrder.TaskTemplate, typeof(Order), line.groupOrder.Order.SysCode, fromAddress, toAddress,0);
                int taskStepIndex = 0;
                foreach (var line2 in selectLines.Where(x=>x.Order==order))
                {
                    taskStepIndex = taskStepIndex + 1;
                    var taskStep = createTaskStep(newObjectSpace, line2, task, toAddress, taskStepIndex, line2.SysCode,null);
                } 
                tasks.Add(task);
            }  
            newObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh(); 
        }
 
        private List<Order> getSelectOrder(IObjectSpace newObjectSpace)
        {
            var orders = new List<Order>();
            foreach (var selectObjec in View.SelectedObjects)
            {
                var selectObjectType = newObjectSpace.GetObjectType(selectObjec);
                var selectObjectKey = newObjectSpace.GetKeyValue(selectObjec);
                var order = newObjectSpace.GetObjectByKey<Order>(selectObjectKey);
                //    var getTasks = order.Tasks.Where(x => x.Template.SysCode == "GoodsIn").Count(); 
                orders.Add(order);
            }
            return orders;
        }
        private List<OrderLine> getSelectOrderLines(IObjectSpace newObjectSpace)
        {
            var orderLines = new List<OrderLine>();
             
            if (View.ObjectTypeInfo.FullName == typeof(Order).FullName)
            {
                foreach (var selectObjec in View.SelectedObjects)
                {
                    var selectOrder = newObjectSpace.GetObjectType(selectObjec);
                    var selectOrderKey = newObjectSpace.GetKeyValue(selectObjec);
                    var order = newObjectSpace.GetObjectByKey<Order>(selectOrderKey);
                    foreach (var line in order.Items)
                    {
                        var orderLine = newObjectSpace.GetObjectByKey<OrderLine>(line.SysCode);

                       
                        //    var getTasks = order.Tasks.Where(x => x.Template.SysCode == "GoodsIn").Count(); 
                        orderLines.Add(orderLine);
                    }
                }
            }
            else
            {
                foreach (var selectObjec in View.SelectedObjects)
                {
                    var selectObjectType = newObjectSpace.GetObjectType(selectObjec);
                    var selectObjectKey = newObjectSpace.GetKeyValue(selectObjec);
                    var orderLine = newObjectSpace.GetObjectByKey<OrderLine>(selectObjectKey);
                    orderLine.TaskTemplate = getTaskTemplate(newObjectSpace, typeof(OrderLine), orderLine);
                    //    var getTasks = order.Tasks.Where(x => x.Template.SysCode == "GoodsIn").Count(); 
                    orderLines.Add(orderLine);
                }
            } 
            return orderLines;
        }
        private String getTaskJsonData(Task task)
        {
            var taskMap = new TaskMap();
            taskMap.Index = task.Index;
            taskMap.TaskSysCode = task.SysCode;
            taskMap.SubTitle = task.SubTitle;
            taskMap.Title = task.Title;
            taskMap.TitleImage = task.TitleImage;
            taskMap.ToLocation = task.ToLocation;
            taskMap.FromLocation = task.FromLocation;
            taskMap.TaskDescription = task.Description;
            taskMap.TaskType = task.Template.SysCode;
            taskMap.StepDoType = task.Template.StepDoType;
            taskMap.IntegrationCode = task.IntegrationCode;
            taskMap.TaskIntegrationCode = task.TaskIntegrationCode;
            taskMap.TaskIntegrationObject = task.TaskIntegrationObject;

            taskMap.TodoStepCount = task.TodoStepCount;
            taskMap.WaitingStepCount = task.WaitingStepCount;
            taskMap.CompletedStepCount = task.WaitingStepCount;
            if (taskMap.TodoStepCount == null) taskMap.TodoStepCount = 0;
            if (taskMap.WaitingStepCount == null) taskMap.WaitingStepCount = 0;
            if (taskMap.CompletedStepCount == null) taskMap.CompletedStepCount = 0;
            return JsonConvert.SerializeObject(taskMap);
        }
        public static Object GetPropertyByPath(object o, string path)
        {
            try
            {
                if (!string.IsNullOrEmpty(path))
                {
                    PropertyInfo res = null;
                    object obj = o;
                    foreach (string propName in path.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (obj == null)
                        {
                            return null;
                        }
                        res = obj.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                        if (res == null)
                        {
                            res = obj.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
                        }
                        if (res == null)
                        {
                            return null;
                        }
                        try
                        {
                            obj = res.GetValue(obj);
                        }
                        catch (Exception)
                        {

                            throw;
                        }

                    }
                    return obj;
                }
                else
                {
                    return "";
                }

            }
            catch (Exception)
            {

                return null;
            }

            //return res;
        }
        private void LabelCreateFromOrderLine_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            this.checkSelectedObjects();
            ApplicationDefination.PopupSettings(70, 70);
            var newObjectSpace = Application.CreateObjectSpace();
            var selectOrderlines = getSelectOrderLines(newObjectSpace);
            CreateLabelFromOrderLine(newObjectSpace, selectOrderlines);
            newObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }
        private  void CreateLabelFromOrderLine(IObjectSpace newObjectSpace, List<OrderLine> selectOrderlines)
        { 
            this.checkSelectedObjects();
            ApplicationDefination.PopupSettings(70, 70);
            this.ExecutePopupSimple<CreateLabelContainerNP>((clientModel, nonobjectSpace) =>
            {
            }, (clientModel) =>
            {

                if (clientModel.Quantity < 1)
                {
                    throw new Exception(string.Format("Etiket Miktarı Grilmelidir"));
                }
                foreach (var line in selectOrderlines)
                {
                    for (int i = 1; i <= clientModel.Quantity; i++)
                    { 
                        var ContainerCode = line.SysCode + "10" + i.ToString();
                        Attribute attribute = null;

                       var findCriteria = GroupOperator.Combine(
                       GroupOperatorType.And
                       , new BinaryOperator("ContainerCode", ContainerCode, BinaryOperatorType.Equal)
                       );

                        attribute = newObjectSpace.FindObject<Attribute>(findCriteria);
                        if(attribute == null)
                        {
                            attribute = newObjectSpace.CreateObject<Attribute>();
                            attribute.ContainerCode = ContainerCode;
                            attribute.BatchCode =clientModel.BatchCode;
                            attribute.ExpireDate = clientModel.ExpireDate;
                        } 

                        attribute.OrderLine = line;
                        attribute.Order = line.Order;
                        //   conatiner.BatchCode = clientModel.BatchCode;
                        //   conatiner.ExpireDate = clientModel.ExpireDate; 
                    }
                }
                var order = newObjectSpace.GetObject<Order>(selectOrderlines.FirstOrDefault().Order);
                order.TransporterQuantity = clientModel.Quantity;
                newObjectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
            });
         
        }
        private void VehiclePlaned_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ApplicationDefination.PopupSettings(30, 50);
            var newObjectSpace = this.Application.CreateObjectSpace();

            var objectKeyValue = newObjectSpace.GetKeyValue(View.CurrentObject);
            var objectKey = newObjectSpace.GetObjectByKey<Trip>(objectKeyValue);
            var trip = newObjectSpace.GetObjectByKey<Trip>(objectKeyValue);
            var selectOrders = new List<Order>();
            var template = newObjectSpace.GetObjectByKey<TaskTemplate>("Transport");
            //foreach (var selectObjec in trip.Orders)
            //{
            //    var selectObjectType = newObjectSpace.GetObjectType(selectObjec);
            //    var selectObjectKey = newObjectSpace.GetKeyValue(selectObjec);
            //    var slcObject = newObjectSpace.GetObjectByKey<Order>(selectObjectKey);
            //    selectOrders.Add(slcObject);
            //}
            this.ExecutePopupSimple<VehiclePlanedNP>((clientModel, nonObjectSpace) =>
            {
                if (trip.Vehicle != null)
                {
                    clientModel.Vehicle = newObjectSpace.GetObject<Vehicle>(trip.Vehicle);

                }
                if (trip.Employee != null)
                {
                    clientModel.Driver = newObjectSpace.GetObject<Person>(trip.Employee);
                }


            }, (clientModel) =>
            {
                int taskIndex = 0;

                trip.Vehicle = newObjectSpace.GetObject<Vehicle>(clientModel.Vehicle);
                trip.Employee = newObjectSpace.GetObject<Person>(clientModel.Driver);
                foreach (var ord in selectOrders)
                {
                    taskIndex = taskIndex + 1;
                    var owner = newObjectSpace.GetObject<Owner>(ord.Owner);
                    var toAddress = newObjectSpace.GetObject<Address>(ord.ToAddress);
                    var fromAddress = newObjectSpace.GetObject<Address>(ord.FromAddress);
                    var toCity = newObjectSpace.GetObject<City>(toAddress.City);
                    var fromCity = newObjectSpace.GetObject<City>(fromAddress.City);
                    var toDistrict = newObjectSpace.GetObject<District>(toAddress.District);
                    var fromDistrict = newObjectSpace.GetObject<District>(fromAddress.District);

                    String taskFrom = string.Format("{0}-{1}-{2}", fromCity.Name, fromDistrict.Name, fromAddress.Name);
                    String taskto = string.Format("{0}-{1}-{2}", fromCity.Name, fromDistrict.Name, fromAddress.Name);
                    String Title = string.Format("{0}", owner.ShortName);

                    //var newTask = generateTask(newObjectSpace, template, trip, ord, taskFrom,taskto, taskIndex);
                    //var1 = createTaskStepFromOrder(newObjectSpace, null, newTask, taskFrom, taskStepIndex);
                    //var taskStep2 = createTaskStepFromOrder(newObjectSpace, null, newTask, taskto, taskStepIndex);
                }
                newObjectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
            });

        }
        private TaskTemplate getTaskTemplate(IObjectSpace newObjectSpace, Type type, object obj)
        {
            var parseCriteria = CriteriaOperator.Parse("ObjectTypeName=?", type.FullName);
            var taskTemplates = newObjectSpace.GetObjects<TaskTemplate>(parseCriteria);
            taskTemplates.Count();
            foreach (var tmp in taskTemplates)
            { 
                var fitCriteria = ObjectSpace.IsObjectFitForCriteria(obj, CriteriaOperator.Parse(tmp.Smart));
                if (fitCriteria.Value)
                {
                    return tmp;
                }
            }

            return null;
        }
        private Task GetTask(
            IObjectSpace objectSpace,
            TaskTemplate template,
            Type taskIntegrationObjectType,
            string taskIntegrationCode,
            String fromLocation,
            string toLocation,
            int index)
        {
            CriteriaOperator findtTaskCriteria = null;

            findtTaskCriteria = GroupOperator.Combine(
            GroupOperatorType.And
            , new BinaryOperator("Template.Syscode", template.SysCode, BinaryOperatorType.Equal)
            , new BinaryOperator("TaskIntegrationObject", taskIntegrationObjectType.FullName, BinaryOperatorType.Equal)
            , new BinaryOperator("TaskIntegrationCode", taskIntegrationCode, BinaryOperatorType.Equal));

            Task task = objectSpace.FindObject<Task>(findtTaskCriteria);
            if (task == null)
            {
                task = objectSpace.CreateObject<Task>();
            }
            task.Index = index;
            task.Title = template.Title;
            task.SubTitle = template.Subtitle;
            var taskIntegrationObject = objectSpace.GetObjectByKey(taskIntegrationObjectType, taskIntegrationCode);
            if (template.TitleReferansObject != null)
            {
                task.Title = GetPropertyByPath(taskIntegrationObject, template.TitleReferansObject).ToString();

            }
            if (template.SubTitleReferansObject != null)
            {
                try
                {
                    task.SubTitle = GetPropertyByPath(taskIntegrationObject, template.SubTitleReferansObject).ToString();
                }
                catch (Exception)
                {
                    throw new Exception(string.Format("İlişkilendirilmiş objectType bulunamadı.Lüffen görev şablonundan SubTitleReferansObject'{0}'  boş gelmektedir.", template.SubTitleReferansObject));

                }
            }

            if (template.IntegrationCode != null)
            {
                var referansValue = GetPropertyByPath(taskIntegrationObject, template.IntegrationCode);
                if (referansValue != null)
                {
                    task.IntegrationCode = referansValue.ToString();
                }
            }
            else
            {
                task.IntegrationCode = taskIntegrationCode;
            }

            task.FromLocation = fromLocation;
            task.ToLocation = toLocation;
            task.Template = template;
            task.TitleImage = template.TitleImage;
            task.TaskIntegrationCode = taskIntegrationCode;
            task.TaskIntegrationObject = taskIntegrationObjectType.FullName;
            task.Description = template.Description;
            return task;

        }
        private TaskStep createTaskStep(IObjectSpace objectSpace, object obj, Task task, String location, int index, string key, Person person)
        {
            TaskStep taskStep = null;
            var objectType = objectSpace.GetObjectType(obj);
            var objectKeyValue = objectSpace.GetKeyValue(obj);
            var spaceObject = objectSpace.GetObject(obj);
            var criteria = CriteriaOperator.Parse("SysCode =? ", key);
            taskStep = objectSpace.FindObject<TaskStep>(criteria);
            if (taskStep == null)
            {
                taskStep = objectSpace.CreateObject<TaskStep>();
                taskStep.SysCode = key;
                taskStep.TaskStepIntegrationCode = objectKeyValue.ToString();
                taskStep.TaskStepIntegrationObject = objectType.FullName;
            }
            var stepItems = new List<TaskStepItemMap>();
            taskStep.Location = location;
            taskStep.Task = task;
            taskStep.Index = index;
            if (person != null)
            {
                taskStep.Assigned = person;
            } 
            return taskStep; 
        } 
        private TaskStepMap getTaskStepItems(IObjectSpace objectSpace, TaskTemplate template, object obj, TaskStep taskStep)
        {
            var stepMap = new TaskStepMap();
            var taskStepItemMaps = new List<TaskStepItemMap>();
            stepMap.SysCode = taskStep.SysCode;
            stepMap.TaskSysCode = taskStep.Task.SysCode;
            stepMap.Index = taskStep.Index;
            if (taskStep.Assigned != null)
            {
                stepMap.Person = taskStep.Assigned.FullName;
            }
            stepMap.TaskTemplateCode = template.SysCode;
            stepMap.TaskStepIntegrationCode = taskStep.TaskStepIntegrationCode;
            stepMap.TaskStepIntegrationObject = taskStep.TaskStepIntegrationObject;
           
            foreach (var tmp in template.TemplateItems)
            {
                var taskStepItemMap = new TaskStepItemMap();
                var tempItem = objectSpace.GetObject<TaskTemplateItem>(tmp);
                object referenceObject = null;
                List<object> referansObjects = new List<object>();
                Type objectType = null;
                if (tmp.ObjectTypeName != null)
                {
                    
                    try
                    {
                        objectType = Helper.getXafType(tmp.ObjectTypeName);
                    }
                    catch (Exception)
                    {
                        throw new UserFriendlyException(string.Format("İlişkilendirilmiş objectType bulunamadı.Lüffen görev şablonuna ait Itemslardan ItemName='{0}' Object Türü='{1}'", tempItem.Name, tmp.ObjectTypeName));
                    }

                    if (tmp.Type == null)
                    {
                        throw new UserFriendlyException(string.Format("Öbject Türü boş olamaz Object Type='{0}'", tmp.ObjectTypeName));

                    }

                    if (tmp.Type.SysCode != "Lookup")
                    {
                        referenceObject = objectSpace.GetObjectByKey(objectType, taskStep.SysCode);
                    }  
                    

                }
                bool? fitCriteria = true;
                if (referenceObject != null)
                { 
                    fitCriteria = ObjectSpace.IsObjectFitForCriteria(referenceObject, CriteriaOperator.Parse(tmp.Smart));
                }
                if (fitCriteria.Value)
                {
                    taskStepItemMap.SysValue = tempItem.SysValue;
                    taskStepItemMap.IsEnableBarcodeScanner = 1;
                    taskStepItemMap.IsEnableMultipleBarcodeScanner = 1;
                    if (tempItem.SysValueReferanceProperty != null)
                    {

                        if (tmp.Type.SysCode == "Lookup"&& objectType.FullName!= "dola.Module.OrderLine")
                        {
                            string properties = null;
                            var criteria = CriteriaOperator.Parse("OrderLine=?", taskStep.SysCode);
                     
                            var objects = objectSpace.GetObjects(objectType, criteria,false);
                           
                                foreach (var nobject in objects)
                                {
                                var objectKey = objectSpace.GetKeyValue(nobject);
                                    var refObject = objectSpace.GetObjectByKey(objectType, objectKey);
                                    var propertyValue = GetPropertyByPath(refObject, tempItem.SysValueReferanceProperty);
                                properties = properties + "," + propertyValue;
                                   // dynamicProperties.Add(propertyValue.ToString());
                                }
                                
                            taskStepItemMap.SysValue = properties.Remove(0,1);
                          //  var dynamicProperty=GetPropertyByPath(referenceObject, tempItem.SysValueReferanceProperty);

                        }
                        else
                        {
                            var dynamicProperty = GetPropertyByPath(referenceObject, tempItem.SysValueReferanceProperty);
                            if (dynamicProperty != null)
                            {
                                taskStepItemMap.SysValue = dynamicProperty.ToString();
                            }
                        }

                        
                    
                }
                    taskStepItemMap.Name = tempItem.Name;
                    taskStepItemMap.DisplayName = tempItem.DisplayName;
                    if (tempItem.Type == null)
                    {
                        throw new ArgumentException(string.Format("Template Tanımlamalarında Tipi belirtilmemiş alan bulunmaktadır. {0}-{1}", tempItem.TaskTemplate.SysCode, tempItem.Name));
                    }
                    taskStepItemMap.Type = tempItem.Type.SysCode;
                    taskStepItemMap.ClientValue = tempItem.ClientValue;
                    if(tempItem.CriteriaType!=null)
                    {
                        taskStepItemMap.Criteria = tempItem.CriteriaType.MathematicalExpression;
                    } 
                    taskStepItemMap.FormVisible = tempItem.FormVisible;
                    taskStepItemMap.ListVisible = tempItem.ListVisible;
                    taskStepItemMap.Index = tempItem.Index;
                    taskStepItemMap.ValidationMessage = tempItem.ValidationMessage;
                    taskStepItemMap.CharacterLength = tempItem.CharacterLength;
                    taskStepItemMap.IsLoop = tempItem.IsLoop;
                    taskStepItemMaps.Add(taskStepItemMap);
                }
            }
            stepMap.items = taskStepItemMaps;
            var titleObject = template.TemplateItems.FirstOrDefault(x => x.IsTitle);
            if (titleObject != null)
            {
                stepMap.Title = stepMap.items.FirstOrDefault(x => x.Name == titleObject.Name).SysValue;
            }

            return stepMap;
        }
        public void setState(IObjectSpace newObjectSpace, string ObjectType, string ObjectKey, State state)
        {
            var xafType = Helper.getXafType(ObjectType);
            var curentObject = newObjectSpace.GetObjectByKey(xafType, ObjectKey);
            var stateProperty = curentObject.GetType().GetProperty("State");
            if (stateProperty != null)
            {
                stateProperty.SetValue(curentObject, state);
            }
        }
        private void AssignTask_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var newObjectSpace = Application.CreateObjectSpace();
            List<Task> selectObjects = new List<Task>();
            List<Order> orders = new List<Order>();

            foreach (var selectObjec in View.SelectedObjects)
            {
                var selectObjectType = newObjectSpace.GetObjectType(selectObjec);
                var selectObjectKey = newObjectSpace.GetKeyValue(selectObjec);
                var slcObject = newObjectSpace.GetObjectByKey<Task>(selectObjectKey);
                //var order = newObjectSpace.GetObject<Order>(slcObject.Order);
                //orders.Add(order);
                selectObjects.Add(slcObject);
            }
            //  var updatetask=GenerateTask(null,newObjectSpace,selectObjects.FirstOrDefault().Template,orders,)
            TaskAssign(newObjectSpace, selectObjects);
        }
        private void TaskAssign(IObjectSpace newObjectSpace, List<Task> tasks)
        {
            (this).ExecutePopupSimple((clientModel, nonObjectSpace) =>
            {

            }, (Action<LabelTemplateNP>)((clientModel) =>
            {
                TaskAssignedNonePopup(newObjectSpace, tasks);
                newObjectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
            }));

        }

        private void TaskAssignedNonePopup(IObjectSpace newObjectSpace, List<Task> tasks)
        {
            foreach (var obj in tasks)
            {
                var tsk = newObjectSpace.GetObject<Task>(obj);

                var template = newObjectSpace.GetObject<TaskTemplate>(tsk.Template);
                var stateTaskVisible = newObjectSpace.GetObject<State>(template.StateTaskVisible);
                var stateTaskStepVisible = newObjectSpace.GetObject<State>(template.StateTaskVisible);
                setState(newObjectSpace, tsk.TaskIntegrationObject, tsk.TaskIntegrationCode, stateTaskStepVisible);
                obj.State = stateTaskVisible; 

                foreach (var objLine in tsk.TaskSteps)
                {
                    var step = newObjectSpace.GetObject<TaskStep>(objLine);
                    setState(newObjectSpace, objLine.TaskStepIntegrationObject, objLine.TaskStepIntegrationCode, stateTaskStepVisible);
                    step.State = stateTaskStepVisible;
                    var taskstepItems = getTaskStepItems(newObjectSpace, template, objLine, step);
                    //if (objLine.OrderLine != null)
                    //{
                    //    var requestItemForm = getItemRequestFormItems(newObjectSpace, objLine.OrderLine.Item);
                    //}

                    // taskstepItems.AddRange(requestItemForm);
                    step.JsonData = JsonConvert.SerializeObject(taskstepItems);
                }
                tsk.JsonData = getTaskJsonData(tsk);
            }
        }

        //private void TemplateLabelCreate_Execute(object sender, SimpleActionExecuteEventArgs e)
        //{
        //    ApplicationDefination.PopupSettings(30, 50);
        //    var newObjectSpace = this.Application.CreateObjectSpace();
        //    var objectKeyValue = newObjectSpace.GetKeyValue(View.CurrentObject);
        //    var labelTemplate = newObjectSpace.GetObjectByKey<ContainerTemplate>(objectKeyValue);
        //    this.ExecutePopupSimple<LabelTemplateNP>((clientModel, nonObjectSpace) =>
        //    {

        //    }, (clientModel) =>
        //    {
        //        int countter = labelTemplate.LastValue;
        //        for (int i = 0; i < clientModel.Quantity; i++)
        //        {
        //            var label = newObjectSpace.CreateObject<Container>();
        //            countter = countter + 1;
        //            var sysCode = string.Format(countter.ToString(labelTemplate.Format));
        //            label.SysCode = sysCode;
        //            ///  label.LinkLabel = labelTemplate;
        //            label.Value = countter;
        //        }

        //        labelTemplate.LastValue = countter;
        //        newObjectSpace.CommitChanges();
        //        View.ObjectSpace.Refresh();
        //    });
        //}
        public Boolean ChecImportProperty(string property, int rowLine, string propertyName)
        {
            if (string.IsNullOrEmpty(property))
            {
                return false;
                throw new ArgumentException(string.Format("Satır={0} Alan={1} boş olamaz", rowLine, propertyName));
            }
            return true;
        }

        private void GenerateTaskForItemAcceptProcess_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            this.checkSelectedObjects();
            ApplicationDefination.PopupSettings(70, 50);
            var newObjectSpace = Application.CreateObjectSpace();
            var orderLines = getSelectOrderLines(newObjectSpace);///getSelectOrder(newObjectSpace);
            newObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
            GenerateTaskForItemAccept(newObjectSpace, orderLines);

        }
        public string executed_SqlQuery<RType>(string query, params object[] parameters)
        {
            var dbContex = new dolaDbContext();
            try
            {
                var returnData = dbContex.Database.SqlQuery(typeof(RType), query, parameters);
                return returnData.ToString(); 
            }
            catch (Exception e)
            {

                throw new InvalidOperationException(e.Message);
            }
        }

        private List<Order> getSelectedOrderFromOrderLine(IObjectSpace newObjectSpace, List<OrderLine> selectedLines)
        {
            List<Order> orders = new List<Order>();
            var groupLines = selectedLines.GroupBy(x => new { x.Order })
       .Select(group => new { groupOrder = group.Key, totalStep = group.Count() });
            foreach (var line in groupLines)
            {
                var order = newObjectSpace.GetObjectByKey<Order>(line.groupOrder.Order.SysCode);
                orders.Add(order);
            }
            return orders;
        }

        private void OrderPickReservation_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            this.checkSelectedObjects();
            ApplicationDefination.PopupSettings(70, 50);
            var newObjectSpace = Application.CreateObjectSpace();
            var newObjectSpaceNP = Application.CreateObjectSpace(typeof(OrderLineMetNP));
            var selectOrderLines = getSelectOrderLines(newObjectSpace);
            var requestOrderLines = new List<OrderLine>();
            var requestMetList = new List<OrderLineMetNP>();
            var state= newObjectSpace.GetObjectByKey<State>("ItemPickReservated");

            foreach (var ord in getSelectedOrderFromOrderLine(newObjectSpace,selectOrderLines))
            {
                ord.State = state;
            }

            foreach (var ordl in selectOrderLines)
            {
        
                ordl.State = state;              
                ordl.TaskTemplate=getTaskTemplate(newObjectSpace,typeof(OrderLine),ordl);
                requestOrderLines.Add(ordl);
                //foreach (var ln in ord.Items)
                //{
                //    var keyValu = newObjectSpace.GetKeyValue(ln);
                //    var ordLine = newObjectSpace.GetObjectByKey<OrderLine>(keyValu);
                //    ordLine.State = state;
                //    requestOrderLines.Add(ordLine);
                //}
            }
            foreach (var rs in requestOrderLines)
            {
                CriteriaOperator stockReservationCriteria = GroupOperator.Combine(
                    GroupOperatorType.And
                    , new BinaryOperator("Item.SysCode", rs.Item.SysCode, BinaryOperatorType.Equal)
                   , new NullOperator("LinkStockItem")
                    , new BinaryOperator("AvailableQuantity", 0, BinaryOperatorType.Greater)
                    );
                CriteriaOperator pickOperationSmart = GroupOperator.Combine(
                GroupOperatorType.And
                , new BinaryOperator("OperationType", EnumOperationType.Picking, BinaryOperatorType.Equal)
                //    , new BinaryOperator("Container.LocationStock.FunctionType", EnumLocationFunctionType.StockTransaction, BinaryOperatorType.Equal)

                );
                var stockItems = newObjectSpace.GetObjects<StockItem>(stockReservationCriteria, false);
                List<StockItem> sortStockItem = null;
                if (rs.Item.RotationType == EnumRotationType.Lifo)
                {
                    sortStockItem = stockItems.OrderBy(x => x.ExpireDate).OrderBy(x => x.Batch).OrderByDescending(x => x.CreateTime).OrderBy(x => x.Container.LocationWarehouse.SysCode).ToList();
                }
                else
                { 
                    sortStockItem = stockItems.OrderBy(x => x.ExpireDate).OrderBy(x => x.Container.LocationWarehouse.SysCode).ToList();
                }

                var sortPicking = new List<SortByLocation>();
                var locationOperation = newObjectSpace.FindObject<LocationOperation>(pickOperationSmart);
                var lIndex = 500;


                foreach (var stockItem in sortStockItem)
                {
                    SortByLocation sortBy = new SortByLocation();
                    sortBy.StockItem = stockItem;
                    if (locationOperation != null)
                    {
                        var locationTarif = locationOperation.OperationTariffs.Where(x => x.LocationWarehouse== stockItem.Container.LocationWarehouse).OrderBy(y => y.Index).FirstOrDefault();
                        if (locationTarif != null)
                        {
                            sortBy.Index = locationOperation.Index;
                        }
                        else
                        {
                            lIndex = lIndex + 1;
                            sortBy.Index = lIndex;
                        }
                    }
                    else
                    {
                        lIndex = lIndex + 1;
                        sortBy.Index = lIndex;
                    }

                    sortPicking.Add(sortBy);
                }
                var tempMets = new List<OrderLineMetNP>();
                var beforeMets = rs.ItemReservations.Sum(x => x.ReservationQuantity);
                var requestQuantity = rs.RequestQuantity - beforeMets;
                double forRequestQuantity = 0;
                int i = rs.ItemReservations.Count();
                foreach (var sortpick in sortPicking)
                {
                    i = i + 1;

                    double setQuantity = 0;
                    double diff = requestQuantity.Value - forRequestQuantity;
                    if (diff == 0) { break; }
                    if (diff >= 0)
                    {
                        if (sortpick.StockItem.AvailableQuantity.Value <= diff)
                        {
                            setQuantity = sortpick.StockItem.AvailableQuantity.Value;
                        }
                        else if (sortpick.StockItem.AvailableQuantity.Value > diff)
                        {
                            setQuantity = diff;
                        }
                        forRequestQuantity = forRequestQuantity + setQuantity;
                        // requestQuantity = requestQuantity - lmetQuantity;
                        var met = new OrderLineMetNP();
                        met.StockItem = sortpick.StockItem;
                        met.OrderLine = rs;
                        met.Reservation = setQuantity;
                        met.Index = sortpick.Index;
                        met.SysCode = rs.SysCode + i.ToString();
                        
                        tempMets.Add(met);
                    }
                }
                requestMetList.AddRange(tempMets);

            }
            this.ExecutePopupSimpleList<OrderLineMetNP>((objectListView, nonObjectSpace) =>
            {
                foreach (var item in requestMetList)
                {
                    objectListView.CollectionSource.Add(item);
                }

            }, (listView) =>
            {
                //if(listView.CollectionSource.List.Count<1)
                //{
                //    throw new ArgumentException(string.Format("Siparişe ait rezervasyon yapılacak her hangi bir ürün bulunmadı!"));
                //}
                //        
                foreach (OrderLineMetNP lmet in listView.CollectionSource.List)
                {

                    var stock = newObjectSpace.GetObject<StockItem>(lmet.StockItem);
                    var OrderLine = newObjectSpace.GetObject<OrderLine>(lmet.OrderLine);
                    var order = newObjectSpace.GetObject<Order>(OrderLine.Order);
                    var newMet = newObjectSpace.CreateObject<StockItemReservation>();

                    newMet.SysCode = lmet.SysCode;
                    newMet.StockItem = stock;
                    newMet.OrderLine = OrderLine;
                    newMet.ReservationQuantity = lmet.Reservation;
                    newMet.Order = order;
                    newMet.Index = lmet.Index;

                    // labelItem.BeforeQuantity = labelItem.AvailableQuantity;
                    // labelItem.AvailableQuantity = labelItem.AvailableQuantity.Value - lmet.ReservationQuantity;                    
                    stock.ReservationQuantity = stock.ItemReservations.Sum(x => x.ReservationQuantity) + lmet.Reservation;
                    if (stock.ReservationQuantity >= stock.AvailableQuantity)
                    {
                        stock.AvailableQuantity = 0;
                    }
                    else
                    {
                        stock.AvailableQuantity = stock.AvailableQuantity - stock.ReservationQuantity;
                    }
                }

                newObjectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
            });

        }
        private void OrderPlanedGoodsOutInTask_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ApplicationDefination.PopupSettings(70, 50);
            var newObjectSpace = this.Application.CreateObjectSpace();
            var selectOrderLines = getSelectOrderLines(newObjectSpace);
            GoodsInOut_FromOrder(newObjectSpace, selectOrderLines);
        } 
         

        private void GenerateTaskForItemPickProcess_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            this.checkSelectedObjects();
            ApplicationDefination.PopupSettings(70, 50); 
            var newObjectSpace = Application.CreateObjectSpace();
            var newObjectSpaceNP = Application.CreateObjectSpace(typeof(OrderLineMetNP));
            var selectLines = getSelectOrderLines(newObjectSpace);
            var groupLines = selectLines.GroupBy(x => new { x.TaskTemplate, x.Order })
            .Select(group => new { groupOrder = group.Key, totalStep = group.Count() });
            var tasks = new List<Task>(); 
            foreach (var line in groupLines)
            {
                var order = newObjectSpace.GetObject<Order>(line.groupOrder.Order);
                order.State = newObjectSpace.GetObjectByKey<State>("ItemPickPlaned");
               
                var task = GetTask(newObjectSpace, line.groupOrder.TaskTemplate, typeof(Order), line.groupOrder.Order.SysCode, "R1", "R2", 0);
                int taskStepIndex = 0;
                foreach (var line2 in selectLines)
                {
                    foreach (var pick in line2.ItemReservations)
                    {
                        if (line.groupOrder.TaskTemplate.SysCode == line2.TaskTemplate.SysCode && line.groupOrder.Order.SysCode == line2.Order.SysCode)
                        {
                            taskStepIndex = taskStepIndex + 1;
                            var taskStep = createTaskStep(newObjectSpace, pick, task, line2.Order.LocationWarehouse.SysCode, taskStepIndex, pick.SysCode, null);

                        }
                    }
                    //line2.TaskTemplate = line.groupOrder.TaskTemplate;
                }
                tasks.Add(task);
            }
            //  });
            newObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();

        }
        private void GenerateTaskForVehicleLoadProcess_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            this.checkSelectedObjects();
            ApplicationDefination.PopupSettings(70, 50);
            this.ExecutePopupSimple<TaskAssignedNP>((clientModel, nonObjectSpace) =>
            {
            }, (clientModel) =>
            {
                var newObjectSpace = Application.CreateObjectSpace();
                var selectObjects = View.SelectedObjects;
                List<Container> containeries = new List<Container>();

                foreach (var item in selectObjects)
                {
                    var objectKey = newObjectSpace.GetKeyValue(item);
                    var trip = newObjectSpace.GetObjectByKey<Trip>(objectKey);

                    foreach (var con in trip.Containeries)
                    {
                        var container = newObjectSpace.GetObjectByKey<Container>(con.SysCode); 
                        containeries.Add(container);
                    }
                    
                }

                if (containeries.FirstOrDefault()==null)
                {
                    throw new ArgumentException(string.Format("Lütfen En Az bir tane Lojistik Taşıma Birimi seçiniz (Konteyner)"));
                }


                var groupLines = containeries.GroupBy(x => new { x.Trip })
              .Select(group => new { groupOrder = group.Key, totalStep = group.Count() });

                foreach (var line in groupLines)
                {
                  
                    var template = getTaskTemplate(newObjectSpace, typeof(Trip), line);
                    var trip = newObjectSpace.GetObject<Trip>(line.groupOrder.Trip); 
                    trip.TaskTemplate = template;
                    trip.State = newObjectSpace.GetObjectByKey<State>("VehicleLoadCreatedTask");

                    if (trip.TaskTemplate == null)
                    {
                        throw new ArgumentException(string.Format("Seçmiş olduğunuz sipariş görev şablonu bulunamadı."));
                    }

                    string vehicle = null;
                    if (trip.Vehicle == null)
                    {
                        vehicle = "Rampa Sevk02";
                    }
                    var task = GetTask(
                        newObjectSpace,
                        trip.TaskTemplate,
                        typeof(Trip),
                        trip.SysCode,
                        trip.Containeries.FirstOrDefault().LocationWarehouse.SysCode,
                        vehicle,
                        1
                );
                    var index = 0;
                    foreach (var cnc in containeries)
                    {
                        index = index + 1;
                        var taskStep = createTaskStep(newObjectSpace, cnc, task,cnc.LocationWarehouse.SysCode, index, cnc.SysCode, clientModel.Person);
                    }
                }
                newObjectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
            });
        }

       

        private void TaskAssignedOrderGoodsIn_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            this.checkSelectedObjects();
            ApplicationDefination.PopupSettings(70, 50);

            this.ExecutePopupSimple<TaskAssignedNP>((client, nonObjectSpace) =>
            {
                //foreach (var item in locations)
                //{
                //    client.Locations.Add(item);
                //}

            }, (clientModel) =>
            {
                var newObjectSpace = Application.CreateObjectSpace();
                var selectOrderLines = getSelectOrderLines(newObjectSpace);
                var groupLines = selectOrderLines.GroupBy(x => new { x.TaskTemplate, x.Order})
                .Select(group => new { groupOrder = group.Key, totalStep = group.Count()});
                  

                var tasks = new List<Task>();
                foreach (var line in groupLines)
                {
                    CriteriaOperator findtTaskCriteria = GroupOperator.Combine(
                    GroupOperatorType.And
                    , new BinaryOperator("Template.Syscode", line.groupOrder.TaskTemplate.SysCode, BinaryOperatorType.Equal)
                    , new BinaryOperator("TaskIntegrationCode", line.groupOrder.Order.SysCode, BinaryOperatorType.Equal)
                    //      , new BinaryOperator("District.SysCode", this.Route.Description.District.SysCode)
                    );
                    var task = newObjectSpace.FindObject<Task>(findtTaskCriteria);
                    if (task == null)
                    {
                        throw new ArgumentException(string.Format("{0} = Siparişine Ait Görevler oluşturulmalıdır ", line.groupOrder.Order.SysCode));
                    }
                    tasks.Add(task);

                }
           
                foreach (var obj in tasks)
                {
                    var template = newObjectSpace.GetObject<TaskTemplate>(obj.Template);
                    var stateTaskVisible = newObjectSpace.GetObject<State>(template.StateTaskVisible);
                    var stateTaskStepVisible = newObjectSpace.GetObject<State>(template.StateTaskVisible);
                    setState(newObjectSpace, obj.TaskIntegrationObject, obj.TaskIntegrationCode, stateTaskStepVisible);
                    obj.State = stateTaskVisible; 
                    foreach (var objLine in obj.TaskSteps)
                    {
                        var step = newObjectSpace.GetObject<TaskStep>(objLine);
                        setState(newObjectSpace, objLine.TaskStepIntegrationObject, objLine.TaskStepIntegrationCode, stateTaskStepVisible);
                        step.State = stateTaskStepVisible;
                        if (clientModel.Person != null)
                        {
                            step.Assigned = newObjectSpace.GetObject<Person>(clientModel.Person);
                        }
                       
                        var taskstepItems = getTaskStepItems(newObjectSpace, template, objLine, step);
                        //if (objLine.OrderLine != null)
                        //{
                        //    var requestItemForm = getItemRequestFormItems(newObjectSpace, objLine.OrderLine.Item);
                        //}
                        // taskstepItems.AddRange(requestItemForm);
                        step.JsonData = JsonConvert.SerializeObject(taskstepItems);
                    }
                    obj.JsonData = getTaskJsonData(obj);
                }
                newObjectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
            }
        );
        }
        private void GenerateTaskForLocationReplenanchmentProcess_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            this.checkSelectedObjects();
            ApplicationDefination.PopupSettings(70, 50);
            var newObjectSpace = Application.CreateObjectSpace();
            var stock = newObjectSpace.GetObjects(typeof(StockItem));
            foreach (var operation in View.SelectedObjects)
            {
                var keyValue = newObjectSpace.GetKeyValue(operation);
                var gloperation = newObjectSpace.GetObjectByKey<LocationOperation>(keyValue);
                foreach (var tar in gloperation.OperationTariffs)
                {
                    var gltarif = newObjectSpace.GetObject<LocationOperationTariff>(tar);
                    foreach (StockItem stockItem in stock)
                    {
                        var fitCriteria = newObjectSpace.IsObjectFitForCriteria(stockItem, gltarif.StockItemRule);
                        StockItemLocationTransfer labelTransfer = newObjectSpace.CreateObject<StockItemLocationTransfer>();
                        labelTransfer.ToLocation = stockItem.Container.LocationWarehouse;
                        labelTransfer.FromLocation = tar.LocationWarehouse;
                        //labelTransfer.Quantity = tar.Glob
                    }
                }
            }
        }

        private void GenerateTaskForLocationItemCheckProcess_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            this.checkSelectedObjects();
            ApplicationDefination.PopupSettings(70, 50);
            var newObjectSpace = Application.CreateObjectSpace();
            var locations = new List<LocationWarehouse>();
            foreach (var slco in View.SelectedObjects)
            {
                var objectKey = newObjectSpace.GetKeyValue(slco);
                var Location = newObjectSpace.GetObjectByKey<LocationWarehouse>(objectKey);
                locations.Add(Location);
            }

            (this).ExecutePopupSimple((client, nonObjectSpace) =>
            {
                //foreach (var item in locations)
                //{
                //    client.Locations.Add(item);
                //}

            }, (Action<StockCheckNP>)((clientModel) =>
            {
                var nStockCheck = newObjectSpace.GetObject<StockControl>(clientModel.StockControl);

                if (clientModel.Person == null || clientModel.StockControl == null)
                {
                    throw new ArgumentException("You must select Person and control template");

                }
                createStockControl(clientModel, newObjectSpace, locations, nStockCheck);

                newObjectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
            })
            );
        }

        private void createStockControl(StockCheckNP clientModel, IObjectSpace newObjectSpace, List<LocationWarehouse> locations, StockControl nStockCheck)
        {
            foreach (var lc in locations)
            {
                var newcheck = newObjectSpace.CreateObject<StockControlStep>();
                newcheck.StockControl = nStockCheck;
                newcheck.Location = lc;
                newcheck.Person = newObjectSpace.GetObject(clientModel.Person);
            }

            newObjectSpace.CommitChanges();

            var task = GetTask(
                newObjectSpace,
                nStockCheck.TaskTemplate,
                typeof(StockControl),
                nStockCheck.SysCode,
                locations.FirstOrDefault().SysCode,
                locations.LastOrDefault().SysCode,
                1
                );
            foreach (var lc in nStockCheck.Locations)
            {
                var taskStep = createTaskStep(newObjectSpace, lc, task, lc.SysCode, lc.Index, lc.SysCode, clientModel.Person);
                lc.TaskStep = taskStep;
            }
        }

        private void TaskAssignedStockControl_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            this.checkSelectedObjects();
            ApplicationDefination.PopupSettings(70, 50);
            var newObjectSpace = Application.CreateObjectSpace();
            var stockControlSteps = new List<StockControlStep>();
            var TaskList = new List<Task>();

            foreach (var item in View.SelectedObjects)
            {
                var keyValue = newObjectSpace.GetKeyValue(item);
                var stockStep = newObjectSpace.GetObjectByKey<StockControlStep>(keyValue);
                stockControlSteps.Add(stockStep);
            }

            var task = newObjectSpace.GetObject<Task>(stockControlSteps.FirstOrDefault().TaskStep.Task);

            TaskList.Add(task);
            TaskAssignedNonePopup(newObjectSpace, TaskList);
            newObjectSpace.CommitChanges();
            newObjectSpace.Refresh();
        }

        private void ItemTempToSTock_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            this.checkSelectedObjects();
            ApplicationDefination.PopupSettings(70, 50);
            var newObjectSpace = Application.CreateObjectSpace();
            List<StockItem> stockItems = new List<StockItem>();
            //stockItems.Clear();

            foreach (var obj in View.SelectedObjects)
            {
                var keyValue = newObjectSpace.GetKeyValue(obj);
                var tempItem = newObjectSpace.GetObjectByKey<TaskStepTransactionVW>(keyValue);
                var stockState = StokcItemTempToStock(tempItem, newObjectSpace);
                if (stockState == "success")
                {
                    DeleteTaskStepTransation(newObjectSpace, keyValue.ToString());
                }

            }
            newObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }
        public string StokcItemTempToStock(TaskStepTransactionVW temp, IObjectSpace objectSpace)
        {
            //var newStockItem = objectSpace.CreateObject<StockItem>();
            var quantity = Convert.ToDouble(temp.Quantity);
            var AvailableQuantity = Convert.ToDouble(temp.Quantity);
            var Container = objectSpace.GetObjectByKey<Container>(temp.ContainerCode);
            var cheat = Convert.ToDouble(temp.Temperature);
            var Location = objectSpace.GetObject<LocationWarehouse>(temp.LocationWarehouse);
            var template = objectSpace.GetObjectByKey<TaskTemplate>(temp.TaskTemplate_SysCode);
            var Batch = temp.Batch;
            var ExpireDate = temp.ExpireDate;
            var Item = objectSpace.GetObject<Item>(temp.Item);
           // var Container = temp.Container;
            var messageSqlParam = new SqlParameter
            {
                ParameterName = "message",
                DbType = System.Data.DbType.String,
                Size = 4000,
                Direction = System.Data.ParameterDirection.Output
            };
            var codeSqlParam = new SqlParameter
            {
                ParameterName = "code",
                DbType = System.Data.DbType.String,
                Size = 50,
                Direction = System.Data.ParameterDirection.Output
            };
            var stockIdSqlParam = new SqlParameter
            {
                ParameterName = "stockId",
                DbType = System.Data.DbType.Int32,
                Size = 50,
                Direction = System.Data.ParameterDirection.Output
            };

            object[] sqlParams = { messageSqlParam, codeSqlParam, stockIdSqlParam };


            try
            {
                var procedure = (

    @"
   exec pr_stockTransaction 
   @Container = " + "'" + Container + "'"
+ ",@Location_SysCode=" + "'" + Location.SysCode + "'"
+ ",@Batch=" + "'" + temp.Batch + "'"
+ ",@Item_SysCode=" + "'" + Item.SysCode + "'"
+ ",@ExpireDate=" + "'" + temp.ExpireDate + "'"
+ ",@Quantity=" + temp.Quantity
+ ",@TransactionCode=" + "'" + temp.TransactionCode + "'"
+ ",@TaskIntegrationObject=" + "'" + temp.TaskStepIntegrationObject + "'"
+ ",@TaskStepIntegrationObject=" + "'" + temp.TaskStepIntegrationObject + "'"
+ ",@TaskStepState=" + "'" + temp.TaskStepState + "'"
+ ",@stockTransactionType=" + "'" + null + "'"
+ ",@TaskIntegrationCode=" + "'" + temp.TaskIntegrationCode + "'"
+ ",@TaskStep_SysCode=" + "'" + temp.TaskStep_SysCode + "'"
+ ",@TaskStepIntegrationCode=" + "'" + temp.TaskStepIntegrationCode + "'"
+ ",@Task_SysCode=" + "'" + temp.Task_SysCode + "'"
+ ",@TaskTemplate_SysCode=" + "'" + template.SysCode + "'"
+ ",@TaskTemplateOprationType=" + "'" + (int)template.OperationType + "'"
+ ",@Temperature=" + cheat
+ ",@Client=" + "'" + null + "'"
+ ",@Message = @message OUTPUT"
+ ",@stockId = @stockId OUTPUT"
+ ",@code = @code output"
);
                var dbContex = new dolaDbContext();
                var returnData = dbContex.Database.ExecuteSqlCommand(procedure, sqlParams);
                if (stockIdSqlParam.SqlValue == null)
                {
                    throw new Exception("stok alımı başarısız oldu");

                }
            }
            catch (Exception e)
            {

                throw new Exception(string.Format("stok alımı başarısız oldu hata={0}'", e.Message));

            }
            return messageSqlParam.SqlValue.ToString();
        }

        public void DeleteTaskStepTransation(IObjectSpace objectSpace, string transactionCode)
        {
            var delCriteria = string.Format("", transactionCode);
            var objects = objectSpace.GetObjects<TaskTransactionTempory>(CriteriaOperator.Parse("TransactionCode =?", transactionCode));
            foreach (var dlobj in objects)
            {
                objectSpace.Delete(dlobj);
            }

        }
        private void DeleteTaskStepTransactionTemplate_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            this.checkSelectedObjects();
            ApplicationDefination.PopupSettings(70, 50);
            var newObjectSpace = Application.CreateObjectSpace();
            foreach (var obj in View.SelectedObjects)
            {
                var keyValue = newObjectSpace.GetKeyValue(obj);
                DeleteTaskStepTransation(newObjectSpace, keyValue.ToString());
            }
            newObjectSpace.CommitChanges();
            newObjectSpace.Refresh();
        }
        private void DeleteStockControlStep_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            this.checkSelectedObjects();
            ApplicationDefination.PopupSettings(70, 50);
            var newObjectSpace = Application.CreateObjectSpace();
            foreach (var obj in View.SelectedObjects)
            {
                var keyValue = newObjectSpace.GetKeyValue(obj);
                var stepControl = newObjectSpace.GetObjectByKey<StockControlStep>(keyValue);
                var transactions = newObjectSpace.GetObjects<TaskStepTransactionVW>(CriteriaOperator.Parse("TaskStepIntegrationCode=?", keyValue));
                if (transactions.Count > 0)
                {
                    foreach (var tran in transactions)
                    {
                        DeleteTaskStepTransation(newObjectSpace, tran.TransactionCode);
                    }
                }

                newObjectSpace.Delete(stepControl.TaskStep);
                newObjectSpace.Delete(stepControl);
            }
            newObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }
        private void GenerateTaskForItemPackingProcess_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var newObjectSpace = Application.CreateObjectSpace();
            var printer = newObjectSpace.GetObjectByKey<Device>("47656037288");
            ReportsModuleV2 reportsModule = ReportsModuleV2.FindReportsModule(Application.Modules);             
            IObjectSpace reportObjectSpace = ReportDataProvider.ReportObjectSpaceProvider.CreateObjectSpace(typeof(ReportDataV2));
            IReportDataV2 reportData = reportObjectSpace.GetObject<ReportDataV2>(printer.Label);
            var report = ReportDataProvider.ReportsStorage.LoadReport(reportData);
            CriteriaOperator objectsCriteria = ((BaseObjectSpace)ObjectSpace).GetObjectsCriteria(((ObjectView)View).ObjectTypeInfo, View.SelectedObjects);
            SortProperty[] sortProperties = { new SortProperty("SysCode", SortingDirection.Descending) };

            reportsModule.ReportsDataSourceHelper.SetupBeforePrint(report, null, objectsCriteria, true, null, false);
          

            if (reportsModule != null && reportsModule.ReportsDataSourceHelper != null)
            {
                report.CreateDocument();
                PrintToolBase tool = new PrintToolBase(report.PrintingSystem);

                tool.PrinterSettings.PrinterName = printer.Name;
                tool.PrintingSystem.StartPrint += PrintingSystem_StartPrint; ;
                tool.PrintingSystem.EndPrint += PrintingSystem_EndPrint; ;
                tool.Print();
            }
           
        }
        private void PrintingSystem_StartPrint(object sender, PrintDocumentEventArgs e)
        {
            Console.WriteLine("finished");
        }
        private void PrintingSystem_EndPrint(object sender, EventArgs e)
        {
            Console.WriteLine("statrt");
        } 

        private void TaskAssignedTrip_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            this.checkSelectedObjects();
            ApplicationDefination.PopupSettings(70, 50);
            var newObjectSpace = Application.CreateObjectSpace();

            this.ExecutePopupSimple<TaskAssignedNP>((client, nonObjectSpace) =>
            {
                //foreach (var item in locations)
                //{
                //    client.Locations.Add(item);
                //}

            }, (clientModel) =>
            {
                var trips = new List<Trip>();
             
                var selectObjects = View.SelectedObjects;
                foreach (var slItem in selectObjects)
                {
                    var key = newObjectSpace.GetKeyValue(slItem);
                    var trip = newObjectSpace.GetObjectByKey<Trip>(key);
                    trip.TaskTemplate = getTaskTemplate(newObjectSpace, typeof(Trip), trip);
                    trips.Add(trip);
                }
                if (trips.FirstOrDefault() == null)
                {
                    throw new ArgumentException("Enaz bir Trip seçmelisiniz");

                }

                var tasks = new List<Task>();
                foreach (var trip in trips)
                {
                    CriteriaOperator findtTaskCriteria = GroupOperator.Combine(
                    GroupOperatorType.And
                    , new BinaryOperator("Template.Syscode", trip.TaskTemplate.SysCode, BinaryOperatorType.Equal)
                    , new BinaryOperator("TaskIntegrationCode", trip.SysCode, BinaryOperatorType.Equal)
                    //      , new BinaryOperator("District.SysCode", this.Route.Description.District.SysCode)
                    );
                    var task = newObjectSpace.FindObject<Task>(findtTaskCriteria);
                    if (task == null)
                    {
                        throw new ArgumentException(string.Format("{0} = Siparişine Ait Görevler oluşturulmalıdır ", trip.SysCode));
                    }
                    tasks.Add(task);

                }

                foreach (var obj in tasks)
                {
                    var template = newObjectSpace.GetObject<TaskTemplate>(obj.Template);
                    var stateTaskVisible = newObjectSpace.GetObject<State>(template.StateTaskVisible);
                    var stateTaskStepVisible = newObjectSpace.GetObject<State>(template.StateTaskVisible);
                    setState(newObjectSpace, obj.TaskIntegrationObject, obj.TaskIntegrationCode, stateTaskStepVisible);
                    obj.State = stateTaskVisible;
                    foreach (var objLine in obj.TaskSteps)
                    {
                        var step = newObjectSpace.GetObject<TaskStep>(objLine);
                        setState(newObjectSpace, objLine.TaskStepIntegrationObject, objLine.TaskStepIntegrationCode, stateTaskStepVisible);
                        step.State = stateTaskStepVisible;
                        step.Assigned = newObjectSpace.GetObject<Person>(clientModel.Person);
                        var taskstepItems = getTaskStepItems(newObjectSpace, template, objLine, step);
                        //if (objLine.OrderLine != null)
                        //{
                        //    var requestItemForm = getItemRequestFormItems(newObjectSpace, objLine.OrderLine.Item);
                        //}
                        // taskstepItems.AddRange(requestItemForm);
                        step.JsonData = JsonConvert.SerializeObject(taskstepItems);
                    }
                    obj.JsonData = getTaskJsonData(obj);
                }
                newObjectSpace.CommitChanges();
                View.ObjectSpace.CommitChanges();
            } 
        );  
        }

        private void OrderItemMap_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            this.checkSelectedObjects();
            ApplicationDefination.PopupSettings(70, 50);
            var newObjectSpace = Application.CreateObjectSpace();
            List<ExTernalOrderItem> exTernalOrderItemList = new List<ExTernalOrderItem>();
            ExTernalDocument exTernalDocument = null;
           (this).ExecutePopupSimple((client, nonObjectSpace) =>
            {
                if (View.ObjectTypeInfo.FullName == typeof(ExTernalDocument).FullName)
                {
                    var selectedObjectKey = newObjectSpace.GetKeyValue(View.CurrentObject);
                    exTernalDocument = newObjectSpace.GetObjectByKey<ExTernalDocument>(selectedObjectKey);

                    foreach (var exorderTemp in exTernalDocument.Lines)
                    {
                        var exorderTempKey = newObjectSpace.GetKeyValue(exorderTemp);
                        var getOrder = newObjectSpace.GetObjectByKey<ExTernalOrderItem>(exorderTempKey);
                        exTernalOrderItemList.Add(getOrder);
                    }

                }
                else if (View.ObjectTypeInfo.FullName == typeof(ExTernalOrderItem).FullName)
                {
                    
                    foreach (var selectObjec in View.SelectedObjects)
                    {
                        var exorderTempKey = newObjectSpace.GetKeyValue(selectObjec);
                        var getOrder = newObjectSpace.GetObjectByKey<ExTernalOrderItem>(exorderTempKey);
                        exTernalOrderItemList.Add(getOrder);
                    }
                    exTernalDocument = exTernalOrderItemList.FirstOrDefault().Document;  
                }

            }, (Action<OrderItemNP>)((clientModel) =>
            { 
                ExOrderItemConvert(newObjectSpace, exTernalOrderItemList);
                newObjectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
                exTernalDocument = null;
            })
            ); 
        }

        //public OrderDailyPlaned GetOrderDailyPlaned(IObjectSpace newObjectSpace, DateTime requestDateday,String FileName , Owner owner)
        //{ 
        //    if (orderDailyPlanedStatic ==null )
        //    {
        //        orderDailyPlanedStatic = newObjectSpace.GetObjectByKey<OrderDailyPlaned>(FileName);
        //        if (orderDailyPlanedStatic == null)
        //        {
        //            orderDailyPlanedStatic = newObjectSpace.CreateObject<OrderDailyPlaned>();
        //            orderDailyPlanedStatic.SysCode = FileName;
        //            orderDailyPlanedStatic.PlanedStartTime = requestDateday;
        //            orderDailyPlanedStatic.IntegrationCode = FileName;
        //            newObjectSpace.CommitChanges();
        //        } 
        //    } 
        //    return orderDailyPlanedStatic;
        //}

        private void ExOrderItemConvert(IObjectSpace newObjectSpace ,List<ExTernalOrderItem> exTernalOrderItemList)
        {
            var externalDocuments = exTernalOrderItemList.GroupBy(x => new { x.Document.SysCode}).Select(group => new { groupOrder = group.Key, TotalItem = group.Count() });
            int errorCount = 0;
            foreach (var orderItem in exTernalOrderItemList)
            {//=BİRLEŞTİR("KFC",METNEÇEVİR(ŞİMDİ(),"yyyymmddhhmmss"),C4,I4,SATIR())
                
                OrderLine orderLine = null;
                Order order = null;
                orderLine = newObjectSpace.GetObjectByKey<OrderLine>(orderItem.Teslimat_Kod);
                var orderKey = orderItem.Document.Owner.SysCode + orderItem.Document.RequestPlanedDate.Value.ToString("yyyymmddhhmmss") + orderItem.Gonderen_Magaza_Kod + orderItem.Teslim_Magaza_Kod;
                order = newObjectSpace.GetObjectByKey<Order>(orderKey);
                if (order == null)
                { 
                    order = newObjectSpace.CreateObject<Order>();
                    order.SysCode = orderKey;
                    order.FromAddress = newObjectSpace.GetObjectByKey<Address>(orderItem.Gonderen_Magaza_Kod);
                    order.ToAddress = newObjectSpace.GetObjectByKey<Address>(orderItem.Teslim_Magaza_Kod);
                    order.Owner = newObjectSpace.GetObjectByKey<Owner>(orderItem.Document.Owner.SysCode);
                    order.RequestTime = orderItem.Document.RequestPlanedDate;
                    newObjectSpace.CommitChanges();
                }

                try
                {
                    if (orderLine == null)
                    {
                        orderLine = newObjectSpace.CreateObject<OrderLine>();
                        orderLine.SysCode = orderItem.Teslimat_Kod;
                        order.Items.Add(orderLine);
                        orderLine.Item = newObjectSpace.GetObjectByKey<Item>(orderItem.Urun_Kod);
                        orderLine.RequestQuantity = Convert.ToDouble(orderItem.Miktar);
                        orderLine.IntegrationCode = orderItem.Teslimat_Kod;
                        orderLine.Owner = newObjectSpace.GetObjectByKey<Owner>(orderItem.Document.Owner.SysCode);
                        order.Items.Add(orderLine);
                    }
                }
                catch (Exception e)
                {
                    orderItem.Message = e.Message;
                    orderItem.StateIntegration = newObjectSpace.GetObjectByKey<State>("Issue");
                    errorCount = errorCount + 1;
                }

            }

            //try
            //{
            //newOrderItem = newObjectSpace.GetObjectByKey<OrderItem>(keyvalue);
            //exOrderItem.StateIntegration = newObjectSpace.GetObjectByKey<State>("Created");
            //if (newOrderItem == null)
            //{
            //    newOrderItem = newObjectSpace.CreateObject<OrderItem>();
            //    newOrderItem.SysCode = exOrderItem.SysCode;
            //}

            //var fromAddress = newObjectSpace.GetObjectByKey<Address>(exOrderItem.Gonderen_Magaza_Kod);

            //if (fromAddress == null)
            //{
            //    errorCount = errorCount + 1;
            //    exOrderItem.Message = exOrderItem.Message + ", Not Found From Address";
            //    exOrderItem.StateIntegration = newObjectSpace.GetObjectByKey<State>("Issue");
            //}

            //if (fromAddress != null && fromAddress.ContactPhone != exOrderItem.Gonderen_Magaza_iletisim_tel)
            //{
            //    fromAddress.ContactPhone = exOrderItem.Gonderen_Magaza_iletisim_tel;
            //}
            //var toAddress = newObjectSpace.GetObjectByKey<Address>(exOrderItem.Teslim_Magaza_Kod);

            //if (toAddress == null)
            //{
            //    exOrderItem.Message = exOrderItem.Message + ", Not Found To Address";
            //    exOrderItem.StateIntegration = newObjectSpace.GetObjectByKey<State>("Issue");
            //    errorCount = errorCount + 1;
            //}
            //if (toAddress != null && toAddress.ContactPhone != exOrderItem.Teslim_Magaza_iletisim_tel)
            //{
            //    toAddress.ContactPhone = exOrderItem.Teslim_Magaza_iletisim_tel;
            //}

            //newOrderItem.FromAddress = fromAddress;
            //newOrderItem.ToAddress = toAddress;
            //newOrderItem.Item = newObjectSpace.GetObjectByKey<Item>(exOrderItem.Urun_Kod);
            //newOrderItem.Owner = newObjectSpace.GetObjectByKey<Owner>(exOrderItem.Document.Owner.SysCode);
            //newOrderItem.RequestDate = exTernalDocument.RequestPlanedDate;
            //newOrderItem.RequestQuantity = Convert.ToDouble(exOrderItem.Miktar);
            //newOrderItem.IntegrationCode = newOrderItem.SysCode;
            // orderDailyPlanedStatic.Lines.Add(newOrderItem);

            //}
            //catch (Exception e)
            //{
            //    exOrderItem.Message = e.Message;
            //    exOrderItem.StateIntegration = newObjectSpace.GetObjectByKey<State>("Issue");
            //    errorCount = errorCount + 1;
            //}


            //   }

            foreach (var exdoc in externalDocuments)
            {
                var exdocument = newObjectSpace.GetObjectByKey<ExTernalDocument>(exdoc.groupOrder.SysCode);
                exdocument.DocumentMapProcessCount = exdoc.TotalItem;
                exdocument.DocumentMapSuccessCount = exdoc.TotalItem - errorCount;
                //exdocument.DocumentMapErrorCount = errorCount;
            }

            //orderDailyPlanedStatic = null;
            ExTernalDocumentImportStatic = null;
        }

        private void OrderOneToOnePrint_Execute(object senders, SimpleActionExecuteEventArgs e)
        {
            var newObjectSpace = Application.CreateObjectSpace();
            var printer = newObjectSpace.GetObjectByKey<Device>("47656037288");
            ReportsModuleV2 reportsModule = ReportsModuleV2.FindReportsModule(Application.Modules);
            IObjectSpace reportObjectSpace = ReportDataProvider.ReportObjectSpaceProvider.CreateObjectSpace(typeof(ReportDataV2));
            IReportDataV2 reportData = reportObjectSpace.GetObject<ReportDataV2>(printer.Label);
            var report = ReportDataProvider.ReportsStorage.LoadReport(reportData);
            CriteriaOperator objectsCriteria = ((BaseObjectSpace)ObjectSpace).GetObjectsCriteria(((ObjectView)View).ObjectTypeInfo, View.SelectedObjects);
            SortProperty[] sortProperties = { new SortProperty("SysCode", SortingDirection.Descending) };

            reportsModule.ReportsDataSourceHelper.SetupBeforePrint(report, null, objectsCriteria, true, null, false);

          

            //if (reportsModule != null && reportsModule.ReportsDataSourceHelper != null)
            //{
            //    report.CreateDocument();
            //    PrintToolBase tool = new PrintToolBase(report.PrintingSystem);

            //    //tool.PrinterSettings.PrinterName = printer.Name;
            //    var curentRow=report.GetCurrentRow();
            //    var key = newObjectSpace.GetKeyValue(report.GetCurrentRow());
            //    var orderOne = newObjectSpace.GetObjectByKey<OrderItem>(key);
            //    orderOne.PrintCount = 1;
                 
            //    //+= (sender, args) => ListView_SelectionChanged(listView, dashBoardViewItem, modelDashboardItem);
            //    tool.PrintingSystem.StartPrint += (sender, args) => OrderOnePrintingSystem_StartPrint(sender,args,1,orderOne);
            ////    tool.PrinterSettings.Copies = short.Parse(orderOne.Miktar);
            //    tool.PrintingSystem.EndPrint += OrderOneSystem_EndPrint; ;
            //    //tool.PrintingSystem.ExportToPdf("test.pdf");
            //    tool.Print();
            //}

        }
        //private void OrderOnePrintingSystem_StartPrint(object sender,PrintDocumentEventArgs e, short count,OrderItem orderone)
        //{
             
        //    orderone.PrintCount = orderone.PrintCount + 1;
        //}
        private void OrderOneSystem_EndPrint(object sender, EventArgs e)
        {
           
              
        }

        private ExTernalDocument getExTernalDocumentImport(OrderImportNP orderImportNP, IObjectSpace objectSpace, int TotalRecord)
        {
            var dotStart = orderImportNP.File.FileName.IndexOf(".");
            var documentName = orderImportNP.File.FileName.Substring(0, dotStart).Replace(" ", "");
            var tempSysCode = orderImportNP.Owner.ShortName + documentName;
            ExTernalDocumentImportStatic = objectSpace.FindObject<ExTernalDocument>(CriteriaOperator.Parse("SysCode=?", documentName)); 
            if (ExTernalDocumentImportStatic == null || ExTernalDocumentImportStatic.DocumentName != documentName)
            { 
                if (ExTernalDocumentImportStatic == null)
                {
                    ExTernalDocumentImportStatic = objectSpace.CreateObject<ExTernalDocument>();
                    ExTernalDocumentImportStatic.SysCode = documentName;
                    ExTernalDocumentImportStatic.DocumentName = documentName;
                    ExTernalDocumentImportStatic.DocumentImportCount = TotalRecord;
                    ExTernalDocumentImportStatic.Owner = objectSpace.GetObject<Owner>(orderImportNP.Owner);
                    ExTernalDocumentImportStatic.RequestPlanedDate = orderImportNP.RequestPlanedDate;
                } 
            }
            return ExTernalDocumentImportStatic;
        }

        private void OrderItemImport_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var newObjectSpace = Application.CreateObjectSpace();
            ExTernalDocumentImportStatic = null;


            this.ExecutePopupSimple<OrderImportNP>((clientModel, npObjectSpace) =>
            {

            }, (ClientModel) =>
            {
                var ci = new System.Globalization.CultureInfo("tr-tr", false);
                var importOrder = new List<OrderImportNP>();
                var csvConfig = new CsvConfiguration(ci)
                {
                    // PrepareHeaderForMatch = (string header, int index) => header.ToLower(),
                    PrepareHeaderForMatch = args => args.Header.ToLowerInvariant(),
                    Delimiter = ClientModel.Delidelimeter,
                    HasHeaderRecord = false
                };
                OrderItemCSVes = null;
                var reader = new StreamReader(new MemoryStream(ClientModel.File.Content));
                var csv = new CsvReader(reader, csvConfig);
                OrderItemCSVes = new List<OrderItemCSV>();
                csv.Read();
                OrderItemCSVes.AddRange(csv.GetRecords<OrderItemCSV>());
                var a = ExTernalDocumentImportStatic;
                getExTernalDocumentImport(ClientModel, newObjectSpace, OrderItemCSVes.Count());
                int lineCount = 0;
                ExTernalDocumentImportStatic.DocumentImportCount = OrderItemCSVes.Count();
                foreach (var rc in OrderItemCSVes)
                {
                    lineCount = lineCount + 1;
                    ExTernalOrderItem newExTernalOrderItem = null;
                    if (rc.Teslimat_Kod == null||rc.Teslimat_Kod== "#N/A")
                    {
                        throw new ArgumentException(string.Format("Sipariş satırının {0}'da teslimat kodu boş veya geçersiz {1}", lineCount, rc.Teslimat_Kod));
                    }
                    newExTernalOrderItem = newObjectSpace.GetObjectByKey<ExTernalOrderItem>(rc.Teslimat_Kod);
                    if (newExTernalOrderItem == null)
                    {
                        newExTernalOrderItem = newObjectSpace.CreateObject<ExTernalOrderItem>();
                        newExTernalOrderItem.SysCode = rc.Teslimat_Kod;
                    }
                        newExTernalOrderItem.Bolge_Muduru = rc.Bolge_Muduru;  
                        newExTernalOrderItem.Gonderen_Magaza_iletisim_tel = rc.Gonderen_Magaza_iletisim_tel;
                        newExTernalOrderItem.Teslimat_Kod = rc.Teslimat_Kod;
                        newExTernalOrderItem.Tasima_Tipi = rc.Tasima_Tipi;
                        newExTernalOrderItem.Owner = newObjectSpace.GetObject<Owner>(ClientModel.Owner);
                        var teslimMagazaKodu = rc.Teslim_Magaza_Kod;
                        var gonderimMagazaKodu = rc.Gonderen_Magaza_Kod;
                        if (Int32.Parse(rc.Teslim_Magaza_Kod)< 10)
                        {
                            teslimMagazaKodu = "00" + rc.Teslim_Magaza_Kod;
                        }
                        else if(Int32.Parse(rc.Teslim_Magaza_Kod) >= 10 && Int32.Parse(rc.Teslim_Magaza_Kod) < 100)
                        {
                            teslimMagazaKodu = "0" + rc.Teslim_Magaza_Kod;
                        }

                        if (Int32.Parse(rc.Gonderen_Magaza_Kod) < 10)
                        {
                            gonderimMagazaKodu = "00" + rc.Gonderen_Magaza_Kod;
                        }
                        else if (Int32.Parse(rc.Gonderen_Magaza_Kod) >= 10 && Int32.Parse(rc.Gonderen_Magaza_Kod) < 100)
                        {
                            gonderimMagazaKodu = "0" + rc.Gonderen_Magaza_Kod;
                        }
                            newExTernalOrderItem.Teslim_Magaza_Kod = teslimMagazaKodu;
                        newExTernalOrderItem.Gonderen_Magaza_Adi = rc.Gonderen_Magaza_Adi;
                        newExTernalOrderItem.Gonderen_Magaza_Kod = gonderimMagazaKodu;
                        newExTernalOrderItem.Teslim_Magaza_Adi = rc.Teslim_Magaza_Adi;
                        newExTernalOrderItem.Teslim_Magaza_iletisim_tel = rc.Teslim_Magaza_iletisim_tel;
                        newExTernalOrderItem.Urun_Adi = rc.Urun_Adi;
                        newExTernalOrderItem.Urun_Kod = rc.Urun_Kod;
                        newExTernalOrderItem.Miktar = rc.Miktar;
                        newExTernalOrderItem.Adet_GR = rc.Adet_GR; 
                        ExTernalDocumentImportStatic.Lines.Add(newExTernalOrderItem); 
                 
                }

                ExTernalDocumentImportStatic.DocumentImportCount = ExTernalDocumentImportStatic.Lines.Count();
                newObjectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
                //ExOrderItemConvert(newObjectSpace, ExTernalDocumentImportStatic);
                //newObjectSpace.CommitChanges();
                //ExTernalDocumentImportStatic = null;
               // View.ObjectSpace.Refresh();
            } 
            );
           
          
           
        }
    }
}

public class ImportOrder
{
    public string musteri_siparis_kod { get; set; }
    public string irsaliye_tarihi   {get;set;}
    public string cikis_tarihi { get; set; }
    public string teslim_adress_kod { get; set; }
    public string teslim_adres_name { get; set; }
    public string teslim_sehir { get; set; }
    public string teslim_ilce { get; set; }
    public string teslim_adres { get; set; }
    public string malzeme_kod { get; set; }
    public string malzeme_ad { get; set; }
    public string istenen_adet { get; set; } 
    public string cikis_birim_tip { get; set; }
    public string agirlik { get; set; }  

}