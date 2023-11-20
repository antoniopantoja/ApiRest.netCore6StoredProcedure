using API_DesafioNet.Interfaces.Repositorys;
using API_DesafioNet.Interfaces.Services;
using API_DesafioNet.Models;
using API_DesafioNet.Repositorys;
using API_DesafioNet.ViewModels;
using Newtonsoft.Json;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace API_DesafioNet.Services
{

    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<RetornoPostVm> GravarClienteAsync(ClienteModel cliente)
        {   

            BaseRepository Ds = new BaseRepository();
            RetornoPostVm retornoPostVm = new RetornoPostVm();

            ValidarCliente(cliente, OperacaoModel.Insert.GetDescription());
            string Base64 = cliente.Logotipo;
            string name_file = "FotoPerfil_" + DateTime.Now.ToString("_yyyyMMdd_HHmmss") + ".png";
            cliente.Logotipo = Ds.getConfiguration("File", "Servidor") + name_file;

            retornoPostVm = retornoPostVm.JSonParaObjectlist(JsonConvert.SerializeObject(_clienteRepository.ClienteAsync(cliente, OperacaoModel.Insert.GetDescription())));
            cliente.Logotipo = Ds.getConfiguration("File", "Local") + name_file;

            if (cliente.Logotipo != "" && retornoPostVm.Code == 0)
            {
                Image imagem = Image.FromStream(new MemoryStream(Convert.FromBase64String(Base64)));
                imagem.Save(cliente.Logotipo, ImageFormat.Png);
            }

            return retornoPostVm;

        }

        public async Task<RetornoPostVm> AtualizarClienteAsync(ClienteModel cliente)
        {
            BaseRepository Ds = new BaseRepository();
            RetornoPostVm retornoPostVm = new RetornoPostVm();

            ValidarCliente(cliente, OperacaoModel.Update.GetDescription());
            string Base64 = "";
            string name_file = "FotoPerfil_" + DateTime.Now.ToString("_yyyyMMdd_HHmmss") + ".png";

            if (cliente.ClienteId == 0)
                throw new Exception($"O campo ClienteId está vazio");

            if (cliente.Logotipo != "")
            {
                Base64 = cliente.Logotipo;
                cliente.Logotipo = Ds.getConfiguration("File", "Servidor") + name_file;
            }

            retornoPostVm = retornoPostVm.JSonParaObjectlist(JsonConvert.SerializeObject(_clienteRepository.ClienteAsync(cliente, OperacaoModel.Update.GetDescription())));

            if (Base64 != "" && retornoPostVm.Code == 0)
            {
                cliente.Logotipo = Ds.getConfiguration("File", "Local") + name_file;
                Image imagem = Image.FromStream(new MemoryStream(Convert.FromBase64String(Base64)));
                imagem.Save(cliente.Logotipo, ImageFormat.Png);
            }

            return retornoPostVm;
        }

        public async Task<RetornoPostVm> DeletarClienteAsync(ClienteModel cliente)
        {
            BaseRepository Ds = new BaseRepository();
            RetornoPostVm retornoPostVm = new RetornoPostVm();


            if (cliente.ClienteId == 0)
                throw new Exception($"O campo ClienteId está vazio");


            retornoPostVm = retornoPostVm.JSonParaObjectlist(JsonConvert.SerializeObject(_clienteRepository.ClienteAsync(cliente, OperacaoModel.Delete.GetDescription())));


            return retornoPostVm;
        }

        public async Task<List<ClienteModel>> ListarClienteAsync(ClienteModel cliente)
        {
            BaseRepository Ds = new BaseRepository();
            RetornoPostVm retornoPostVm = new RetornoPostVm();
            List<ClienteModel> listarCliente = JsonConvert.DeserializeObject<List<ClienteModel>>(retornoPostVm.JSonSubRemove(JsonConvert.SerializeObject(_clienteRepository.ClienteAsync(cliente, OperacaoModel.List.GetDescription()))));

            return listarCliente;
        }

        public void ValidarCliente(ClienteModel cliente, string operacao)
        {

            if (string.IsNullOrEmpty(cliente.Logotipo) && operacao == "I")
                throw new Exception($"O campo Logotipo está vazio");

            if (string.IsNullOrEmpty(cliente.Nome))
                throw new Exception($"O campo Nome está vazio");

            if (string.IsNullOrEmpty(cliente.Email))
                throw new Exception($"O campo Email está vazio");
        }

    }
}
