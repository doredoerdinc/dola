 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;

using LgsLib.StateMachine;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;

namespace LgsLib.Base.StateLGS
{
	[DefaultProperty(nameof(Caption))]
	[DisplayName("StateMachine State")]
	[ImageName("BO_State")]
	[Table("StateMachineState")]
	[RuleIsReferenced("StateIsReferenced", DefaultContexts.Delete, typeof(StateMachineTransition), "TargetState", InvertResult = true, MessageTemplateMustBeReferenced = "If you want to delete this State, you must be sure it is not referenced in any Transition as TargetState.", FoundObjectMessageFormat = "{0:SourceState.Caption}")]
	public class StateMachineState : IState, IStateAppearancesProvider, IObjectSpaceLink, INotifyPropertyChanging, INotifyPropertyChanged, IMasterObjectInitializer {
		private Int32 id;
		private StateMachine stateMachine;
		private String caption;
		private String markerValue;
		private String targetObjectCriteria;
		private IList<StateMachineTransition> transitions;
		private IList<StateMachineAppearance> appearances;
		public StateMachineState() {
			Transitions = new List<StateMachineTransition>();
			Appearances = new List<StateMachineAppearance>();
		}
		[Key]
		[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
		public Int32 ID {
			get { return id; }
			set { id = value; }
		}
		public virtual StateMachine StateMachine {
			get { return stateMachine; }
			set { SetReferencePropertyValue(ref stateMachine, value); }
		}
		[RuleRequiredField("StateMachineState.Caption", DefaultContexts.Save)]
		public String Caption {
			get { return caption; }
			set { SetPropertyValue(ref caption, value); }
		}
		[Browsable(false)]
		public String MarkerValue {
			get { return markerValue; }
			set { SetPropertyValue(ref markerValue, value); }
		}
		[NotMapped]
		[ImmediatePostData]
		[DataSourceProperty(nameof(AvailableMarkerObjects))]
		public MarkerObject Marker {
			get {
				return new StateMachineLogic(ObjectSpace).GetMarkerObjectFromMarkerValue(MarkerValue, this, ObjectSpace);
			}
			set {
				MarkerValue = new StateMachineLogic(ObjectSpace).GetMarkerValueFromMarkerObject(value, this, ObjectSpace);
			}
		}
		[FieldSize(FieldSizeAttribute.Unlimited)]
		[CriteriaOptions("StateMachine.TargetObjectType")]
		public String TargetObjectCriteria {
			get { return targetObjectCriteria; }
			set { SetPropertyValue(ref targetObjectCriteria, value); }
		} 

        [Aggregated]
		[InverseProperty(nameof(StateMachineTransition.SourceState))]
		public virtual IList<StateMachineTransition> Transitions {
			get { return transitions; }
			set { SetReferencePropertyValue(ref transitions, value); }
		}
		[Aggregated]
		public virtual IList<StateMachineAppearance> Appearances {
			get { return appearances; }
			set { SetReferencePropertyValue(ref appearances, value); }
		}
 
		IList<MarkerObject> _AvailableMarkerObjects;
		[Browsable(false)]
		[NotMapped]
		public IList<MarkerObject> AvailableMarkerObjects
		{
			get
			{
				if (_AvailableMarkerObjects == null)
				{
					_AvailableMarkerObjects = new StateMachineLogic(ObjectSpace).GetAvailableMarkerObjects(this, ObjectSpace);
				}
				return _AvailableMarkerObjects;
			}
			set { _AvailableMarkerObjects = value; }
		}

		IStateMachine IState.StateMachine {
			get { return StateMachine; }
		}
		Object IState.Marker {
			get { return (Marker != null) ? Marker.Marker : null; }
		}
		IList<ITransition> IState.Transitions {
			get { return Transitions.ToList<ITransition>(); }
		}
		IList<IAppearanceRuleProperties> IStateAppearancesProvider.Appearances {
			get { return Appearances.ToList<IAppearanceRuleProperties>(); }
		}
		IObjectSpace _ObjectSpace;
		[NotMapped]
		[Browsable(false)]
		public IObjectSpace ObjectSpace
		{
			get { return _ObjectSpace; }
			set { _ObjectSpace = value; }
		}
		#region IMasterObjectInitializer Members
		void IMasterObjectInitializer.SetMasterObject(object masterObject) {
			StateMachine stateMachine = masterObject as StateMachine;
			if(stateMachine != null) {
				StateMachine = stateMachine;
			}
		}
		#endregion
		#region INotifyPropertyChanging & INotifyPropertyChanged
		private PropertyChangingEventHandler propertyChanging;
		private PropertyChangedEventHandler propertyChanged;
		protected bool SetPropertyValue<T>(ref T propertyValue, T newValue, [CallerMemberName]string propertyName = null) where T : struct {
			if(EqualityComparer<T>.Default.Equals(propertyValue, newValue)) {
				return false;
			}
			OnPropertyChanging(propertyName);
			propertyValue = newValue;
			OnPropertyChanged(propertyName);
			return true;
		}
		protected bool SetPropertyValue<T>(ref T? propertyValue, T? newValue, [CallerMemberName]string propertyName = null) where T : struct {
			if(EqualityComparer<T?>.Default.Equals(propertyValue, newValue)) {
				return false;
			}
			OnPropertyChanging(propertyName);
			propertyValue = newValue;
			OnPropertyChanged(propertyName);
			return true;
		}
		protected bool SetPropertyValue(ref string propertyValue, string newValue, [CallerMemberName]string propertyName = null) {
			if(propertyValue == newValue) {
				return false;
			}
			OnPropertyChanging(propertyName);
			propertyValue = newValue;
			OnPropertyChanged(propertyName);
			return true;
		}
		protected bool SetReferencePropertyValue<T>(ref T propertyValue, T newValue, [CallerMemberName]string propertyName = null) where T : class {
			if(propertyValue == newValue) {
				return false;
			}
			OnPropertyChanging(propertyName);
			propertyValue = newValue;
			OnPropertyChanged(propertyName);
			return true;
		}
		private void OnPropertyChanging(string propertyName) {
			//if(propertyChanging != null) {
			//	propertyChanging(this, new PropertyChangingEventArgs(propertyName));
			//}
		}
		private void OnPropertyChanged(string propertyName) {
			//if(propertyChanged != null) {
			//	propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			//}
		}
		event PropertyChangingEventHandler INotifyPropertyChanging.PropertyChanging {
			add { propertyChanging += value; }
			remove { propertyChanging -= value; }
		}
		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged {
			add { propertyChanged += value; }
			remove { propertyChanged -= value; }
		}
		#endregion
	}
}
