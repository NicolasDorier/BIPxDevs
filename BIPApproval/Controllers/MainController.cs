using BIPApproval.Models;
using Microsoft.WindowsAzure.Storage.Blob;
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
			approval.BIPs.Add(new BIPModel("BIP 100", "https://gtf.org/garzik/bitcoin/BIP100-blocksizechangeproposal.pdf"));
			approval.BIPs.Add(new BIPModel("BIP 101", "https://github.com/bitcoin/bips/blob/master/bip-0101.mediawiki"));
			approval.BIPs.Add(new BIPModel("8MB Only", null));
			approval.BIPs.Add(new BIPModel("BIP 102", "https://github.com/jgarzik/bips/blob/2015_2mb_blocksize/bip-0102.mediawiki"));
			approval.BIPs.Add(new BIPModel("BIP SIPA", "https://gist.github.com/sipa/c65665fc360ca7a176a6"));


			var container = Storage.CreateBlobContainer();
			foreach(var blob in container
				.ListBlobs("BlockSize/AcceptedDevs", true, BlobListingDetails.All)
				.OfType<CloudBlockBlob>()
				.Where(o=>o.Name.EndsWith(".asc")))
			{
				approval.Devs.Add(new DevModel()
				{
					Name = blob.Metadata.ContainsKey("Name") ? blob.Metadata["Name"].Replace("%20", " ") : blob.Name.Replace(".asc", ""),
					Approvals = new List<string>()
					{
						"Ok",
						"?",
						"No",
						"lol",
						"sushi"
					}
				});
			}

			return View(approval);
		}

	}
}
