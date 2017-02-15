using App1.Interfaces;
using App1.Model;
using App1.Services.Standard;

namespace App1.Services.Standard
{
    public class MyItemStore : BaseStore<MyItem>, IMyItemStore
    {
        public override string Identifier => "MyItem";
    }
}
