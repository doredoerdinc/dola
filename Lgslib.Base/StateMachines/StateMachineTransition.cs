
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using LgsLib.StateMachine;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

using System.ComponentModel.DataAnnotations.Schema;
 

namespace LgsLib.Base.StateLGS
{
	[Table("StateMachineTransition")]
	[DefaultProperty(nameof(Caption))]
	[DisplayName("StateMachine Transition")]
	[ImageName("BO_Transition")]
	public class StateMachineTransition : ITransition, ITransitionUISettings, IMasterObjectInitializer, INotifyPropertyChanged {
		private Int32 id;
		private String caption;
		private StateMachineState sourceState;
		private StateMachineState targetState;
		private Int32 index;
		private Boolean saveAndCloseView;
		[Key]
		[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
		public Int32 ID {
			get { return id; }
			set { id = value; }
		}
		public String Caption {
			get {
				String result = caption;
				if(String.IsNullOrEmpty(result) && (TargetState != null)) {
					result = TargetState.Caption;
				}
				return result;
			}
			set { SetPropertyValue(ref caption, value); }
		}
		public virtual StateMachineState SourceState {
			get { return sourceState; }
			set { SetReferencePropertyValue(ref sourceState, value); }
		}
		[ImmediatePostData]
		[DataSourceProperty("SourceState.StateMachine.States")]
		[RuleRequiredField("StateMachineTransition.TargetState", DefaultContexts.Save)]
		public virtual StateMachineState TargetState {
			get { return targetState; }
			set { SetReferencePropertyValue(ref targetState, value); }
		}
		public Int32 Index {
			get { return index; }
			set { SetPropertyValue(ref index, value); }
		}
		public Boolean SaveAndCloseView {
			get { return saveAndCloseView; }
			set { SetPropertyValue(ref saveAndCloseView, value); }
		}
		IState ITransition.TargetState {
			get { return TargetState; }
		}


		string _BeforeAction;
		[DataSourceProperty("Application.ActionDesign.Actions")]
		public string BeforeAction
		{
			get { return _BeforeAction; }
			set { _BeforeAction = value; }
		}


		#region IMasterObjectInitializer Members
		void IMasterObjectInitializer.SetMasterObject(object masterObject) {
			StateMachineState state = masterObject as StateMachineState;
			if(state != null) {
				SourceState = state;
			}
		}
		#endregion
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
