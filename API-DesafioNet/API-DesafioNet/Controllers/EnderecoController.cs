using API_DesafioNet.Interfaces.Services;
using API_DesafioNet.Models;
using API_DesafioNet.Services;
using API_DesafioNet.ViewModels;
using Google.Apis.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace API_DesafioNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _enderecoService;
        public EnderecoController(IEnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }


        /// <summary>
        /// Gravar Endereco.
        /// </summary>
        /// <param name="endereco">Modelo do Endereco.</param>
        /// <response code="200">O Retono da API do Endereco com sucesso.</response>
        /// <response code="400">O modelo do Endereco enviado é inválido.</response>
        /// <response code="500">Ocorreu um erro.</response>
        #region Gravar Endereco
        [HttpPost]
        [Route("Gravar")]
        public async Task<IActionResult> Post(List<EnderecoModel> endereco, int clienteId)
        {
            try
            {
                RetornoPostVm Ds = await _enderecoService.GravarEnderecoAsync(endereco, clienteId);
                if (Ds == null)
                    return NotFound(new { message = "dados não encontrado." });

                return Ok(Ds);
            }
            catch (Exception e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message.Length > 0 ? new
                {
                    Code = 400,
                    Message = e.Message.ToString(),
                    Output = 0
                } : new RetornoPostVm());
            }
        }
        #endregion

        /// <summary>
        /// Atualizar Endereco.
        /// </summary>
        /// <param name="endereco">Modelo do Endereco.</param>
        /// <response code="200">O Retono da API do Endereco com sucesso.</response>
        /// <response code="400">O modelo do Endereco enviado é inválido.</response>
        /// <response code="500">Ocorreu um erro.</response>
        #region Atualizar Endereco
        [HttpPut]
        [Route("Atualizar")]
        public async Task<IActionResult> Put(List<EnderecoModel> endereco, int clienteId)
        {
            try
            {
                RetornoPostVm Ds = await _enderecoService.AtualizarEnderecoAsync(endereco, clienteId);
                if (Ds == null)
                    return NotFound(new { message = "dados não encontrado." });

                return Ok(Ds);
            }
            catch (Exception e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message.Length > 0 ? new
                {
                    Code = 400,
                    Message = e.Message.ToString(),
                    Output = 0
                } : new RetornoPostVm());
            }
        }
        #endregion

        /// <summary>
        /// Deletar Endereco.
        /// </summary>
        /// <param name="LogradouroId">Parâmetro do Id do LogradouroId.</param>
        /// <response code="200">O Retono da API do Endereco com sucesso.</response>
        /// <response code="400">O modelo do Endereco enviado é inválido.</response>
        /// <response code="500">Ocorreu um erro.</response>
        #region Deletar Endereco
        [HttpDelete]
        [Route("Deletar")]
        public async Task<IActionResult> Delete(int LogradouroId)
        {
            try
            {
                RetornoPostVm Ds = await _enderecoService.DeletarEnderecoAsync(LogradouroId);
                if (Ds == null)
                    return NotFound(new { message = "dados não encontrado." });

                return Ok(Ds);
            }
            catch (Exception e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message.Length > 0 ? new
                {
                    Code = 400,
                    Message = e.Message.ToString(),
                    Output = 0
                } : new RetornoPostVm());
            }
        }
        #endregion

        /// <summary>
        /// Listar Endereco.
        /// </summary>
        /// <param name="clienteId">Parâmetro do Id do Cliente.</param>
        /// <response code="200">O Retono da API do Endereco com sucesso.</response>
        /// <response code="400">O modelo do Endereco enviado é inválido.</response>
        /// <response code="500">Ocorreu um erro.</response>
        #region Listar Endereco
        [HttpPost]
        [Route("Listar")]
        public async Task<IActionResult> Listar(int clienteId)
        {
            try
            {
                List<EnderecoModel> Ds = await _enderecoService.ListarEnderecoAsync(clienteId);
                if (Ds == null)
                    return NotFound(new { message = "dados não encontrado." });

                return Ok(Ds);
            }
            catch (Exception e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message.Length > 0 ? new
                {
                    Code = 400,
                    Message = e.Message.ToString(),
                    Output = 0
                } : new RetornoPostVm());
            }
        }
        #endregion 
    }
}
