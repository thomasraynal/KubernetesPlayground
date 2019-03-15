using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace KubernetesPlayground.Api.Service
{
    public class ItemDocument : MongoDbGenericRepository.Models.Document
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static IEnumerable<ItemDocument> Create(int nb)
        {
            var rand = new Random();
            var data = new string(Enumerable.Repeat(chars, 10).Select(s => s[rand.Next(s.Length)]).ToArray());

            return Enumerable.Range(0, nb).Select(_ => new ItemDocument() { Data = data });

        }
        
        [BsonElement]
        public String Data { get; set; }
    }
}
