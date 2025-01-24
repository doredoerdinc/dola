using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DevExpress.ExpressApp.DC;

using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp;
using System.ComponentModel.DataAnnotations.Schema;
using LgsLib.Base.PermissionPolicy;
using DevExpress.Persistent.Validation;
using System.Xml.Serialization;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Drawing;
using Region = LgsLib.Base;

namespace LgsLib.Base
{
	[Table("Region")]
	[DefaultClassOptions]
	public class Region : BaseLookupC
	{
		public Region()
		{

		} 

        long _Latitude;
		public long Latitude
		{
			get { return _Latitude; }
			set { _Latitude = value; }
		}

		long _longitude;
		public long longitude
		{
			get { return _longitude; }
			set { _longitude = value; }
		}

		int _Radius;
		public int Radius
		{
			get { return _Radius; }
			set { _Radius = value; }
		}

	}
}
