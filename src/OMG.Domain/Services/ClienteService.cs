﻿using OMG.Domain.Base.Contract;
using OMG.Domain.Contracts.Service;
using OMG.Domain.Entities;

namespace OMG.Domain.Services;

public class ClienteService(IRepositoryEntity<Cliente> clienteRepository) : IClienteService
{
    private readonly IRepositoryEntity<Cliente> repository = clienteRepository;

    public async Task<Cliente> Get(int id)
    => await repository.Get(id);
}
