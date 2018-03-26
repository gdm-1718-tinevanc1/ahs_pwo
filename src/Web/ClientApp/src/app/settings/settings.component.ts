import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { PageTitleService } from '../core/shared/services/page-title.service';
import { TranslateService } from '@ngx-translate/core';
import { ProfileService } from '../core/shared/services/profile.service'
import { Iprofile } from '../core/shared/models/Iprofile';
import { AuthenticationService } from '../core/shared/services/authentication.service'
import { SettingsService } from './shared/services/settings.service'


@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent implements OnInit {
  imageSrc;
  message = {
    succes: '',
    error: ''
  }
  profileId = this.authenticationService.profileId;
  profile: Iprofile;
  credentials = {
    id: this.profileId,
    setting: {
      language: 'nl',
      color: 'blue'
    }
  }
  constructor(
    private pageTitleService: PageTitleService,
    private profileService:ProfileService, 
    private settingsService:SettingsService, 
    private authenticationService:AuthenticationService,
    public http:Http,
    private translate: TranslateService) {}

  ngOnInit() {
    this.translate.setDefaultLang(this.credentials.setting.language);

    this.translate.get('Instellingen').subscribe((res: string) => {
      this.pageTitleService.setTitle(res)
    });
    this.pageTitleService.setSubTitle("")
    this.profileService.getProfileById(this.profileId).subscribe(
      result => { 
        this.profile = result,
        this.credentials.id = this.profile.id;
        this.credentials.setting = this.profile.setting;
        this.translate.setDefaultLang(this.credentials.setting.language);

        console.log(this.credentials)
      },
      err => console.log('err')
    )


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

  changeColor(){
    let body = document.querySelector('body');
    if(this.credentials.setting.color  == "blue"){
      body.style.setProperty('--primary-color', '#00AACC')
      body.style.setProperty('--secondary-color', '#00CAE5')
    } else{
      body.style.setProperty('--primary-color', '#CC820A')
      body.style.setProperty('--secondary-color', '#ECAA3F')
    }
  }

  public save() {
    if(this.imageSrc){
      let credentials = {
        "id": this.profileId,
        "image": this.imageSrc
      } 
  
      console.log(credentials)
      // this.extImageSrc.forEach(function (a) { this.credentials.mediums.push(a) });
      this.profileService.updateProfile(credentials).subscribe(
        res => {
          this.message.error =  '';
          this.message.succes =  "Uw wijzigingen zijn opgeslaan"
          setTimeout(()=>{ this.message.succes =  "" }, 3000);
        },
        err => {
          console.log("Error occured");
          this.message.succes = '';
          this.message.error = "Uw wijzigingen zijn niet opgeslaan, gelieve opnieuw te proberen"
          setTimeout(()=>{ this.message.error =  "" }, 3000);
        }
      );
    }

    console.log(this.credentials)
    this.settingsService.saveSettings(this.credentials).subscribe(
      res => {
        this.message.error =  '';
        this.message.succes =  "Uw wijzigingen zijn opgeslaan"
        setTimeout(()=>{ this.message.succes =  "" }, 3000);
      },
      err => {
        console.log("Error occured");
        this.message.succes = '';
        this.message.error = "Uw wijzigingen zijn niet opgeslaan, gelieve opnieuw te proberen"
        setTimeout(()=>{ this.message.error =  "" }, 3000);
      }
    );
  } 
}

