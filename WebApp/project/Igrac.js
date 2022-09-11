import { kreirajSelect } from "./helper.js";

export class Igrac {
  constructor(igraci) {
    this.igraci = igraci;
  }

  crtaj(host, classes, text) {
    kreirajSelect(host, this.igraci, text, classes);
  }
}
