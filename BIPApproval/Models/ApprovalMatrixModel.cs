using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIPApproval.Models
{
	public class DevModel
	{
		public DevModel()
		{
			Approvals = new List<bool?>();
		}
		public List<bool?> Approvals
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}
	}

	public class BIPModel
	{
		
		public BIPModel()
		{

		}
		public BIPModel(string name, string link)
		{
			Name = name;
			Link = link;
		}
		public string Name
		{
			get;
			set;
		}
		public string Link
		{
			get;
			set;
		}
	}
	public class ApprovalMatrixModel
	{
		public ApprovalMatrixModel()
		{
			BIPs = new List<BIPModel>();
			Devs = new List<DevModel>();
		}
		public List<BIPModel> BIPs
		{
			get;
			set;
		}
		public List<DevModel> Devs
		{
			get;
			set;
		}
	}
}
