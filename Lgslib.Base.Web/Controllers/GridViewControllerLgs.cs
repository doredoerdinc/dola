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
using DevExpress.ExpressApp.Web.Controls;
using System.Web.UI.WebControls;
using DevExpress.ExpressApp.Model;
using LgsLib.Base;

using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Web;
using System.ComponentModel;
using System.Drawing;
using DevExpress.Utils;
using DevExpress.ExpressApp.Maps.Web;
using DevExpress.ExpressApp.PivotGrid.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace LgsLib.Base.Web
{ 
    public interface IModelListViewWidth
    {
        int Width { get; set; }
    }
     public partial class GridViewControllerLgs : ViewController<DevExpress.ExpressApp.ListView>
    {
        private  bool gridControl = true;
        private ASPxGridView gridView;
        private ASPxGridListEditor gridListEditor;
        private List<State> objectStates;
        public GridViewControllerLgs()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            if (View != null) { 
            var newObjectSpce = Application.CreateObjectSpace();
            //var criteria = CriteriaOperator.Parse("", View.ObjectTypeInfo.FullName);
            objectStates = newObjectSpce.GetObjects<State>().ToList();
            }
            var webGridListEditor = View?.Editor as ASPxGridListEditor;
            if (webGridListEditor != null)
            {
                webGridListEditor.IsAdaptive = true;
            }
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated(); 
            var listEditor = View.Editor as ASPxGridListEditor;
            if (listEditor != null && listEditor.Model.DataAccessMode == CollectionSourceDataAccessMode.Server)
            {
                foreach (var column in listEditor.Grid.Columns)
                {
                    var commandColumn = column as GridViewCommandColumn;
                    if (commandColumn != null && commandColumn.ShowSelectCheckbox)
                    {
                        commandColumn.SelectAllCheckboxMode = GridViewSelectAllCheckBoxMode.Page;
                    }
                }
            } 

            if (gridControl&&View.Editor is ASPxGridListEditor)
            {
                gridControl = true;
                gridListEditor = (ASPxGridListEditor)View.Editor;  
                GridSeetings(gridListEditor);
            }
            if (View.Editor is ASPxPivotGridListEditor)
            { 
               var  pivotGridListEditor = (ASPxPivotGridListEditor)View.Editor;
                pivotGridListEditor.ChartControl.Visible = false;
                
            }
        } 
        private void GridSeetings(ASPxGridListEditor gridListEditor)
        {
            gridView = gridListEditor.Grid;
            gridView.Init += GridtView_Init;
           // gridView.Settings.ShowStatusBar = GridViewStatusBarMode.Hidden;
      //      gridView.SettingsResizing.ColumnResizeMode = ColumnResizeMode.NextColumn;
             
            foreach (WebColumnBase column in gridView.Columns)
            {
                var dataColumn = column as GridViewDataColumn;
                if (dataColumn != null)
                {
                    dataColumn.CellStyle.Wrap = DefaultBoolean.False;
                }
            }

            // gridView.SettingsResizing.Visualization = ResizingMode.Postponed;
            //gridView.SettingsResizing.ColumnResizeMode = ColumnResizeMode.Control;
            gridView.Styles.Header.Wrap = DevExpress.Utils.DefaultBoolean.True;
            gridView.ClientSideEvents.ColumnResized = "function(s,e){e.processOnServer = true;}";
            gridView.ClientSideEvents.Init = "function(s, e) {s.SetHeight(document.documentElement.clientHeight-160);}";

        }

        public void GridtView_Init(object sender, EventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            Style style = new Style(); 
            IModelGridSettings setting = gridListEditor.Model as IModelGridSettings;
            if (setting.RowCellSize > 0)
            {
                style.Font.Size = setting.RowCellSize;
            }
            grid.HtmlDataCellPrepared += GridView_HtmlDataCellPrepared; 
            gridListEditor.Grid.Styles.AlternatingRow.BackColor = Color.FromArgb(244, 244, 244);
            gridListEditor.Grid.Styles.FocusedRow.BackColor = Color.FromArgb(10, 140, 220);

            gridListEditor.Grid.HeaderFilterFillItems += Grid_HeaderFilterFillItems;
            

            foreach (GridViewDataColumn clm in gridListEditor.Grid.DataColumns)
            {
                var columnType= clm.GetType();
                if(columnType!= typeof(DevExpress.Web.GridViewDataDateColumn))
                { 
                   clm.SettingsHeaderFilter.Mode = DevExpress.Web.GridHeaderFilterMode.CheckedList;
                }
                clm.HeaderStyle.Wrap = DefaultBoolean.True;
                //clm.CellStyle.Wrap = DefaultBoolean.True;
                //clm.CellStyle.MergeFontWith(style);
                // clm.DataItemTemplate= new MyDivTemplate(clm.FieldName);
            }
           

            //IDataItemTemplateInfoProvider columnInfoProvider = (IDataItemTemplateInfoProvider)gridListEditor;
            //foreach (GridViewColumn column in gridListEditor.Grid.Columns)
            //{
            //    IXafColumnInfo columnInfo = columnInfoProvider.GetColumnInfo(column);
            //    IMemberInfo memberDescriptor = columnInfo.MemberInfo;
            //    clm.SettingsHeaderFilter.Mode = DevExpress.Web.GridHeaderFilterMode.CheckedList;


            //    //clm.HeaderStyle.Wrap = DefaultBoolean.True;
            //    //clm.CellStyle.Wrap = DefaultBoolean.True;
            //    //clm.CellStyle.MergeFontWith(style);
            //    // clm.DataItemTemplate= new MyDivTemplate(clm.FieldName);
            //}

        }

        private void Grid_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
        
            e.Values.Add(FilterValue.CreateShowBlanksValue(e.Column, "Boş Olanlar"));
            e.Values.Add(FilterValue.CreateShowNonBlanksValue(e.Column, "Boş olmayanlar"));


        }

        private void GridView_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.FieldName == "State.TranslateValue")
            {
                e.Cell.ForeColor = Color.White;

                foreach (var st in objectStates)
                {
                    if (st.TranslateValue == (e.CellValue as string))
                    {
                        e.Cell.BackColor = st.Color;
                    }
                }
            }
        }

        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }

    public class MyDivTemplate : ITemplate
    {
        private string dataFieldName;
        public MyDivTemplate(string dataFieldName)
        {
            this.dataFieldName = dataFieldName;
        }
        void ITemplate.InstantiateIn(Control container)
        {
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)container;
            HtmlGenericControl div = new HtmlGenericControl();
            div.TagName = "div";
            div.Style["text-overflow"] = "ellipsis";
            //div.Style["color"] = "red";  
            //div.Style["font-weight"] = "bold";  
            div.Style["width"] = "30px";
            div.Style["overflow"] = "hidden";
            div.Style["white-space"] = "wrap";
       //     div.InnerText = gridContainer.Grid.GetRowValues(gridContainer.ItemIndex, dataFieldName).ToString();
            container.Controls.Add(div);
        }
    }
}
