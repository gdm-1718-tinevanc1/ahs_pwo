import { Component, OnInit, Directive, AfterViewInit, Input, ViewChild, ElementRef, ViewChildren, QueryList  } from '@angular/core';
import {MatChipInputEvent, MatTabLinkBase} from '@angular/material';
import {ENTER, COMMA} from '@angular/cdk/keycodes';
import { ProjectService } from '../../../core/shared/services/project.service'
import { TypeService } from '../../shared/services/type.service'
import { FinancingformService } from '../../shared/services/financingform.service'
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router'; 
import { Iproject } from '../../../core/shared/models/IProject';
import { AuthenticationService } from '../../../core/shared/services/authentication.service'
import { SharedService } from '../../../core/shared/services/shared.service'
import { ValidateComponent } from '../validate/validate.component';

@Component({
  selector: 'app-form-metadata',
  templateUrl: './form-metadata.component.html',
  styleUrls: ['./form-metadata.component.scss']
})
export class FormMetadataComponent implements OnInit {
  isAdmin = this.authenticationService.isAdmin;
  @ViewChildren('cmp') components: QueryList<ValidateComponent>;
  valueObject = JSON.stringify(this.sharedService.sharedNode.validateObject);
  language = this.sharedService.sharedNode.language;

  
  //tags options
  visible: boolean = true;
  selectable: boolean = true;
  removable: boolean = true;
  addOnBlur: boolean = true;
  separatorKeysCodes = [ENTER, COMMA];

  message = {
    error: '',
    succes: '',
    warning: ''
  };
  succes = "";

  id: number;

  /* validate */ 
  isExpandedValidate = {
    /* participant: false,
    tags: false,
    financingforms: false */
  };
  /* amount fields */
  AmountParticipant:number = 1;
  AmountFinancingsforms:number = 1;
  AmountLinks:number = 1;
  Arr = Array;
  
  /* tags */
  /* tags = [];
  partners = []; */

  projectCredentials = {
    tags: [],
    partners: [],
    participants: [],
    financingforms: [],
    links: [{}],
    partnerValidate: this.valueObject,
    participantValidate: this.valueObject,
    financingformValidate: this.valueObject,
    linkValidate: this.valueObject,
    profileId: this.authenticationService.profileId
  };
// type MyArrayType = Array<{id: number, text: string}>;


  links = [];
  financingforms = [];
  participants;

  financingforms_data = [];
  participants_type = [];
  participants_data = [];
  participants_filter_data = [];

  editable_by  = {
    partners: [],
    participants: [],
    financingforms: [],
    links: []
  }

