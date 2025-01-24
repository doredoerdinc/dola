﻿#region Copyright (c) 2000-2020 Developer Express Inc.
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

using DevExpress.ExpressApp;
using System;
using System.Collections.Generic;
using System.Text;
namespace LgsLib.StateMachine.NonPersistent {
	public abstract class StateMachine<T> : IStateMachine {
		private IObjectSpace objectSpace;
		public StateMachine(IObjectSpace objectSpace) {
			this.objectSpace = objectSpace;
			States = new List<IState>();
			Active = true;
		}
		public abstract string Name { get; }
		public Type TargetObjectType {
			get { return typeof(T); }
		}
		public bool Active { get; set; }
		public abstract string StatePropertyName { get; }
		public abstract IState StartState { get; }
		public IList<IState> States { get; private set; }
		public IState FindCurrentState(object targetObject) {
			return new StateMachineLogic(objectSpace).FindCurrentState(this, targetObject, StartState);
		}
		public void ExecuteTransition(object targetObject, IState targetState) {
			new StateMachineLogic(objectSpace).ExecuteTransition(targetObject, targetState);
		}
	}
}
