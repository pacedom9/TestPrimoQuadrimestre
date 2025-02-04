using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using TestPrimoQuadrimestre.Dto;

namespace TestPrimoQuadrimestre.Controllers
{
    [Route("api/persone")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private static readonly string FilePath = "persone.json"; 

        private List<Persona> listaPersone = new List<Persona>();

        public PersonaController()
        {
            listaPersone = CaricaPersoneDaFile(); 
        }

        // GET: api/persone
        [HttpGet]
        public IEnumerable<Persona> StampaPersone()
        {
            return listaPersone;
        }

        // POST api/persone
        [HttpPost]
        public IActionResult CreaPersona(Persona personaCreata)
        {
            listaPersone.Add(personaCreata);
            SalvaPersoneSuFile();
            return Ok("Persona creata!");
        }

        // PUT api/persone/{id}
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
            SalvaPersoneSuFile(); 
            return Ok("Persona aggiornata!");
        }

        // DELETE api/persone/{id}
        [HttpDelete("{id}")]
        public IActionResult EliminaPersona(Guid id)
        {
            var persona = listaPersone.FirstOrDefault(p => p.Id == id);

            if (persona == null)
            {
                return NotFound("Persona non trovata!");
            }

            listaPersone.Remove(persona);
            SalvaPersoneSuFile();
            return Ok("Persona eliminata!");
        }

        // GET api/persone/{id}
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

        
        private List<Persona> CaricaPersoneDaFile()
        {
            if (!System.IO.File.Exists(FilePath))
            {
                return new List<Persona>();
            }

            try
            {
                var jsonData = System.IO.File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<List<Persona>>(jsonData) ?? new List<Persona>();
            }
            catch (Exception)
            {
                return new List<Persona>();
            }
        }
        private void SalvaPersoneSuFile()
        {
            try
            {
                var jsonData = JsonSerializer.Serialize(listaPersone, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                System.IO.File.WriteAllText(FilePath, jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nel salvataggio delle persone su file: {ex.Message}");
            }
        }

    }
}
