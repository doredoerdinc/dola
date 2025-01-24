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

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
namespace LgsLib.Base {
	[ImageName("ModelEditor_ModelMerge")]
	[RuleCombinationOfPropertiesIsUnique(null, DefaultContexts.Save, "UserId, ContextId")]
	public class ModelDifference : IModelDifference, IObjectSpaceLink, INotifyPropertyChanged {
		private Int32 id;
		private String userId;
		private String userName;
		private String contextId;
		private Int32 version;
		private IList<ModelDifferenceAspect> aspects;
		private IObjectSpace objectSpace;
		public ModelDifference() {
			Aspects = new List<ModelDifferenceAspect>();
		}
		[Key]
		[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
		public Int32 ID {
			get { return id; }
			protected set { id = value; }
		}
		[Browsable(false)]
		[ModelDefault("AllowEdit", "False")]
		public String UserId {
			get { return userId; }
			set {
				if(SetPropertyValue(ref userId, value)) {
					userName = "";
				}
			}
		}
		public String UserName {
			get {
				if(String.IsNullOrWhiteSpace(userId)) {
					userName = ModelDifferenceDbStore.SharedModelDifferenceName;
				}
				else if(String.IsNullOrEmpty(userName)) {
					if(ModelDifferenceDbStore.UserTypeInfo != null) {
						List<DataViewExpression> expressions = new List<DataViewExpression>();
						expressions.Add(new DataViewExpression("A", ModelDifferenceDbStore.UserNamePropertyName));
						IList dataView = objectSpace.CreateDataView(ModelDifferenceDbStore.UserTypeInfo.Type, expressions,
							new BinaryOperator(
								ModelDifferenceDbStore.UserTypeInfo.KeyMember.Name,
								ModelDifferenceDbStore.UserIdTypeConverter.ConvertFromInvariantString(userId)), null);
						if(dataView.Count > 0) {
							Object val = ((XafDataViewRecord)dataView[0])["A"];
							if((val != null) && (val != DBNull.Value)) {
								userName = val.ToString();
							}
						}
					}
					else {
						userName = userId;
					}
				}
				return userName;
			}
		}
		public String ContextId {
			get { return contextId; }
			set { SetPropertyValue(ref contextId, value); }
		}
		[Browsable(false)]
		public Int32 Version {
			get { return version; }
			set { SetPropertyValue(ref version , value); }
		}
		[InverseProperty(nameof(ModelDifferenceAspect.Owner)), Aggregated]
		public virtual IList<ModelDifferenceAspect> Aspects {
			get { return aspects; }
			set { SetReferencePropertyValue(ref aspects, value); }
		}
		IList<IModelDifferenceAspect> IModelDifference.Aspects {
			get { return Aspects.ToList<IModelDifferenceAspect>(); }
		}
		IObjectSpace IObjectSpaceLink.ObjectSpace {
			get { return objectSpace; }
			set { objectSpace = value; }
		}
		#region INotifyPropertyChanged
		private PropertyChangedEventHandler propertyChanged;
		protected bool SetPropertyValue<T>(ref T propertyValue, T newValue, [CallerMemberName]string propertyName = null) where T : struct {
			if(EqualityComparer<T>.Default.Equals(propertyValue, newValue)) {
				return false;
			}
			propertyValue = newValue;
			OnPropertyChanged(propertyName);
			return true;
		}
		protected bool SetPropertyValue<T>(ref T? propertyValue, T? newValue, [CallerMemberName]string propertyName = null) where T : struct {
			if(EqualityComparer<T?>.Default.Equals(propertyValue, newValue)) {
				return false;
			}
			propertyValue = newValue;
			OnPropertyChanged(propertyName);
			return true;
		}
		protected bool SetPropertyValue(ref string propertyValue, string newValue, [CallerMemberName]string propertyName = null) {
			if(propertyValue == newValue) {
				return false;
			}
			propertyValue = newValue;
			OnPropertyChanged(propertyName);
			return true;
		}
		protected bool SetReferencePropertyValue<T>(ref T propertyValue, T newValue, [CallerMemberName]string propertyName = null) where T : class {
			if(propertyValue == newValue) {
				return false;
			}
			propertyValue = newValue;
			OnPropertyChanged(propertyName);
			return true;
		}
		private void OnPropertyChanged(string propertyName) {
			if(propertyChanged != null) {
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged {
			add { propertyChanged += value; }
			remove { propertyChanged -= value; }
		}
		#endregion
	}
}
