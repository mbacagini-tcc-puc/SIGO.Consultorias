using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;
using SIGO.Consultorias.Application.Services;
using System.IO;
using System.Threading.Tasks;

namespace SIGO.Consultorias.AttachmentManagement
{
    public class FileService : IFileService
    {
        private readonly string _bucketName;
        private readonly AmazonS3Client _s3Client;

        public FileService(IConfiguration configuration)
        {
            var region = configuration["S3:Region"];
            var bucketName = configuration["S3:Bucket"];
            var accessKeyId = configuration["S3:AccessKeyId"];
            var accessKeySecret = configuration["S3:AccessKeySecret"];
            
            _s3Client = new AmazonS3Client(accessKeyId, accessKeySecret, RegionEndpoint.GetBySystemName(region));
            _bucketName = bucketName;
        }


        public async Task Upload(Stream file, string fileName)
        {
            using (var fileTransferUtility = new TransferUtility(_s3Client))
            {
                await fileTransferUtility.UploadAsync(file, _bucketName, fileName);
            }
        }

        public async Task Excluir(string fileName)
        {
            await _s3Client.DeleteObjectAsync(new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = fileName
            });
        }
    }
}
