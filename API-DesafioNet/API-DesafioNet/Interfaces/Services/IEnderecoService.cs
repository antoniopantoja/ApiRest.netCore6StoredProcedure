using API_DesafioNet.Models;
using API_DesafioNet.ViewModels;

namespace API_DesafioNet.Interfaces.Services
{
    public interface IEnderecoService
    {
        Task<RetornoPostVm> GravarEnderecoAsync(List<EnderecoModel> endereco, int clienteId);
        Task<RetornoPostVm> AtualizarEnderecoAsync(List<EnderecoModel> endereco, int clienteId);
        Task<RetornoPostVm> DeletarEnderecoAsync(int LogradouroId);
        Task<List<EnderecoModel>> ListarEnderecoAsync(int clienteId);
    }
}
