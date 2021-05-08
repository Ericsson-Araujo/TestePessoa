using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TestePessoaWeb.Domain;
using TestePessoaWeb.Repository;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TestePessoaWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly ITestePessoaRepository _repo;

        public PessoasController(ITestePessoaRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repo.GetAllPessoasAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Falha inesperada. Mensagem: {ex.Message}");
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                var result = await _repo.GetPessoaAsyncById(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Falha inesperada. Mensagem: {ex.Message}");
            }
        }

        [HttpGet("getByName/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            try
            {
                var result = await _repo.GetPessoasAsyncByName(name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Falha inesperada. Mensagem: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Pessoa model)
        {
            try
            {
                _repo.Add<Pessoa>(model);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"api/pessoas/{model.Id}", model);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Falha inesperada. Mensagem: {ex.Message}");
            }

            return BadRequest();
        }

        [HttpPut("{PessoaId}")]
        public async Task<IActionResult> Put(int PessoaId, Pessoa model)
        {
            try
            {
                if (model == null || model.Id != PessoaId)
                    return BadRequest();

                Pessoa pessoa = await _repo.GetPessoaAsyncById(PessoaId);

                if (pessoa == null)
                    return NotFound();

                pessoa = (Pessoa)model.Clone();
                _repo.Update(pessoa);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"api/pessoas/{model.Id}", pessoa);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Falha inesperada. Mensagem: {ex.Message}");
            }

            return BadRequest();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                Pessoa pessoa = await _repo.GetPessoaAsyncById(Id);

                if (pessoa == null)
                    return NotFound();

                _repo.Remove(pessoa);

                if (await _repo.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Falha inesperada. Mensagem: {ex.Message}");
            }

            return BadRequest();
        }
    }
}
