import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { ProfileService } from './core/shared/services/profile.service'
import { TranslateService } from '@ngx-translate/core';
import { AuthenticationService } from './core/shared/services/authentication.service'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'PWO';
  isAdmin = false;
  profileId = this.authenticationService.profileId;

  constructor(
    private profileService:ProfileService, 
    private authenticationService:AuthenticationService,
    public http:Http,
    private translate: TranslateService
  ){}

  ngOnInit(){
    this.profileService.getProfileById(this.profileId).subscribe(
      result => { 
        // this.profile = result,
        let body = document.querySelector('body');
        if(result.setting.color  == "blue"){
          body.style.setProperty('--primary-color', '#00AACC')
          body.style.setProperty('--secondary-color', '#00CAE5')
        } else{
          body.style.setProperty('--primary-color', '#CC820A')
          body.style.setProperty('--secondary-color', '#ECAA3F')
        }

        this.translate.setDefaultLang(result.setting.language);
      },
      err => console.log('err')
    )
  }
}

