using Amazon.S3;
using Amazon.S3.Model;
using HeatChart.Infrastructure.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace HeatChart.Web.Core.AWS
{
    public class S3UploadObject
    {
        private static  string AWSAccessKey  = ConfigurationReader.AWSAccessKey; 
        private static string AWSSecretKey = ConfigurationReader.AWSSecretKey;
        private static string BucketName = ConfigurationReader.BucketName;

        public static void WriteAnObject(Stream inputStream,  string fileName, string folderName)
        {            
            IAmazonS3 s3Client;

            PutObjectResponse response = null;
            using (s3Client = GetAmazonS3Client())
            {
                try
                {
                    PutObjectRequest putRequest = new PutObjectRequest
                    {
                        BucketName = BucketName,
                        CannedACL = S3CannedACL.PublicRead,//PERMISSION TO FILE PUBLIC ACCESIBLE
                        Key = string.Format("{0}/{1}", folderName, fileName),
                        InputStream = inputStream, //SEND THE FILE STREAM                                   
                    };
                    putRequest.Metadata.Add("x-amz-meta-title", fileName);

                    response = s3Client.PutObject(putRequest);

                }
                catch (AmazonS3Exception amazonS3Exception)
                {
                    if (amazonS3Exception.ErrorCode != null &&
                        (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                        ||
                        amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                    {
                        Console.WriteLine("Check the provided AWS Credentials.");
                        Console.WriteLine(
                            "For service sign up go to http://aws.amazon.com/s3");
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error occurred. Message:'{0}' when writing an object"
                            , amazonS3Exception.Message);
                    }
                }              
            }
        }

        public static HttpResponseMessage DownloadAnObject(string fileName, string folderName)
        {
            HttpResponseMessage response = null;

            IAmazonS3 s3Client;
            using (s3Client = GetAmazonS3Client())
            {
                try
                {

                    GetObjectRequest getObjectRequest = new GetObjectRequest();
                    getObjectRequest.BucketName = BucketName;
                    getObjectRequest.Key = string.Format("{0}/{1}", folderName, fileName);
                        
                    GetObjectResponse getObjectResponse = s3Client.GetObject(getObjectRequest);

                    using (Stream stream = getObjectResponse.ResponseStream)
                    {
                        long length = stream.Length;
                        byte[] bytes = new byte[length];
                        int bytesToRead = (int)length;
                        int numBytesRead = 0;
                        do
                        {
                            int chunkSize = 1000;
                            if (chunkSize > bytesToRead)
                            {
                                chunkSize = bytesToRead;
                            }
                            int n = stream.Read(bytes, numBytesRead, chunkSize);
                            numBytesRead += n;
                            bytesToRead -= n;
                        }
                        while (bytesToRead > 0);

                        response = new HttpResponseMessage();

                        response.Content = new ByteArrayContent(bytes.ToArray());
                        response.Content.Headers.Add("x-filename", fileName);
                        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                        response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                        response.Content.Headers.ContentDisposition.FileName = fileName;

                        response.StatusCode = HttpStatusCode.OK;
                    }
                }
                catch (AmazonS3Exception e)
                {
                    Console.WriteLine("Object download has failed.");
                    Console.WriteLine("Amazon error code: {0}",
                        string.IsNullOrEmpty(e.ErrorCode) ? "None" : e.ErrorCode);
                    Console.WriteLine("Exception message: {0}", e.Message);
                }
                catch(Exception ex)
                {
                    throw;
                }

                return response;
            }
        }

        private static AmazonS3Client GetAmazonS3Client()
        {
            return new AmazonS3Client(AWSAccessKey, AWSSecretKey, Amazon.RegionEndpoint.USEast2);
        }
    }
}