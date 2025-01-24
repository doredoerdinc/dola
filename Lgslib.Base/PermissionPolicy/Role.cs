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

using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using System;
using DevExpress.Persistent.Base.Security;
using System.ComponentModel.DataAnnotations.Schema;

namespace LgsLib.Base.PermissionPolicy {
	[DisplayName("Role"), ImageName("BO_Role")]
	[DefaultClassOptions]
	[Table("Role")]
	public class Role : PermissionPolicyRoleBase, IPermissionPolicyRoleWithUsers, ICanInitializeRole {
		public Role() {
			Users = new List<User>();
			TypePermissions = new List<TypePermissionObject>();
		}
		static Role() {
		}
		//[Base.VisibleInListView(false)]
		public virtual IList<User> Users { get; set; }
		IEnumerable<IPermissionPolicyUser> IPermissionPolicyRoleWithUsers.Users {
			get {
				return Users.OfType<IPermissionPolicyUser>();
			}
		}
		public bool AddUser(object user) {
			bool result = false;
			User permissionPolicyUser = user as User;
			if(permissionPolicyUser != null) {
				Users.Add(permissionPolicyUser);
				result = true;
			}
			return result;
		}
	}
}
