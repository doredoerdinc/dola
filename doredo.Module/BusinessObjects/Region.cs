using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Xml;
using System.Xml.Serialization;
using LgsLib.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DevExpress.Persistent.Base;
 
namespace dola.Module
{

	public interface IGeoPoint
	{
		long Latitude { get; set; }
		long Longitude { get; set; }
		string Description { get; set; }  
		DateTime Date { get; set;}

	}
	 



	//[Table("RegionPoints")]
	//[DefaultClassOptions]
 //  public class RegionPoints:BaseObjectC, IGeoPoint
	//{
	//	public RegionPoints() { }

	//	Region _Region;
	//	public virtual Region Region
	//	{
	//		get { return _Region; }
	//		set { _Region = value; }
	//	}

	//	public long Latitude { get; set; }
	//	public long Longitude { get; set; }
	//	public string Description { get; set; }
	//	public int Radus { get; set; }
	//    public DateTime Date { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	//}
}
