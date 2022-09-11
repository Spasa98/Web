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
    public class KoloController : ControllerBase
    {
        public LigaContext Context { get; set; }

        public KoloController(LigaContext context)
        {
            Context = context;
        }
        [Route("DodajKolo")]
        [HttpPost]
        public async Task<ActionResult> DodajKolo([FromBody] Kolo kolo)
        {
            try
            {  
                Context.Kola.Add(kolo);
                await Context.SaveChangesAsync();
                return Ok(kolo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PreuzetiKola")]
        [HttpGet]
        public async Task<ActionResult> PreuzetiKola()
        {
            return Ok(await Context.Kola.Select(p =>
            new
            {
                id = p.ID,
                rbr = p.rbrKola
            }).ToListAsync());
        }

        [Route("UtakmiceJednogKola")]
        [HttpGet]
        public async Task<ActionResult> UtakmiceKola(int koloID)
        {
            try
            {   var utakmice = await Context.Utakmice
                    .Include(x=>x.UtakmicaKolo)
                    .Where(x=>x.UtakmicaKolo.ID==koloID)
                    .Select(p=>new{
                        id=p.ID,
                        pobednik=p.Pobednik,
                        datum=p.DatumIVremeUtakmice.ToShortDateString(),
                        vreme=p.DatumIVremeUtakmice.ToShortTimeString(),
                        ceoDatum=p.DatumIVremeUtakmice,
                        imeDomacina=p.DomacaEkipa.EkipaDomacin.ImeEkipe,
                        goloviDomacin=p.brGolovaDomacin,
                        imeGosta=p.GostujucaEkipa.EkipaGost.ImeEkipe,
                        goloviGost=p.brGolovaGost,
                        idDomacina=p.DomacaEkipa.EkipaDomacin.ID,
                        idGosta=p.GostujucaEkipa.EkipaGost.ID,
                    }
                    ).ToListAsync();
                return Ok(utakmice);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("UtakmiceRednogBrojaKola")]
        [HttpGet]
        public async Task<ActionResult> UtakmiceRednogBrojaKola(int rbrKola)
        {
            try
            {   var utakmice = await Context.Utakmice
                    .Include(x=>x.UtakmicaKolo)
                    .Where(x=>x.UtakmicaKolo.rbrKola==rbrKola)
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

        [Route("UtakmiceZadnjegKola")]
        [HttpGet]
        public async Task<ActionResult> UtakmiceZadnjegKola()
        {
            try
            {   
                var kolo=await this.Context.Kola.Where(x=>x.pocetakKola<=DateTime.Now).OrderByDescending(x=>x.pocetakKola).FirstOrDefaultAsync();
                var utakmice = await Context.Utakmice
                    .Include(x=>x.UtakmicaKolo)
                    .Where(x=>x.UtakmicaKolo==kolo)
                    .Select(p=>new{
                        id=p.ID,
                        pobednik=p.Pobednik,
                        datum=p.DatumIVremeUtakmice.ToShortDateString(),
                        vreme=p.DatumIVremeUtakmice.ToShortTimeString(),
                        imeDomacina=p.DomacaEkipa.EkipaDomacin.ImeEkipe,
                        goloviDomacin=p.brGolovaDomacin,
                        imeGosta=p.GostujucaEkipa.EkipaGost.ImeEkipe,
                        goloviGost=p.brGolovaGost,
                        idKola=p.UtakmicaKolo.ID
                    }
                    ).ToListAsync();
                return Ok(utakmice);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }            

        [Route("IzmeniKolo")]
        [HttpPut]
        public async Task<ActionResult> IzmeniDatumKola([FromBody] Kolo kolo)
        {
            try
            {  
                Context.Kola.Update(kolo);
                await Context.SaveChangesAsync();
                return Ok(kolo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("IzbrisatiKolo")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiKolo(int KoloID)
        {
            if(KoloID<=0)
            {
                return BadRequest("Pogresan id");
            }
            try
            {
                var kolo=await Context.Kola.Where(x=>x.ID==KoloID).FirstOrDefaultAsync();
                Context.Kola.Remove(kolo);
                await Context.SaveChangesAsync();
                return Ok(kolo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}