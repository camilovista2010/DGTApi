using System.Data; 

namespace DGT.Data.Connection
{
    public interface IConnectionFactory
    {
        public IDbConnection GetConnection { get; }
        public void CloseConnection(); 
        public void InitDatabase();
    }
}
