using API_DesafioNet.Models;
using API_DesafioNet.ViewModels;

namespace API_DesafioNet.Interfaces.Services
{
    public interface IClienteService
    {
        Task<RetornoPostVm> GravarClienteAsync(ClienteModel cliente);
        Task<RetornoPostVm> AtualizarClienteAsync(ClienteModel cliente);
        Task<RetornoPostVm> DeletarClienteAsync(ClienteModel cliente);
        Task<List<ClienteModel>> ListarClienteAsync(ClienteModel cliente);
    }
}
