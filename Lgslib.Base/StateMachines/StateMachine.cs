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
using System.Drawing;
using System.Runtime.CompilerServices;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Linq;
using LgsLib.StateMachine;
using System.ComponentModel.DataAnnotations.Schema;
 
using DevExpress.ExpressApp;

namespace LgsLib.Base.StateLGS {
	[DomainComponent]
	public class StringObject : INotifyPropertyChanged {
		private String name;
		public StringObject(String name) {
			Name = name;
		}
		public String Name {
			get { return name; }
			set { SetPropertyValue(ref name, value); }
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
	[Table("StateMachine")]
	[NavigationItem("State Machine")]
	[DefaultProperty(nameof(Name))]
	[DisplayName("StateMachine")]
	[ImageName("BO_StateMachine")]
	//[RuleCriteria("StateMachine.StartState", DefaultContexts.Save, "(Active = false) or ((StartState is not null) and (Active = true))", SkipNullOrEmptyValues = false)]
	public class StateMachine : IStateMachine, IStateMachineUISettings, IObjectSpaceLink, INotifyPropertyChanged {
		private IObjectSpace objectSpace;
		private Type targetObjectType;
		private StringObject statePropertyNameObj;
		private Int32 id;
		private String name;
		private Boolean active;
		private String targetObjectTypeName;
		private String statePropertyNameBase;
		private StateMachineState startState;
		private IList<StateMachineState> states;
		private Boolean expandActionsInDetailView;
		public StateMachine() {
			States = new List<StateMachineState>();
			Active = true;
		}
		[Key]
		[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
		public Int32 ID {
			get { return id; }
			set { id = value; }
		}
		public String Name {
			get { return name; }
			set { SetPropertyValue(ref name, value); }
		}
		public Boolean Active {
			get { return active; }
			set { SetPropertyValue(ref active, value); }
		}
		[Browsable(false)]
		public String TargetObjectTypeName {
			get { return targetObjectTypeName; }
			set { SetPropertyValue(ref targetObjectTypeName, value); }
		}
		[NotMapped]
		[ImmediatePostData]
		[TypeConverter(typeof(StateMachineTypeConverter))]
		[RuleRequiredField("StateMachine.TargetObjectType", DefaultContexts.Save)]
		public Type TargetObjectType {
			get {
				if((targetObjectType == null) && !String.IsNullOrWhiteSpace(TargetObjectTypeName)) {
					ITypeInfo typeInfo = XafTypesInfo.Instance.FindTypeInfo(TargetObjectTypeName);
					if(typeInfo != null) {
						targetObjectType = typeInfo.Type;
					}
				}
				return targetObjectType;
			}
			set {
				targetObjectType = value;
				if(targetObjectType != null) {
					TargetObjectTypeName = targetObjectType.FullName;
				}
				else {
					TargetObjectTypeName = "";
				}
			}
		}
		[Browsable(false)]
		[RuleRequiredField("StateMachine.StatePropertyName", DefaultContexts.Save)]
		public String StatePropertyNameBase {
			get { return statePropertyNameBase; }
			set { SetPropertyValue(ref statePropertyNameBase, value); }
		}
		[NotMapped]
		[ImmediatePostData]
		[DisplayName("State Property Name")]
		[DataSourceProperty(nameof(AvailableStatePropertyNames))]
		public StringObject StatePropertyName {
			get {
				if((statePropertyNameObj == null) && !String.IsNullOrWhiteSpace(StatePropertyNameBase)) {
					statePropertyNameObj = new StringObject(StatePropertyNameBase);
				}
				return statePropertyNameObj;
			}
			set {
				statePropertyNameObj = value;
				if(statePropertyNameObj != null) {
					StatePropertyNameBase = statePropertyNameObj.Name;
				}
				else {
					StatePropertyNameBase = "";
				}
			}
		}
		[NotMapped]
		[Browsable(false)]
		public IList<StringObject> AvailableStatePropertyNames {
			get {
				List<StringObject> result = new List<StringObject>();
				if(TargetObjectType != null) {
					foreach(String availableStatePropertyName in new StateMachineLogic(objectSpace).FindAvailableStatePropertyNames(TargetObjectType)) {
						if((StatePropertyName != null) && (StatePropertyName.Name == availableStatePropertyName)) {
							result.Add(StatePropertyName);
						}
						else {
							result.Add(new StringObject(availableStatePropertyName));
						}
					}
				}
				return result;
			}
		}
		[DataSourceProperty(nameof(States))]
		public virtual StateMachineState StartState {
			get { return startState; }
			set { SetReferencePropertyValue(ref startState, value); }
		}
		[Aggregated]
		[InverseProperty(nameof(StateMachineState.StateMachine))]
		[RuleUniqueValue("StateMachine.UniqueStateMarker", DefaultContexts.Save, TargetPropertyName = "MarkerValue")]
		public virtual IList<StateMachineState> States {
			get { return states; }
			set { SetReferencePropertyValue(ref states, value); }
		}
		public Boolean ExpandActionsInDetailView {
			get { return expandActionsInDetailView; }
			set { SetPropertyValue(ref expandActionsInDetailView, value); }
		}
		public IState FindCurrentState(Object targetObject) {
			return new StateMachineLogic(objectSpace).FindCurrentState(this, targetObject, StartState);
		}
		public void ExecuteTransition(Object targetObject, IState targetState) {
			new StateMachineLogic(objectSpace).ExecuteTransition(targetObject, targetState);
		}
		IList<IState> IStateMachine.States {
			get { return States.ToList<IState>(); }
		}
		string IStateMachine.StatePropertyName {
			get { return StatePropertyName != null ? StatePropertyName.Name : ""; }
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
			//if(propertyChanged != null) {
			//	propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			//}
		}
		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged {
			add { propertyChanged += value; }
			remove { propertyChanged -= value; }
		}
		#endregion
	}
}
