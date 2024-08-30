using OMG.Domain.Enum;

namespace OMG.Domain.Request;

public record PedidoChangeStatusRequest (int idPedido, EPedidoStatus NewStatus);