using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIPApproval
{
	public class Storage
	{
		public static CloudBlobClient CreateBlobClient()
		{
			return new CloudBlobClient(new StorageUri(new Uri(ConfigurationManager.AppSettings["Storage.Uri"])), new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(ConfigurationManager.AppSettings["Storage.Token"]));
		}
	}
}
