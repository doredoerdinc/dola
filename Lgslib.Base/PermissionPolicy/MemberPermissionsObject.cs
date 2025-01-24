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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using System.ComponentModel.DataAnnotations.Schema;
namespace LgsLib.Base.PermissionPolicy {
	[DisplayName("Member Operation Permissions")]
	[ImageName("BO_Security_Permission_Member")]
	[DefaultListViewOptions(true, NewItemRowPosition.Top)]
	[DefaultClassOptions]
	[Table("MemberPermissionsObject")]
	public class MemberPermissionsObject : ICheckedListBoxItemsProvider, IOwnerInitializer, IPermissionPolicyMemberPermissionsObject {
		private SecurityPermissionState? readState;
		private SecurityPermissionState? writeState;
		[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
		public Int32 ID { get; protected set; }
		[FieldSize(FieldSizeAttribute.Unlimited)]
		[VisibleInListView(true)]
		[EditorAlias(EditorAliases.CheckedListBoxEditor)]
		public string Members { get; set; }
		[CriteriaOptions("TypePermissionObject.TargetType")]
		[EditorAlias(EditorAliases.PopupCriteriaPropertyEditor)]
		[FieldSize(FieldSizeAttribute.Unlimited)]
		[DevExpress.ExpressApp.Model.ModelDefault("RowCount", "0")]
		[VisibleInListView(true), VisibleInDetailView(true)]
		public string Criteria { get; set; }
		[DisplayName("Read")]
		public SecurityPermissionState? ReadState {
			get {
				return readState;
			}
			set {
				readState = value;
			}
		}
		[DisplayName("Write")]
		public SecurityPermissionState? WriteState {
			get {
				return writeState;
			}
			set {
				writeState = value;
			}
		}
		[VisibleInListView(false), VisibleInDetailView(false)]
		public virtual TypePermissionObject TypePermissionObject { get; set; }
		[Browsable(false)]
		public bool IsMemberExists {
			get {
				if(string.IsNullOrEmpty(Members)) {
					return false;
				}
				ITypeInfo typeInfo = XafTypesInfo.Instance.FindTypeInfo(TypePermissionObject.TargetType);
				string[] membersArray = Members.Split(';');
				if(membersArray.Length == 0) {
					return false;
				}
				foreach(string member in membersArray) {
					if(typeInfo.FindMember(member.Trim()) == null) {
						return false;
					}
				}
				return true;
			}
		}
		IPermissionPolicyTypePermissionObject IPermissionPolicyMemberPermissionsObject.TypePermissionObject {
			get {
				return TypePermissionObject;
			}
		}
		#region ICheckedListBoxItemsProvider Members
		Dictionary<object, string> ICheckedListBoxItemsProvider.GetCheckedListBoxItems(string targetMemberName) {
			if(TypePermissionObject == null || !(TypePermissionObject is ICheckedListBoxItemsProvider)) {
				return new Dictionary<Object, String>();
			}
			return ((ICheckedListBoxItemsProvider)TypePermissionObject).GetCheckedListBoxItems(targetMemberName);
		}
		protected virtual void OnItemsChanged() {
			if(ItemsChanged != null) {
				ItemsChanged(this, EventArgs.Empty);
			}
		}
		public event EventHandler ItemsChanged;
		#endregion
		#region IMasterObjectInitializer Members
		void IOwnerInitializer.SetMasterObject(object masterObject) {
			TypePermissionObject typePermission = masterObject as TypePermissionObject;
			if(typePermission != null) {
				TypePermissionObject = typePermission;
			}
		}
		#endregion
	}
}
