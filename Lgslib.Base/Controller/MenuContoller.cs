using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.Model;
using System.Drawing;
using DevExpress.ExpressApp.DC;

namespace LgsLib.Base
{
    public partial class MenuContoller : ViewController
    {
        private const string EmptyCriteria = "EmptyCriteria";
        public SingleChoiceAction MenuAction { get; set; }
        public static BoolList BranchActiveActionChoiseItems { get; set; }
        public static ListView CurrentLitView { get; set; }
        public IModelMenuSettings modelMenu;

        public MenuContoller()
        {
            Initialize();
            CreateMenu();
        }

        protected override void OnFrameAssigned()
        {
            base.OnFrameAssigned();
            
        }
        protected override void OnActivated()
        {  
            base.OnActivated();
             RuleBase.CustomFormatValidationMessage += RuleBase_CustomFormatValidationMessage;
            CreateMenu();
            if(View!=null)
            {
                View.SelectionChanged += View_SelectionChanged;
                if (View is ListView)
                {
                    CurrentLitView = View as ListView;
                }
            }    
     
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            RuleBase.CustomFormatValidationMessage -= RuleBase_CustomFormatValidationMessage;
            // View.CurrentObjectChanged -= View_CurrentObjectChanged;
        }
        private void MenuAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            ActionExecuted(sender, e);
        }

        #region Menu
        private void CreateMenu()
        {
            if (View != null)
            { 
                MenuAction.Items.Clear();
                var objView = View as ObjectView;
                if (objView == null)
                    return;
                var actionDesign = objView.Model.Parent.Parent.Application.ActionDesign as IModelActionDesignLGS;
        
                foreach (var daction in actionDesign.DisableActions)
                {
                    if (daction.Action != null)
                    {
                        var action = GetActions().SingleOrDefault(x => x.Model.Id == daction.Action.Id);
                        if (action != null)
                        {
                            action.Active["ActionActivePassive"] = false;
                        }
                       
                    }
                }
                
                if (objView != null){
                    modelMenu = objView.Model as IModelMenuSettings;
                    if (modelMenu != null && modelMenu.MenuSettings.Count > 0){
                        foreach (var item in modelMenu.MenuSettings){
                            if (item.Name != null && !item.Isgroup)
                            {
                                if (item.CallAction != null)
                                {
                                    try
                                    {
                                        var action = GetActions().SingleOrDefault(x => x.Model.Id == item.CallAction.Id);
                                        action.Active["ActionActivePassive"] = true;
                                        var modelAction = action.Model as IModelAction;
                                        modelAction.Caption = item.Caption;
                                        modelAction.ImageName = item.Image;
                                    }
                                    catch (Exception)
                                    {
                                         
                                    }
                             
                                }
                            }
                            else
                            { 
                                GroupMenu(item);
                            }
                        }
                    }
                }
            }
        }

        private void RuleBase_CustomFormatValidationMessage(object sender, CustomFormatValidationMessageEventArgs e)
        {
            //RuleCriteria rule = sender as RuleCriteria;
            //if ((rule != null) && (rule.Id.StartsWith("StateMachine")))
            //{
            //    try
            //    {
            //        ITypeInfo typeinfo = XafTypesInfo.Instance.FindTypeInfo(e.Object.GetType());
            //        DisplayTextCriteriaProcessor processor = new DisplayTextCriteriaProcessor(typeinfo);
            //        processor.Process(rule.CriteriaOperator);
            //        var criteria = rule.CriteriaOperator.ToString();
            //        criteria = criteria.Replace("Is Not Null", "Boş Olamaz");
            //        criteria = criteria.Replace("And", "Ve");
            //        rule.Properties.Criteria = criteria;
            //        e.ResultMessage = ObjectFormatter.Format(e.MessageFormat, rule);
            //        e.Handled = true;
            //    }
            //    catch (Exception)
            //    {
                     
            //    }
                
            //}
        }
        private void GroupMenu(IModelMenuItem item)
        {
            var checkMenuItem = GetChoiseItem(item, EnumChoiceItemType.Item);
            var activeList = checkMenuItem.Enabled;
            bool? isPasif = false;
            activeList["Key"] = true;
            if (item.PassiveCriteria != null)
            {
                isPasif = View.ObjectSpace.IsObjectFitForCriteria(View.CurrentObject, CriteriaOperator.Parse(item.PassiveCriteria));
            }
            if (isPasif == true)
            {
                activeList["Key1"] = false;
                isPasif = false;
            }
            else
            {
                activeList["Key1"] = true;
                isPasif = true;
            }
            if (item.ParentMenu == null && item.Isgroup)
            {
                MenuAction.Items.Add(checkMenuItem);
            }
            var checkParentMenu = GetChoiseItem(item, EnumChoiceItemType.Parent);
            if (checkParentMenu != null)
            {
                checkParentMenu.Items.Add(checkMenuItem);
            }
            else if (item.ParentMenu == null && item.Isgroup)
            {
                MenuAction.Items.Add(checkMenuItem);
            }
        }

