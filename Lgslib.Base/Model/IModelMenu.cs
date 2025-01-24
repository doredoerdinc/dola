using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.Model;
using DevExpress.Data;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.ExpressApp.Filtering;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.Data.Filtering;

using System.Drawing.Design;


using DevExpress.ExpressApp.Editors;
using System.Drawing;
using DevExpress.ExpressApp;



namespace LgsLib.Base
{
    public interface IModelActionDesignLGS:IModelActionDesign
    {
        IModelDisableActions DisableActions { get; }
    } 
   
    [DomainComponent]
    public interface IModelDisableAction : IModelNode
    { 
        [Category("Action")]
        [DataSourceProperty("Application.ActionDesign.Actions")]
        IModelAction Action { get; set; }

    }

    public interface IModelDisableActions : IModelNode, IModelList<IModelDisableAction>
    {

    }

    [KeyProperty("Name")]
    [DomainComponent]
    public interface IModelMenuItem : IModelMenuRelationView
    {
        [Required]
        [Category("MenuOptions")]
        string Name { get; set; }


        [Required]
        [Category("MenuOptions")]
        Boolean Isgroup { get; set; }



        [Required]
        [Category("MenuOptions")]
        [Editor("DevExpress.ExpressApp.Win.Core.ModelEditor.ImageGalleryModelEditorControl, DevExpress.ExpressApp.Win" + XafAssemblyInfo.VersionSuffix + XafAssemblyInfo.AssemblyNamePostfix, typeof(System.Drawing.Design.UITypeEditor))]
         string Image { get; set; }

        [Required]
        [Category("MenuOptions")]
        string Caption { get; set; }

        [Category("MenuOptions")]
        [DataSourceProperty("SourceMenu")]
        IModelMenuItem ParentMenu { get; set; }
        [Browsable(false)]
        IModelList<IModelMenuItem> SourceMenu { get; set; }

        [Category("MenuOptions")]
        [CriteriaOptions("SourceView.ModelClass.TypeInfo")]
        [Editor("DevExpress.ExpressApp.Win.Core.ModelEditor.CriteriaModelEditorControl, DevExpress.ExpressApp.Win" + XafAssemblyInfo.VersionSuffix + XafAssemblyInfo.AssemblyNamePostfix, typeof(System.Drawing.Design.UITypeEditor))]
        string PassiveCriteria { get; set; }

    }

    [DomainComponent]
    public interface IModelMenuRelationView : IModelNode
    {
        [Category("Action")]
        [DataSourceProperty("Application.ActionDesign.Actions")]
        IModelAction CallAction { get; set; }

        [Category("Source")]
     //   [PopupEditorType("SourceView.ModelClass.TypeInfo")]
        [Editor("Mapkup.Base.Win.Model.IModelPropertyPopupEditor, Mapkup.Base.Win, Version=16.1.7.1, Culture=neutral", typeof(UITypeEditor))]
        string SourcePropertyName { get; set; }

        [Category("Source")]
        [DataSourceProperty("Application.Views")]
        [Browsable(false)]
        IModelObjectView SourceView { get; set; }

        [Category("Target")]
       // [PopupEditorType("TargetView.ModelClass.TypeInfo")]
        [Editor("Mapkup.Base.Win.Model.IModelPropertyPopupEditor, Version=16.1.7.1, Mapkup.Base.Win, Culture=neutral", typeof(UITypeEditor))]
        string TargetPropertyName { get; set; }

        [Category("Target")]
        [DataSourceProperty("Application.Views")]
        IModelObjectView TargetView { get; set; }

        [Category("Target")]
        [CriteriaOptions("TargetView.ModelClass.TypeInfo")]
        [Editor("DevExpress.ExpressApp.Win.Core.ModelEditor.CriteriaModelEditorControl, DevExpress.ExpressApp.Win" + XafAssemblyInfo.VersionSuffix + XafAssemblyInfo.AssemblyNamePostfix, typeof(System.Drawing.Design.UITypeEditor))]
        string Criteria { get; set; }
    }
       
    public interface IModelMenuItems : IModelNode, IModelList<IModelMenuItem>
    {
      
    }

    
    public interface IModelMenuSettings : IModelNode
    {
        IModelMenuItems MenuSettings  { get; }
    }
   
    [DomainLogic(typeof(IModelMenuItem))]
    public static class IModelMenuItemLogic
    {
        public static IModelList<IModelMenuItem> Get_SourceMenu(IModelMenuItem model)
        {
            var objectView = model.Parent.Parent as IModelObjectView;
            var menu = objectView as IModelMenuSettings;
            var calculatedModelNodeList = new CalculatedModelNodeList<IModelMenuItem>();
            calculatedModelNodeList.AddRange(menu.MenuSettings);
            return calculatedModelNodeList;
        } 
    }
  
    [DomainLogic(typeof(IModelMenuRelationView))]
    public static class IModelMenuReletionViewLogic
    {
        public static IModelObjectView Get_SourceView(IModelMenuRelationView model)
        {
             return model.Parent.Parent as IModelObjectView;
        }
    }

    public enum PointType { Point = 0, Route = 1, Polygon = 2 }

    


}

