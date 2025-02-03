using Microsoft.AspNetCore.Mvc;
using TestPrimoQuadrimestre.Dto;

namespace TestPrimoQuadrimestre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        List<Persona> listaPersone = new List<Persona>();

        // GET: api/<PersonaController>
        [HttpGet]
        public IEnumerable<Persona> StampaPersone()
        {
            return listaPersone;
        }

        // POST api/<PersonaController>
        [HttpPost]
        public bool CreaPersona(Persona personaCreata)
        {
            listaPersone.Add(personaCreata);
            return true;
        }

        // PUT api/<PersonaController>/5
        [HttpPut("{id}")]
        public IActionResult AggiornaPersona(Guid id, [FromBody] Persona personaAggiornata)
        {
            var persona = listaPersone.FirstOrDefault(p => p.Id == id);

            if (persona == null)
            {
                return NotFound("Persona non trovata!");
            }

            persona.Nome = personaAggiornata.Nome;
            persona.Cognome = personaAggiornata.Cognome;
            persona.DataDiNascita = personaAggiornata.DataDiNascita;
            return Ok("Persona aggiornata!");
        }

        // DELETE api/<PersonaController>/5
        [HttpDelete("{id}")]
        public IActionResult EliminaPersona(Guid id)
        {
            var persona = listaPersone.FirstOrDefault(p => p.Id == id);

            if (persona == null)
            {
                return NotFound("Persona non trovata!");
            }

            listaPersone.Remove(persona);

            return Ok("Persona eliminata!");
        }

        // GET api/<PersonaController>/search/5
        [HttpGet("{id}")]
        public IActionResult CercaPersona(Guid id)
        {
            var personaDaCercare = listaPersone.FirstOrDefault(p => p.Id == id);
            if (personaDaCercare == null)
            {
                return NotFound("Persona non trovata!");
            }
            return Ok(personaDaCercare);
        }
    }
}
