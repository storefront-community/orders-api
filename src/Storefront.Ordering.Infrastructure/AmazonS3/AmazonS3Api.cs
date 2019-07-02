using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Storefront.Ordering.Domain.Entities;
using Storefront.Ordering.Domain.Repositories;

namespace Storefront.Ordering.Infrastructure.AmazonS3
{
    public sealed class AmazonS3Api : IPhotoRepository
    {
        private readonly AmazonS3Options _options;
        private readonly AmazonS3Client _client;

        public AmazonS3Api(AmazonS3Options options)
        {
            _options = options;

            var region = RegionEndpoint.GetBySystemName(_options.Region);
            _client = new AmazonS3Client(_options.AccessKeyId, _options.SecretAccessKey, region);
        }

        public async Task<Photo> FindById(string id)
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

                return new Photo(
                    id: id,
                    binary: memoryStream.ToArray(),
                    contentType: response.Headers.ContentType
                );
            }
        }

        public async Task Add(Photo photo)
        {
            var request = new PutObjectRequest
            {
                BucketName = _options.BucketName,
                Key = photo.Id.ToString(),
                ContentType = photo.ContentType
            };

            using (var stream = new MemoryStream(photo.Binary))
            {
                request.InputStream = stream;
                await _client.PutObjectAsync(request);
            }
        }

        public async Task Remove(Photo photo)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = _options.BucketName,
                Key = photo.Id.ToString()
            };

            await _client.DeleteObjectAsync(request);
        }
    }
}
