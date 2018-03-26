import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../shared/services/authentication.service'
import { ProfileService } from '../shared/services/profile.service'
import { Iprofile } from '../shared/models/Iprofile';
// import { translate as Translate } from 'google-translate-api';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnInit{
  isAdmin = this.authenticationService.isAdmin;
  profileId = this.authenticationService.profileId;
  profile: Iprofile;
  isExpanded = false;
  isExpandedFilter = false;
  isExpandedUser = false


  constructor(
    // private translate: Translate,
    private profileService:ProfileService,
    private authenticationService:AuthenticationService
  ) { }

  ngOnInit() {
    this.profileService.getProfileById(this.profileId).subscribe(
      profile => { 
        this.profile = profile
      })

      // var googleTranslate = require('google-translate');
     /*  const apiKey = "AIzaSyCxgeNWLHHXaMg4QkhDmq-1UXoEnXkmhbM";
      var googleTranslate = require('google-translate')(apiKey);
      
      googleTranslate.translate('My name is Brandon', 'es', function(err, translation) {
        alert('test')
        console.log(translation);
        console.log(err)
        // =>  Mi nombre es Brandon
      });  */
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
