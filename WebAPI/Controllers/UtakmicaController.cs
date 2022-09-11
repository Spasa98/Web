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
    public class UtakmicaController : ControllerBase
    {
        public LigaContext Context { get; set; }

        public UtakmicaController(LigaContext context)
        {
            Context = context;
        }
        [Route("DodajUtakmicu/{GostID}/{DomacinID}/{KoloID}/{goloviDomacin}/{goloviGost}/{datum}")]
        [HttpPost]
        public async Task<ActionResult> DodajUtakmicu(int GostID,int DomacinID,int KoloID,int goloviDomacin,int goloviGost,DateTime datum)
        {
            try
            {  
                if(datum>DateTime.Now)
                {
                    return BadRequest("Datum nije validan");
                }
                if(GostID==DomacinID)
                {
                    return BadRequest("Ne moze igrati ekipa sama sa sobom");
                }
                var gostEkipa=await this.Context.Ekipe.Where(x=>x.ID==GostID).FirstOrDefaultAsync();
                if(gostEkipa == null)
					return NotFound("Ekipa ne ne postoji");
                
                var domacinEkipa=await this.Context.Ekipe.Where(x=>x.ID==DomacinID).FirstOrDefaultAsync();
                if(domacinEkipa == null)
					return NotFound("Ekipa ne postoji");

                var kolo=await this.Context.Kola.Where(x=>x.ID==KoloID).FirstOrDefaultAsync();
                if(kolo == null)
					return NotFound("Ne postoji ovo kolo");
                // foreach(Utakmica u in kolo.KoloUtakmice)
                // {
                //     if(u.DomacaEkipa.ID==DomacinID || u.DomacaEkipa.ID==GostID || u.GostujucaEkipa.ID==DomacinID || u.GostujucaEkipa.ID==GostID)
                //     {
                //         return BadRequest("Ne moze jedna ekipa igrati 2 utakmice u jednom kolu");
                //     }
                // }
                
                Gost gost=new Gost();
                gost.EkipaGost=gostEkipa;
                Context.Gost.Add(gost);

                Domacin domacin=new Domacin();
                domacin.EkipaDomacin=domacinEkipa; 
                Context.Domacin.Add(domacin);
                
                Utakmica utakmica=new Utakmica();
                utakmica.DatumIVremeUtakmice=datum;
                utakmica.DomacaEkipa=domacin;
                utakmica.GostujucaEkipa=gost;
                utakmica.UtakmicaKolo=kolo;
                utakmica.brGolovaDomacin=goloviDomacin;
                utakmica.brGolovaGost=goloviGost;

                if(goloviDomacin>goloviGost)
                {
                    utakmica.Pobednik=1;
                } 
                else if(goloviDomacin<goloviGost)
                {
                    utakmica.Pobednik=2;
                }
                else if(goloviDomacin==goloviGost)
                {
                    utakmica.Pobednik=3;
                }    

                Context.Utakmice.Add(utakmica);

                await Context.SaveChangesAsync();
                return Ok(utakmica);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("AzurirajUtakmicu/{goloviGostiju}/{goloviDomacina}/{datumivreme}/{utakmicaID}/{GostID}/{DomacinID}")]
        [HttpPut]
        public async Task<ActionResult> UpdateRezultat(int goloviGostiju,int goloviDomacina,DateTime datumivreme,int utakmicaID,int GostID,int DomacinID)
        {
            try
            {  

                var gostEkipa=await this.Context.Ekipe.Where(x=>x.ID==GostID).FirstOrDefaultAsync();
                if(gostEkipa == null)
					return NotFound("Ekipa ne ne postoji");
                
                var domacinEkipa=await this.Context.Ekipe.Where(x=>x.ID==DomacinID).FirstOrDefaultAsync();
                if(domacinEkipa == null)
					return NotFound("Ekipa ne postoji");
                
                // var kolo=await this.Context.Kola.Where(x=>x.ID==KoloID).FirstOrDefaultAsync();
                
                Gost gost=new Gost();
                gost.EkipaGost=gostEkipa;
                Context.Gost.Add(gost);

                Domacin domacin=new Domacin();
                domacin.EkipaDomacin=domacinEkipa; 
                Context.Domacin.Add(domacin);

                var utakmica=await this.Context.Utakmice.Where(x=>x.ID==utakmicaID).FirstOrDefaultAsync();
                if(utakmica==null)
                    return NotFound("Ne postoji ova utakmica");
                // if(utakmica.DatumIVremeUtakmice>DateTime.Now)
                // {
                //     return BadRequest("Utakmica jos uvek nije odigrana");
                // }
                utakmica.DomacaEkipa=domacin;
                utakmica.GostujucaEkipa=gost;
                utakmica.brGolovaDomacin=goloviDomacina;
                utakmica.brGolovaGost=goloviGostiju;
                utakmica.DatumIVremeUtakmice=datumivreme;
                //1-pobednik domacin
                //2-pobednik gost
                //3-nereseno
                if(goloviDomacina>goloviGostiju)
                {
                    utakmica.Pobednik=1;
                } 
                else if(goloviDomacina<goloviGostiju)
                {
                    utakmica.Pobednik=2;
                }
                else if(goloviDomacina==goloviGostiju)
                {
                    utakmica.Pobednik=3;
                }    
                Context.Utakmice.Update(utakmica);
                await Context.SaveChangesAsync();
                return Ok(utakmica);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PromeniTerminUtakmice")]
        [HttpPut]
        public async Task<ActionResult> PromeniTerminUtakmice(DateTime NoviTermin,int utakmicaID)
        {
            try
            {  
                var utakmica=await this.Context.Utakmice.Where(x=>x.ID==utakmicaID).FirstOrDefaultAsync();
                if(utakmica==null)
                    return NotFound("Ne postoji ova utakmica");
                // if(utakmica.DatumIVremeUtakmice<DateTime.Now)
                // {
                //     return BadRequest("Utakmica jos vec prosla");
                // }
                utakmica.DatumIVremeUtakmice=NoviTermin; 
                Context.Utakmice.Update(utakmica);
                await Context.SaveChangesAsync();
                return Ok(utakmica);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("DetaljiUtakmice/{utakmicaID}")]
        [HttpGet]
        public async Task<ActionResult> DetaljiUtakmice(int utakmicaID)
        {
            try
            {   var utakmica = await Context.Utakmice
                    .Include(x=>x.UtakmicaStrelci)
                    .Include(x=>x.UtakmicaKolo)
                    .Where(x=>x.ID==utakmicaID)
                    .Select(p=>new{
                        pobednik=p.Pobednik,
                        datum=p.DatumIVremeUtakmice.ToShortDateString(),
                        vreme=p.DatumIVremeUtakmice.ToShortTimeString(),
                        imeDomacina=p.DomacaEkipa.EkipaDomacin.ImeEkipe,
                        idDomacina=p.DomacaEkipa.EkipaDomacin.ID,
                        idGosta=p.GostujucaEkipa.EkipaGost.ID,
                        goloviDomacin=p.brGolovaDomacin,
                        imeGosta=p.GostujucaEkipa.EkipaGost.ImeEkipe,
                       
                        goloviGost=p.brGolovaGost,
                        stadion=p.DomacaEkipa.EkipaDomacin.EkipaStadion.StadionName,
                        kolo=p.UtakmicaKolo.rbrKola,
                        strelci=p.UtakmicaStrelci.Select(q=>
                        new
                        {
                            idstrelac=q.ID,
                            igrac=q.StrelacIgrac.Ime +" "+q.StrelacIgrac.Prezime+" "+q.StrelacIgrac.BrojNaDresu,
                            gol=q.minutGola,
                            ekipa=q.StrelacIgrac.IgracEkipe.ID,
                        })
                    }
                    ).FirstOrDefaultAsync();
                return Ok(utakmica);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }   

        [Route("IzbrisatiUtakmicu")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiUtakmicu(int UtakmicaID)
        {
            if(UtakmicaID<=0)
            {
                return BadRequest("Pogresan id");
            }
            try
            {
                var utakmica=await Context.Utakmice.Where(x=>x.ID==UtakmicaID).FirstOrDefaultAsync();
                if(utakmica==null)
					return NotFound("Utakmica ne postoji");
                var strelac=await this.Context.Strelci
                    .Include(x=>x.StrelacUtakmice
                    ).Where(x=>x.StrelacUtakmice==utakmica).ToListAsync();
                foreach(Strelac s in strelac){
                    Context.Strelci.Remove(s);
                }
                // utakmica.DomacaEkipa=null;
                // utakmica.GostujucaEkipa=null;
                // utakmica.UtakmicaKolo=null;
                // Context.Utakmice.Update(utakmica);
                Context.Utakmice.Remove(utakmica);
                await Context.SaveChangesAsync();
                return Ok(utakmica);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}