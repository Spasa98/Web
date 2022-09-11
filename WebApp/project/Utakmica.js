import { kreirajDugme, kreirajInput, kreirajSelect } from "./helper.js";
import { Igrac } from "./Igrac.js";
import { Kolo } from './Kolo.js'

export class Utakmica {
  constructor(
    id,
    pobednik,
    datum,
    vreme,
    imeDomacina,
    goloviDomacina,
    imeGosta,
    goloviGosta,
    idGosta,
    idDomacina,
    ceoDatum
  ) {
    this.id = id;
    this.pobednik = pobednik;
    this.datum = datum;
    this.vreme = vreme;
    this.imeDomacina = imeDomacina;
    this.imeGosta = imeGosta;
    this.goloviDomacin = goloviDomacina;
    this.goloviGost = goloviGosta;
    this.idGosta = idGosta;
    this.idDomacina = idDomacina;
    this.ceoDatum = ceoDatum;

    this.kontejnerDetaljiUtakmice = null;
    this.kontejnerPrikaz = null;

  }

  crtaj(host) {
    let kontejner = document.createElement("div");
    kontejner.classList ="kontejner-jedne-utakmice";
    // Header
    let header = document.createElement("div");
    header.classList ="header-jedne-utakmice";
    kontejner.appendChild(header);
    this.kreirajLabelu(header, "", this.imeDomacina);
    this.kreirajLabelu(header,"header-rezultat",`${this.goloviDomacin}:${this.goloviGost}`);
    this.kreirajLabelu(header, "", this.imeGosta);
    this.kreirajLabelu(kontejner, "", `${this.datum} - ${this.vreme}`);
    // Footer
    let footer = document.createElement("div");
    footer.classList = "footer-jedne-utakmice";

    let izmeniDugme = this.kreirajDugme("izmeni-dugme-jedne-utakmice", "Izmeni");
    izmeniDugme.addEventListener("click", () => {
      let glavnaInstanca = host.parentElement;
      glavnaInstanca.getElementsByClassName("submit-button")[0].setAttribute("data-id", this.id);

      let domacaEkipa =glavnaInstanca.getElementsByClassName("selekt-domacin")[0];
      domacaEkipa.value = this.idDomacina;

      let gostEkipa = glavnaInstanca.getElementsByClassName("selekt-gost")[0];
      gostEkipa.value = this.idGosta;

      let datum = glavnaInstanca.getElementsByClassName("datumivreme")[0];
      datum.value = this.ceoDatum;

      let goloviDomacin = glavnaInstanca.getElementsByClassName("broj-golova-domacin")[0];
      goloviDomacin.value = this.goloviDomacin;

      let goloviGost =glavnaInstanca.getElementsByClassName("broj-golova-gost")[0];
      goloviGost.value = this.goloviGost;
    });
    let obrisiDugme = this.kreirajDugme(
      "obrisi-dugme-jedne-utakmice",
      "Obrisi"
    );
    obrisiDugme.addEventListener("click", () => {
      fetch(
        `https://localhost:5001/Utakmica/IzbrisatiUtakmicu?UtakmicaID=${this.id}`,
        {
          method: "DELETE",
        }
      ).then((response) => {
        console.log(response);
        if (response.ok) {
          kontejner.remove();
        } else {
          alert("Greška prilikom brisanja!");
        }
      });
    });
    let detaljiUtakmice = this.kreirajDugme(
      // glavnaInstanca = host.parentElement;
      // glavnaInstanca
      //   .getElementsByClassName("submit-button")[0]
      //   .setAttribute("data-id", this.id);
      "opsirnije-dugme",
      "Opsirnije"
    );
    detaljiUtakmice.addEventListener("click", () => {
      // let glavnaInstanca = host.parentElement;
      // glavnaInstanca.getElementsByClassName("glavni-kontejner")[0]
      //   .setAttribute("data-id", this.id);
      this.detaljiUtakmice();
    });
    footer.appendChild(izmeniDugme);
    footer.appendChild(obrisiDugme);
    footer.appendChild(detaljiUtakmice);
    kontejner.appendChild(footer);

    host.appendChild(kontejner);
  }

