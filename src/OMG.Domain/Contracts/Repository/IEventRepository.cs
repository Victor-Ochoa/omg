﻿using OMG.Domain.Enum;

namespace OMG.Domain.Contracts.Repository;

public interface IEventRepository
{
    Task EventChangeStatusPedido(int idPedido, EPedidoStatus oldStatus, EPedidoStatus newStatus);
}
