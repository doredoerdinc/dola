﻿using System;
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
using LgsLib.Base;
using LgsLib.Base.PermissionPolicy;

namespace dola.Module
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppWindowControllertopic.aspx.
    public partial class GlobalWindowsController : WindowController
    {
        public GlobalWindowsController()
        {
             InitializeComponent();
           
            // Target required Windows (via the TargetXXX properties) and create their Actions.
        }
        
        protected override void OnActivated()
        {
            base.OnActivated();
            if (GlobalConst.UserOwner == null && SecuritySystem.CurrentUserName != "Admin")
            {
                var newObjectSpace = Application.CreateObjectSpace();
                var user = newObjectSpace.GetObjectByKey<User>(SecuritySystem.CurrentUserId);
                if (user.Owner == null)
                {
                   
                }
                else
                {
                    GlobalConst.UserOwner = newObjectSpace.GetObjectByKey<Owner>(user.Owner.SysCode);
                }
                

                if (GlobalConst.UserOwner == null)
                {
                    //        throw new System.ArgumentException("Firma bilgileriniz kayıtlı degildir.");
                }

            }
            //var a = 1;
            // Perform various tasks depending on the target Window.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
