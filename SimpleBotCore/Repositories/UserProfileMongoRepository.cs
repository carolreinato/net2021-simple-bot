using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBotCore.Logic;
using System;

namespace SimpleBotCore.Repositories
{
    public class UserProfileMongoRepository : IUserProfileRepository
    {
        MongoClient _client;
        IMongoCollection<SimpleUser> _userProfile;
        public UserProfileMongoRepository(MongoClient client)
        {
            _client = client;
            var db = client.GetDatabase("Bot");
            var collection = db.GetCollection<SimpleUser>("UserProfile");
            _userProfile = collection;
        }

        public void AtualizaCor(string userId, string cor)
        {
            if (cor == null)
                throw new ArgumentNullException(nameof(cor));

            if (!Exists(userId))
                throw new InvalidOperationException("Usuário não existe");

            UpdateCor(userId, cor);
        }

        public void AtualizaIdade(string userId, int idade)
        {
            if (idade <= 0)
                throw new ArgumentOutOfRangeException(nameof(idade));

            if (!Exists(userId))
                throw new InvalidOperationException("Usuário não existe");

            UpdateIdade(userId, idade);
        }

        public void AtualizaNome(string userId, string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (!Exists(userId))
                throw new InvalidOperationException("Usuário não existe");

            UpdateNome(userId, name);
        }

        public SimpleUser Create(SimpleUser user)
        {
            if (Exists(user.Id))
                throw new InvalidOperationException("Usuário ja existente");

            SaveUser(user);

            return user;
        }

        public SimpleUser TryLoadUser(string userId)
        {
            if (Exists(userId))
            {
                return GetUser(userId);
            }

            return null;
        }

        private bool Exists(string userId)
        {
            return _userProfile.Find<SimpleUser>(x => x.Id == userId).Any();
        }

        private SimpleUser GetUser(string userId)
        {
            var query = _userProfile.Find(x => x.Id == userId).Single();
            return query;
        }

        private void SaveUser(SimpleUser user)
        {
            _userProfile.InsertOne(user);
        }

        private void UpdateNome(string userId, string nome)
        {
            var filter = Builders<SimpleUser>.Filter.Eq("_id", userId);
            var update = Builders<SimpleUser>.Update.Set("Nome", nome);

            _userProfile.UpdateOne(filter, update);
        }

        private void UpdateIdade(string userId, int idade)
        {
            var filter = Builders<SimpleUser>.Filter.Eq("_id", userId);
            var update = Builders<SimpleUser>.Update.Set("Idade", idade);

            _userProfile.UpdateOne(filter, update);
        }

        private void UpdateCor(string userId, string cor)
        {
            var filter = Builders<SimpleUser>.Filter.Eq("_id", userId);
            var update = Builders<SimpleUser>.Update.Set("Cor", cor);

            _userProfile.UpdateOne(filter, update);
        }
    }
}
