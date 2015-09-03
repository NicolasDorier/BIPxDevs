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
            Approvals = new List<Approval?>();
        }
        public List<Approval?> Approvals
        {
            get;
            set;
        }

        public string FriendlyName
        {
            get;
            set;
        }
        public string Id
        {
            get;
            set;
        }

        public string Group
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
            DevGroups = new List<IGrouping<string, DevModel>>();
		}
		public List<BIPModel> BIPs
		{
			get;
			set;
		}
		public List<IGrouping<string,DevModel>> DevGroups
		{
			get;
			set;
		}
	}
}
