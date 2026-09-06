using Microsoft.AspNetCore.Mvc;
using Questãoprática.Models;

namespace Questãoprática.Controllers
{
    public class PessoaController : ControllerBase
    {
        private static List<Pessoa> ListaPessoas = new List<Pessoa>();
        [HttpPost]
        public IActionResult Cadastrar(Pessoa pessoa)
        {
            var resultado = ListaPessoas
          .Where(p => p.nome == pessoa.nome).FirstOrDefault();

            if (resultado is null)
            {
                ListaPessoas.Add(pessoa);
                return Ok("Cadastrado com sucesso");
            }
            return BadRequest("Pessoa já cadastrada");

        }

        [HttpPut]
        [Route("Atualizar")]
        public IActionResult Atualizar(Pessoa pessoa)
        {

            var resultado = ListaPessoas
           .Where(p => p.nome == pessoa.nome).FirstOrDefault();

            if (resultado is null)
                return NotFound("Nome informado não existe");

            ListaPessoas.Remove(resultado);
            ListaPessoas.Add(pessoa);
            return Ok("Dados atualizados com sucesso");
        }

        [HttpDelete()]
        [Route("Remover/{cpf}")]
        public IActionResult Remover(string cpf)
        {
            var resultado = ListaPessoas
                .Where(p => p.CPF == cpf).FirstOrDefault();

            if (resultado is null)
                return NotFound("CPF informado não existe");

            ListaPessoas.Remove(resultado);
            return Ok("Pessoa removida com sucesso");
        }
        [HttpGet]
        [Route("BuscarPessoas")]
        public IActionResult ListarPessoas()
        {
            return Ok(ListaPessoas);
        }
        [HttpGet]
        [Route("obterPorCpf")]
        public IActionResult obterporCpf(string cpf)
        {
            var resultado = ListaPessoas.Where(p => p.CPF == cpf);
            if (resultado.Count() == 0)
            {
                return NotFound("Pessoa não encontrada");
            }
            return Ok(resultado);
        }
        [HttpGet]
        [Route("BuscarPessoasIMCBom")]
        public IActionResult BuscarPessoasIMCBom()
        {
            var resultado = ListaPessoas
                .Where(p => p.peso / (p.altura * p.altura) >= 18 &&
                            p.peso / (p.altura * p.altura) <= 24)
                .ToList();

            if (resultado.Count == 0)
            {
                return NotFound("Nenhuma pessoa com IMC bom encontrada");
            }

            return Ok(resultado);
        }
        [HttpGet]
        [Route("BuscarPorNome/{nome}")]
        public IActionResult BuscarPorNome(string nome)
        {
            var resultado = ListaPessoas
                .Where(p => p.nome.Contains(nome, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (resultado.Count == 0)
            {
                return NotFound("Nenhuma pessoa encontrada com esse nome");
            }

            return Ok(resultado);
        }

    }
}
