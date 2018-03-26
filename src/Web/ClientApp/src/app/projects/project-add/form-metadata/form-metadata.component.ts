import { Component, OnInit, Directive, AfterViewInit, Input, ViewChild, ElementRef, ViewChildren, QueryList  } from '@angular/core';
import {MatChipInputEvent} from '@angular/material';
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
  AuthenticatedUser = this.authenticationService.profileId;
  @ViewChildren('cmp') components: QueryList<ValidateComponent>;
  valueObject = JSON.stringify(this.sharedService.sharedNode.validateObject);
  language: string;
  
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
    participant: false,
    tags: false,
    financingforms: false,
    projectpartner: false,
    links: false
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

  parseJsonPartnerValidate;
  parseJsonParticipantValidate;
  parseJsonFinancingformValidate;
  parseJsonLinkValidate;

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
    this.sharedService.language.subscribe((val: string) => {
      this.language = val;
    });
// projectCredentials.financingformValidate.validate
    // this.projectCredentials = new Iproject();
    this.id = this.route.parent.snapshot.params.id;
    if(this.id){
      this.projectService.getProjectMetadataById(this.id).subscribe(
        project => { 
          this.projectCredentials = project

          this.parseJsonPartnerValidate = JSON.parse(this.projectCredentials.partnerValidate.toString());
          this.parseJsonParticipantValidate = JSON.parse(this.projectCredentials.participantValidate.toString());
          this.parseJsonFinancingformValidate= JSON.parse(this.projectCredentials.financingformValidate.toString());
          this.parseJsonLinkValidate = JSON.parse(this.projectCredentials.linkValidate.toString());
        
          this.projectCredentials.partnerValidate = JSON.parse(this.projectCredentials.partnerValidate.toString());
          this.projectCredentials.participantValidate = JSON.parse(this.projectCredentials.participantValidate.toString());
          this.projectCredentials.financingformValidate= JSON.parse(this.projectCredentials.financingformValidate.toString());
          this.projectCredentials.linkValidate = JSON.parse(this.projectCredentials.linkValidate.toString());


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

          this.editable_by.partners = this.parseJsonPartnerValidate.validate.editable_by; this.editable_by.partners.push(project.profileId)
          this.editable_by.participants = this.parseJsonParticipantValidate.validate.editable_by; this.editable_by.participants.push(project.profileId)
          this.editable_by.financingforms = this.parseJsonFinancingformValidate.validate.editable_by; this.editable_by.financingforms.push(project.profileId)
          this.editable_by.links = this.parseJsonLinkValidate.validate.editable_by; this.editable_by.links.push(project.profileId)


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
    if(this.isAdmin){
      this.save();
    }
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
