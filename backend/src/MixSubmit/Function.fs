namespace MixSubmit

open System
open System.IO
open System.Net.Http
open System.Text
open Amazon
open Amazon.Lambda.Core
open Amazon.Lambda.APIGatewayEvents
open Amazon.S3
open Amazon.S3.Transfer
open Newtonsoft.Json

[<assembly: LambdaSerializer(typeof<Amazon.Lambda.Serialization.Json.JsonSerializer>)>]

do ()

module Function =

    let functionHandler (_: APIGatewayProxyRequest, context: ILambdaContext) =
        let client = new AmazonS3Client()

        let stream = new MemoryStream()

        let bucketName =
            Environment.GetEnvironmentVariable("BUCKET")

        // Write dummy text to the stream
        stream.Write(Encoding.UTF8.GetBytes("Hello World!"))

        let uploadRequest =
            TransferUtilityUploadRequest(
                InputStream = stream,
                Key = "test.txt",
                BucketName = bucketName,
                CannedACL = S3CannedACL.PublicRead
            )

        let fileTransferUtility =
            new TransferUtility(client)

        task {
            do! fileTransferUtility.UploadAsync(uploadRequest)

            return APIGatewayProxyResponse(StatusCode = 200, Body = "Uploaded!")
        }
