using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;

using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using System.Collections;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;

 

namespace LgsLib.Base
{

    [DomainComponent]
    public interface IModelDashboardViewItemRelation : IModelDashboardViewItem
    {
        // IModelViewItemRelation ItemRelation { get; }
        bool IsFirtLoad { get; set; }
        IModelList<IModelDashBoardRelationView> RelationListViews { get; } 
    }

    [KeyProperty("Name")]
    [DomainComponent]
    public interface IModelDashBoardRelationView : IModelNode
    {
        
        string WherePropertyName { get; set; }

        string WherePropertyNameValue { get; set; }

        [DataSourceProperty("DashboardViewItems")]
        IModelDashboardViewItem DashboardViewItem { get; set; }

        [Browsable(false)]
        IModelList<IModelDashboardViewItem> DashboardViewItems { get; }
        
    }

    [DomainLogic(typeof(IModelDashBoardRelationView))]
    public static class IModelDashBoardTargetRelationViewLogic
    {  
        public static IModelList<IModelDashboardViewItem> Get_DashboardViewItems(this IModelDashBoardRelationView dhbViews)
        {
            var dashBoardItem = dhbViews.Parent.Parent.Parent.Parent as IModelDashboardView;
            var calculatedModelNodeList = new CalculatedModelNodeList<IModelDashboardViewItem>();
            foreach (var dhbViewItem in dashBoardItem.Items)
            {
                calculatedModelNodeList.Add((IModelDashboardViewItem)dhbViewItem);
            }

            return calculatedModelNodeList;
        }       

    }
 
}
