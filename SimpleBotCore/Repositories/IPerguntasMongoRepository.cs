using SimpleBotCore.Logic;

namespace SimpleBotCore.Repositories
{
    public interface IPerguntasMongoRepository
    {
        void InsertPergunta(SimpleUser user, string pergunta);
    }
}