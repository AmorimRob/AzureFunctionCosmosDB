using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace CosmosFunction
{
    public static class SCICosmosFunction
    {
        static string lastIdExecuted = string.Empty;

        static DocumentClient documentClient = new DocumentClient(new System.Uri("https://rsamorim.documents.azure.com:443/"), 
            "tMaooTljM32BJcjg3tNCBL51ANZNJ2HaelT3jYlaM07TaYFht8IHMRLaQtmLbGYWNMQPT1c8z2OiMxq0l9P7cQ==");
        
        [FunctionName("SCICosmosFunction")]
        public static void Run([CosmosDBTrigger(
            databaseName: "SampleCosmos",
            collectionName: "Clubs",
            ConnectionStringSetting = "DBConnection",
            LeaseCollectionName = "leases")]IReadOnlyList<Document> documents, TraceWriter log)
        {
           
            if (documents != null && documents.Count > 0 && lastIdExecuted != documents[0].Id)
            {
                var document = documents[0];

                document.SetPropertyValue("Name", $"SCFI MACKENZIE {System.DateTime.Now.ToString()}");
               
                documentClient.ReplaceDocumentAsync(document);
                
                lastIdExecuted = documents[0].Id;

            }
        }  
    }
}