        private ChoiceActionItem GetChoiseItem(IModelMenuItem item, EnumChoiceItemType type)
        {
            ChoiceActionItem menuItem = null;
            var newMenuItem = new ChoiceActionItem();
            newMenuItem.ImageName = item.Image;
            newMenuItem.Id = item.Name;
            newMenuItem.Caption = item.Caption;

            var objSpace = Application.CreateObjectSpace();
            var curentObject = View.CurrentObject;
         
          if (item.TargetView != null){
                newMenuItem.Data = item.TargetView.ModelClass.TypeInfo.Type;
            }
            if (type == EnumChoiceItemType.Parent && item.ParentMenu != null) {
                menuItem = MenuAction.Items.FindItemByID(item.ParentMenu.Name);
                if (menuItem == null && MenuAction.Items.Count > 0){
                    for (int i = 0; i < MenuAction.Items.Count; i++)
                    {
                        foreach (var row in MenuAction.Items[i].Items)
                        {
                            menuItem = MenuAction.Items[i].Items.FindItemByID(item.ParentMenu.Name);
                            return menuItem;
                        }
                    }

                }

            }
            else if (type == EnumChoiceItemType.Item)
            {
                menuItem = MenuAction.Items.FindItemByID(item.Name);
                if (menuItem == null){
                    return newMenuItem;
                }
            }
            return menuItem;
        }
        #endregion Menu    
        #region ExecutedMethdods
        protected virtual void ActionExecuted(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            var modelMenuMapItem = modelMenu.MenuSettings.FirstOrDefault(x => x.Name == e.SelectedChoiceActionItem.Id) as IModelMenuRelationView;
            if (modelMenuMapItem != null && modelMenuMapItem.CallAction != null)
            {
                var axecAction = GetActions().SingleOrDefault(x => x.Model.Id == modelMenuMapItem.CallAction.Id);
                if (axecAction != null)
                {
                    // var simpleAction = axecAction as SimpleObjectSpaceAction;
                    var simpleAction = axecAction as SimpleAction;
                    if (simpleAction != null)
                    {
                        simpleAction.Active["ActionActivePassive"] = true;
                        simpleAction.DoExecute();
                        simpleAction.Active["ActionActivePassive"] = false;
                    }
                }
            }

        }
      
        public IEnumerable<ActionBase> GetActions()
        {
            var controllers = Frame.Controllers.Values as System.Collections.Generic.List<DevExpress.ExpressApp.Controller>;
            return controllers.SelectMany(x => x.Actions.AsEnumerable());
        }
        private void Initialize()
        {           
            var singleChoiseMenu = new SingleChoiceAction(this, "MenuAction", PredefinedCategory.Unspecified);
            singleChoiseMenu.Caption = "Menu";
            singleChoiseMenu.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.MenuAction_Execute);
            singleChoiseMenu.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            MenuAction = singleChoiseMenu;
        }
        void View_SelectionChanged(object sender, EventArgs e)
        {
            CreateMenu();
        }
     
      #endregion GlobalCriteria
        public enum EnumChoiceItemType
        {
            Parent = 1, Item = 2
        }
 
    }
}
