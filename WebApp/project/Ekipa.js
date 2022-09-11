import { kreirajSelect } from "./helper.js";

export class Ekipa {
  constructor(ekipe) {
    this.ekipe = ekipe;
  }

  crtaj(host, classes, text) {
    kreirajSelect(host, this.ekipe, text, classes);
  }
}
