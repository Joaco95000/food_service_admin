using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IDao<T>
    {
        int Insert(T t);
        int Update(T t);
        int Delete(T t);
        DataTable Select();
    }
}
