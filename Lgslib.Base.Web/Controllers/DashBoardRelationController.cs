using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Drawing;
using System.Data.Entity;

using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Filtering;
using DevExpress.ExpressApp.Model;

using DevExpress.ExpressApp.Reports;
using DevExpress.ExpressApp.Actions;
using LgsLib.Base;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.ExpressApp.PivotGrid.Web;
using DevExpress.Web;
using DevExpress.ExpressApp.Web.Utils;
using DevExpress.ExpressApp.Web.Templates;
using System.Globalization;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.EF;
using System.Data.Entity.Core.Common;

namespace LgsLib.Module.Web
{
    public partial class DashBoardRelationController : ViewController<DashboardView>
    {
        private const string EmptyCriteria = "EmptyCriteria";

        private bool IsFirstLoad { get; set; }


        protected override void OnActivated()
        {
            base.OnActivated();
            IsFirstLoad = true;
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            ActiveItem(true);
            IsFirstLoad = false;


        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            IsFirstLoad = true;
            ActiveItem(false);
        }

        protected override void OnFrameAssigned()
        {
            base.OnFrameAssigned();

        }

        ListView SetViewCriteria(ListView filterListView, ListView sourceListView, IModelDashBoardRelationView relationViewItem)
        {
            var filterListViewDatasource = filterListView.CollectionSource;
            CriteriaOperator criteria;
            if (sourceListView.SelectedObjects.Count > 0)
            {
                criteria = (CriteriaSelectionOperator(sourceListView, relationViewItem));
            }
            else
            {
                criteria = CollectionSource.EmptyCollectionCriteria;

            }

            filterListViewDatasource.Criteria[EmptyCriteria] = criteria;
            return filterListView;

        }

        CriteriaOperator CriteriaSelectionOperator(ListView listView, IModelDashBoardRelationView relationView)
        {
            List<object> objects = new List<object>();
            var selectionCriteria = listView.Editor as ISelectionCriteria;
            var ciriteriaInOperator = new InOperator();

            foreach (var slcObject in listView.SelectedObjects)
            {
                var modelListView = listView.Model;
                if (modelListView.DataAccessMode == CollectionSourceDataAccessMode.DataView)
                {
                    var dataViewRecord = slcObject as EFDataViewRecord;
                    int propertyIndex = -1;
                    for (int i = 0; i < dataViewRecord.GetProperties().Count; i++)
                    {
                        if (dataViewRecord.GetProperties()[i].Name == relationView.WherePropertyNameValue)
                        {
                            propertyIndex = i;
                        }
                    }
                    if (propertyIndex > -1)
                    {
                        objects.Add(dataViewRecord[propertyIndex]);
                    }


                }
                else
                {
                    objects.Add(slcObject.GetType().GetProperty(relationView.WherePropertyNameValue).GetValue(slcObject, null));
                }

            }

            ciriteriaInOperator = new InOperator(relationView.WherePropertyName, objects);
            return ciriteriaInOperator;


            //return selectionCriteria != null ? CriteriaOperator.Parse(listView.ObjectTypeInfo.KeyMember.Name) : ciriteriaInOperator;
        }

