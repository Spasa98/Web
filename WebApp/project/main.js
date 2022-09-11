import { Kolo } from './Kolo.js'

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