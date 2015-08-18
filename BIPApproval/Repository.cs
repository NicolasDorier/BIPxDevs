using BIPApproval.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BIPApproval
{
    public class Repository
    {
        public IEnumerable<BIPModel> GetBIPs()
        {
            yield return new BIPModel("BIP 100", "https://gtf.org/garzik/bitcoin/BIP100-blocksizechangeproposal.pdf");
            yield return new BIPModel("BIP 101", "https://github.com/bitcoin/bips/blob/master/bip-0101.mediawiki");
            yield return new BIPModel("8MB Only", null);
            yield return new BIPModel("BIP 102", "https://github.com/jgarzik/bips/blob/2015_2mb_blocksize/bip-0102.mediawiki");
            yield return new BIPModel("BIP SIPA", "https://gist.github.com/sipa/c65665fc360ca7a176a6");
        }

        public IEnumerable<DevModel> GetDevs()
        {
            var bips = GetBIPs().ToList();
            var container = Storage.CreateBlobContainer();
            foreach(var blob in container
                .ListBlobs("BlockSize/AcceptedDevs", true, BlobListingDetails.All)
                .OfType<CloudBlockBlob>()
                .Where(o => o.Name.EndsWith(".asc")))
            {
                var dev = new DevModel();
                dev.Id = blob.Name.Split('/').Last().Split('.').First();
                dev.FriendlyName = blob.Metadata.ContainsKey("Name") ? blob.Metadata["Name"].Replace("%20", " ") : dev.Id;

                var vm = GetDevViewModel(dev.Id);
                for(int i = 0; i < bips.Count;i++)
                {
                    var opinion = vm.Opinions.Where(o => o.Name.Equals(bips[i].Name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                    dev.Approvals.Add(opinion == null ? null : new bool?(opinion.Approve));
                }
                yield return dev;
            }
        }

        private string GetFriendlyName(string devId)
        {
            var blob = GetAscBlob(devId);
            try
            {

                blob.FetchAttributes();
                return blob.Metadata.ContainsKey("Name") ? blob.Metadata["Name"].Replace("%20", " ") : null;
            }
            catch(StorageException)
            {
                return null;
            }
        }

        private static CloudBlockBlob GetAscBlob(string devId)
        {
            var container = Storage.CreateBlobContainer();
            var blob = container.GetBlockBlobReference("BlockSize/AcceptedDevs/" + devId + ".asc");
            return blob;
        }
        private static CloudBlockBlob GetSigBlob(string devId)
        {
            var container = Storage.CreateBlobContainer();
            var blob = container.GetBlockBlobReference("BlockSize/DevsPost/" + devId + ".sig");
            return blob;
        }

        public DevViewModel GetDevViewModel(string devId)
        {
            DevViewModel vm = new DevViewModel();
            vm.Id = devId;
            vm.ASCLink = GetASCLink(devId);
            vm.MessageLink = GetSigLink(devId);
            vm.FriendlyName = GetFriendlyName(devId);
            string sig = null;
            string pubkey = null;
            try
            {
                sig = new WebClient().DownloadString(vm.MessageLink);
                pubkey = new WebClient().DownloadString(vm.ASCLink);
            }
            catch
            {
            }
            if(sig == null || pubkey == null)
                return vm;

            string message;
            if(!CryptoHelper.VerifySig(pubkey, sig, out message))
                return vm;
            vm.Load(message);
            return vm;
        }        

        public string GetASCLink(string dev)
        {
            var container = Storage.CreateBlobContainer();
            return container.Uri.AbsoluteUri + "/BlockSize/AcceptedDevs/" + dev + ".asc";
        }

        public string GetSigLink(string devName)
        {
            var container = Storage.CreateBlobContainer();
            return container.Uri.AbsoluteUri + "/BlockSize/DevsPost/" + devName + ".sig";
        }

        public void UpdateDevViewModel(DevEditViewModel dev)
        {
            GetSigBlob(dev.Id).UploadText(dev.Message);
        }

        internal bool SaveHash(byte[] hash)
        {
            //Poorman stringify
            var hashStr = System.Text.Encoding.ASCII.GetString(hash);
            var container = Storage.CreateBlobContainer();
            var hashBlob = container.GetBlockBlobReference("BlockSize/Hashes/" + hashStr);
            if(hashBlob.Exists())
                return false;
            hashBlob.UploadText("");
            return true;
        }
    }
}
