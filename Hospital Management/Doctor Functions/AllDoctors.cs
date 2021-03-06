using Hospital_Management.Common;
using Hospital_Management.DoctorModels;
using Hospital_Management.Models.Doctor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management.Doctor_Functions
{
    public static class AllDoctors
    {
        [FunctionName("AllDoctors")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "AllDoctors")] HttpRequest req,
            ILogger log)
        {
            //declaring the response
            List<Doctor> lstDoctors = null;
            log.LogInformation("Calling Azure Function -- DoctorGetId");
            // initialising Azure Cosomosdb database connection.
            AzureCosmoDBActivity objCosmosDBActivitiy = new AzureCosmoDBActivity();
            await objCosmosDBActivitiy.InitiateConnection();
            // retriving existing student information based on doctorGuid and partition key i.e. DoctorId value
            lstDoctors = await objCosmosDBActivitiy.GetAllDoctors();
            return new OkObjectResult(lstDoctors);
        }
    }
}
