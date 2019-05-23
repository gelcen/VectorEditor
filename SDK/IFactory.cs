using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDK
{
    public interface IFactory<T>
    {
        T CreateInstance(string name);

        List<string> GetNamesList();
    }
}
