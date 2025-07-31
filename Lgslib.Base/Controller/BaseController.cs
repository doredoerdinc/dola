using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using DevExpress.ExpressApp.Filtering;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.DC;
using LgsLib.Base.PermissionPolicy;

namespace LgsLib.Base
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class BaseController : ViewController
    {

        public BaseController()
        {
            InitializeComponent();
            PermissionObjectInsert.TargetObjectType=typeof(TypePermissionObject);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            FilterListViewController_Activated();


            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
            
         
        } 
        protected override void OnFrameAssigned()
        { 
            base.OnFrameAssigned();
        }
        private void FilterListViewController_Activated()
        {
          
        }

        private void PermissionObjectInsert_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var newObjectSpace = Application.CreateObjectSpace();
            var role = newObjectSpace.GetObject<Role>(View.CurrentObject as Role);
            
            foreach (var type in ObjectSpace.TypesInfo.PersistentTypes)
            {
                var newObject = newObjectSpace.CreateObject<TypePermissionObject>();
                newObject.TargetTypeFullName = type.FullName;
                newObject.WriteState = SecurityPermissionState.Allow;
                newObject.ReadState = SecurityPermissionState.Allow;
                newObject.DeleteState = SecurityPermissionState.Allow;
                newObject.CreateState = SecurityPermissionState.Allow;
                newObject.Role =role;
            }
            newObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();

        }

      
    }
    public enum AutoCommitModes
    {
        None = 0,
        Commit = 1,
        CommitAndRefresh = 2
    }
    public static class ControllerExtend
    { 
        public static void ShowViewParamters(this ViewController controller, View popupView, EventHandler<DialogControllerAcceptingEventArgs> acceptHandler)
        {

            ShowViewParameters svp = new ShowViewParameters();
            svp.CreatedView = popupView;
            svp.Context = TemplateContext.PopupWindow;
            svp.TargetWindow = TargetWindow.NewWindow;
            DialogController dialog = controller.Application.CreateController<DialogController>(); 
            dialog.Cancelling += delegate (object sender, EventArgs e)
            {
                var dc = sender as DialogController;
                if (dc.Frame.View != null)
                {
                    dc.Frame.View.ObjectSpace.Committing += delegate (object sender1, System.ComponentModel.CancelEventArgs e1)
                    {
                        e1.Cancel = true;
                    };
                }
            };
            dialog.Accepting += acceptHandler;
            svp.Controllers.Add(dialog);
            svp.CreateAllControllers = true;
            controller.Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(null, null));
        }
        public static void ExecutePopupSimple<TModel>(this ViewController controller, Action<TModel, IObjectSpace> modifyPopupObjectAction, Action<TModel> executePopupObjectInParentAction, Action<TModel, IObjectSpace> executePopupObjectAction = null)
        {
            var newObjectSapce = controller.Application.CreateObjectSpace(typeof(TModel));
            var newObject = newObjectSapce.CreateObject<TModel>();
            var newView = controller.Application.CreateDetailView(newObjectSapce, newObject);
            if (modifyPopupObjectAction != null)
            {
                modifyPopupObjectAction(newObject, newObjectSapce);
            }
            ShowViewParamters(controller, newView, (sender, e) =>
            {
                try
                {
                    if (executePopupObjectAction != null)
                    { 
                        executePopupObjectAction(newObject, newObjectSapce); 
                    }
                    else if (executePopupObjectInParentAction != null)
                    {
                        executePopupObjectInParentAction(newObject);
                    }
                }
                catch (Exception ex)
                {
                    controller.View.ObjectSpace.Rollback();

                    throw ex;
                }
            });
        }
        public static void ExecutePopupSimpleDynamicList<TModel>(
            this ViewController controller,
            String criteria,
            Action<ListView, IObjectSpace> executePopupObjectAction,
            Action<ListView> executePopupObjectInParentAction)
            where TModel : class
        {
            IObjectSpace newObjectSapce = controller.Application.CreateObjectSpace(typeof(TModel));
            var objectSpace = controller.Application.CreateObjectSpace(typeof(TModel));

            // ListView ID'si alınır (Modelden)
            var listViewId = controller.Application.FindListViewId(typeof(TModel));

            var collectionSource = controller.Application.CreateCollectionSource(
                objectSpace, typeof(TModel), listViewId);

            // Kriter uygulanır
            if (criteria != null)
            {
                collectionSource.Criteria["DynamicFilter"] = CriteriaOperator.Parse(criteria);
            }

            var listView = controller.Application.CreateListView(listViewId, collectionSource, true);


            ShowViewParamters(controller, listView, (sender, e) =>
            {
                try
                {

                    if (executePopupObjectAction != null)
                    {
                        //   Validator.RuleSet.Validate(newObjectSapce, newObject, "Save,Accept");
                        executePopupObjectAction(listView, newObjectSapce);

                    } 
                }
                catch (Exception ex)
                {
                    controller.View.ObjectSpace.Rollback();

                    throw ex;
                }
            });
        }
        public static void ExecutePopupSimpleList<TModel>(this ViewController controller, Action<ListView, IObjectSpace> modifyPopupObjectAction, Action<ListView> executePopupObjectInParentAction, Action<ListView, IObjectSpace> executePopupObjectAction = null)
        {
            var newObjectSapce = controller.Application.CreateObjectSpace(typeof(TModel));
            var newObject = newObjectSapce.CreateObject<TModel>();
            var listView = controller.Application.CreateListView(newObjectSapce, typeof(TModel), true);
            if (modifyPopupObjectAction != null)
            {
                modifyPopupObjectAction(listView, newObjectSapce);
            }
            ShowViewParamters(controller, listView, (sender, e) =>
            {
                try
                {

                    if (executePopupObjectAction != null)
                    {
                        //   Validator.RuleSet.Validate(newObjectSapce, newObject, "Save,Accept");
                        executePopupObjectAction(listView, newObjectSapce);

                    }
                    else if (executePopupObjectInParentAction != null)
                    {
                        executePopupObjectInParentAction(listView);
                    }
                }
                catch (Exception ex)
                {
                    controller.View.ObjectSpace.Rollback();

                    throw ex;
                }
            }); 
        }
        public static void ShowMessageParamters(this ViewController controller, View popupView)
        {
            ShowViewParameters svp = new ShowViewParameters();
             svp.TargetWindow = TargetWindow.NewWindow;
            svp.Context = TemplateContext.PopupWindow;
            svp.CreatedView = popupView;
            svp.Context = TemplateContext.PopupWindow;
            DialogController dialog = controller.Application.CreateController<DialogController>();
            //dialog.AcceptAction.ActionMeaning = ActionMeaning.Unknown; 
            //svp.Controllers.Add(dialog);
            //svp.CreateAllControllers = true;
            controller.Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(null, null));
        }  
        public static void checkSelectedObjects(this ViewController controller)
        {
            if (controller.View.SelectedObjects.Count<= 0)
            {
                SimpleMessage(controller, "Lütfen En Az bir kayıt seçiniz");
                return;
            }
        }

        public static void SimpleMessage(this ViewController controller, String message)
        {
            if(controller==null)
            {
                controller = new ViewController();
            }
            var newObjectSapce = controller.Application.CreateObjectSpace(typeof(ClientMessage));
            var newObject = newObjectSapce.CreateObject<ClientMessage>();
            newObject.Message = message;
            var view = controller.Application.CreateDetailView(newObjectSapce, newObject);
            ShowMessageParamters(controller, view);
           // newObjectSapce.Refresh();
        } 
    }
    [DomainComponent]
    public class ClientMessage :NonePercentObject
    {
        public ClientMessage() { }

        String _Message;
        public String Message
        {
            get { return _Message; }
            set { _Message = value; }
        } 

    }
}
 