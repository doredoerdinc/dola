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

using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.Security;
using DevExpress.Persistent.Validation;
using System.Linq;
using System;
using LgsLib.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace LgsLib.Base.PermissionPolicy {
	[DisplayName("User"), ImageName("BO_User")]
	[DefaultClassOptions]
	[Table("User")]
	public class User : INotifyPropertyChanged, IPermissionPolicyUser, ISecurityUser, IAuthenticationActiveDirectoryUser, IAuthenticationStandardUser, ISecurityUserWithRoles {
		private bool changePasswordOnFirstLogon;
		public const string ruleId_RoleRequired = "Role required";
		public const string ruleId_UserNameRequired = "User Name required";
		public const string ruleId_UserNameIsUnique = "User Name is unique";
        public enum EnumUserType
        {
            WebApplication = 0,
            MobileApplication = 1, 
        }
        public User() {
			IsActive = true;
			Roles = new List<Role>();
			Regions = new List<Region>(); 
		}
		public virtual IList<Region> Regions { get; set; }

		[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int ID { get; protected set; }
		[RuleRequiredField(ruleId_UserNameRequired, "Save", "The user name must not be EmptyCapacity")]
		[RuleUniqueValue(ruleId_UserNameIsUnique, "Save", "The login with the entered UserName was already registered within the system")]
		public string UserName { get; set; }
		public bool IsActive { get; set; }
		public bool ChangePasswordOnFirstLogon {
			get { return changePasswordOnFirstLogon; }
			set {
				changePasswordOnFirstLogon = value;
				if(PropertyChanged != null) {
					PropertyChangedEventArgs args = new PropertyChangedEventArgs("ChangePasswordOnFirstLogon");
					PropertyChanged(this, args);
				}
			}
		}
		[Browsable(false), SecurityBrowsable]
		public string StoredPassword { get; set; }
		[RuleRequiredField(ruleId_RoleRequired, DefaultContexts.Save, TargetCriteria = "IsActive=True", CustomMessageTemplate = "An active user must have at least one role assigned")]
		public virtual IList<Role> Roles { get; set; }
		IEnumerable<IPermissionPolicyRole> IPermissionPolicyUser.Roles {
			get {
				return Roles.OfType<IPermissionPolicyRole>();
			}
		}
		bool ISecurityUser.IsActive {
			get { return IsActive; }
		}
		string ISecurityUser.UserName {
			get { return UserName; }
		}
		string IAuthenticationActiveDirectoryUser.UserName {
			get { return UserName; }
			set { UserName = value; }
		}
		bool IAuthenticationStandardUser.ComparePassword(string password) {
			return PasswordCryptographer.VerifyHashedPasswordDelegate(StoredPassword, password);
		}
		bool IAuthenticationStandardUser.ChangePasswordOnFirstLogon {
			get { return ChangePasswordOnFirstLogon; }
			set { ChangePasswordOnFirstLogon = value; }
		}
		string IAuthenticationStandardUser.UserName {
			get { return UserName; }
		} 

        bool _CanEditModel;
        public bool CanEditModel
        {
            get { return _CanEditModel; }
            set { _CanEditModel = value; }
        }

        EnumUserType _UserType;
        public EnumUserType UserType
        {
            get { return _UserType; }
            set { _UserType = value; }
        }

        String _MobilePassword;
        public String MobilePassword
        {
            get { return _MobilePassword; }
            set { _MobilePassword = value; }
        }

        String _Token;
        public String Token
        {
            get { return _Token; }
            set { _Token = value; }
        }

        Owner _Owner;
        public virtual Owner Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }


        IList<ISecurityRole> ISecurityUserWithRoles.Roles {
			get {
				IList<ISecurityRole> result = new List<ISecurityRole>();
				foreach(Role role in Roles) {
					result.Add(role);
				}
				return new ReadOnlyCollection<ISecurityRole>(result);
			}
		}
		public void SetPassword(string password) {
			StoredPassword = PasswordCryptographer.HashPasswordDelegate(password);
		}
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
