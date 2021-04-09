using System;
using Azure.Identity;
using Azure.Storage.Blobs;

namespace CloudClientSecurity01
{
    class Program
    {
        static void Main(string[] args)
        {
            var storageUri = Environment.GetEnvironmentVariable("APP_STORAGE_URI");
            var containerName = Environment.GetEnvironmentVariable("APP_STORAGE_CONTAINER");

            var credential = new ChainedTokenCredential(
                new ManagedIdentityCredential(),
                new EnvironmentCredential());

            var blobServiceClient = new BlobServiceClient(
                new Uri(storageUri),
                credential);

            var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = blobContainerClient.GetBlobClient($"{DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss")}.jpg");
            blobClient.Upload("test.jpg");
        }
    }
}
