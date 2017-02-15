using App1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Interfaces
{
    public interface IMyItemStore : IBaseStore<MyItem>
    {
        //Add additional interface methods
        //specific to this data store.
    }
}
