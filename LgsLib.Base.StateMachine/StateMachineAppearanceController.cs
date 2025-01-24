#region Copyright (c) 2000-2020 Developer Express Inc.
/*
{*******************************************************************}
{                                                                   }
{       Developer Express .NET Component Library                    }
{                                                                   }
{                                                                   }
{       Copyright (c) 2000-2020 Developer Express Inc.              }
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
#endregion Copyright (c) 2000-2020 Developer Express Inc.

using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.ComponentModel;
using DevExpress.ExpressApp;

namespace LgsLib.StateMachine {
	public class StateMachineAppearanceController : StateMachineControllerBase<ObjectView> {
		private void ObjectSpace_Reloaded(object sender, EventArgs e) {
			if(Frame != null) { 
				AppearanceController _controller = AppearanceController;
				if(_controller != null) {
					_controller.ResetRulesCache();
				}
			}
		}
		private void ObjectSpace_Commited(object sender, EventArgs e) { 
			if(Frame != null) {
				AppearanceController _controller = AppearanceController;
				if(_controller != null) {
					_controller.ResetRulesCache();
				}
			}
		}
		private void ObjectSpace_Refreshing(object sender, CancelEventArgs e) {
			AppearanceController _controller = AppearanceController;
			if(_controller != null) {
				_controller.ResetRulesCache();
			}
		}
		private void ObjectSpace_RollingBack(object sender, CancelEventArgs e) {
			AppearanceController _controller = AppearanceController;
			if(_controller != null) {
				_controller.ResetRulesCache();
			}
		}
		private void appearanceController_CollectAppearanceRules(object sender, CollectAppearanceRulesEventArgs e) {
			IList<IStateMachine> stateMachines = GetStateMachines();
			foreach(IStateMachine stateMachine in stateMachines) {
				foreach(IState state in stateMachine.States) {
					if(state is IStateAppearancesProvider) {
						e.AppearanceRules.AddRange(((IStateAppearancesProvider)state).Appearances);
					}
				}
			}
		}
		private void stateMachineController_TransitionExecuted(object sender, ExecuteTransitionEventArgs e) {
			HardRefreshAppearance();
		}
		private void HardRefreshAppearance() {
			AppearanceController _controller = AppearanceController;
			if(_controller != null) {
				_controller.ResetRulesCache();
				_controller.Refresh();
			}
		}
		protected AppearanceController AppearanceController {
			get { return Frame.GetController<AppearanceController>(); }
		}
		protected override void OnViewControlsCreated() {
			AppearanceController _controller = AppearanceController;
			if(_controller != null) {
				_controller.AppearanceEndUpdate();
				if(IsIStateMachineProviderView) {
					ListView listView = View as ListView;
					if((listView != null) 
						&& (listView.CollectionSource != null)
						&& !listView.CollectionSource.IsLoaded) {
						listView.CollectionSource.CollectionChanged += new EventHandler(collectionSource_CollectionChanged);
					}
				}
			}
		}
		private void collectionSource_CollectionChanged(object sender, EventArgs e) {
			((CollectionSourceBase)sender).CollectionChanged -= new EventHandler(collectionSource_CollectionChanged);
			HardRefreshAppearance();
		}
		protected override void OnActivated() {
			base.OnActivated();
			AppearanceController _controller = AppearanceController;
			if(_controller != null) {
				_controller.AppearanceBeginUpdate();
				_controller.CollectAppearanceRules += new EventHandler<CollectAppearanceRulesEventArgs>(appearanceController_CollectAppearanceRules);
			}
			StateMachineController stateMachineController = Frame.GetController<StateMachineController>();
			if(stateMachineController != null) {
				stateMachineController.TransitionExecuted += new EventHandler<ExecuteTransitionEventArgs>(stateMachineController_TransitionExecuted);
			}
			View.ObjectSpace.Refreshing += new EventHandler<CancelEventArgs>(ObjectSpace_Refreshing);
			View.ObjectSpace.RollingBack += new EventHandler<CancelEventArgs>(ObjectSpace_RollingBack);
			View.ObjectSpace.Committed += new EventHandler(ObjectSpace_Commited);
			View.ObjectSpace.Reloaded += new EventHandler(ObjectSpace_Reloaded);
		}
		protected override void OnDeactivated() {
			View.ObjectSpace.Refreshing -= new EventHandler<CancelEventArgs>(ObjectSpace_Refreshing);
			View.ObjectSpace.RollingBack -= new EventHandler<CancelEventArgs>(ObjectSpace_RollingBack);
			View.ObjectSpace.Committed -= new EventHandler(ObjectSpace_Commited);
			View.ObjectSpace.Reloaded -= new EventHandler(ObjectSpace_Reloaded);
			StateMachineController stateMachineController = Frame.GetController<StateMachineController>();
			if(stateMachineController != null) {
				stateMachineController.TransitionExecuted -= new EventHandler<ExecuteTransitionEventArgs>(stateMachineController_TransitionExecuted);
			}
			AppearanceController _controller = AppearanceController;
			if(_controller != null) {
				_controller.CollectAppearanceRules -= new EventHandler<CollectAppearanceRulesEventArgs>(appearanceController_CollectAppearanceRules);
				_controller.AppearanceEndUpdate();
			}
			base.OnDeactivated();
		}
	}
}
