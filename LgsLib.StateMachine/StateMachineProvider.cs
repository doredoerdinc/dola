

using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.ExpressApp;

namespace LgsLib.StateMachine {
	public interface IStateMachineProvider {
		IList<IStateMachine> GetStateMachines();
	}
}
