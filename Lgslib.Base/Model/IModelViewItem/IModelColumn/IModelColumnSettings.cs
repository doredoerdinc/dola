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
 


namespace LgsLib.Base
{
    [DomainComponent]
     public interface IModelColumnSettings : IModelColumn,IModelCurrentListView
    {
      
        [Category(ModelCategory.ColumnList)]
        [Editor("DevExpress.ExpressApp.Win.Core.ModelEditor.ImageGalleryModelEditorControl, DevExpress.ExpressApp.Win.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
        string Image { get; set; }
     /* 
        [DataSourceProperty("GridControlMouseEventType")]
        [Category(ModelCategory.ColumnList)]
        GridMouseEventType MouseEvent { get; set; }
        */
        
        [Category(ModelCategory.ColumnList)]
        [DataSourceProperty("Application.Views")]
        IModelView PopupView { get; set; }

        [Category(ModelCategory.ColumnList)]
        [DataSourceProperty("Application.ActionDesign.Actions")]
        IModelAction CallAction { get; set; }

    }

    
}

 