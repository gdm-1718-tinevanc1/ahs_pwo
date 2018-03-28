import { Component, OnInit } from '@angular/core';
import { concat } from 'rxjs/observable/concat';
import { Router } from '@angular/router'; 
import { ProjectService } from '../../../core/shared/services/project.service'
import { AuthenticationService } from '../../../core/shared/services/authentication.service'

import { ActivatedRoute } from '@angular/router';
import { Iproject } from '../../../core/shared/models/IProject';

@Component({
  selector: 'app-form-media',
  templateUrl: './form-media.component.html',
  styleUrls: ['./form-media.component.scss']
})
export class FormMediaComponent implements OnInit {
  isAdmin = this.authenticationService.isAdmin;
  AuthenticatedUser = this.authenticationService.profileId;

  id: null;
  message = {
    error: '',
    succes: '',
    warning: ''
  };
  credentials: Iproject;
  images = [];
  imageSrc = {image: '', typeMedia: null};
  iconSrc = {image: '', typeMedia: null};
  extImageSrc = [];

  editable_by = []

  
  constructor(
    private projectService:ProjectService,
    private authenticationService:AuthenticationService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.id = this.route.parent.snapshot.params.id;
    if(this.id){
      this.projectService.getProjectMediaById(this.id).subscribe(
        project => { 
          this.credentials = project
          this.images = project.mediums
          this.editable_by.push(project.profileId)
          this.sortImages()
        }
      )
    }
  }
  
  sortImages(){
    var groupByName = {};
   
    this.images.forEach(function (a) {
      groupByName [a.typeMedia] = groupByName [a.typeMedia] || [];
      groupByName [a.typeMedia].push({ image: a.image, typeMedia: a.typeMedia });
    });
    
    if(groupByName[1]) this.imageSrc = groupByName[1][0]
    if(groupByName[2]) this.iconSrc = groupByName[2][0]
    if(groupByName[3]) this.extImageSrc = groupByName[3]

  }
  previewImage($event, type) {
    var files = $event.target.files;
      for(var i = 0; i < files.length; i++){
        let self = this;
        (function(file) {
          var reader = new FileReader();
          reader.onload  = (e) => {
            switch (type) {
              case 'iconSrc':
                  self.iconSrc = {image: reader.result, typeMedia: 2};
                  break;
              case 'imageSrc':
                  self.imageSrc = {image: reader.result, typeMedia: 1};
                  break;
              case 'extImageSrc':
                  self.extImageSrc.push({image: reader.result, typeMedia: 3});
                  break;
            }
          };
          reader.readAsDataURL(file);
        })(files[i]);
        }
  }

  toDataUrl(url, callback) {
    var xhr = new XMLHttpRequest();
    xhr.onload = function() {
        var reader = new FileReader();
        reader.onloadend = function() {
            callback(reader.result);
        }
        reader.readAsDataURL(xhr.response);
    };
    xhr.open('GET', url);
    xhr.responseType = 'blob';
    xhr.send();
 } 
  cancel(type) {
    switch (type) {
      case 'iconSrc':
        this.iconSrc = {image: '', typeMedia: null};
        break;
      case 'imageSrc':
        this.imageSrc = {image: '', typeMedia: null};
        break;
      case 'extImageSrc':
        this.extImageSrc = [];
        break;
    }
  }

  public save() {
    this.credentials.mediums = []; 
    if(this.imageSrc.image) this.credentials.mediums.push(this.imageSrc)
    if(this.iconSrc.image) this.credentials.mediums.push(this.iconSrc)
    for(var i = 0; i< this.extImageSrc.length; i++) this.credentials.mediums.push(this.extImageSrc[i])

    this.projectService.saveProjects(this.credentials, 'media').subscribe(
      res => {
        this.message.error =  '';
        this.message.succes =  "Uw wijzigingen zijn opgeslaan"
        setTimeout(()=>{ this.message.succes =  "" }, 3000);
        if(res){
          this.router.navigate(['/project', res.id, 'budget']);
        }
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
