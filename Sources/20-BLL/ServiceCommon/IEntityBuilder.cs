using Hulkey.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hulkey.SLL.ServiceCommon
{
    /// <summary>
    /// Interface d'un builder
    /// Un builder fabrique une instance d'une entite
    /// </summary>
    public interface IEntityBuilder<T_BUILD_TYPE>
    {
        T_BUILD_TYPE Build();

        IEntityBuilder<T_BUILD_TYPE> WithExistingProduit(int iID);
        IEntityBuilder<T_BUILD_TYPE> WithDefaultValue();
    }

}
