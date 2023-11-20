using API_DesafioNet.Interfaces.Repositorys;
using API_DesafioNet.Interfaces.Services;
using API_DesafioNet.Models;
using API_DesafioNet.Repositorys;
using API_DesafioNet.ViewModels;
using Newtonsoft.Json;

namespace API_DesafioNet.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;
        public EnderecoService(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }
        public async Task<RetornoPostVm> GravarEnderecoAsync(List<EnderecoModel> endereco, int clienteId)
        {
            BaseRepository Ds = new BaseRepository();
            RetornoPostVm retornoPostVm = new RetornoPostVm();

            ValidarEnderecoCliente(endereco);

            if (clienteId == 0)
                throw new Exception($"O campo ClienteId está vazio");

            for (int i = 0; endereco.Count > i; i++)
            {
                var REndereco = retornoPostVm.JSonParaObjectlist(JsonConvert.SerializeObject(_enderecoRepository.EnderecoAsync(endereco, i, clienteId, OperacaoModel.Insert.GetDescription())));
                retornoPostVm.Message = REndereco.Message; retornoPostVm.Code = REndereco.Code;
            }

            return retornoPostVm;
        }

        public async Task<RetornoPostVm> AtualizarEnderecoAsync(List<EnderecoModel> endereco, int clienteId)
        {
            BaseRepository Ds = new BaseRepository();
            RetornoPostVm retornoPostVm = new RetornoPostVm();

            if (clienteId == 0)
                throw new Exception($"O campo ClienteId está vazio");

            for (int i = 0; endereco.Count > i; i++)
            {
                var REndereco = retornoPostVm.JSonParaObjectlist(JsonConvert.SerializeObject(_enderecoRepository.EnderecoAsync(endereco, i, clienteId, OperacaoModel.Update.GetDescription())));
                retornoPostVm.Message = REndereco.Message; retornoPostVm.Code = REndereco.Code;
            }

            return retornoPostVm;
        }

        public async Task<RetornoPostVm> DeletarEnderecoAsync(int LogradouroId)
        {
            BaseRepository Ds = new BaseRepository();
            RetornoPostVm retornoPostVm = new RetornoPostVm();

            var REndereco = retornoPostVm.JSonParaObjectlist(JsonConvert.SerializeObject(_enderecoRepository.DeletarEnderecoAsync(LogradouroId, OperacaoModel.Delete.GetDescription())));
            retornoPostVm.Message = REndereco.Message; retornoPostVm.Code = REndereco.Code;

            return retornoPostVm;
        }

        public async Task<List<EnderecoModel>> ListarEnderecoAsync(int clienteId)
        {
            BaseRepository Ds = new BaseRepository();
            RetornoPostVm retornoPostVm = new RetornoPostVm();

            if (clienteId == 0)
                throw new Exception($"O campo ClienteId está vazio");

            List<EnderecoModel> listarCliente = JsonConvert.DeserializeObject<List<EnderecoModel>>(retornoPostVm.JSonSubRemove(JsonConvert.SerializeObject(_enderecoRepository.ListarEnderecoAsync(clienteId, OperacaoModel.List.GetDescription()))));

            return listarCliente;
        }

        public void ValidarEnderecoCliente(List<EnderecoModel> endereco)
        {
            if (endereco.Count >= 1)
            {

                for (int i = 0; endereco.Count > i; i++)
                {
                    if (string.IsNullOrEmpty(endereco[i].Logradura))
                        throw new Exception($"O campo Logradura está vazio No Endereço {i + 1}");

                    if (string.IsNullOrEmpty(endereco[i].Uf))
                        throw new Exception($"O campo Uf está vazio No Endereço {i + 1}");

                    if (string.IsNullOrEmpty(endereco[i].Cep))
                        throw new Exception($"O campo Cep está vazio No Endereço {i + 1}");

                    if (string.IsNullOrEmpty(endereco[i].Cidade))
                        throw new Exception($"O campo Cidade está vazio No Endereço {i + 1}");

                    if (string.IsNullOrEmpty(endereco[i].Bairro))
                        throw new Exception($"O campo Bairro está vazio No Endereço {i + 1}");

                    if (endereco[i].Numero == 0)
                        throw new Exception($"O campo Numero está vazio No Endereço {i + 1}");
                }

            }
            else
            {
                throw new Exception("Endereço está vazio.");
            }

        }
    }
}
