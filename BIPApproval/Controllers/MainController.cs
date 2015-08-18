﻿using BIPApproval.Models;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
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

        [Route("Opinion/{devId}")]
        public ActionResult Dev(string devId)
        {
            var repo = new Repository();
            return View(repo.GetDevViewModel(devId));
        }

        [Route("EditOpinion/{devId}")]
        public ActionResult DevEdit(string devId)
        {
            var editVm = GetDevEditViewModel(devId);
            return View(editVm);
        }

        private static DevEditViewModel GetDevEditViewModel(string devId)
        {
            var repo = new Repository();
            var vm = repo.GetDevViewModel(devId);
            var editVm = new DevEditViewModel();
            editVm.Id = vm.Id;
            editVm.FriendlyName = vm.FriendlyName;
            editVm.MessageLink = vm.MessageLink;
            editVm.ASCLink = vm.ASCLink;
            if(vm.Message != null)
                editVm.Message = new WebClient().DownloadString(repo.GetSigLink(editVm.Id));
            editVm.SignatureSample = repo.GetSigLink("zsample");
            editVm.ASCLink = repo.GetASCLink("zsample");
            return editVm;
        }

        [Route("EditOpinion")]
        [HttpPost]
        public ActionResult DevEditPost(DevEditViewModel editVm)
        {
            var repo = new Repository();
            var original = GetDevEditViewModel(editVm.Id);
            original.Message = editVm.Message;
            var asc = new WebClient().DownloadString(repo.GetASCLink(editVm.Id));
            string extracted;
            if(!CryptoHelper.VerifySig(asc, editVm.Message, out extracted))
            {
                ModelState.AddModelError("Message", "Incorrectly signed");
            }
            else
            {
                byte[] hash = null;
                using(SHA256 sha = new SHA256Managed())
                {
                    hash = sha.ComputeHash(Encoding.UTF8.GetBytes(extracted));
                }
                if(repo.SaveHash(hash))
                {
                    repo.UpdateDevViewModel(original);
                }
                else
                {
                    ModelState.AddModelError("Message", "Replay attack detected");
                }
            }
            return View("DevEdit", original);
        }
    }
}
