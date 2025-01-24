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
    public interface IModelTargetViewItem : IModelNode
    {
        string Name { get; set; }
        [Browsable(false)]
        IModelList<IModelColumn> TargetSourceColumns { get; }

        [Category(ModelCategory.ColumnList)]
        [DataSourceProperty("TargetSourceColumns")]
        IModelColumn TargetColumn { get; set; }

    }
    [DomainLogic(typeof(IModelTargetViewItem))]
    public static class IModelTargetViewItemLogic
    {

        public static IModelListView GetTargetImodelListView(this IModelTargetViewItem item)
        {
            var curentViewItem = item.Parent.Parent.Parent as IModelListView;
            return curentViewItem;
        }

        public static IModelList<IModelColumn> Get_TargetSourceColumns(this IModelTargetViewItem item)
        {
            var curentView = item.GetTargetImodelListView();
            var calculatedModelNodeList = new CalculatedModelNodeList<IModelColumn>();
            calculatedModelNodeList.AddRange(curentView.Columns);
            return calculatedModelNodeList;
        }

        
    }    
}
