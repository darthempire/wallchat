using System;
using wallchat.Model.App.EF;

namespace wallchat.DAL.App.Contracts
{
    public interface IDatabaseFactory : IDisposable
    {
        DatabaseContext Get ();
    }
}