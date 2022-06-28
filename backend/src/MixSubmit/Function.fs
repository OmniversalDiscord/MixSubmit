namespace MixSubmit

open System.Net.Http
open Amazon.Lambda.Core
open Amazon.Lambda.APIGatewayEvents
open Newtonsoft.Json

[<assembly: LambdaSerializer(typeof<Amazon.Lambda.Serialization.Json.JsonSerializer>)>]

do ()

module Function =
    let private client = new HttpClient()

    let private getCallingIp () =
        client.DefaultRequestHeaders.Accept.Clear()
        client.DefaultRequestHeaders.Add("User-Agent", "AWS Lambda .Net Client")

        task {
            let! msg =
                client
                    .GetStringAsync("http://checkip.amazonaws.com/")
                    .ConfigureAwait(false)

            return msg.Replace("\n", "")
        }

    let functionHandler (apigProxyEvent: APIGatewayProxyRequest, context: ILambdaContext) =
        task {
            let! location = getCallingIp ()
            
            let name = apigProxyEvent.PathParameters["name"]
            
            let body =
                dict
                    [ "message", $"hello {name}!"
                      "location", location ]

            return
                APIGatewayProxyResponse(
                    Body = JsonConvert.SerializeObject(body),
                    StatusCode = 200,
                    Headers = dict [ "Content-Type", "application/json" ]
                )
        }
