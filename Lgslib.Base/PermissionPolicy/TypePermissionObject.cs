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
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Linq;
using DevExpress.Persistent.Base.Security;

namespace LgsLib.Base.PermissionPolicy {
	[DisplayName("Type Permission Object")]
	[ImageName("BO_Security_Permission_Type")]
	[DefaultListViewOptions(true, NewItemRowPosition.Top)]
	[DefaultClassOptions]
	[Table("TypePermissionObject")]
	public class TypePermissionObject : ICheckedListBoxItemsProvider, IPermissionPolicyTypePermissionObject {
		private Type targetType;
		private SecurityPermissionState? readState;
		private SecurityPermissionState? writeState;
		private SecurityPermissionState? createState;
		private SecurityPermissionState? deleteState;
		private SecurityPermissionState? navigateState;
		[NotMapped]
		[ImmediatePostData]
		[RuleRequiredField]
		[TypeConverter(typeof(SecurityTargetTypeConverter))]
		[VisibleInListView(true)]
		public Type TargetType {
			get {
				if((targetType == null) && !String.IsNullOrWhiteSpace(TargetTypeFullName)) {
					targetType = ReflectionHelper.FindType(TargetTypeFullName);
				}
				return targetType;
			}
			set {
				targetType = value;
				if(targetType != null) {
					TargetTypeFullName = targetType.FullName;
				}
				else {
					TargetTypeFullName = "";
				}
				OnItemsChanged();
			}
		}
		[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
		public int ID { get; protected set; }
		[Browsable(false)]
		public string TargetTypeFullName { get;  set; }
		public virtual PermissionPolicyRoleBase Role { get; set; }
		IPermissionPolicyRole IPermissionPolicyTypePermissionObject.Role {
			get {
				return Role;
			}
		}
		[Aggregated]
		public virtual IList<MemberPermissionsObject> MemberPermissions { get; set; }
		[Aggregated]
		public virtual IList<ObjectPermissionsObject> ObjectPermissions { get; set; }
		IEnumerable<IPermissionPolicyMemberPermissionsObject> IPermissionPolicyTypePermissionObject.MemberPermissions {
			get {
				return MemberPermissions.OfType<IPermissionPolicyMemberPermissionsObject>();
			}
		}
		IEnumerable<IPermissionPolicyObjectPermissionsObject> IPermissionPolicyTypePermissionObject.ObjectPermissions {
			get {
				return ObjectPermissions.OfType<IPermissionPolicyObjectPermissionsObject>();
			}
		}
		public IPermissionPolicyObjectPermissionsObject CreateObjectPermission() {
			ObjectPermissionsObject permissionPolicyObjectPermissionsObject = new ObjectPermissionsObject();
			permissionPolicyObjectPermissionsObject.TypePermissionObject = this;
			ObjectPermissions.Add(permissionPolicyObjectPermissionsObject);
			return permissionPolicyObjectPermissionsObject;
		}
		public IPermissionPolicyMemberPermissionsObject CreateMemberPermission() {
			MemberPermissionsObject permissionPolicyMemberPermissionsObject = new MemberPermissionsObject();
			permissionPolicyMemberPermissionsObject.TypePermissionObject = this;
			MemberPermissions.Add(permissionPolicyMemberPermissionsObject);
			return permissionPolicyMemberPermissionsObject;
		}
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
		[DisplayName("Create")]
		public SecurityPermissionState? CreateState {
			get {
				return createState;
			}
			set {
				createState = value;
			}
		}
		[DisplayName("Delete")]
		public SecurityPermissionState? DeleteState {
			get {
				return deleteState;
			}
			set {
				deleteState = value;
			}
		}
		[DisplayName("Navigate")]
		public SecurityPermissionState? NavigateState {
			get {
				return navigateState;
			}
			set {
				navigateState = value;
			}
		}
		public TypePermissionObject() {
			MemberPermissions = new List<MemberPermissionsObject>();
			ObjectPermissions = new List<ObjectPermissionsObject>();
		}
		Dictionary<object, string> ICheckedListBoxItemsProvider.GetCheckedListBoxItems(string targetMemberName) {
			Dictionary<Object, String> result = new Dictionary<Object, String>();
			if(targetMemberName == "Members" && TargetType != null) {
				ITypeInfo typeInfo = XafTypesInfo.Instance.FindTypeInfo(TargetType);
				foreach(IMemberInfo memberInfo in typeInfo.Members) {
					if(memberInfo.IsVisible || (memberInfo.FindAttribute<SecurityBrowsableAttribute>() != null)) {
						string caption = CaptionHelper.GetMemberCaption(memberInfo);
						if(result.ContainsKey(memberInfo.Name)) {
							throw new LightDictionary<string, string>.DuplicatedKeyException(memberInfo.Name, result[memberInfo.Name], caption);
						}
						result.Add(memberInfo.Name, caption);
					}
				}
			}
			return result;
		}
		protected virtual void OnItemsChanged() {
			if(ItemsChanged != null) {
				ItemsChanged(this, EventArgs.Empty);
			}
		}
		public event EventHandler ItemsChanged;
	}
}
