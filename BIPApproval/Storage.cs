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
			var uri = new Uri(ConfigurationManager.AppSettings["Storage.Uri"]);
			var accountName = uri.Host.Split('.').First();
			return new CloudBlobClient(new StorageUri(uri), new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(accountName, ConfigurationManager.AppSettings["Storage.Token"]));
		}

		public static CloudBlobContainer CreateBlobContainer()
		{
			return CreateBlobClient().GetContainerReference(ConfigurationManager.AppSettings["Storage.Container"]);
		}
	}
}