  kreirajLabelu(host, classes, text) {
    let label = document.createElement("h3");
    label.classList = classes;
    label.innerText = text;

    host.appendChild(label);
  }

  kreirajDugme(klasa, text) {
    let button = document.createElement("button");
    button.className = klasa;
    button.innerText = text;
    return button;
  }

  // =====================================================================

  detaljiUtakmice() {
    console.log("ovdeeee",this.id)
    let idUtakmice=this.id;
    let n = document.body.getElementsByClassName("glavni-kontejner").length;
    for (let i = 0; i < n; i++) {
      let brisanjeSadrzaja = document.body.getElementsByClassName("glavni-kontejner")[0];
      brisanjeSadrzaja.remove();
      console.log(brisanjeSadrzaja);
    }
    this.crtajUtakmicu(document.body);
    console.log("ovdee",idUtakmice)
  }


  crtajUtakmicu(host) {
    this.kontejnerDetaljiUtakmice = document.createElement("div");
    this.kontejnerDetaljiUtakmice.className =
      "glavni-kontejner"
    host.appendChild(this.kontejnerDetaljiUtakmice);

    let kontejnerForma = document.createElement("div");
    kontejnerForma.className = "kontejner-forme";
    this.kontejnerDetaljiUtakmice.appendChild(kontejnerForma);

    this.kontejnerPrikaz = document.createElement("div");
    this.kontejnerPrikaz.className =
      "kontejner-prikaz-utakmice";
    this.kontejnerDetaljiUtakmice.appendChild(this.kontejnerPrikaz);

    this.kreirajFormu(kontejnerForma);
    this.crtajPrikaz(this.kontejnerPrikaz);
  }


  crtajPrikaz(host,strelacID)
  {
    console.log("pomocnaaaa:  "+strelacID)
    fetch(`https://localhost:5001/Utakmica/DetaljiUtakmice/${this.id}`).then((response) => {
      response.json().then((detalji) => {
        console.log(detalji)
        
        let red = this.crtajRed(host,"redKola");
        this.kreirajLabelu(red,"labela","Broj kola: "+detalji.kolo);
        this.kreirajLabelu(red,"labela",detalji.datum+"-"+detalji.vreme);
        this.kreirajLabelu(red,"labela","Stadion: "+detalji.stadion);

        red = this.crtajRed(host,"redUtakmice");
        if(detalji.pobednik===1)
        {
          this.kreirajLabelu(red,"labelap",detalji.imeDomacina);
          this.kreirajLabelu(red,"labela",detalji.goloviDomacin+" : "+detalji.goloviGost);
          this.kreirajLabelu(red,"labelag",detalji.imeDomacina);
        }
        else if(detalji.pobednik===2)
        {
          this.kreirajLabelu(red,"labelap",detalji.imeDomacina);
          this.kreirajLabelu(red,"labela",detalji.goloviDomacin+" : "+detalji.goloviGost);
          this.kreirajLabelu(red,"labelag",detalji.imeGosta);
        }
        else
        {
          this.kreirajLabelu(red,"labela",detalji.imeDomacina);
          this.kreirajLabelu(red,"labela",detalji.goloviDomacin+" : "+detalji.goloviGost);
          this.kreirajLabelu(red,"labela",detalji.imeGosta);
        }
        

        let brojstrelaca=detalji.strelci.length
        let i=0;

        for(i=0;i<brojstrelaca;i++){
    
          if(detalji.strelci[i].ekipa==detalji.idDomacina) 
          {
            red = this.crtajRed(host,"redStrelacD");
          }
          else
          {
            red = this.crtajRed(host,"redStrelacG");
          }
            this.kreirajLabelu(red,"labela",detalji.strelci[i].igrac);
            this.kreirajLabelu(red,"labela",detalji.strelci[i].gol+"'");
            let footer = document.createElement("div");
            footer.classList = "redDugmici";
            let izmeniStrelcaDugme = this.kreirajDugme(
              "obrisi-strelca",
              "Izmeni"
            );
            let obrisiStrelcaDugme = this.kreirajDugme(
              "obrisi-strelca",
              "Obrisi"
            );

            obrisiStrelcaDugme.setAttribute("data-id", detalji.strelci[i].idstrelac);
            obrisiStrelcaDugme.onclick = (event) => this.obrisiStrelca(event.target.getAttribute("data-id"));
          
            izmeniStrelcaDugme.setAttribute("data-id",detalji.strelci[i].idstrelac,);
            izmeniStrelcaDugme.setAttribute("data-igrac",detalji.strelci[i].igrac);
            izmeniStrelcaDugme.setAttribute("data-gol",detalji.strelci[i].gol);
            izmeniStrelcaDugme.onclick = (event) => this.izmeniStrelca(event.target.getAttribute("data-id"),event.target.getAttribute("data-igrac"),event.target.getAttribute("data-gol"));
            
            
            

            footer.appendChild(izmeniStrelcaDugme)
            footer.appendChild(obrisiStrelcaDugme)
            host.appendChild(footer)  
        }
      })
    })
 }
izmeniStrelca(id,igrac,minut)
{
  console.log(id,igrac,minut);
  let kontejnerForma = document.body.getElementsByClassName("kontejner-forme")[0];
  let minutgola =kontejnerForma.getElementsByClassName("minutgola")[0];
  minutgola.value = minut;
  
}
 obrisiStrelca(strelacid)
 {
  fetch(`https://localhost:5001/Igrac/IzbrisatiStrelca/${strelacid}`,
  {
    method: "DELETE",
  }
  ).then((response) => {
    response.json().then((resp) => {
      console.log(resp);
      if (resp.id) 
      {
        this.kontejnerPrikaz.innerHTML=""
        this.crtajPrikaz(this.kontejnerPrikaz);
       
      }
      });
    });
  }

