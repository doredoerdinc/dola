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
using System.Text;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.StateMachine;
namespace LgsLib.StateMachine {
	public class StateMachineRefreshStatePropertyNameController : ViewController<DetailView> {
		private void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e) {
			if(e.PropertyName == "TargetObjectType") {
				PropertyEditor statePropertyNameEditor = View.FindItem("StatePropertyName") as PropertyEditor;
				if(statePropertyNameEditor != null) {
					statePropertyNameEditor.Refresh();
				}
			}
		}
		protected override void OnActivated() {
			base.OnActivated();
			View.ObjectSpace.ObjectChanged += new EventHandler<ObjectChangedEventArgs>(ObjectSpace_ObjectChanged);
		}
		protected override void OnDeactivated() {
			View.ObjectSpace.ObjectChanged -= new EventHandler<ObjectChangedEventArgs>(ObjectSpace_ObjectChanged);
			base.OnDeactivated();
		}
		public StateMachineRefreshStatePropertyNameController() {
			TargetObjectType = typeof(IStateMachine);
		}
	}
}
