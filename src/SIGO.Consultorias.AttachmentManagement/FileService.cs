using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;
using SIGO.Consultorias.Application.Services;
using System.IO;
using System.Threading.Tasks;

namespace SIGO.Consultorias.AttachmentManagement
{
    public class FileService : IFileService
    {
        private readonly IConfiguration _configuration;

        public FileService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Upload(Stream file, string fileName)
        {
            var region = _configuration["S3:Region"];
            var bucketName = _configuration["S3:Bucket"];
            var accessKeyId = _configuration["S3:AccessKeyId"];
            var accessKeySecret = _configuration["S3:AccessKeySecret"];

            using (var s3Client = new AmazonS3Client(accessKeyId, accessKeySecret, RegionEndpoint.GetBySystemName(region)))
            {
                using (var fileTransferUtility = new TransferUtility(s3Client))
                {
                    await fileTransferUtility.UploadAsync(file, bucketName, fileName);
                }
            }
        }
    }
}
