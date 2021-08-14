using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Logic
{
    public class Perguntas
    {
        public string Pergunta { get; set; }
        public SimpleUser User { get; set; }
    }
}
