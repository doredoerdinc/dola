#region Copyright (c) 2000-2020 Developer Express Inc.
/*
{*******************************************************************}
{                                                                   }
{       Developer Express .NET Component Library                    }
{                                                                   }
{                                                                   }
{       Copyright (c) 2000-2020 Developer Express Inc.              }
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
#endregion Copyright (c) 2000-2020 Developer Express Inc.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Localization;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
namespace LgsLib.StateMachine {
	public class StateMachineCacheController : ViewController {
		protected Type stateMachineStorageType;
		protected List<IStateMachine> cache = new List<IStateMachine>();
		protected bool isCompleteCache = false;
		private bool isLoading;
		public StateMachineCacheController() : base() { }
		public ReadOnlyCollection<IStateMachine> CachedStateMachines {
			get {
				return cache.AsReadOnly();
			}
		}
		public void ClearCache() {
			cache.Clear();
			isCompleteCache = false;
		}
		private void EnsureCache() {
			if(isLoading) {
				return;
			}
			isLoading = true;
			try {
				if(!isCompleteCache) {
					if(stateMachineStorageType == null) {
						stateMachineStorageType = ((StateMachineModule)Application.Modules.FindModule(typeof(StateMachineModule))).StateMachineStorageType;
					}
                    if (ObjectSpace.CanInstantiate(stateMachineStorageType))
                    {
                        IList stateMachines = ObjectSpace.GetObjects(stateMachineStorageType, null);
                        if (stateMachines != null)
                        {
                            foreach (object stateMachineObject in stateMachines)
                            {
                                IStateMachine stateMachine = (IStateMachine)stateMachineObject;
                                cache.Add(stateMachine);
                            }
                            isCompleteCache = true;
                        }
                    }
                }
			}
			finally {
				isLoading = false;
			}
		}
		public virtual IList<IStateMachine> GetStateMachinesByType(Type targetObjectType) {
			EnsureCache();
			List<IStateMachine> result = new List<IStateMachine>();
			foreach(IStateMachine stateMachine in cache) {
				if(stateMachine.Active && stateMachine.TargetObjectType.IsAssignableFrom(targetObjectType)) {
					result.Add(stateMachine);
				}
			}
			return result;
		}
		protected override void OnActivated() {
			base.OnActivated();
			ObjectSpace.Reloaded += new EventHandler(ObjectSpace_Reloaded);  
            //controller.CanCloseWindow = false;   
        } 

        protected override void OnDeactivated() {
			ObjectSpace.Reloaded -= new EventHandler(ObjectSpace_Reloaded);
			base.OnDeactivated();
			ClearCache();
		}
		private void ObjectSpace_Reloaded(object sender, EventArgs e) {
			ClearCache();
		}
	}
	public abstract class StateMachineControllerBase<T> : ViewController<T> where T : ObjectView {
		protected bool IsIStateMachineProviderView {
			get {
				return ((View != null) && (View.ObjectTypeInfo != null) && typeof(IStateMachineProvider).IsAssignableFrom(View.ObjectTypeInfo.Type));
			}
		}
		protected object FindSampleTargetObject() { 
			Object selectedObject = (View.SelectedObjects.Count == 1) ? View.SelectedObjects[0] : null;
			if(IsIStateMachineProviderView) { 
				ListView listView = View as ListView;
				if(selectedObject != null) {
					selectedObject = GetTypedObject(selectedObject);
				}
				else if((selectedObject == null) 
					&& (listView != null)
					&& (listView.CollectionSource != null) 
					&& (!listView.CollectionSource.IsServerMode || (listView.CollectionSource.IsServerMode && listView.CollectionSource.IsLoaded))
					&& (listView.CollectionSource.List != null)
					&& (listView.CollectionSource.List.Count > 0)) {
					selectedObject = listView.CollectionSource.List[0];
				}
			}
			return selectedObject;
		}
		protected Object GetTypedObject(object obj) {
			if(obj is IObjectRecord) {
				return ObjectSpace.GetObject(obj);
			}
			return obj;
		}
		protected void OnGetStateMachines(StateMachinesEventArgs args) {
			if(GetStateMachinesByType != null) {
				GetStateMachinesByType(this, args);
			}
		}
		protected StateMachineCacheController StateMachineCacheController {
			get {
				if(Frame != null) {
					return Frame.GetController<StateMachineCacheController>();
				}
				return null;
			}
		}
		protected IList<IStateMachine> GetStateMachines() {
			List<IStateMachine> stateMachines = new List<IStateMachine>();
			if(View != null) {
				object sampleTargetObject = FindSampleTargetObject();
				if(sampleTargetObject is IStateMachineProvider) {
					foreach(IStateMachine stateMachine in ((IStateMachineProvider)(sampleTargetObject)).GetStateMachines()) {
						if(stateMachine.Active) {
							stateMachines.Add(stateMachine);
						}
					}
				}
				Type currentObjectType = ((sampleTargetObject != null) && !(sampleTargetObject is IObjectRecord)) ? sampleTargetObject.GetType() : View.ObjectTypeInfo.Type;
				StateMachinesEventArgs stateMachinesEventArgs = new StateMachinesEventArgs(currentObjectType);
				OnGetStateMachines(stateMachinesEventArgs);
				if(stateMachinesEventArgs.Handled) {
					stateMachines.AddRange(stateMachinesEventArgs.StateMachines);
				}
				else {
					StateMachineCacheController stateMachineCacheController = StateMachineCacheController;
					if(stateMachineCacheController != null) {
						stateMachines.AddRange(stateMachineCacheController.GetStateMachinesByType(currentObjectType));
					}
				}
			}
			return stateMachines;
		}
		public event EventHandler<StateMachinesEventArgs> GetStateMachinesByType;
	}
	public enum ChangeStateActionItemsMode { GroupByStateMachine, FlatList }
	public class AvailableStateMachineEntry {
		public AvailableStateMachineEntry(IStateMachine stateMachine, IEnumerable<ITransition> transitions, bool enabled) {
			this.StateMachine = stateMachine;
			this.Transitions = transitions;
			this.Enabled = enabled;
		}
		public IStateMachine StateMachine { get; private set; }
		public bool Enabled { get; set; } 
		public IEnumerable<ITransition> Transitions { get; private set; }
	}
	public class CustomGetAvailableTransitionsEventArgs : HandledEventArgs {
		public CustomGetAvailableTransitionsEventArgs() {
			AvailableStateMachineEntries = new AvailableStateMachineEntry[] { };
		}
		public IEnumerable<AvailableStateMachineEntry> AvailableStateMachineEntries { get; set; }
	}
	public class StateMachineController : StateMachineControllerBase<ObjectView> {
		private const string EditModeKey = "ViewIsInEditMode";
		private const string SecurityKey = "EnabledBySecurity";
		private const ChangeStateActionItemsMode ChangeStateActionItemsModeDefaultValue = ChangeStateActionItemsMode.FlatList;
		private SingleChoiceAction changeStateAction;
		private Dictionary<object, List<SimpleAction>> panelActions = new Dictionary<object, List<SimpleAction>>();
		private void View_SelectionChanged(object sender, EventArgs e) {
			UpdateActionState();
		}
		private void changeStateAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e) {
			ITransition transition = e.SelectedChoiceActionItem.Data as ITransition;
			if(transition != null) {
				
				if(Frame != null) {

                    if (transition.BeforeAction != null)
                    {
                        foreach (var action in transition.BeforeAction.Split(','))
                        {
                            var execAction = GetActions().SingleOrDefault(x => x.Model.Id == action);
                            if (execAction != null)
                            {

                                var simpleAction = execAction as SimpleAction;
                                execAction.Active["ActionActivePassive"] = true;
                                simpleAction.DoExecute();
                                execAction.Active["ActionActivePassive"] = false;

                            }
                            else
                            {
                                throw new ArgumentException(string.Format("Seçili Before Action Bulunamadı ActionId={0}", transition.BeforeAction));
                            }

                        }
						
					}
                    ExecuteTransition(e.CurrentObject, transition);
                    UpdateActionState(); 
				}
			}
		}

        private void SimpleAction_Changed(object sender, ActionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SimpleAction_CustomizeControl(object sender, CustomizeControlEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SimpleAction_ProcessCreatedView(object sender, ActionBaseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SimpleAction_Disposing(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SimpleAction_Disposed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SimpleAction_Executed(object sender, ActionBaseEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void SimpleAction_ExecuteCanceled(object sender, ActionBaseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SimpleAction_ExecuteCompleted(object sender, ActionBaseEventArgs e)
        { 
        }

        private void SimpleAction_Execute(object sender, SimpleActionExecuteEventArgs e) {
			ChoiceActionItem transitionItem = e.Action.Tag as ChoiceActionItem;
			if(transitionItem != null) {
				changeStateAction.DoExecute(transitionItem);
			}
		}

		public IEnumerable<ActionBase> GetActions()
		{
			var controllers = Frame.Controllers.Values as System.Collections.Generic.List<DevExpress.ExpressApp.Controller>;
			return controllers.SelectMany(x => x.Actions.AsEnumerable());
		}
		private int TransitionsComparison(ITransition t1, ITransition t2) {
			ITransitionUISettings tui1 = t1 as ITransitionUISettings;
			ITransitionUISettings tui2 = t2 as ITransitionUISettings;
			if(tui1 == null) {
				if(tui2 == null) {
					return 0;
				}
				else {
					return 1;
				}
			}
			if(tui2 == null) {
				return -1;
			}
			if(originalTransitionsList != null && tui1.Index == 0 && tui2.Index == 0) {
				return originalTransitionsList.IndexOf(t1) - originalTransitionsList.IndexOf(t2);
			}
			return tui1.Index - tui2.Index;
		}
		private IList<ITransition> originalTransitionsList = null;
		private IEnumerable<ITransition> GetOrderedTransitions(IList<ITransition> transitionsList) {
			List<ITransition> sortedTransitionsList = new List<ITransition>(transitionsList);
			originalTransitionsList = transitionsList;
			sortedTransitionsList.Sort(TransitionsComparison);
			originalTransitionsList = null;
			return sortedTransitionsList;
		}
		private void ResetActionsPanel() {
			DetailView detailView = View as DetailView;
			if(detailView != null) {
				foreach(string key in panelActions.Keys) {
					ActionContainerViewItem actionContainer = detailView.FindItem(key) as ActionContainerViewItem;
					if(actionContainer != null) {
						actionContainer.ClearActions();  
					}
				}
				foreach(List<SimpleAction> actionsList in panelActions.Values) {
					foreach(SimpleAction simpleAction in actionsList) {
						simpleAction.Execute -= new SimpleActionExecuteEventHandler(SimpleAction_Execute);
					}
				}
			}
			panelActions.Clear();
		}
		private void InitializeActionsPanel(String stateMachineName, ChoiceActionItemCollection transitionItems) {
			List<SimpleAction> actionsList = new List<SimpleAction>();
			foreach(ChoiceActionItem transitionItem in transitionItems) {
				SimpleAction action = CreateSimpleTransitionAction(transitionItem);
				actionsList.Add(action);
			}
			string actionsContainerId = "StateMachineActionContainer_" + stateMachineName.Replace(" ", "_");
			panelActions[actionsContainerId] = actionsList;
			AddStateMachineActionsContainerToDetailViewLayout((DetailView)View, actionsContainerId, stateMachineName);
		}
		private SimpleAction CreateSimpleTransitionAction(ChoiceActionItem transitionItem) {
			SimpleAction action = new SimpleAction(this, Guid.NewGuid().ToString(), "StateMachineActions");
			action.Enabled[EditModeKey] = ((DetailView)View).ViewEditMode == ViewEditMode.Edit;
			foreach(string key in transitionItem.Active.GetKeys()) {
				action.Enabled[key] = transitionItem.Active[key];
			}
			action.Tag = transitionItem;
			action.Caption = transitionItem.Caption;
			action.Execute += new SimpleActionExecuteEventHandler(SimpleAction_Execute);
			return action;
		}
		private void AddStateMachineActionsContainerToDetailViewLayout(DetailView detailView, string actionsContainerId, string caption) {
			IModelActionContainerViewItem modelActionContainerViewItem = (IModelActionContainerViewItem)detailView.Model.Items[actionsContainerId];
			if(modelActionContainerViewItem == null) {
				modelActionContainerViewItem = detailView.Model.Items.AddNode<IModelActionContainerViewItem>(actionsContainerId);
				ModelApplicationBase modelApplication = (ModelApplicationBase)detailView.Model.Application;
				string currentAspect = modelApplication.CurrentAspect;
				modelApplication.SetCurrentAspect("");
				modelActionContainerViewItem.Caption = caption;
				modelApplication.SetCurrentAspect(currentAspect);
				IModelViewLayoutElement rootLayoutElement = detailView.Model.Layout.Count > 0 ? detailView.Model.Layout[0] : null;
				IModelLayoutViewItem containerLayoutItem = null;
				if(rootLayoutElement is IModelLayoutGroup) {
					containerLayoutItem = ((IModelLayoutGroup)rootLayoutElement).AddNode<IModelLayoutViewItem>(modelActionContainerViewItem.Id);
				}
				else {
					containerLayoutItem = detailView.Model.Layout.AddNode<IModelLayoutViewItem>(modelActionContainerViewItem.Id);
				}
				containerLayoutItem.ViewItem = modelActionContainerViewItem;
				containerLayoutItem.ShowCaption = true;
				detailView.AddItem(modelActionContainerViewItem);
			}
		}
		private void detailView_ViewEditModeChanged(object sender, EventArgs e) {
			UpdateActionState();
		}
		private void View_ModelChanged(object sender, EventArgs e) {
			UpdateActionState();
		}
		private void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e) {
			List<string> stateProperties = new List<string>();
			foreach(IStateMachine stateMachine in GetStateMachines()) {
				stateProperties.Add(stateMachine.StatePropertyName);
			}
			if(stateProperties.Count > 0 && e.Object == View.CurrentObject && stateProperties.Contains(e.PropertyName)) {
				UpdateActionState();
			}
		}
		private void View_ControlsCreated(object sender, EventArgs e) {
			DetailView detailView = View as DetailView;
			if(detailView != null) {
				RegisterActionsInPanelContainers(detailView);
			}
		}
		private void ObjectSpace_Committing(Object sender, CancelEventArgs e) {
			foreach(Object obj in View.ObjectSpace.ModifiedObjects) {
				IStateMachine stateMachine = obj as IStateMachine;
				if((stateMachine != null) && (stateMachine.TargetObjectType != null) && !String.IsNullOrWhiteSpace(stateMachine.StatePropertyName)) {
					ITypeInfo typeInfo = Application.TypesInfo.FindTypeInfo(stateMachine.TargetObjectType);
					if((typeInfo != null) && (typeInfo.FindMember(stateMachine.StatePropertyName) == null)) {
						String message = SystemExceptionLocalizer.GetExceptionMessage(
							ExceptionId.CannotFindThePropertyWithinTheClass, stateMachine.StatePropertyName, stateMachine.TargetObjectType);
						throw new UserFriendlyException(message);
					}
				}
			}
		}
		private IEnumerable<AvailableStateMachineEntry> GetAvailableTransitions() {
			CustomGetAvailableTransitionsEventArgs args = new CustomGetAvailableTransitionsEventArgs();
			if(CustomGetAvailableTransitions != null) {
				CustomGetAvailableTransitions(this, args);
			}
			if(args.Handled && args.AvailableStateMachineEntries != null) {
				return args.AvailableStateMachineEntries;
			}
			else {
				List<AvailableStateMachineEntry> result = new List<AvailableStateMachineEntry>();
				Object viewCurrentObject = (View != null && View.SelectedObjects != null && View.SelectedObjects.Count == 1) ? View.SelectedObjects[0] : null;
				if(viewCurrentObject != null) {
					IList<IStateMachine> stateMachines = GetStateMachines();
					Object viewCurrentTypedObject = null;
					if(stateMachines.Count > 0) {
						viewCurrentTypedObject = GetTypedObject(viewCurrentObject);
					}
					if(viewCurrentTypedObject != null) {
						foreach(IStateMachine stateMachine in stateMachines) {
							bool isTransitionEnabled = DataManipulationRight.CanEdit(stateMachine.TargetObjectType, stateMachine.StatePropertyName, viewCurrentTypedObject, null, View.ObjectSpace);
							IState currentState = stateMachine.FindCurrentState(viewCurrentTypedObject);
							if((currentState != null) && (currentState.Transitions.Count > 0)) {
								result.Add(new AvailableStateMachineEntry(stateMachine, GetOrderedTransitions(currentState.Transitions), isTransitionEnabled));
							}
						}
					}
				}
				return result;
			}
		}
		protected virtual ChoiceActionItem CreateTransitionItem(IStateMachine stateMachine, ITransition transition) {
			string itemId = string.Concat(stateMachine.Name, ".", transition.Caption);
			return new ChoiceActionItem(itemId, transition.Caption, transition);
		}
		private void RefreshChangeStateActionItems() {
			IEnumerable<AvailableStateMachineEntry> availableTransitions = GetAvailableTransitions();
			foreach(AvailableStateMachineEntry availableTransitionsEntry in availableTransitions) {
				ChoiceActionItemCollection stateMachineItems = null;
				IStateMachine stateMachine = availableTransitionsEntry.StateMachine;
				if(ChangeStateActionItemsMode == ChangeStateActionItemsMode.GroupByStateMachine) {
					ChoiceActionItem machineActionItem = new ChoiceActionItem(stateMachine.Name, stateMachine.Name, stateMachine);
					machineActionItem.Enabled[SecurityKey] = availableTransitionsEntry.Enabled;
					changeStateAction.Items.Add(machineActionItem);
					foreach(ITransition transition in availableTransitionsEntry.Transitions) {
						machineActionItem.Items.Add(CreateTransitionItem(stateMachine, transition));
					}
					stateMachineItems = machineActionItem.Items;
				}
				else {
					stateMachineItems = new ChoiceActionItemCollection();
					bool isFirstEntry = true;
					foreach(ITransition transition in availableTransitionsEntry.Transitions) {
						ChoiceActionItem changeStateItem = CreateTransitionItem(availableTransitionsEntry.StateMachine, transition);
						changeStateItem.Active[SecurityKey] = availableTransitionsEntry.Enabled;
						if(isFirstEntry) {
							changeStateItem.BeginGroup = isFirstEntry;
							isFirstEntry = false;
						}
						stateMachineItems.Add(changeStateItem);
						changeStateAction.Items.Add(changeStateItem);
					}
				}
				if(View is DetailView && (stateMachine is IStateMachineUISettings) && ((IStateMachineUISettings)stateMachine).ExpandActionsInDetailView) {
					InitializeActionsPanel(stateMachine.Name, stateMachineItems);
				}
			}
		}
		private void RefreshActionsPanel(DetailView detailView) {
			if(detailView != null) {
				changeStateAction.Enabled[EditModeKey] = detailView.ViewEditMode == ViewEditMode.Edit;
				UpdatePanelActions();
				bool allContainersAreOnTheView = true;
				foreach(string key in panelActions.Keys) {
					ActionContainerViewItem actionContainer = detailView.FindItem(key) as ActionContainerViewItem;
					if(actionContainer == null) {
						allContainersAreOnTheView = false;
						break;
					}
				}
				if(allContainersAreOnTheView) {
					RegisterActionsInPanelContainers(detailView);
				}
				else if(detailView.IsControlCreated) {
					View.BreakLinksToControls();
					View.LoadModel();
					View.CreateControls();
				}
			}
			else {
				changeStateAction.Enabled.RemoveItem(EditModeKey);
			}
		}
		private void StateMachineCacheController_Activated(object sender, EventArgs e) {
			UpdateActionState();
		}
		private void RegisterActionsInPanelContainers(DetailView detailView) {
			foreach(string key in panelActions.Keys) {
				ActionContainerViewItem actionContainer = detailView.FindItem(key) as ActionContainerViewItem;
				if(actionContainer != null) {
					actionContainer.ClearActions();  
					foreach(SimpleAction action in panelActions[key]) {
						actionContainer.Register(action);
					}
				}
			}
		}
		private void SubscribeChangeStateActionChanged() {
			if(View is DetailView && changeStateAction.LockCount == 0) {
				changeStateAction.Changed -= new EventHandler<ActionChangedEventArgs>(stateMachineAction_Changed);
				changeStateAction.Changed += new EventHandler<ActionChangedEventArgs>(stateMachineAction_Changed);
			}
		}
		private void UnsubscribeChangeStateActionChanged() {
			changeStateAction.Changed -= new EventHandler<ActionChangedEventArgs>(stateMachineAction_Changed);
		}
		private void stateMachineAction_Changed(object sender, ActionChangedEventArgs e) {
			if(e.ChangedPropertyType == ActionChangedType.Active || e.ChangedPropertyType == ActionChangedType.Enabled) {
				UpdatePanelActions();
			}
		}
		private void UpdatePanelActions() {
			if(View is DetailView) {
				foreach(List<SimpleAction> actionsList in panelActions.Values) {
					foreach(SimpleAction simpleAction in actionsList) {
						simpleAction.Enabled["StateMachineActionEnabled"] = changeStateAction.Enabled;
						simpleAction.Active["StateMachineActionActive"] = changeStateAction.Active;
					}
				}
			}
		}
		protected internal virtual void UpdateActionState() {
			StateMachineCacheController stateMachineCacheController = StateMachineCacheController;
			if(stateMachineCacheController != null && !stateMachineCacheController.Active) {
				stateMachineCacheController.Activated += new EventHandler(StateMachineCacheController_Activated);
			}
			else {
				ISupportUpdate updatable = null;
				try {
					DetailView detailView = View as DetailView;
					UnsubscribeChangeStateActionChanged();
					changeStateAction.Items.Clear();
					if((detailView != null) && (detailView.LayoutManager != null)) {
						updatable = detailView.LayoutManager.Container as ISupportUpdate;
						if(updatable != null) {
							updatable.BeginUpdate();
						}
						ResetActionsPanel();
					}
					RefreshChangeStateActionItems();
					RefreshActionsPanel(detailView);
					if(ActionStateUpdated != null) {
						ActionStateUpdated(this, EventArgs.Empty);
					}
				}
				finally {
					if(updatable != null) {
						updatable.EndUpdate();
					}
					SubscribeChangeStateActionChanged();
				}
			}
		}
		protected override void BeginUpdate() {
			base.BeginUpdate();
			UnsubscribeChangeStateActionChanged();
		}
		protected override void EndUpdate() {
			base.EndUpdate();
			SubscribeChangeStateActionChanged();
			UpdatePanelActions();
		}
		protected internal void ExecuteTransition(object targetObject, ITransition transition) {
			targetObject = GetTypedObject(targetObject);
			ExecuteTransitionEventArgs args = new ExecuteTransitionEventArgs(targetObject, transition);
			OnStateMachineTransitionExecuting(args);
			if(!args.Cancel) {
				transition.TargetState.StateMachine.ExecuteTransition(targetObject, transition.TargetState);
				ObjectSpace.SetModified(targetObject);
				OnStateMachineTransitionExecuted(args);
				ITransitionUISettings transitionSettings = transition as ITransitionUISettings;
				if(View is DetailView && transitionSettings.SaveAndCloseView) {
					View.ObjectSpace.CommitChanges();
					View.Close();
				}
				else if(View is ListView && Frame != null) {
					//ModificationsController autocommitController = Frame.GetController<ModificationsController>();
					//if((autocommitController != null) && (autocommitController.ModificationsHandlingMode == ModificationsHandlingMode.AutoCommit)) {
						View.ObjectSpace.CommitChanges();
						ListView listView = (ListView)View;
						if(DataAccessModeHelper.IsLightMode(listView.CollectionSource.DataAccessMode)) {
							listView.CollectionSource.Reload();
						}
					//}
				}
			}
		}
		protected virtual void OnStateMachineTransitionExecuting(ExecuteTransitionEventArgs args) {
			if(TransitionExecuting != null) {
				TransitionExecuting(this, args);
			}
		}
		protected virtual void OnStateMachineTransitionExecuted(ExecuteTransitionEventArgs args) {
			if(TransitionExecuted != null) {
				TransitionExecuted(this, args);
			}
		}
		protected override void OnActivated() {
			base.OnActivated();
			View.SelectionChanged += new EventHandler(View_SelectionChanged);
			View.ControlsCreated += new EventHandler(View_ControlsCreated);
			View.ModelChanged += new EventHandler(View_ModelChanged);
			View.ObjectSpace.ObjectChanged += new EventHandler<ObjectChangedEventArgs>(ObjectSpace_ObjectChanged);
			View.ObjectSpace.Committing += ObjectSpace_Committing;
			DetailView detailView = View as DetailView;
			if(detailView != null) {
				detailView.ViewEditModeChanged += new EventHandler<EventArgs>(detailView_ViewEditModeChanged);
			}
			UpdateActionState();
		}
		protected override void OnDeactivated() {
			DetailView detailView = View as DetailView;
			if(detailView != null) {
				detailView.ViewEditModeChanged -= new EventHandler<EventArgs>(detailView_ViewEditModeChanged);
			}
			View.ObjectSpace.ObjectChanged -= new EventHandler<ObjectChangedEventArgs>(ObjectSpace_ObjectChanged);
			View.ObjectSpace.Committing -= ObjectSpace_Committing;
			View.ModelChanged -= new EventHandler(View_ModelChanged);
			View.ControlsCreated -= new EventHandler(View_ControlsCreated);
			View.SelectionChanged -= new EventHandler(View_SelectionChanged);
			StateMachineCacheController stateMachineCacheController = StateMachineCacheController;
			if(stateMachineCacheController != null) {
				stateMachineCacheController.Activated -= new EventHandler(StateMachineCacheController_Activated);
			}
			base.OnDeactivated();
		}
		public StateMachineController() {
			changeStateAction = new SingleChoiceAction(this, "ChangeStateAction", PredefinedCategory.Edit);
			changeStateAction.ImageName = "Action_StateMachine";
			changeStateAction.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
			changeStateAction.Caption = "Change State";
			changeStateAction.ToolTip = "Change state of the current object";
			changeStateAction.ItemType = SingleChoiceActionItemType.ItemIsOperation;
			changeStateAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
			changeStateAction.Execute += new SingleChoiceActionExecuteEventHandler(changeStateAction_Execute);
			SubscribeChangeStateActionChanged();
			ChangeStateActionItemsMode = ChangeStateActionItemsModeDefaultValue;
		}
		public SingleChoiceAction ChangeStateAction {
			get { return changeStateAction; }
		}
		[DefaultValue(ChangeStateActionItemsModeDefaultValue)]
		public ChangeStateActionItemsMode ChangeStateActionItemsMode { get; set; }
		public event EventHandler<ExecuteTransitionEventArgs> TransitionExecuting;
		public event EventHandler<ExecuteTransitionEventArgs> TransitionExecuted;
		public event EventHandler<EventArgs> ActionStateUpdated;
		public event EventHandler<CustomGetAvailableTransitionsEventArgs> CustomGetAvailableTransitions;
	}
	public class StateMachinesEventArgs : HandledEventArgs {
		Type targetObjectType;
		private List<IStateMachine> _stateMachines = new List<IStateMachine>();
		public StateMachinesEventArgs(Type targetObjectType)
			: base(false) {
			this.targetObjectType = targetObjectType;
		}
		public void Add(IStateMachine stateMachine) {
			_stateMachines.Add(stateMachine);
		}
		public void AddRange(IEnumerable<IStateMachine> stateMachines) {
			_stateMachines.AddRange(stateMachines);
		}
		public Type TargetObjectType {
			get {
				return targetObjectType;
			}
		}
		public ReadOnlyCollection<IStateMachine> StateMachines {
			get { return _stateMachines.AsReadOnly(); }
		}
	}
	public class ExecuteTransitionEventArgs : CancelEventArgs {
		public ExecuteTransitionEventArgs(object targetObject, ITransition transition)
			: base(false) {
			Guard.ArgumentNotNull(transition, "transition");
			TargetObject = targetObject;
			Transition = transition;
		}
		public object TargetObject { get; private set; }
		public ITransition Transition { get; private set; }
	}
}
