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
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
namespace LgsLib.Base {
	[ImageName("ModelEditor_ModelMerge")]
	[DefaultProperty(nameof(ModelDifferenceAspect.DisplayName))]
	public class ModelDifferenceAspect : IModelDifferenceAspect, INotifyPropertyChanged, IObjectSpaceLink {
		private Int32 id;
		private String name;
		private String xml;
		private ModelDifference owner;
		private IObjectSpace objectSpace;
		[Key]
		[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
		public Int32 ID {
			get { return id; }
			protected set { id = value; }
		}
		[NotMapped]
		public String DisplayName {
			get {
				return string.IsNullOrEmpty(name) ? CaptionHelper.GetLocalizedText("Texts", "DefaultAspectText", "(Default language)") : name;
			}
		}
		[VisibleInListView(false), VisibleInLookupListView(false)]
		public String Name {
			get { return name; }
			set { SetPropertyValue(ref name, value); }
		}
		[FieldSize(FieldSizeAttribute.Unlimited)]
		public String Xml {
			get { return xml; }
			set { SetPropertyValue(ref xml, value); }
		}
		[RuleRequiredField(null, DefaultContexts.Save)]
		public virtual ModelDifference Owner {
			get { return owner; }
			set { SetReferencePropertyValue(ref owner, value); }
		}
		IModelDifference IModelDifferenceAspect.Owner {
			get { return Owner; }
			set { Owner = value as ModelDifference; }
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
			if((propertyChanged != null) && (objectSpace == null || objectSpace.IsModified || objectSpace.IsDeleting)) {
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
