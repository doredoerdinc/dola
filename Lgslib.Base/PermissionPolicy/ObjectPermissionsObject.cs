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
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations.Schema;
namespace LgsLib.Base.PermissionPolicy {
	[DisplayName("Object Operation Permissions")]
	[ImageName("BO_Security_Permission_Object")]
	[DefaultListViewOptions(true, NewItemRowPosition.Top)]
	[DefaultClassOptions]
	[Table("ObjectPermissionsObject")]
	public class ObjectPermissionsObject : IOwnerInitializer, IPermissionPolicyObjectPermissionsObject {
		private SecurityPermissionState? readState;
		private SecurityPermissionState? writeState;
		private SecurityPermissionState? deleteState;
		private SecurityPermissionState? navigateState;
		[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
		public int ID { get; protected set; }
		[FieldSize(FieldSizeAttribute.Unlimited)]
		[CriteriaOptions("TypePermissionObject.TargetType")]
		[VisibleInListView(true)]
		[EditorAlias(EditorAliases.PopupCriteriaPropertyEditor)]
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
		[VisibleInListView(false), VisibleInDetailView(false)]
		public virtual TypePermissionObject TypePermissionObject { get; set; }
		IPermissionPolicyTypePermissionObject IPermissionPolicyObjectPermissionsObject.TypePermissionObject {
			get {
				return TypePermissionObject;
			}
		}
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
