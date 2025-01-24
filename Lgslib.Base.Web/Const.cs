using DevExpress.ExpressApp.Web.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace LgsLib.Base.Web
{
   public static class ApplicationDefination
    {
        public static void PopupSettings(int width, int height)
        {
            XafPopupWindowControl.ShowPopupMode = ShowPopupMode.Centered;
            XafPopupWindowControl.DefaultHeight = Unit.Percentage(height);
            XafPopupWindowControl.DefaultWidth = Unit.Percentage(width);
            XafPopupWindowControl.PopupTemplateType = PopupTemplateType.FindDialog;
           
        }

        public static void PopupSettingsInitial()
        {
            XafPopupWindowControl.ShowPopupMode = ShowPopupMode.Centered;
            XafPopupWindowControl.PopupTemplateType = PopupTemplateType.ByDefault;
            XafPopupWindowControl.DefaultHeight = Unit.Percentage(80);
            XafPopupWindowControl.DefaultWidth = Unit.Percentage(80);
        }
    }
}
