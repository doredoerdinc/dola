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
    public interface IModelGetListViewItems : IModelNode
    {
        #region SourceView
        [Category(ModelCategory.ViewList)]
        [DataSourceProperty("GetSourceListView")]
        IModelListView SourceListView { get; set; }
        #endregion SourceView
    }

    [DomainLogic(typeof(IModelGetListViewItems))]
    public static class IModelGetListViewItemsLogic
    {

        public static IModelListView GetSourceListView(this IModelGetListViewItems item)
        {
            IModelListView mlistView=null;
            if(item.Parent.Parent.Parent is IModelListView)
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

    }    
}
