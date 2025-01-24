using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DevExpress.ExpressApp.DC;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp;

using LgsLib.Base.PermissionPolicy;
using DevExpress.Persistent.Validation;
using System.Xml.Serialization;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Drawing;
using LgsLib.Base;

namespace LgsLib.Base
{

	[Table("TaskLGSType")]
	[DefaultClassOptions]
	public class TaskLGSType : BaseLookupC
	{

	}

    [Table("TaskLGS")]
	[DefaultClassOptions]
	public class TaskLGS : BaseObjectState
	{
		public TaskLGS()
        {
			Coments = new List<Comment>();
		}
		public virtual IList<Comment> Coments { get; set; }


		TaskLGSType _Type;
		public TaskLGSType Type
		{
			get { return _Type; }
			set { _Type = value; }
		}

		String _Subject;
		public String Subject
		{
			get { return _Subject; }
			set { _Subject = value; }
		}

		String _Description;
		public String Description
		{
			get { return _Description; }
			set { _Description = value; }
		}

		TaskLGSType _TaskLGSType;
		public virtual TaskLGSType TaskLGSType
		{
			get { return _TaskLGSType; }
			set { _TaskLGSType = value; }
		}

        DateTime? _PlanedStartTime;
        public DateTime? PlanedStartTime
        {
            get { return _PlanedStartTime; }
            set { _PlanedStartTime = value; }
        }

        DateTime? _PlanedFinishTime;
        public DateTime? PlanedFinishTime
        {
            get { return _PlanedFinishTime; }
            set { _PlanedFinishTime = value; }
        }

        User _RequestedUser;
        public virtual User RequestedUser
        {
            get { return _RequestedUser; }
            set { _RequestedUser = value; }
        } 


        DateTime _StartTime;
		public DateTime StartTime
		{
			get { return _StartTime; }
			set { _StartTime = value; }
		}

		DateTime _FinishTime;
		public DateTime FinishTime
		{
			get { return _FinishTime; }
			set { _FinishTime = value; }
		}

		User _AssignedUser;
		public User AssignedUser
		{
			get { return _AssignedUser; }
			set { _AssignedUser = value; }
		}


	}

	[Table("Comment")]
	[DefaultClassOptions]
	public class Comment : BaseObjectC
	{
        String _Description;
		[FieldSize(4000)]
		public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        TaskLGS _Task;
        public virtual TaskLGS Task
        {
            get { return _Task; }
            set { _Task = value; }
        }



    }
}
