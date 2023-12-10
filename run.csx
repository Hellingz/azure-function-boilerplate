#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");

    // Läs eventuella queryparametrar
    string name = req.Query["name"];

    // Läs request body, om det finns
    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    dynamic data = JsonConvert.DeserializeObject(requestBody);
    name = name ?? data?.name;

    // Logga information
    log.LogInformation($"Name={name}");

    return name != null
        ? (ActionResult)new OkObjectResult($"Hello, {name}, how you doing?")
        : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
}
