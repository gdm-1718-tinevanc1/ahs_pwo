import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-translate',
  template: '',

})

export class TranslateComponent implements OnInit {

  constructor(private translate: TranslateService) {
    translate.setDefaultLang('nl');
  }

  switchLanguage(language: string) {
    this.translate.use(language);
  }

  ngOnInit() {
  }

}
