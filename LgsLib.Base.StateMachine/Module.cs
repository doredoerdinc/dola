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
using System.ComponentModel;
using System.Drawing;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Design;
using LgsLib.StateMachine.Resources;
using DevExpress.ExpressApp.Updating;
using DevExpress.Utils;
using DevExpress.Utils.Design;
using DevExpress.ExpressApp;

namespace LgsLib.StateMachine {
	[DXToolboxItem(true)]
	[ToolboxTabName(XafAssemblyInfo.DXTabXafModules)]
	[ToolboxBitmap(typeof(StateMachineModule), "Resources.Toolbox_Module_StateMachine.ico")]
	[Description("Provides State Machine functionality in XAF applications.")]
	public sealed class StateMachineModule : ModuleBase {
		private Boolean isStateMachineStorageTypeInitialized = false;
		private static Type stateMachineStorageType = null; 
		private static StateMachineStorageTypeResolver resolver = new StateMachineStorageTypeResolver();
		protected override ModuleTypeList GetRequiredModuleTypesCore() {
			ModuleTypeList result = base.GetRequiredModuleTypesCore();
			result.Add(typeof(DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule));
			result.Add(typeof(DevExpress.ExpressApp.Validation.ValidationModule));
			return result;
		}
		protected override IEnumerable<Type> GetRegularTypes() {
			return null;
		}
		protected override IEnumerable<Type> GetDeclaredExportedTypes() {
			if(StateMachineStorageType != null) {
				return new Type[] { StateMachineStorageType };
			}
			else {
				return Type.EmptyTypes;
			}
		}
		protected override IEnumerable<Type> GetDeclaredControllerTypes() {
			return new Type[] {
				typeof(LgsLib.StateMachine.DisableStatePropertyController),
				typeof(LgsLib.StateMachine.StateMachineAppearanceController),
				typeof(LgsLib.StateMachine.StateMachineController),
				typeof(LgsLib.StateMachine.StateMachineRefreshStatePropertyNameController),
				typeof(LgsLib.StateMachine.StateMachineCacheController),
				typeof(LgsLib.StateMachine.StateMasterObjectInitializingController),
			};
		}
		public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
			return ModuleUpdater.EmptyModuleUpdaters;
		}
		public static void RegisterDomainComponentEntities(ITypesInfo typesInfo, string stateMachineEntityName, string stateEntityName, string transitionEntityName, string stateAppearanceEntityName) {
			typesInfo.RegisterEntity(stateMachineEntityName, resolver.FindType("Dc.IDCStateMachine"));
			typesInfo.RegisterEntity(stateEntityName, resolver.FindType("Dc.IDCState"));
			typesInfo.RegisterEntity(transitionEntityName, resolver.FindType("Dc.IDCTransition"));
			typesInfo.RegisterEntity(stateAppearanceEntityName, resolver.FindType("Dc.IDCStateAppearance"));
		}
		[Description("Specifies a persistent type used to store state machines in the database.")]
		[TypeConverter(typeof(BusinessClassTypeConverter<IStateMachine>))]
		public Type StateMachineStorageType {
			get {
				if(!isStateMachineStorageTypeInitialized) {
					isStateMachineStorageTypeInitialized = true;
					stateMachineStorageType = resolver.GetDefaultStateMachineStorageType();
				}
				return stateMachineStorageType; 
			}
			set {
				if(value != null) {
					DevExpress.ExpressApp.Utils.Guard.TypeArgumentIs(typeof(IStateMachine), value, "value");
				}
				isStateMachineStorageTypeInitialized = true;
				stateMachineStorageType = value; 
			}
		}
	}
}
