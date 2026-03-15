using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ProductNotFoundException(int id):NotFoundException($"The With Id {id} Not Found ! !")
    {

    }
}
