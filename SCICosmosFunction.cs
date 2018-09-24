using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace CosmosFunction
{
    public static class SCICosmosFunction
    {
        [FunctionName("SCICosmosFunction")]
        public static void Run([CosmosDBTrigger(
            databaseName: "SampleCosmos",
            collectionName: "Clubs",
            ConnectionStringSetting = "DBConnection",
            LeaseCollectionName = "leases")]IReadOnlyList<Document> documents, TraceWriter log)
        {
           
            if (documents != null && documents.Count > 0)
            {
                log.Verbose("Documents modified " + documents.Count);
                log.Verbose("First document Id " + documents[0].Id);

            }
        }  
    }
}
