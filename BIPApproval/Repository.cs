using BIPApproval.Models;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var container = Storage.CreateBlobContainer();
            foreach(var blob in container
                .ListBlobs("BlockSize/AcceptedDevs", true, BlobListingDetails.All)
                .OfType<CloudBlockBlob>()
                .Where(o => o.Name.EndsWith(".asc")))
            {
                yield return new DevModel()
                {
                    Name = blob.Metadata.ContainsKey("Name") ? blob.Metadata["Name"].Replace("%20", " ") : blob.Name.Replace(".asc", ""),
                    Approvals = new List<bool?>()
					{
						true,
						false,
						false,
						true,
						true
					}
                };
            }
        }

        public string GetASCLink(string dev)
        {
            var container = Storage.CreateBlobContainer();
            return container.Uri.AbsoluteUri + "/Blocksize/AcceptedDevs/" + dev + ".asc";
        }

        public string GetSigLink(string devName)
        {
            var container = Storage.CreateBlobContainer();
            return container.Uri.AbsoluteUri + "/Blocksize/DevsPost/" + devName + ".sig";
        }
    }
}
