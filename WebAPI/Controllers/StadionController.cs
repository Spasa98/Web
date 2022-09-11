using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StadionController : ControllerBase
    {
        public LigaContext Context { get; set; }

        public StadionController(LigaContext context)
        {
            Context = context;
        }
        
        [Route("DodajStadion")]
        [HttpPost]
        public async Task<ActionResult> DodajStadion([FromBody] Stadion stadion)
        {
            try
            {  
                Context.Stadioni.Add(stadion);
                await Context.SaveChangesAsync();
                return Ok(stadion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PrikaziStadion")]
        [HttpGet]
        public async Task<ActionResult> PrikaziStadion([FromQuery] int StaidonID)
        {
            try
            {   
                var stadion=await Context.Stadioni
                    .Where(x=>x.ID==StaidonID)
                    .Select(p=>new{ImeStadiona=p.StadionName})
                    .FirstOrDefaultAsync();
                if(stadion==null)
                {
                    return NotFound("Ne postoji stadion s ovim ID-jem");
                } 
                return Ok(stadion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("IzmeniStadion")]
        [HttpPut]
        public async Task<ActionResult> IzmeniStadion([FromBody] Stadion stadion)
        {
            try
            {  
                Context.Stadioni.Update(stadion);
                await Context.SaveChangesAsync();
                return Ok(stadion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzbrisatiStadion")]
        [HttpDelete]
        public async Task<ActionResult> Izbrisi(int StaidonID)
        {
            if(StaidonID<=0)
            {
                return BadRequest("Pogresan id");
            }
            try
            {
                var stadion=await Context.Stadioni.Where(x=>x.ID==StaidonID).FirstOrDefaultAsync();
                Context.Stadioni.Remove(stadion);
                await Context.SaveChangesAsync();
                return Ok(stadion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}