using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;

namespace dola.Module.Win
{
    public class RefreshFieldPickerController : ObjectViewController<DetailView, TaskTemplateItem>
    {
        PropertyEditor dependentPropertyEditor, targetPropertyEditor;
        protected override void OnActivated()
        {
            base.OnActivated();
            dependentPropertyEditor = (PropertyEditor)View.FindItem("SysValueReferanceProperty");
            targetPropertyEditor = (PropertyEditor)View.FindItem("ObjectType");
            if ((targetPropertyEditor != null) && (dependentPropertyEditor != null))
            {
                targetPropertyEditor.ValueStored += targetPropertyEditor_ValueStored;
            }
        }
        void targetPropertyEditor_ValueStored(object sender, EventArgs e)
        {
            dependentPropertyEditor.Refresh();
        }
    }
}
 