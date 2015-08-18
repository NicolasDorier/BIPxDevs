using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIPApproval.Models
{
    public class OpinionModel
    {
        public string Name
        {
            get;
            set;
        }
        public bool Approve
        {
            get;
            set;
        }
        public string Content
        {
            get;
            set;
        }
    }
    public class DevViewModel
    {
        public DevViewModel()
        {
            BIPs = new List<string>();
            Opinions = new List<OpinionModel>();
        }
        public List<String> BIPs
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string ASCLink
        {
            get;
            set;
        }
        public string MessageLink
        {
            get;
            set;
        }

        public List<OpinionModel> Opinions
        {
            get;
            set;
        }
    }
}
