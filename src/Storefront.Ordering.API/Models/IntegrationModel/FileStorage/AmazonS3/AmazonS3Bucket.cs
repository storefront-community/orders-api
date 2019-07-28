using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace Storefront.Ordering.API.Models.IntegrationModel.FileStorage.AmazonS3
{
    [ExcludeFromCodeCoverage]
    public sealed class AmazonS3Bucket : IFileStorage
    {
        private readonly AmazonS3Options _options;
        private readonly AmazonS3Client _client;

        public AmazonS3Bucket(AmazonS3Options options)
        {
            _options = options;

            var region = RegionEndpoint.GetBySystemName(_options.Region);
            _client = new AmazonS3Client(_options.AccessKeyId, _options.SecretAccessKey, region);
        }

        public async Task<StoredFile> FindById(string id)
        {
            var request = new GetObjectRequest
            {
                BucketName = _options.BucketName,
                Key = id
            };

            using (var response = await _client.GetObjectAsync(request))
            using (var responseStream = response.ResponseStream)
            using (var memoryStream = new MemoryStream())
            {
                await responseStream.CopyToAsync(memoryStream);

                return new StoredFile
                {
                    Id = id,
                    Binary = memoryStream.ToArray(),
                    ContentType = response.Headers.ContentType
                };
            }
        }

        public async Task Add(StoredFile file)
        {
            var request = new PutObjectRequest
            {
                BucketName = _options.BucketName,
                Key = file.Id.ToString(),
                ContentType = file.ContentType
            };

            using (var stream = new MemoryStream(file.Binary))
            {
                request.InputStream = stream;
                await _client.PutObjectAsync(request);
            }
        }

        public async Task Remove(StoredFile file)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = _options.BucketName,
                Key = file.Id.ToString()
            };

            await _client.DeleteObjectAsync(request);
        }
    }
}
