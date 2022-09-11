import { Ekipa } from "./Ekipa.js";
import { Utakmica } from "./Utakmica.js";
import { kreirajDugme, kreirajInput } from "./helper.js";

export class Kolo {
  constructor(id, rbr) {
    this.id = id;
    this.rbr = rbr;
    this.kontejner = null;
    this.kontejnerUtakmice = null;
    // this.full=null;
    this.ekipe = [];
  }

  crtaj(host) {
    // this.full = document.createElement("div");
    // this.full.className = "full-kontejner flex-wrap border m-4 p-5 flex";
    // host.appendChild(this.full);

    this.kontejner = document.createElement("div");
    this.kontejner.className = "glavni-kontejner";
    host.appendChild(this.kontejner);

    let kontejnerForma = document.createElement("div");
    kontejnerForma.className = "kontejner-forme";
    let rbrKola = document.createElement("h2");
    rbrKola.innerText = `Broj kola: ${this.rbr}`;
    rbrKola.classList = "naslovRBRKola";
    kontejnerForma.appendChild(rbrKola);
    this.kontejner.appendChild(kontejnerForma);

    this.kontejnerUtakmice = document.createElement("div");
    this.kontejnerUtakmice.className ="kontejner-utakmice";
    this.kontejner.appendChild(this.kontejnerUtakmice);

    this.kreirajFormu(kontejnerForma);
    this.prikaziUtakmice();
  }

  kreirajFormu(host) {
    fetch(`https://localhost:5001/Ekipa/PreuzetiEkipe`).then((response) => {
      response.json().then((sveEkipe) => {
        console.log(sveEkipe);
        let ekipaDomacin = new Ekipa(sveEkipe);
        ekipaDomacin.crtaj(host, "selekt-domacin", "Domaca ekipa");
        let ekipaGost = new Ekipa(sveEkipe);
        ekipaGost.crtaj(host, "selekt-gost", "Gostujuca ekipa");

        kreirajInput(
          host,
          "datumivreme",
          "Datum i vreme",
          "datetime-local"
        );
        kreirajInput(
          host,
          "broj-golova-domacin",
          "Broj golova domacin",
          "number"
        );
        kreirajInput(
          host,
          "broj-golova-gost",
          "Broj golova gost",
          "number"
        );
        let submitButton = kreirajDugme(
          "submit-button",
          "Posalji"
        );
        host.appendChild(submitButton);
        submitButton.addEventListener("click", (event) => {
          if (event.target.getAttribute("data-id")) {
            this.izmeniUtakmicu(event);
          } else {
            this.dodajUtakmicu();
            
          }
        });
      });
    });
  }

  izmeniUtakmicu(event) {
    let gostID = this.kontejner.getElementsByClassName("selekt-gost")[0];
    let domacinID = this.kontejner.getElementsByClassName("selekt-domacin")[0];
    let goloviDomacin = this.kontejner.getElementsByClassName("broj-golova-domacin")[0];
    let goloviGost =this.kontejner.getElementsByClassName("broj-golova-gost")[0];
    let datum = this.kontejner.getElementsByClassName("datumivreme")[0];

    if(goloviDomacin==="")
    {
      alert("Unesi golove domacina");
    }
    if(goloviGost==="")
    {
      alert("Unesi golove gosta");
    }
    
    fetch(
      `https://localhost:5001/Utakmica/AzurirajUtakmicu/
      ${goloviGost.value}/${goloviDomacin.value}/${datum.value}
      /${event.target.getAttribute("data-id")}/${gostID.value}/${domacinID.value}`,
      {
        method: "PUT",
      }
    ).then((response) => {
      response.json().then((resp) => {
        console.log(resp);
        event.target.removeAttribute("data-id");
        gostID.value = "";
        domacinID.value = "";
        goloviDomacin.value = "";
        goloviGost.value = "";
        datum.value = null;
        this.prikaziUtakmice();
      });
    });
  }

  dodajUtakmicu() {
    // this.kontejner.remove()
    let gostID = this.kontejner.getElementsByClassName("selekt-gost")[0].value;
    let domacinID =this.kontejner.getElementsByClassName("selekt-domacin")[0].value;
    let goloviDomacin = this.kontejner.getElementsByClassName("broj-golova-domacin")[0].value;
    let goloviGost =this.kontejner.getElementsByClassName("broj-golova-gost")[0].value;
    let datum = this.kontejner.getElementsByClassName("datumivreme")[0].value;
    if(gostID==="")
    {
      alert("Odaberi gostujucu ekipu");
    }
    if(domacinID==="")
    {
      alert("Odaberi domacu ekipu");
    }
    if(goloviDomacin==="")
    {
      alert("Unesi golove domacina");
    }
    if(goloviGost==="")
    {
      alert("Unesi golove gosta");
    }

    fetch(
      `https://localhost:5001/Utakmica/DodajUtakmicu/${gostID}/${domacinID}/${this.id}/${goloviDomacin}/${goloviGost}/${datum}`,
      {
        method: "POST",
      }
    ).then((response) => {
      response.json().then((resp) => {
        console.log(resp);
        if (resp.id) {
          this.prikaziUtakmice();
        }
      });
    });
  }

  prikaziUtakmice() {
    fetch(
      `https://localhost:5001/Kolo/UtakmiceJednogKola?koloID=${this.id}`
    ).then((response) => {
      response.json().then((sveUtakmice) => {
        this.kontejnerUtakmice.innerHTML = "";

        sveUtakmice.forEach((utakmica) => {
          console.log(utakmica);
          let jednaUtakmica = new Utakmica(
            utakmica.id,
            utakmica.pobednik,
            utakmica.datum,
            utakmica.vreme,
            utakmica.imeDomacina,
            utakmica.goloviDomacin,
            utakmica.imeGosta,
            utakmica.goloviGost,
            utakmica.idGosta,
            utakmica.idDomacina,
            utakmica.ceoDatum
          );
          jednaUtakmica.crtaj(this.kontejnerUtakmice);
        });
      });
    });
  }
}
