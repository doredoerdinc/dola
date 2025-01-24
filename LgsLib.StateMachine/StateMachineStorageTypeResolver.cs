#region Copyright (c) 2000-2019 Developer Express Inc.
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

using DevExpress.Utils;
using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.StateMachine;
namespace LgsLib.StateMachine
{
	public class StateMachineStorageTypeResolver
	{
		protected virtual bool IsLoadedAssembly(string assemblyName)
		{
			return AssemblyHelper.IsLoadedAssembly(assemblyName);
		}
		protected virtual bool TryGetType(string assemblyName, string typeFullName, out Type result)
		{
			return DevExpress.ExpressApp.Utils.Reflection.AssemblyHelper.TryGetType(assemblyName, typeFullName, out result);
		}
		public Type GetDefaultStateMachineStorageType()
		{
			Type result = null;
			String assemblyName = "DevExpress.Xpo" + XafAssemblyInfo.VersionSuffix;
			if (IsLoadedAssembly(assemblyName))
			{
				result = FindType("Xpo.XpoStateMachine");
			}
			return result;
		}
		public Type FindType(string name)
		{
			Type result;
			String assemblyName = "DevExpress.Persistent.BaseImpl" + XafAssemblyInfo.VersionSuffix;
			String typeFullName = "LgsLib.StateMachine." + name;
			if (!TryGetType(assemblyName, typeFullName, out result) && !ModuleHelper.IsDesignMode)
			{
				throw new Exception(string.Format("Cannot find the '{0}' type or '{1}' assembly.", typeFullName, assemblyName));
			}
			return result;
		}
	}
}