        IEnumerable<object> Getkeys(ListView listView)
        {
            var returnKey = listView.SelectedObjects.OfType<object>().Select(o => ObjectSpace.GetKeyValue(o));
            return returnKey;
        }
        private void ActiveItem(bool IsActive)
        {
            if (IsFirstLoad)
            {
                foreach (var dashBoardViewItem in View.Items.OfType<DashboardViewItem>().OrderBy(x => x.Id))
                {
                    var innnerListView = dashBoardViewItem.InnerView as ListView;
                    var listView = dashBoardViewItem.InnerView as ListView; 
                    var modelDashboardItem = dashBoardViewItem.Model as IModelDashboardViewItemRelation; 
                    if (modelDashboardItem != null && modelDashboardItem.RelationListViews.Count > 0)
                    { 
                        if (listView == null) return; 
                        if (modelDashboardItem != null && modelDashboardItem.RelationListViews.Count > 0)
                        {
                            if (listView.Editor is ASPxGridListEditor && IsActive)
                            {
                                listView.SelectionChanged += (sender, args) => ListView_SelectionChanged(listView, dashBoardViewItem, modelDashboardItem);
                                WebWindow.CurrentRequestWindow.PagePreRender += (sender, erg) => CurrentRequestWindow_PagePreRender(sender, erg, dashBoardViewItem.Id); 
                            }
                            else
                            {
                                try
                                {
                                    listView.SelectionChanged -= (sender, args) => ListView_SelectionChanged(listView, dashBoardViewItem, modelDashboardItem);
                                    try
                                    {
                                        WebWindow.CurrentRequestWindow.PagePreRender -= (sender, erg) => CurrentRequestWindow_PagePreRender(sender, erg, dashBoardViewItem.Id);
                                    }
                                    catch (Exception)
                                    { 
                                    }
                                   
                                }
                                catch (Exception)
                                { 
                                }
                          
                            }

                        } 
                    }
                    if (modelDashboardItem!=null&&!modelDashboardItem.IsFirtLoad&& listView!=null)
                    {
                        listView.CollectionSource.Criteria[EmptyCriteria] = CollectionSource.EmptyCollectionCriteria;
                    }
                }
            }
        } 

        private void ListView_SelectionChanged(ListView sourceListView, DashboardViewItem dashBoardViewItem, IModelDashboardViewItemRelation modelRelation)
        {
            foreach (var relationView in modelRelation.RelationListViews)
            {
                if (relationView.DashboardViewItem != null)
                {
                    var filterDashboarItem = (DashboardViewItem)View.FindItem(relationView.DashboardViewItem.Id);

                    if (filterDashboarItem != null && filterDashboarItem.Frame != null)
                    {

                        var criteriaListView = SetViewCriteria(filterDashboarItem.Frame.View as ListView, sourceListView, relationView); 
                    }
                }


            }
        }

        private void CurrentRequestWindow_PagePreRender(object sender, EventArgs e, String dashboardId)
        {
            try
            {
                if (View == null) return;
                DashboardViewItem sourceItem = (DashboardViewItem)View.FindItem(dashboardId);
                if (sourceItem == null) return;
                if (sourceItem.InnerView == null) return;
                ListView listView = (ListView)sourceItem.InnerView;
                ASPxGridListEditor editor = (ASPxGridListEditor)listView.Editor;
                if (editor == null) return;
                ICallbackManagerHolder holder = (ICallbackManagerHolder)WebWindow.CurrentRequestPage;
                string script = holder.CallbackManager.GetScript();
                script = string.Format(CultureInfo.InvariantCulture, @"
function(s, e) {{
    if(e.isChangedOnServer){{
        {0}
    }}else{{
        var xafCallback = function() {{
            s.EndCallback.RemoveHandler(xafCallback);
            {0}
        }};
        s.EndCallback.AddHandler(xafCallback);
    }}
}}
                ", script);
                ClientSideEventsHelper.AssignClientHandlerSafe(editor.Grid, "SelectionChanged", script, "DashBoardRelationController");

            }
            catch (Exception)
            {
                 
            }
    
        }

        void SelectionChanged(ListView sourceListView, DashboardViewItem dashBoardViewItem, IModelDashboardViewItemRelation modelRelation)
        {
            foreach (var relationView in modelRelation.RelationListViews)
            {
                if (relationView.DashboardViewItem != null)
                {
                    var filterDashboarItem = (DashboardViewItem)View.FindItem(relationView.DashboardViewItem.Id);

                    if (filterDashboarItem != null && filterDashboarItem.Frame != null)
                    {

                        var criteriaListView = SetViewCriteria(filterDashboarItem.Frame.View as ListView, sourceListView, relationView);
                        return;

                    }
                }


            }
        }
    }
}