  constructor(
    private projectService:ProjectService,
    private typeService:TypeService,
    private financingformService:FinancingformService,
    private authenticationService:AuthenticationService,
    private sharedService:SharedService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
    // this.projectCredentials = new Iproject();
    this.id = this.route.parent.snapshot.params.id;
    if(this.id){
      this.projectService.getProjectMetadataById(this.id).subscribe(
        project => { 
          this.projectCredentials = project
          console.log(this.projectCredentials)
        
          if(this.projectCredentials.participantValidate){
            this.projectCredentials.partnerValidate = JSON.parse(this.projectCredentials.partnerValidate.toString());
            this.projectCredentials.participantValidate = JSON.parse(this.projectCredentials.participantValidate.toString());
            this.projectCredentials.financingformValidate = JSON.parse(this.projectCredentials.financingformValidate.toString());
            this.projectCredentials.linkValidate = JSON.parse(this.projectCredentials.linkValidate.toString());
          } else{
            this.projectCredentials.partnerValidate = JSON.parse(this.valueObject);
            this.projectCredentials.participantValidate = JSON.parse(this.valueObject);
            this.projectCredentials.financingformValidate = JSON.parse(this.valueObject);
            this.projectCredentials.linkValidate = JSON.parse(this.valueObject);
          }

          if(project.participants.length) this.AmountParticipant = project.participants.length;
          if(project.financingforms.length) this.AmountFinancingsforms = project.financingforms.length;
          if(project.links.length) this.AmountLinks = project.links.length;
          this.financingforms = [];
          this.participants = [];
          for(let i = 0; i < project.links.length; i++) { this.links.push(project.links[i].name) }
          for(let i = 0; i < project.participants.length; i++) { this.participants.push(project.participants[i].participantId) }
          for(let i = 0; i < project.financingforms.length; i++) { this.financingforms.push(project.financingforms[i].financingformId) }
          if(project.participants.length == 0) { this.getParticipants(); }
          // this.participants = [2];
          // this.participants_type = [2];

          for(let i = 0; i < this.participants.length; i++) { this.changetype(this.participants[i], i, 'old')
          this.participants_type.push(project.participants[i].participant.typeParticipant)
          // console.log('test:', project.participants[i].participant.typeParticipant)
          // console.log('test:', this.participants_type)
          this.succes = "succes";

          this.editable_by.partners = this.projectCredentials.partners.validate.editable_by.push(this.projectCredentials.profileId)
          this.editable_by.participants = this.projectCredentials.participants.validate.editable_by.push(this.projectCredentials.profileId)
          this.editable_by.financingforms = this.projectCredentials.financingforms.validate.editable_by.push(this.projectCredentials.profileId)
          this.editable_by.links = this.projectCredentials.links.validate.editable_by.push(this.projectCredentials.profileId)
          }
        }
      )
    } else{
      this.projectCredentials.partnerValidate = JSON.parse(this.projectCredentials.partnerValidate.toString());
      this.projectCredentials.participantValidate = JSON.parse(this.projectCredentials.participantValidate.toString());
      this.projectCredentials.financingformValidate = JSON.parse(this.projectCredentials.financingformValidate.toString());
      this.projectCredentials.linkValidate = JSON.parse(this.projectCredentials.linkValidate.toString());
    }

     this.getFinancingforms();
     this.getParticipants();
  }

  getFinancingforms(){
    this.typeService.getFinancingforms().subscribe(
      financingforms => { 
        this.financingforms_data = financingforms
      })
  }

  getParticipants(){
    this.typeService.getParticipants().subscribe(
      participants => { 
        this.participants_data = participants
        // this.participants = participants
      })
  }

  addTag(event: MatChipInputEvent, type): void {
    let input = event.input;
    let value = event.value;

    if ((value || '').trim()) {
      if(type == 'tag'){
        this.projectCredentials.tags.push({ name: value.trim() });
      } else if( type == 'partner'){
        this.projectCredentials.partners.push({ name: value.trim() });
      }
    }

    if (input) {
      input.value = '';
    }
  }

  removeTag(value: any, type): void {
    if(type == 'tag'){
      let index = this.projectCredentials.tags.indexOf(value);
      if (index >= 0) {
        this.projectCredentials.tags.splice(index, 1);
      }
    } 
    else if( type == 'partner'){
      let index = this.projectCredentials.partners.indexOf(value);
      if (index >= 0) {
        this.projectCredentials.partners.splice(index, 1);
      }
    }
  }

  plusField(field){
    switch (field) {
      case 'AmountParticipant':
        this.AmountParticipant = this.AmountParticipant + 1;
        break;
      case 'AmountFinancingsforms':
        this.AmountFinancingsforms = this.AmountFinancingsforms +1 ;
        break;
      case 'AmountLinks':
        this.AmountLinks = this.AmountLinks +1 ;
        break;
    }

  }
  minusField(field) {
    switch (field) {
      case 'AmountParticipant':
        if(this.AmountParticipant > 1){
          this.AmountParticipant = this.AmountParticipant - 1;
        }
        break;
      case 'AmountFinancingsforms':
        if(this.AmountFinancingsforms > 1){
          this.AmountFinancingsforms = this.AmountFinancingsforms - 1 ;
        }
        break;
      case 'AmountLinks':
        if(this.AmountLinks > 1){
          this.AmountLinks = this.AmountLinks - 1 ;
        }
        break;
    }
  }

