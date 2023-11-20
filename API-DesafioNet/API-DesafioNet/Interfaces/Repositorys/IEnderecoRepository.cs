using API_DesafioNet.Models;
using System.Data;

namespace API_DesafioNet.Interfaces.Repositorys
{
    public interface IEnderecoRepository
    {
        DataSet EnderecoAsync(List<EnderecoModel> endereco, int index, int clienteId, string operacao);
        DataSet DeletarEnderecoAsync(int LogradouroId, string operacao);
        DataSet ListarEnderecoAsync(int clienteId, string operacao);
    }
}
