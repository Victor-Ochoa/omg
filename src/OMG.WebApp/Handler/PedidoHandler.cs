﻿using OMG.Domain.Base;
using OMG.Domain.Handler;
using OMG.Domain.ViewModels;
using OMG.Domain;
using System.Net.Http.Json;
using System.Net.Http;

namespace OMG.WebApp.Handler;

public class PedidoHandler(IHttpClientFactory httpClientFactory) : IPedidoHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuracao.HttpClientNameOMGApi);
    public async Task<Response<IEnumerable<PedidoCard>>> GetPedidoCardList()
    {
        var response = await _client.GetAsync("api/View/Pedido/Card");

        if (response.IsSuccessStatusCode)
            return new Response<IEnumerable<PedidoCard>>(await response.Content.ReadFromJsonAsync<IEnumerable<PedidoCard>>(), (int)response.StatusCode);

        return new Response<IEnumerable<PedidoCard>>((int)response.StatusCode, await response.Content.ReadAsStringAsync());
    }

    public async Task<Response<PedidoModal>> GetPedidoModal(int Id)
    {
        var response = await _client.GetAsync($"api/View/Pedido/Modal/{Id}");

        if (response.IsSuccessStatusCode)
            return new Response<PedidoModal>(await response.Content.ReadFromJsonAsync<PedidoModal>(), (int)response.StatusCode);

        return new Response<PedidoModal>((int)response.StatusCode, await response.Content.ReadAsStringAsync());
    }
}