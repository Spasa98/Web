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
    public class IgracController : ControllerBase
    {
        public LigaContext Context { get; set; }

        public IgracController(LigaContext context)
        {
            Context = context;
        }

        [Route("DodajIgraca")]
        [HttpPost]
        public async Task<ActionResult> DodajIgraca([FromBody] Igrac igrac)
        {
            try
            {  
                Ekipa ekipa=await this.Context.Ekipe.Where(x=>x.ID==igrac.IgracEkipeID).FirstOrDefaultAsync();
				if(ekipa == null)
					return NotFound("Ekipa ne postoji");
                igrac.IgracEkipe=ekipa;
                Context.Igraci.Add(igrac);
                await Context.SaveChangesAsync();
                return Ok(igrac);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzbrisatiIgraca")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiIgraca(int IgracID)
        {
            if(IgracID<=0)
            {
                return BadRequest("Pogresan id");
            }
            try
            {
                var igrac=await Context.Igraci.Where(x=>x.ID==IgracID).FirstOrDefaultAsync();
                if(igrac==null)
					return NotFound("Ekipa ne postoji");
                Context.Igraci.Remove(igrac);
                await Context.SaveChangesAsync();
                return Ok(igrac);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzmeniEkipuIgraca")]
        [HttpPut]
        public async Task<ActionResult> IzmeniEkipuIgraca([FromQuery] int idEkipe,int idIgraca)
        {
            try
            {  
                Ekipa ekipa=await this.Context.Ekipe.Where(x=>x.ID==idEkipe).FirstOrDefaultAsync();
				if(ekipa == null)
					return NotFound("Ekipa ne postoji");
                Igrac igrac=await this.Context.Igraci.Where(x=>x.ID==idIgraca).FirstOrDefaultAsync();
				if(igrac == null)
					return NotFound("Igrac ne postoji");
                igrac.IgracEkipe=ekipa;
        
                Context.Igraci.Update(igrac);
                await Context.SaveChangesAsync();
                return Ok(igrac);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [Route("IzbrisatiStrelca/{StrelacID}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiStrelca(int StrelacID)
        {
            if(StrelacID<=0)
            {
                return BadRequest("Pogresan id");
            }
            try
            {
                var igrac=await Context.Strelci.Where(x=>x.ID==StrelacID).FirstOrDefaultAsync();
                if(igrac==null)
					return NotFound("Strelac ne postoji");
                Context.Strelci.Remove(igrac);
                await Context.SaveChangesAsync();
                return Ok(igrac);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("DodajStrelca/{igracID}/{utakmicaID}/{minut}")]
        [HttpPost]
        public async Task<ActionResult> DodajStrelca(int igracID,int utakmicaID,int minut)
        {
            try
            {  
                // int strelciDomacina=0;
                // int strelciGostiju=0;
                Igrac igrac=await this.Context.Igraci.Where(x=>x.ID==igracID).FirstOrDefaultAsync();
				if(igrac == null)
					return NotFound("Igrac ne postoji");
                Utakmica utakmica =await this.Context.Utakmice.Where(x=>x.ID==utakmicaID).FirstOrDefaultAsync();
                if(utakmica==null)
					return NotFound("Utakmica ne postoji");
                // foreach (Strelac s in utakmica.UtakmicaStrelci)
                // {
                //     if(s.StrelacUtakmice.)
                //         strelciDomacina++;
                // }
                Strelac strelac=new Strelac();

                strelac.StrelacIgrac=igrac;
                strelac.StrelacUtakmice=utakmica;
                strelac.minutGola=minut;

                Context.Strelci.Add(strelac);
                await Context.SaveChangesAsync();
                return Ok(strelac);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzmeniStrelca/{strelacID}/{igracID}/{minutGola}")]
        [HttpPut]
        public async Task<ActionResult> IzmeniStrelca(int strelacID,int igracID,int minutGola)
        {
            try
            {  
                // int strelciDomacina=0;
                // int strelciGostiju=0;
                Strelac strelac=await this.Context.Strelci.Where(x=>x.ID==strelacID).FirstOrDefaultAsync();
				if(strelac == null)
					return NotFound("Strelac ne postoji");
                Igrac igrac=await this.Context.Igraci.Where(x=>x.ID==igracID).FirstOrDefaultAsync();
				if(igrac == null)
					return NotFound("Igrac ne postoji");
                strelac.StrelacIgrac=igrac;
                strelac.minutGola=minutGola;

                Context.Strelci.Update(strelac);
                await Context.SaveChangesAsync();
                return Ok(strelac);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        
    }
}