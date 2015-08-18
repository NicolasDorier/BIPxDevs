using BIPApproval.Models;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            var repo = new Repository();
            DevViewModel vm = new DevViewModel();
            vm.Name = devName;
            vm.ASCLink = repo.GetASCLink(devName);
            vm.MessageLink = repo.GetSigLink(devName);
            try
            {
                string txt = new WebClient().DownloadString(vm.MessageLink);

            }
            catch
            {
            }
            return View(vm);
        }

    }
}
