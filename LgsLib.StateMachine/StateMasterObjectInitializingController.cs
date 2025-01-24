﻿#region Copyright (c) 2000-2019 Developer Express Inc.
/*
{*******************************************************************}
{                                                                   }
{       Developer Express .NET Component Library                    }
{                                                                   }
{                                                                   }
{       Copyright (c) 2000-2019 Developer Express Inc.              }
{       ALL RIGHTS RESERVED                                         }
{                                                                   }
{   The entire contents of this file is protected by U.S. and       }
{   International Copyright Laws. Unauthorized reproduction,        }
{   reverse-engineering, and distribution of all or any portion of  }
{   the code contained in this file is strictly prohibited and may  }
{   result in severe civil and criminal penalties and will be       }
{   prosecuted to the maximum extent possible under the law.        }
{                                                                   }
{   RESTRICTIONS                                                    }
{                                                                   }
{   THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES           }
{   ARE CONFIDENTIAL AND PROPRIETARY TRADE                          }
{   SECRETS OF DEVELOPER EXPRESS INC. THE REGISTERED DEVELOPER IS   }
{   LICENSED TO DISTRIBUTE THE PRODUCT AND ALL ACCOMPANYING .NET    }
{   CONTROLS AS PART OF AN EXECUTABLE PROGRAM ONLY.                 }
{                                                                   }
{   THE SOURCE CODE CONTAINED WITHIN THIS FILE AND ALL RELATED      }
{   FILES OR ANY PORTION OF ITS CONTENTS SHALL AT NO TIME BE        }
{   COPIED, TRANSFERRED, SOLD, DISTRIBUTED, OR OTHERWISE MADE       }
{   AVAILABLE TO OTHER INDIVIDUALS WITHOUT EXPRESS WRITTEN CONSENT  }
{   AND PERMISSION FROM DEVELOPER EXPRESS INC.                      }
{                                                                   }
{   CONSULT THE END USER LICENSE AGREEMENT FOR INFORMATION ON       }
{   ADDITIONAL RESTRICTIONS.                                        }
{                                                                   }
{*******************************************************************}
*/
#endregion Copyright (c) 2000-2019 Developer Express Inc.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.StateMachine;

namespace LgsLib.StateMachine {
	public class StateMasterObjectInitializingController : ViewController<ListView> {
		private NewObjectViewController controller;
		public StateMasterObjectInitializingController() {
			TargetViewNesting = Nesting.Nested;
			TargetObjectType = typeof(IMasterObjectInitializer);
		}
		protected override void OnActivated() {
			base.OnActivated();
			controller = Frame.GetController<NewObjectViewController>();
			if(controller != null) {
				controller.ObjectCreated += MasterObjectInitializingController_ObjectCreated;
			}
		}
		void MasterObjectInitializingController_ObjectCreated(object sender, ObjectCreatedEventArgs e) {
			IMasterObjectInitializer obj = e.CreatedObject as IMasterObjectInitializer;
			PropertyCollectionSource collectionSource = View.CollectionSource as PropertyCollectionSource;
			if(obj != null && collectionSource != null) {
				obj.SetMasterObject(e.ObjectSpace.GetObject(collectionSource.MasterObject));
			}
		}
		protected override void OnDeactivated() {
			if(controller != null) {
				controller.ObjectCreated -= MasterObjectInitializingController_ObjectCreated;
			}
		}
	}
}
