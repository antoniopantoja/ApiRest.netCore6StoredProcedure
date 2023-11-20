using API_DesafioNet.Interfaces.Services;
using API_DesafioNet.Models;
using API_DesafioNet.Repositorys;
using API_DesafioNet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace API_DesafioNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        /// <summary>
        /// Gravar Cliente.
        /// </summary>
        /// <param name="Cliente">Modelo do Cliente.</param>
        /// <response code="200">O Retono da API do Cliente com sucesso.</response>
        /// <response code="400">O modelo do Cliente enviado é inválido.</response>
        /// <response code="500">Ocorreu um erro.</response>
        #region Gravar Cliente
        [HttpPost]
        [Route("Gravar")] 
        public async Task<IActionResult> Post(ClienteModel Cliente)
        {
            try
            {
                RetornoPostVm Ds = await _clienteService.GravarClienteAsync(Cliente);
                if (Ds == null)
                    return NotFound(new { message = "dados não encontrado." });

                return Ok(Ds);
            }
            catch (Exception e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message.Length > 0 ?  new { Code = 400, Message = e.Message.ToString(), Output = 0
                } : new RetornoPostVm());
            }
        }
        #endregion

        /// <summary>
        /// Atualizar Cliente.
        /// </summary>
        /// <param name="Cliente">Modelo do Cliente.</param>
        /// <response code="200">O Retono da API do Cliente com sucesso.</response>
        /// <response code="400">O modelo do Cliente enviado é inválido.</response>
        /// <response code="500">Ocorreu um erro.</response>
        #region Atualizar Cliente
        [HttpPut]
        [Route("Atualizar")]
        public async Task<IActionResult> Put(ClienteModel Cliente)
        {
            try
            {
                RetornoPostVm Ds = await _clienteService.AtualizarClienteAsync(Cliente);
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
        /// Deletar Cliente.
        /// </summary>
        /// <param name="Cliente">Parâmetro do Id do Cliente.</param>
        /// <response code="200">O Retono da API do Cliente com sucesso.</response>
        /// <response code="400">O modelo do Cliente enviado é inválido.</response>
        /// <response code="500">Ocorreu um erro.</response>
        #region Deletar Cliente
        [HttpDelete]
        [Route("Deletar")]
        public async Task<IActionResult> Delete(ClienteModel Cliente)
        {
            try
            {
                RetornoPostVm Ds = await _clienteService.DeletarClienteAsync(Cliente);
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
        /// Listar Cliente.
        /// </summary>
        /// <param name="Cliente">Modelo do Id do Cliente.</param>
        /// <response code="200">O Retono da API do Cliente com sucesso.</response>
        /// <response code="400">O modelo do Cliente enviado é inválido.</response>
        /// <response code="500">Ocorreu um erro.</response>
        #region Listar Cliente
        [HttpPost]
        [Route("Listar")]
        public async Task<IActionResult> Listar(ClienteModel Cliente)
        {
            try
            {
                List<ClienteModel> Ds = await _clienteService.ListarClienteAsync(Cliente);
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
