using OMG.Domain.Base;
using OMG.Domain.Enum;

namespace OMG.Domain.Events;

public class EventChangeStatus : Event
{
    public int IdPedido { get; set; }
    public EPedidoStatus OldStatus { get; set; }
    public EPedidoStatus NewStatus { get; set; }

}
