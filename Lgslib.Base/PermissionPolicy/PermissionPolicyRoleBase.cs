#region Copyright (c) 2000-2017 Developer Express Inc.
/*
{*******************************************************************}
{                                                                   }
{       Developer Express .NET Component Library                    }
{       eXpressApp Framework                                        }
{                                                                   }
{       Copyright (c) 2000-2017 Developer Express Inc.              }
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
#endregion Copyright (c) 2000-2017 Developer Express Inc.

using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
namespace LgsLib.Base.PermissionPolicy {
	public class PermissionPolicyRoleBase : IPermissionPolicyRole, ISecurityRole, ISecuritySystemRole, INavigationPermissions {
		public PermissionPolicyRoleBase() {
			TypePermissions = new List<TypePermissionObject>();
			NavigationPermissions = new List<PermissionPolicyNavigationPermissionObject>();
		}
		[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
		public int ID { get; protected set; }
		public string Name { get; set; }
		public bool IsAdministrative { get; set; }
		public bool CanEditModel { get; set; }
		[VisibleInListView(false)]
		public SecurityPermissionPolicy PermissionPolicy { get; set; }
		[Browsable(false)]
		[DefaultValue(false)]
		public bool IsAllowPermissionPriority { get; set; }
		[Aggregated]
		public virtual IList<TypePermissionObject> TypePermissions { get; set; }
		IEnumerable<IPermissionPolicyTypePermissionObject> IPermissionPolicyRole.TypePermissions {
			get {
				return TypePermissions.OfType<IPermissionPolicyTypePermissionObject>();
			}
		}
		public IPermissionPolicyTypePermissionObject CreateTypePermissionObject(Type targetType) {
			TypePermissionObject permissionPolicyTypePermissionObject = new TypePermissionObject();
			permissionPolicyTypePermissionObject.TargetType = targetType;
			permissionPolicyTypePermissionObject.Role = this;
			TypePermissions.Add(permissionPolicyTypePermissionObject);
			return permissionPolicyTypePermissionObject;
		}		
		string ISecurityRole.Name {
			get { return Name; }
		}
		[Aggregated]
		public virtual IList<PermissionPolicyNavigationPermissionObject> NavigationPermissions { get; set; }
		IEnumerable<IPermissionPolicyNavigationPermissionObject> INavigationPermissions.NavigationPermissions {
			get {
				return NavigationPermissions.OfType<IPermissionPolicyNavigationPermissionObject>();
			}
		}
		public IPermissionPolicyNavigationPermissionObject CreateNavigationPermissionObject(string itemPath) {
			PermissionPolicyNavigationPermissionObject navigationPermissionObject = new PermissionPolicyNavigationPermissionObject();
			navigationPermissionObject.ItemPath = itemPath;
			navigationPermissionObject.Role = this;
			NavigationPermissions.Add(navigationPermissionObject);
			return navigationPermissionObject;
		}
	}
}
