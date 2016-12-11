using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Spatial;
using Newtonsoft.Json;

namespace DemoDocumentDB
{
    class Program
    {
        private static readonly string endpointUrl = "endpointUrl";
        private static readonly string authorizationKey = "authorizationKey";
        private static readonly string databaseId = "databaseId";
        private static readonly string collectionId = "collectionId";
        static void Main(string[] args)
        {
            using (DocumentClient client = new DocumentClient(new Uri(endpointUrl), authorizationKey))
            {
                FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };
                Uri collectionUri = UriFactory.CreateDocumentCollectionUri(databaseId, collectionId);
                var familyQuery = client.CreateDocumentQuery<Family>(collectionUri, queryOptions);
                var testResult = familyQuery.ToList();
            }
        }
    }
    internal sealed class Family
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }
        [JsonProperty(PropertyName = "parents")]
        public Parent[] Parents { get; set; }
        [JsonProperty(PropertyName = "children")]
        public Child[] Children { get; set; }
        public Address Address { get; set; }
        public bool IsRegistered { get; set; }
        [JsonProperty("location")]
        public Point Location { get; set; }

    }
    internal sealed class Address
    {
        public string State { get; set; }
        public string County { get; set; }
        public string City { get; set; }
    }
    internal sealed class Parent
    {
        [JsonProperty(PropertyName = "familyName")]
        public string FamilyName { get; set; }
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }
    }

    internal sealed class Child
    {
        [JsonProperty(PropertyName = "familyName")]
        public string FamilyName { get; set; }
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }
        public string Gender { get; set; }
        [JsonProperty(PropertyName = "grade")]
        public int Grade { get; set; }
        public Pet[] Pets { get; set; }
    }

    internal sealed class Pet
    {
        public string GivenName { get; set; }
    }
}
}
