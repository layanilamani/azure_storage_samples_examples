using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;


namespace AzureStorageSamples
{
    class Program
    {
        static void Main(string[] args)
        {
           // UploadBlob();

            AddInQue();
        }

        private static void UploadBlob()
        {
            var act = CloudStorageAccount.DevelopmentStorageAccount.CreateCloudBlobClient();

            var container = act.GetContainerReference("aptech");

            if (!container.Exists())
            {
                container.Create(BlobContainerPublicAccessType.Container);
            }

            var blob = container.GetBlockBlobReference("abc.docx");

            blob.UploadFromFile(@"D:\mazhar\Enterprise Application Development using Azure.docx", System.IO.FileMode.Open);

        }

        private static void AddInQue()
        {
            var act = CloudStorageAccount.DevelopmentStorageAccount.CreateCloudQueueClient();

            var que = act.GetQueueReference("aptech");

            if (!que.Exists())
            {
                que.Create();
            }

            que.AddMessage(new Microsoft.WindowsAzure.Storage.Queue.CloudQueueMessage("salam"));            
            

        }
    }
}
