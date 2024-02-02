import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ShortcutService {
  private records: Record<string, string>[] = [
    { original: 'spisová značka', shortcut: 'Sp. zn.' },
    { original: 'krajský soud', shortcut: 'Krajský<br>soud' },
    { original: 'jméno / název dlužníka', shortcut: 'Jméno /<br> Název dlužníka' },
    { original: 'datum narození / ičo', shortcut: 'Datum narození /<br>IČO' },
    { original: 'okamžik zveřejnění', shortcut: 'okamžik <br> zveřejnění'},
    { original: 'vedlejší dokument', shortcut: 'vedlejší <br> dokument'},
    { original: 'datum právní moci', shortcut: 'datum <br> právní moci'},
    { original: 'spisová značka incidenčního sporu', shortcut: 'sp. zn. <br> incid. sporu'},
    { original: 'senátní značka vs/ns', shortcut: 'senátní zn. <br> vs/ns'},
    { original: 'platní věřitelé', shortcut: 'platní <br> věřitelé'},
    { original: 'číslo spisu', shortcut: 'č. spisu' },
    { original: 'jméno fyzické osoby', shortcut: 'jméno <br> fyzické osoby' },
    { original: 'příjmení fyzické osoby', shortcut: 'příjmení <br> fyzické osoby' },
    { original: 'název společnosti', shortcut: 'název <br> společnosti' },
    { original: 'identifikační číslo (ičo)', shortcut: 'id. číslo (ičo)' },
    { original: 'jméno a příjmení / název dlužníka', shortcut: 'jméno a příjmení / <br>název dlužníka' },
    { original: 'datum zproštění/odvolání', shortcut: 'datum zproštění / <br>odvolání' },
    { original: 'ohlášený společník', shortcut: 'ohlášený <br>společník' },
    { original: 'určená provozovna', shortcut: 'určená <br>provozovna' },
    { original: 'datum ustanovení', shortcut: 'datum <br>ustanovení' },
    { original: 'datum narození', shortcut: 'Datum <br>narození' },
    { original: 'úřední hodiny vč. přehodných uzavření', shortcut: 'Úřední hodiny vč. <br>přehodných uzavření' },
    { original: 'odborné zaměření', shortcut: 'odborné <br>zaměření' },
    { original: 'datum určení', shortcut: 'datum <br>určení' },
    { original: 'jméno a příjmení', shortcut: 'jméno a <br>příjmení'},
    { original: 'datum vydání prověrky', shortcut: 'datum <br>vydání prověrky'},
    { original: 'platnost prověrky do', shortcut: 'platnost <br>prověrky do'},
    { original: 'platnost prověrky od', shortcut: 'platnost <br>prověrky od'},
  ];

  getShortcuts(): Record<string, string>[] {
    return this.records;
  }
}