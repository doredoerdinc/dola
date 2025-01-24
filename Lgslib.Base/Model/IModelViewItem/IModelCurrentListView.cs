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
    [KeyProperty("Name")]
    public interface IModelCurrentListView : IModelNode
    {
        [Category(ModelCategory.ColumnList)]
        string Name { get; set; }
        //[Browsable(false)]
        IModelList<IModelColumn> SourceColumns { get; }
     
       // [ModelBrowsable(typeof(DashboardViewItemRelationVisibility))]
        [Category(ModelCategory.ColumnList)]
        [DataSourceProperty("SourceColumns")]
        IModelColumn SourceColumn { get; set; }       
    }

    [DomainLogic(typeof(IModelCurrentListView))]
    public static class IModelCurrentListViewLogic
    {
        public static IModelListView Get_CurrentImodelListView(this IModelCurrentListView item)
        {
            IModelListView mlistView=null;
         
            if (item.Parent.Parent is IModelListView)
            {
                mlistView = item.Parent.Parent as IModelListView;
            }

            if(item.Parent.Parent.Parent.Parent is IModelListView)
            {
                mlistView = item.Parent.Parent.Parent as IModelListView;
            }

            if (item.Parent.Parent is IModelDashboardViewItem)
            {
                var modelDashBoardViewItem = item.Parent.Parent as IModelDashboardViewItem;
                mlistView = modelDashBoardViewItem.View as IModelListView;
            }


            return mlistView;
        }

        public static IModelList<IModelColumn> Get_SourceColumns(this IModelCurrentListView item)
        {
            var curentView = item.Get_CurrentImodelListView();
            var calculatedModelNodeList = new CalculatedModelNodeList<IModelColumn>();
            if (curentView != null)
            {
                calculatedModelNodeList.AddRange(curentView.Columns);
            }
            return calculatedModelNodeList;
        }

    }    
}
