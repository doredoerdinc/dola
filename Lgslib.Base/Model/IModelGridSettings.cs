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
    public interface IModelGridSettings : IModelListView
    {
        [Category("Behavior")]
        int RowCellSize { get; set; }
    }  


}

