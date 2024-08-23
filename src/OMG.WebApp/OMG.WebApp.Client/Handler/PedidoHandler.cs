﻿using Microsoft.Extensions.Configuration;
using OMG.Domain.Base;
using OMG.Domain;
using OMG.Domain.Handler;
using OMG.Domain.ViewModels;
using System.Net.Http;
using System.Net.Http.Json;

namespace OMG.WebApp.Client.Handler;

public class PedidoHandler(IHttpClientFactory httpClientFactory) : IPedidoHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuracao.HttpClientNameOMGApi);
    public async Task<Response<IEnumerable<PedidoCard>>> GetPedidoCardList()
    {
        var response = await _client.GetAsync("api/View/Pedido/Card");

        if (response.IsSuccessStatusCode)
            return new Response<IEnumerable<PedidoCard>>(await response.Content.ReadFromJsonAsync<IEnumerable<PedidoCard>>(), (int)response.StatusCode);

        return new Response<IEnumerable<PedidoCard>>((int) response.StatusCode, await response.Content.ReadAsStringAsync());
    }

    public Task<Response<PedidoModal>> GetPedidoModal(int Id)
    {
        throw new NotImplementedException();
    }
}
