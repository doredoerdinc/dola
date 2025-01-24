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
    public interface IModelGetActions : IModelNode
    {
        [Category(ModelCategory.ViewList)]
        [DataSourceProperty("Application.ActionDesign.Actions")]
        IModelAction Action { get; set; }
    }
    
}
