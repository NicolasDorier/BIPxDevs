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
            Repository repo = new Repository();
			ApprovalMatrixModel approval = new ApprovalMatrixModel();
            approval.BIPs.AddRange(repo.GetBIPs());
            approval.Devs.AddRange(repo.GetDevs());
			return View(approval);
		}

        [Route("Opinion/{devName}")]
        public ActionResult Dev(string devName)
        {

            return null;
        }

	}
}
