using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Runtime.CompilerServices;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
 
namespace LgsLib.Base.StateLGS
{
	[Table("StateMachineAppearance")]
	[DisplayName("StateMachine Appearance")]
	[ImageName("BO_Appearance")]
	[Appearance("StateMachineAppearance.AppearanceForAction", TargetItems = "BackColor; FontColor; FontStyle", Enabled = false, Criteria = "AppearanceItemType='Action'")]
	public class StateMachineAppearance : IAppearanceRuleProperties, INotifyPropertyChanged {
		private Int32 id;
		private String targetItems;
		private String appearanceItemType;
		private String context;
		private Int32 priority;
		private FontStyle? fontStyle;
		private Int32 fontColorInt;
		private Int32 backColorInt;
		private ViewItemVisibility? visibility;
		private Boolean? enabled;
		private String method;
		private StateMachineState state;
		public StateMachineAppearance() {
			AppearanceItemType = "ViewItem";
			Context = "Any";
		}
		[Key]
		[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
		public Int32 ID {
			get { return id; }
			set { id = value; }
		}
		[RuleRequiredField("StateMachineAppearance.TargetItems", DefaultContexts.Save)]
		public String TargetItems {
			get { return targetItems; }
			set { SetPropertyValue(ref targetItems, value); }
		}
		[ImmediatePostData]
		public String AppearanceItemType {
			get { return appearanceItemType; }
			set { SetPropertyValue(ref appearanceItemType, value); }
		}
		[Browsable(false)]
		[FieldSize(FieldSizeAttribute.Unlimited)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public String Criteria {
			get {
				if((State != null) && (State.StateMachine != null) && (State.Marker != null)) {
					return new BinaryOperator(State.StateMachine.StatePropertyName.Name, State.Marker).ToString();
				}
				else {
					return "0=1";
				}
			}
			set { }
		}
		public String Context {
			get { return context; }
			set { SetPropertyValue(ref context, value); }
		}
		[Browsable(false)]
		public Type DeclaringType {
			get {
				if((State != null) && (State.StateMachine != null)) {
					return State.StateMachine.TargetObjectType;
				}
				else {
					return null;
				}
			}
		}
		public Color? FontColor {
			get { return Color.FromArgb(FontColorInt); }
			set { FontColorInt = value.HasValue ? value.Value.ToArgb() : 0; }
		}
		public Color? BackColor {
			get { return Color.FromArgb(BackColorInt); }
			set { BackColorInt = value.HasValue ? value.Value.ToArgb() : 0; }
		}
		public Int32 Priority {
			get { return priority; }
			set { SetPropertyValue(ref priority, value); }
		}
		public FontStyle? FontStyle {
			get { return fontStyle; }
			set { SetPropertyValue(ref fontStyle, value); }
		}
		[Browsable(false)]
		public Int32 FontColorInt {
			get { return fontColorInt; }
			set { SetPropertyValue(ref fontColorInt, value); }
		}
		[Browsable(false)]
		public Int32 BackColorInt {
			get { return backColorInt; }
			set { SetPropertyValue(ref backColorInt, value); }
		}
		public ViewItemVisibility? Visibility {
			get { return visibility; }
			set { SetPropertyValue(ref visibility, value); }
		}
		public Boolean? Enabled {
			get { return enabled; }
			set { SetPropertyValue(ref enabled, value); }
		}
		[Browsable(false)]
		public String Method {
			get { return method; }
			set { SetPropertyValue(ref method, value); }
		}
		[Browsable(false)]
		public virtual StateMachineState State {
			get { return state; }
			set { SetReferencePropertyValue(ref state, value); }
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
