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
     public class Product : TableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }

        //public string Detail { get; set; }

    }
    class Program
    {
        static void Main(string[] args)
        {
            //  CreateBlobExample();
            // CreateTableExample();

            //SelectFromTable();

            AddInQue();

        }

        private static void SelectFromTable()
        {
            CloudStorageAccount act = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
            var client = act.CreateCloudTableClient();

            var table = client.GetTableReference("Products2");

            var q = new TableQuery<Product>();
            q.Select(new string[] { "Id", "Name" });
            q.Take(int.MaxValue);

            var result = table.ExecuteQuery<Product>(q);

            var cnt = result.Count();
            foreach (var item in result)
            {
                Console.WriteLine(item.Id);
            }

        }

        private static void CreateTableExample()
        {
            CloudStorageAccount act = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
            var client = act.CreateCloudTableClient();

            var table = client.GetTableReference("Products2");

            if (!table.Exists())
            {
                table.Create();
                //table.SetPermissions(new TablePermissions() { SharedAccessPolicies = })
            }

            var p = new Product();
            p.RowKey = "Id";
            p.Id = 3;
            p.Name = "Samsung S1000";
            p.Price = 105000;
            p.PartitionKey = p.RowKey + p.Id;
            //   p.Detail = "Some detail";

            var op = TableOperation.Insert(p);

            table.Execute(op);

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

        private static void CreateBlobExample()
        {
            CloudStorageAccount act = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
            var client = act.CreateCloudBlobClient();

            var cont = client.GetContainerReference("aptech2");

            if (!cont.Exists())
            {
                cont.Create();
                cont.SetPermissions(new BlobContainerPermissions() { PublicAccess = BlobContainerPublicAccessType.Container });

            }

            var blob1 = cont.GetBlockBlobReference("abc.pdf");

            blob1.UploadFromFile(@"E:\books\Building Applications in C INTL.pdf", FileMode.Open, null, null);
        }
    }
}
