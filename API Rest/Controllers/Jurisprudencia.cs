using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Rest.Data;
using API_Rest.Models;

namespace API_Rest.Controllers
{
    [ApiController]
    [Route("jurisprudencia")]
    public class JurisprudenciaController : ControllerBase
    {
        private readonly AppDbContext _context;
        public JurisprudenciaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<JurisprudenciaModel> dados)
        {
            _context.Jurisprudencias.AddRange(dados);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Dados gravados com sucesso"} );
        }

        [HttpGet]
        public async Task<ActionResult<List<JurisprudenciaModel>>> Get(
            [FromQuery] string? numeroProcesso,
            [FromQuery] string? classe,
            [FromQuery] string? assunto,
            [FromQuery] string? relator,
            [FromQuery] string? orgaoJulgador,
            [FromQuery] DateTime? dataInicio,
            [FromQuery] DateTime? dataFim,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {

            var query = _context.Jurisprudencias.AsQueryable();

            if (!string.IsNullOrWhiteSpace(numeroProcesso))
                query = query.Where(j => j.NumeroProcesso.Contains(numeroProcesso));

            if (!string.IsNullOrWhiteSpace(classe))
                query = query.Where(j => j.Classe.Contains(classe));

            if (!string.IsNullOrWhiteSpace(assunto))
                query = query.Where(j => j.Assunto.Contains(assunto));

            if (!string.IsNullOrWhiteSpace(relator))
                query = query.Where(j => j.Relator.Contains(relator));

            if (!string.IsNullOrWhiteSpace(orgaoJulgador))
                query = query.Where(j => j.OrgaoJulgador.Contains(orgaoJulgador));

            if (dataInicio.HasValue)
                query = query.Where(j => j.DataJulgamento >= dataInicio.Value);

            if (dataFim.HasValue)
                query = query.Where(j => j.DataJulgamento <= dataFim.Value);

            // Paginação
            var total = await query.CountAsync();
            var data = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new
            {
                total,
                page,
                pageSize,
                data
            });
        }
    }


}