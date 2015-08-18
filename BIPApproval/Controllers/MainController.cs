using BIPApproval.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BIPApproval.Controllers
{
	public class MainController : Controller
	{
		//
		// GET: /Main/

		public ActionResult Index()
		{
			ApprovalMatrixModel approval = new ApprovalMatrixModel();
			approval.BIPs.Add(new BIPModel("BIP 100", "http://www.google.com/url?sa=t&rct=j&q=&esrc=s&source=web&cd=1&cad=rja&uact=8&ved=0CB4QFjAAahUKEwi3ga7-rbLHAhXoYqYKHS15Ctk&url=http%3A%2F%2Fgtf.org%2Fgarzik%2Fbitcoin%2FBIP100-blocksizechangeproposal.pdf&ei=VADTVbeHJ-jFmQWt8qnIDQ&usg=AFQjCNGBbyNyZx5u1mwSMT-FB7dqM5PP9g&sig2=iB-oYR_SsUUVdiNOVdHopA&bvm=bv.99804247,d.dGY"));
			approval.BIPs.Add(new BIPModel("BIP 101", "https://github.com/bitcoin/bips/blob/master/bip-0101.mediawiki"));
			approval.BIPs.Add(new BIPModel("8MB Only", null));
			approval.BIPs.Add(new BIPModel("BIP 102", "https://github.com/jgarzik/bips/blob/2015_2mb_blocksize/bip-0102.mediawiki"));
			approval.BIPs.Add(new BIPModel("BIP SIPA", "https://gist.github.com/sipa/c65665fc360ca7a176a6"));
			approval.Devs.Add(new DevModel()
			{
				Name = "Nico",
				Approvals = new List<string>()
				{
					"Ok",
					"?",
					"No",
					"lol",
					"sushi"
				}
			});
			return View(approval);
		}

	}
}
