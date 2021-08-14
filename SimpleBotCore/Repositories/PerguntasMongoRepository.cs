using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBotCore.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Repositories
{
    public class PerguntasMongoRepository : IPerguntasMongoRepository
    {
        MongoClient _client;
        IMongoCollection<Perguntas> _perguntas;
        public PerguntasMongoRepository(MongoClient client)
        {
            _client = client;
            var db = client.GetDatabase("Bot");
            var collection = db.GetCollection<Perguntas>("Perguntas");
            _perguntas = collection;
        }

        public void InsertPergunta(SimpleUser user, string pergunta)
        {
            var insert = new Perguntas() { Pergunta = pergunta, User = user };
            try
            {
                _perguntas.InsertOne(insert);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
