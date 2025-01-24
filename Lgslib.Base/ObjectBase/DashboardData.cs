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
using System.Runtime.CompilerServices;
using DevExpress.Persistent.Base;
namespace LgsLib.Base {
	public class DashboardData : IDashboardData, INotifyPropertyChanged {
		private Int32 id;
		private string content;
		private string title;
		private bool synchronizeTitle;
		[Key]
		[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
		public Int32 ID {
			get { return id; }
			protected set { id = value; }
		}
		[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
		[EditorBrowsable(EditorBrowsableState.Never), MaxLength]
		public string Content {
			get { return content; }
			set { SetPropertyValue(ref content, value); }
		}
		[VisibleInDetailView(false)]
		[VisibleInListView(true)]
		public string Title {
			get { return title; }
			set { SetPropertyValue(ref title, value); }
		}
		[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
		public bool SynchronizeTitle {
			get { return synchronizeTitle; }
			set { SetPropertyValue(ref synchronizeTitle, value); }
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