  crtajRed(host,classes)
  {
      let red = document.createElement("div");
      red.className = classes;
      host.appendChild(red);
      return red;
  }

  kreirajFormu(host) {
    let vratiSeNaPocetak = kreirajDugme(
      "submit-button",
      "Pocetna stranica"
    );

    fetch(`https://localhost:5001/Ekipa/IgraciEkipe/${this.idDomacina}/${this.idGosta}`).then((response) => {
      response.json().then((igraci) => {
        console.log(igraci);
        let igrac = new Igrac(igraci);
        igrac.crtaj(host, "selekt-igrac", "Igraci ekipa:");

        kreirajInput(
          host,
          "minutgola input",
          "Minut postignutog gola:",
          "number"
        );
        let dodajStrelca = kreirajDugme(
          "submit-button",
          "Dodaj strelca"
        );
        host.appendChild(dodajStrelca)
        dodajStrelca.addEventListener("click", () => {
          this.dodajStrelca();
        });

     

      })
    })
    host.appendChild(vratiSeNaPocetak);
        vratiSeNaPocetak.addEventListener("click", () => {
          this.vratiSeNaPocetnuStranicu();
        });
  }

  dodajStrelca()
  {
    let igrac = this.kontejnerDetaljiUtakmice.getElementsByClassName("selekt-igrac")[0].value;
    let minutgola = this.kontejnerDetaljiUtakmice.getElementsByClassName("minutgola")[0].value;

    console.log(minutgola);
    fetch(`https://localhost:5001/Igrac/DodajStrelca/${igrac}/${this.id}/${minutgola}`,
    {
      method: "POST",
    }
    ).then((response) => {
    response.json().then((resp) => {
      console.log(resp);
      if (resp.id) {
        this.kontejnerPrikaz.innerHTML=""
        this.crtajPrikaz(this.kontejnerPrikaz,resp.id);
        console.log("pera:"+resp.id);
      }
    });
  });
  }


  vratiSeNaPocetnuStranicu()
  {
    let brisanjeSadrzaja = document.body.getElementsByClassName("glavni-kontejner")[0];
    brisanjeSadrzaja.remove();

    fetch("https://localhost:5001/Kolo/PreuzetiKola")
    .then(response=>{
      response.json().then(svaKola=>{
          console.log(svaKola)

          svaKola.forEach(kolo => {
              var instancaKlase =new Kolo(kolo.id,kolo.rbr);
              instancaKlase.crtaj(document.body)
          });
      })
    });
  }
}
