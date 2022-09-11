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
    public class EkipaController : ControllerBase
    {
        public LigaContext Context { get; set; }

        public EkipaController(LigaContext context)
        {
            Context = context;
        }
        
        [Route("DodajEkipu")]
        [HttpPost]
        public async Task<ActionResult> DodajEkipu([FromBody] Ekipa ekipa)
        {
            try
            {  
                Stadion stadion=await this.Context.Stadioni.Where(x=>x.ID==ekipa.EkipaStadionID).FirstOrDefaultAsync();
				if(stadion == null)
					return NotFound("Stadion ne postoji");
                ekipa.Bodovi=0;
                ekipa.EkipaStadion=stadion;

                Context.Ekipe.Add(ekipa);
                await Context.SaveChangesAsync();
                return Ok(ekipa);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PreuzetiEkipe")]
        [HttpGet]
        public async Task<ActionResult> PreuzetiEkipe()
        {
            return Ok(await Context.Ekipe.Select(p =>
            new
            {
                id = p.ID,
                ime=p.ImeEkipe,
                bodovi=p.Bodovi,
                stadion=p.EkipaStadion.StadionName,
            }).ToListAsync());
        }

        [Route("PrikaziEkipu")]
        [HttpGet]
        public async Task<ActionResult> PrikaziEkipu(int EkipaID)
        {
            try
            {   var ekipa = await Context.Ekipe
                    .Include(p=>p.EkipaStadion)
                    .Where(x=>x.ID==EkipaID)
                    .Select(p=>new{
                        id=p.ID,
                        ime=p.ImeEkipe,
                        bodovi=p.Bodovi,
                        stadion=p.EkipaStadion.StadionName
                    }
                    ).ToListAsync();
                return Ok(ekipa);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }            


        [Route("SveUtakmiceEkipe/{EkipaID}")]
        [HttpGet]
        public async Task<ActionResult> SveUtakmiceEkipe(int EkipaID)
        {
            try
            {   var utakmice = await Context.Utakmice
                    .Include(x=>x.UtakmicaKolo)
                    .Where(x=>x.GostujucaEkipa.EkipaGost.ID==EkipaID || x.DomacaEkipa.EkipaDomacin.ID==EkipaID)
                    .Select(p=>new{
                        pobednik=p.Pobednik,
                        datum=p.DatumIVremeUtakmice.ToShortDateString(),
                        vreme=p.DatumIVremeUtakmice.ToShortTimeString(),
                        imeDomacina=p.DomacaEkipa.EkipaDomacin.ImeEkipe,
                        goloviDomacin=p.brGolovaDomacin,
                        imeGosta=p.GostujucaEkipa.EkipaGost.ImeEkipe,
                        goloviGost=p.brGolovaGost,
                    }
                    ).ToListAsync();
                return Ok(utakmice);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("DomacaUtakmiceEkipe")]
        [HttpGet]
        public async Task<ActionResult> DomaceUtakmiceEkipe(int EkipaID)
        {
            try
            {   var utakmice = await Context.Utakmice
                    .Include(x=>x.UtakmicaKolo)
                    .Where(x=>x.DomacaEkipa.EkipaDomacin.ID==EkipaID)
                    .Select(p=>new{
                        pobednik=p.Pobednik,
                        datum=p.DatumIVremeUtakmice.ToShortDateString(),
                        vreme=p.DatumIVremeUtakmice.ToShortTimeString(),
                        imeDomacina=p.DomacaEkipa.EkipaDomacin.ImeEkipe,
                        goloviDomacin=p.brGolovaDomacin,
                        imeGosta=p.GostujucaEkipa.EkipaGost.ImeEkipe,
                        goloviGost=p.brGolovaGost,
                    }
                    ).ToListAsync();
                return Ok(utakmice);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("GostujuceUtakmiceEkipe")]
        [HttpGet]
        public async Task<ActionResult> GostujuceUtakmiceEkipe(int EkipaID)
        {
            try
            {   var utakmice = await Context.Utakmice
                    .Include(x=>x.UtakmicaKolo)
                    .Where(x=>x.GostujucaEkipa.EkipaGost.ID==EkipaID)
                    .Select(p=>new{
                        pobednik=p.Pobednik,
                        datum=p.DatumIVremeUtakmice.ToShortDateString(),
                        vreme=p.DatumIVremeUtakmice.ToShortTimeString(),
                        imeDomacina=p.DomacaEkipa.EkipaDomacin.ImeEkipe,
                        goloviDomacin=p.brGolovaDomacin,
                        imeGosta=p.GostujucaEkipa.EkipaGost.ImeEkipe,
                        goloviGost=p.brGolovaGost,
                    }
                    ).ToListAsync();
                return Ok(utakmice);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }  

        [Route("PobedjeneUtakmiceEkipe")]
        [HttpGet]
        public async Task<ActionResult> PobedjeneUtakmiceEkipe(int EkipaID)
        {
            try
            {   var utakmice = await Context.Utakmice
                    .Include(x=>x.UtakmicaKolo)
                    .Where(x=>x.GostujucaEkipa.EkipaGost.ID==EkipaID && x.Pobednik==2 || x.DomacaEkipa.EkipaDomacin.ID==EkipaID && x.Pobednik==1)
                    .Select(p=>new{
                        pobednik=p.Pobednik,
                        datum=p.DatumIVremeUtakmice.ToShortDateString(),
                        vreme=p.DatumIVremeUtakmice.ToShortTimeString(),
                        imeDomacina=p.DomacaEkipa.EkipaDomacin.ImeEkipe,
                        goloviDomacin=p.brGolovaDomacin,
                        imeGosta=p.GostujucaEkipa.EkipaGost.ImeEkipe,
                        goloviGost=p.brGolovaGost,
                    }
                    ).ToListAsync();
                return Ok(utakmice);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzgubljeneUtakmiceEkipe")]
        [HttpGet]
        public async Task<ActionResult> IzgubjeneUtakmiceEkipe(int EkipaID)
        {
            try
            {   var utakmice = await Context.Utakmice
                    .Include(x=>x.UtakmicaKolo)
                    .Where(x=>x.GostujucaEkipa.EkipaGost.ID==EkipaID && x.Pobednik==1 || x.DomacaEkipa.EkipaDomacin.ID==EkipaID && x.Pobednik==2)
                    .Select(p=>new{
                        pobednik=p.Pobednik,
                        datum=p.DatumIVremeUtakmice.ToShortDateString(),
                        vreme=p.DatumIVremeUtakmice.ToShortTimeString(),
                        imeDomacina=p.DomacaEkipa.EkipaDomacin.ImeEkipe,
                        goloviDomacin=p.brGolovaDomacin,
                        imeGosta=p.GostujucaEkipa.EkipaGost.ImeEkipe,
                        goloviGost=p.brGolovaGost,
                    }
                    ).ToListAsync();
                return Ok(utakmice);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        

        [Route("NereseneUtakmiceEkipe")]
        [HttpGet]
        public async Task<ActionResult> NereseneUtakmiceEkipe(int EkipaID)
        {
            try
            {   var utakmice = await Context.Utakmice
                    .Include(x=>x.UtakmicaKolo)
                    .Where(x=>x.GostujucaEkipa.EkipaGost.ID==EkipaID && x.Pobednik==3 || x.DomacaEkipa.EkipaDomacin.ID==EkipaID && x.Pobednik==3)
                    .Select(p=>new{
                        pobednik=p.Pobednik,
                        datum=p.DatumIVremeUtakmice.ToShortDateString(),
                        vreme=p.DatumIVremeUtakmice.ToShortTimeString(),
                        imeDomacina=p.DomacaEkipa.EkipaDomacin.ImeEkipe,
                        goloviDomacin=p.brGolovaDomacin,
                        imeGosta=p.GostujucaEkipa.EkipaGost.ImeEkipe,
                        goloviGost=p.brGolovaGost,
                    }
                    ).ToListAsync();
                return Ok(utakmice);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }    

        [Route("IgraciEkipe/{domacin}/{gost}")]
        [HttpGet]
        public async Task<ActionResult> IgraciEkipe(int domacin,int gost)
        {
            try
            {   var igraci = await Context.Igraci
                    .Where(x=>x.IgracEkipe.ID==domacin || x.IgracEkipe.ID==gost)
                    .Select(p=>new{
                        id=p.ID,
                        ime=p.Ime+" "+p.Prezime,
                        licnoime=p.Ime,
                        prezime=p.Prezime,
                        broj=p.BrojNaDresu,
                        idEkipe=p.IgracEkipe.ID
                    }
                    ).ToListAsync();
                return Ok(igraci);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [Route("IzmeniStadionEkipe")]
        [HttpPut]
        public async Task<ActionResult> IzmeniEkipu([FromQuery] int idEkipe,int idStadiona)
        {
            try
            {  
                Stadion stadion=await this.Context.Stadioni.Where(x=>x.ID==idStadiona).FirstOrDefaultAsync();
				if(stadion == null)
					return NotFound("Stadion ne postoji");
                Ekipa ekipa=await this.Context.Ekipe.Where(x=>x.ID==idEkipe).FirstOrDefaultAsync();
				if(stadion == null)
					return NotFound("Ekipa ne postoji");
                
                ekipa.EkipaStadion=stadion;
                Context.Ekipe.Update(ekipa);
                await Context.SaveChangesAsync();
                return Ok(ekipa);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [Route("AzurirajBodve")]
        [HttpPut]
        public async Task<ActionResult> AzururajBodove([FromQuery] int idEkipe)
        {
            try
            {  
                int bodovi=0;
                Ekipa ekipa=await this.Context.Ekipe.Where(x=>x.ID==idEkipe).FirstOrDefaultAsync();
				if(ekipa == null)
					return NotFound("Ekipa ne postoji");
                
                var utakmica=await this.Context.Utakmice
                    .Include(x=>x.GostujucaEkipa)
                    .Include(x=>x.DomacaEkipa)
                    .Where(x=>x.GostujucaEkipa.EkipaGost==ekipa || x.DomacaEkipa.EkipaDomacin==ekipa).ToListAsync();
                
                foreach(Utakmica u in utakmica){
                    if(u.Pobednik==1 && u.DomacaEkipa.EkipaDomacin==ekipa)
                    {
                        bodovi=bodovi+3;
                        ekipa.Bodovi=bodovi;
                    }
                    else if(u.Pobednik==2 && u.GostujucaEkipa.EkipaGost==ekipa)
                    {
                        bodovi=bodovi+3;
                        ekipa.Bodovi=bodovi;
                    }
                    else if(u.Pobednik==3)
                    {
                        bodovi=bodovi+1;
                        ekipa.Bodovi=bodovi;
                    }
                }
                Context.Ekipe.Update(ekipa);
                await Context.SaveChangesAsync();
                return Ok(ekipa);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [Route("IzbrisatiEkipu")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiEkipu(int EkipaID)
        {
            if(EkipaID<=0)
            {
                return BadRequest("Pogresan id");
            }
            try
            {
                var ekipa=await Context.Ekipe.Where(x=>x.ID==EkipaID).FirstOrDefaultAsync();
                if(ekipa==null)
					return NotFound("Ekipa ne postoji");
                Context.Ekipe.Remove(ekipa);
                await Context.SaveChangesAsync();
                return Ok(ekipa);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}