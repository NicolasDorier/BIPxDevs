using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIPApproval.Models
{
	public class OpinionModel
	{
		public OpinionModel()
		{
			Content = "";
		}
		public string Name
		{
			get;
			set;
		}
		public Approval Approve
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

	public class DevEditViewModel
	{
		public string SignatureSample
		{
			get;
			set;
		}
		public string ResultSample
		{
			get;
			set;
		}

		public string Message
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

		public string Id
		{
			get;
			set;
		}
		public string FriendlyName
		{
			get;
			set;
		}
	}

	public class DevViewModel
	{
		public DevViewModel()
		{
			Opinions = new List<OpinionModel>();
			Presentation = "";
		}
		public string Id
		{
			get;
			set;
		}

		public string FriendlyName
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

		public string Message
		{
			get;
			set;
		}

		public List<OpinionModel> Opinions
		{
			get;
			set;
		}


		public void Load(string data)
		{
			Message = data;
			Presentation = "";
			StringReader reader = new StringReader(data);
			OpinionModel currentOpinion = null;
			StringBuilder builder = new StringBuilder();
			while(true)
			{
				var line = reader.ReadLine();
				if(line == null)
					break;
				if(line.StartsWith("Proposal", StringComparison.InvariantCultureIgnoreCase))
				{
					if(currentOpinion != null && builder != null)
						currentOpinion.Content = builder.ToString();
					else if(builder != null)
						Presentation = builder.ToString();
					currentOpinion = new OpinionModel();
					Opinions.Add(currentOpinion);
					builder = new StringBuilder();
					currentOpinion.Name = line.Split(':').Last().Trim();
				}
				else if(line.StartsWith("Approval", StringComparison.InvariantCultureIgnoreCase) && currentOpinion != null)
				{
					var approval = line.Split(':').Last().Trim();
					currentOpinion.Approve = approval.Equals("Yes", StringComparison.InvariantCultureIgnoreCase) ? Approval.Yes :
                                             approval.Equals("No", StringComparison.InvariantCultureIgnoreCase) ? Approval.No: Approval.NA;
				}
				else
				{
					if(builder != null)
						builder.AppendLine(line);
				}
			}
			if(currentOpinion != null && builder != null)
				currentOpinion.Content = builder.ToString();
			else if(builder != null)
				Presentation = builder.ToString();
		}

		public string Presentation
		{
			get;
			set;
		}
	}
}
