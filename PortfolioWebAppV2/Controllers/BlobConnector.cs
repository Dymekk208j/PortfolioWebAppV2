﻿using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using PortfolioWebAppV2.Models.DatabaseModels;

namespace PortfolioWebAppV2.Controllers
{
    public static class BlobConnector
    {
        static BlobConnector()
        {
            //TODO: Nie ustawia automatycznie uprawnień na kontenerach na publiczne, przez co nie będzie mozna pobrać obrazka. Trzeba robić to ręcznie przy tworzeniu kontenera.
            StorageCredentials storageCredentials = new StorageCredentials("damiandziuraportfolio",
                "eL6FmFEJ8sp+zC3NRIfFY8rBg4cS1ySypJs8b+52p7OJv2si1+YiaARwZyp6GOzvrPH3EC89Op8rU4gYbB47+w==");
            CloudStorageAccount cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            CloudBlobContainer iconsContainer = cloudBlobClient.GetContainerReference("icons");
            iconsContainer.CreateIfNotExistsAsync();

            CloudBlobContainer tempProjectImages = cloudBlobClient.GetContainerReference("tempprojectimages");
            tempProjectImages.CreateIfNotExistsAsync();

            CloudBlobContainer projectImages = cloudBlobClient.GetContainerReference("projectimages");
            projectImages.CreateIfNotExistsAsync();
        }

        public static void RemoveImage(Image image)
        {
            // CloudBlockBlob cblob = image.TempraryProject ? TempProjectImages.GetBlockBlobReference(image.FileName) : ProjectImages.GetBlockBlobReference(image.FileName);

            // cblob.DeleteIfExists();
        }

        public static void UploadImage(HttpPostedFileBase file, Image image)
        {
            //  CloudBlockBlob cblob = image.TempraryProject ? TempProjectImages.GetBlockBlobReference(image.FileName) : ProjectImages.GetBlockBlobReference(image.FileName);

            // cblob.Properties.ContentType = "image/png";
            // cblob.UploadFromStream(file.InputStream);
        }

        public static void UploadIcon(HttpPostedFileBase file, int projectId, bool temp)
        {
            //string fileName = temp ? Project.GetIconName(projectId) : TempProject.GetIconName(projectId);

            //CloudBlockBlob cblob = IconsContainer.GetBlockBlobReference(fileName);
            //cblob.Properties.ContentType = "image/png";


            //cblob.UploadFromStream(file.InputStream);
        }

        public static void RemoveIcon(int projectId, bool temp)
        {
            //string fileName = temp ? Project.GetIconName(projectId) : TempProject.GetIconName(projectId);

            //CloudBlockBlob cblob = IconsContainer.GetBlockBlobReference(fileName);
            //cblob.DeleteIfExists();
        }

        public static void MoveIconFromTempToProject(int tempProjectId, int projectId)
        {
            //CloudBlockBlob oldFile = IconsContainer.GetBlockBlobReference(TempProject.GetIconName(tempProjectId));
            //CloudBlockBlob newFile = IconsContainer.GetBlockBlobReference(Project.GetIconName(projectId));
            //newFile.StartCopy(oldFile);
            //oldFile.DeleteIfExists();
        }

        public static void MoveImageFromTempToProject(Image image, int projectId)
        {
            //CloudBlockBlob oldFile = TempProjectImages.GetBlockBlobReference(image.FileName);
            //CloudBlockBlob newFile = ProjectImages.GetBlockBlobReference("Project" + projectId + image.OriginalFileName);
            //newFile.StartCopy(oldFile);
            //oldFile.DeleteIfExists();
        }
    }
}