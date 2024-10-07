using OMG.Domain.Base;
using OMG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.Domain.Handler;

public interface IClienteHandler
{
    Task<Response<Cliente>> CreateOrUpdate(Cliente cliente);
}
