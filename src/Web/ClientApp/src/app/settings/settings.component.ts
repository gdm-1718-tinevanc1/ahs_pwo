import { Component, OnInit } from '@angular/core';
import { PageTitleService } from '../core/shared/services/page-title.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent implements OnInit {
  imageSrc;
  constructor(
    private pageTitleService: PageTitleService,
    private translate: TranslateService) {
      translate.setDefaultLang('nl');
    }

  ngOnInit() {
    this.translate.get('Instellingen').subscribe((res: string) => {
      this.pageTitleService.setTitle(res)
    });
    this.pageTitleService.setSubTitle("")
  }

  switchLanguage(language: string) {
    this.translate.use(language);
  }

  previewImage($event) {
    var files = $event.target.files;
    for(var i = 0; i < files.length; i++){
      let self = this;
      (function(file) {
        var reader = new FileReader();
        reader.onload  = (e) => {
          self.imageSrc = reader.result;
        };
        reader.readAsDataURL(file);
      })(files[i]);
      }
  }
  
  cancel() {
    this.imageSrc = '';
  }
}

