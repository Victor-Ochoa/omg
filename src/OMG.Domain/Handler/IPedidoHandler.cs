using OMG.Domain.Base;
using OMG.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.Domain.Handler
{
    public interface IPedidoHandler
    {
        Task<Response<IEnumerable<PedidoCard>>> GetPedidoCardList();
        Task<Response<PedidoModal>> GetPedidoModal(int Id);
    }
}
