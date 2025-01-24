using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using LgsLib.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DevExpress.Persistent.Base;
using System.Drawing;
using DevExpress.ExpressApp.DC;
namespace dola.Module
{  

	[DefaultClassOptions]
	[Table("Person")]
	[XafDefaultProperty(nameof(FullName))]
	public class Person:BaseObjectState
    {
        public Person() 
		{ 
            Documents = new List<DocumentTracking>();
        }

		public override void OnSaving()
		{
			if (IsNewObject && this.IdentityCode != null)
			{

				SysCode = IdentityCode.Replace(" ", "");
			}
			base.OnSaving();
		} 
		public virtual IList<DocumentTracking> Documents { get; set; }

		Owner _Owner;
		public virtual Owner Owner
		{
			get { return _Owner; }
			set { _Owner = value; }
		}

       
        String _FirstName;
		public String FirstName
		{
			get { return _FirstName; }
			set { _FirstName = value; }
		}

		String _LastName;
		public String LastName
		{
			get { return _LastName; }
			set { _LastName = value; }
		}

		String _FullName;
		public String FullName
		{
			get { return _FullName; }
			set { _FullName = value; }
		}

		String _IdentityCode;
		public String IdentityCode
		{
			get { return _IdentityCode; }
			set { _IdentityCode = value; }
		}

		 

		String _PhoneNumber;
		public String PhoneNumber
		{
			get { return _PhoneNumber; }
			set { _PhoneNumber = value; }
		}

		String _Email;
		public String Email
		{
			get { return _Email; }
			set { _Email = value; }
		}

		Department _Department;
		public Department Department
		{
			get { return _Department; }
			set { _Department = value; }
		}

		JobTitle _JobTitle;
		public JobTitle JobTitle
		{
			get { return _JobTitle; }
			set { _JobTitle = value; }
		}

		String _IntegrationCode;
		public String IntegrationCode
		{
			get { return _IntegrationCode; }
			set { _IntegrationCode = value; }
		}
    }

	[Table("Department")]
	[DefaultClassOptions]
	public class Department:BaseLookupC
	{
		public Department() { }
		  

	}

	[Table("JobTitle")]
	[DefaultClassOptions]
	public class JobTitle : BaseLookupC
	{
		public JobTitle() { } 

	}

}
