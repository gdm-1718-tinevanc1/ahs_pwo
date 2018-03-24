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
      /* const apiKey = "AIzaSyCxgeNWLHHXaMg4QkhDmq-1UXoEnXkmhbM";
      var googleTranslate = require('google-translate')(apiKey);
      
      googleTranslate.translate('My name is Brandon', 'es', function(err, translation) {
        alert('test')
        console.log(translation);
        console.log(err)
        // =>  Mi nombre es Brandon
      }); */
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}


/* this.projectService.saveProjects(this.projectCredentials, 'metadata').subscribe(
      res => {
        this.projectCredentials.partnerValidate = JSON.parse(this.projectCredentials.partnerValidate.toString());
        this.projectCredentials.participantValidate = JSON.parse(this.projectCredentials.participantValidate.toString());
        this.projectCredentials.financingformValidate = JSON.parse(this.projectCredentials.financingformValidate.toString());
        this.projectCredentials.linkValidate = JSON.parse(this.projectCredentials.linkValidate.toString());

        this.message.succes =  "Uw wijzigingen zijn opgeslaan"
        if(res){
          this.router.navigate(['/project', res.id, 'metadata']);
        }
      },
      err => {
        console.log("Error occured");
        this.message.error = "Uw wijzigingen zijn niet opgeslaan, gelieve opnieuw te proberen"
      }
    ); */