  changetype(event, i, type){
    let participantType;

    if(type == "old"){
      participantType = event - 1;
    } else{
      participantType = event;
    }
    this.typeService.getParticipantsByType(participantType).subscribe(
      participants => { 
        this.participants_filter_data[i] = participants
    })
  }

  save() {
    this.projectCredentials.partnerValidate = JSON.stringify(this.projectCredentials.partnerValidate);
    this.projectCredentials.participantValidate = JSON.stringify(this.projectCredentials.participantValidate);
    this.projectCredentials.financingformValidate = JSON.stringify(this.projectCredentials.financingformValidate);
    this.projectCredentials.linkValidate = JSON.stringify(this.projectCredentials.linkValidate);

    this.projectCredentials.links = []
    console.log(this.links)
    for(let link of this.links){
      this.projectCredentials.links.push({ name: link });
    }
    this.projectCredentials.financingforms = []
    for(let financingform of this.financingforms){
      this.projectCredentials.financingforms.push({ financingformId: financingform });
    }
    this.projectCredentials.participants = []
    for(let participant of this.participants){
      this.projectCredentials.participants.push({ participantId: participant });
    }
    console.log(this.projectCredentials)
    this.projectService.saveProjects(this.projectCredentials, 'metadata').subscribe(
      res => {
        this.projectCredentials.partnerValidate = JSON.parse(this.projectCredentials.partnerValidate.toString());
        this.projectCredentials.participantValidate = JSON.parse(this.projectCredentials.participantValidate.toString());
        this.projectCredentials.financingformValidate = JSON.parse(this.projectCredentials.financingformValidate.toString());
        this.projectCredentials.linkValidate = JSON.parse(this.projectCredentials.linkValidate.toString());

        this.message.error =  '';
        this.message.succes =  "Uw wijzigingen zijn opgeslaan";
        setTimeout(()=>{ this.message.succes =  "" }, 3000);
        if(res){
          this.router.navigate(['/project', res.id, 'metadata']);
        }
      },
      err => {
        console.log("Error occured");
        this.message.succes =  '';
        this.message.error = "Uw wijzigingen zijn niet opgeslaan, gelieve opnieuw te proberen";
        setTimeout(()=>{ this.message.error =  "" }, 3000);

      }
    );
  }

  validate(){
    var validates = [];
    this.components.forEach(validate => validates.push(validate.validate));
    if(this.succes === 'succes'){
      if(typeof this.projectCredentials.participantValidate === 'object'){
        if(this.language === 'nl'){
          this.projectCredentials.partnerValidate.validate = validates[0]
          this.projectCredentials.participantValidate.validate = validates[1]
          this.projectCredentials.financingformValidate.validate = validates[2]
          this.projectCredentials.linkValidate.validate = validates[3]   
        } else{
          this.projectCredentials.partnerValidate.validate = validates[0]
          this.projectCredentials.participantValidate.validate = validates[1]
          this.projectCredentials.financingformValidate.validate = validates[2]
          this.projectCredentials.linkValidate.validate = validates[3]
        }
      // console.log(this.projectCredentials)
      }
    }

   this.save();

  }

  addFinancingform(event){
    let credentials = { name: event.target.value};
    this.financingformService.postFinancingform(credentials).subscribe(
      res => {
        console.log(res);
        this.getFinancingforms()
      },
      err => {
        console.log("Error occured");
        this.message.error = "Uw financieringsvorm is niet opgeslaan, gelieve opnieuw te proberen"
      }
    );
    alert('test')
    // this.financingforms_data.push(event)
  }

  addLink(event, i){
    this.links.splice(i, 1) 
    if(event.target.value){
      this.links.push(event.target.value)
    }
    if(this.links.length) this.AmountLinks = this.links.length;

  }
}
