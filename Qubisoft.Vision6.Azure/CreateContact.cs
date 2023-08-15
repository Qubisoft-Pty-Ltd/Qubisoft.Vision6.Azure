using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Qubisoft.Vision6.Azure.Models;

namespace Qubisoft.Vision6.Azure
{
    public static class CreateContact
    {
        [FunctionName("CreateContact")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Contact contact = JsonConvert.DeserializeObject<Contact>(requestBody);

            try
            {
                string vision6ApiKey = Environment.GetEnvironmentVariable("Vision6APIKey");
                string vision6RemoveUnsubscribers = Environment.GetEnvironmentVariable("Vision6RemoveUnsubscribers");
                int vision6ListId = Int32.Parse(Environment.GetEnvironmentVariable("Vision6ListId"));
                Vision6Client v6 = new Vision6Client();
                await v6.Authenticate(vision6ApiKey);
                await v6.CreateContact(vision6ListId, contact, vision6RemoveUnsubscribers);
            } catch (Vision6Exception e)
            {
                return new BadRequestObjectResult(e);
            }

            return new OkObjectResult("Contact created.");
        }
    }
}
