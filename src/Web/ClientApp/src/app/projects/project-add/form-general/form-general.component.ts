import { Component, OnInit, Directive, AfterViewInit, Input, ViewChild, ElementRef, ViewChildren, QueryList  } from '@angular/core';
import { ProjectService } from '../../../core/shared/services/project.service'
import { StatusService } from '../../../core/shared/services/status.service'
import { SharedService } from '../../../core/shared/services/shared.service'
import { Router } from '@angular/router'; 
import { ActivatedRoute } from '@angular/router';
import { AceEditorModule } from 'ng2-ace-editor';
import { Iproject } from '../../../core/shared/models/iproject';
import { ValidateComponent } from '../validate/validate.component';
import { AuthenticationService } from '../../../core/shared/services/authentication.service'

@Component({
  selector: 'app-form-general',
  templateUrl: './form-general.component.html',
  styleUrls: ['./form-general.component.scss']
})
export class FormGeneralComponent implements OnInit {
  isAdmin = this.authenticationService.isAdmin;
  AuthenticatedUser = this.authenticationService.profileId;
  language: string;

  @ViewChildren('cmp') components: QueryList<ValidateComponent>;
  valueObject = JSON.stringify(this.sharedService.sharedNode.valueObject);
  id: number;
  message = {
    error: '',
    succes: '',
    warning: ''
  };

  projectCredentials = {
    title: this.valueObject,
    shorttitle: this.valueObject,
    subtitle: this.valueObject,
    description: this.valueObject,
    startdate: null,
    enddate: null,
    profiles: [{profileId: this.authenticationService.profileId}],
    statusId: 2
  };
  states: {};
  isExpandedValidate = {
    title: false,
    shorttitle: false,
    subtitle: false,
    description: false
  };
  editable_by  = {
    title: [],
    shorttitle: [],
    subtitle: [],
    description: [],
    date: []
  }

  parseJsonTitle;
  parseJsonSubtitle;
  parseJsonShorttitle;
  parseJsonDescription;

  constructor(
    private projectService:ProjectService,
    private statusService:StatusService,
    private sharedService:SharedService,
    private authenticationService:AuthenticationService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.sharedService.language.subscribe((val: string) => {
      this.language = val;
    });

    this.statusService.getStates().subscribe(
      states => {
        this.states = states;
    })

    this.id = this.route.parent.snapshot.params.id;
    if(this.id){
      this.projectService.getProjectGeneralById(this.id).subscribe(
        project => { 
          this.projectCredentials = project

          this.parseJsonTitle = JSON.parse(this.projectCredentials.title);
          this.parseJsonSubtitle = JSON.parse(this.projectCredentials.subtitle);
          this.parseJsonShorttitle = JSON.parse(this.projectCredentials.shorttitle);
          this.parseJsonDescription = JSON.parse(this.projectCredentials.description);

          this.projectCredentials.title = JSON.parse(this.projectCredentials.title.toString());
          this.projectCredentials.subtitle = JSON.parse(this.projectCredentials.subtitle.toString());
          this.projectCredentials.shorttitle = JSON.parse(this.projectCredentials.shorttitle.toString());
          this.projectCredentials.description = JSON.parse(this.projectCredentials.description.toString());

          this.editable_by.date.push(project.profileId)
          if(this.language === 'nl'){

            this.editable_by.title = this.parseJsonTitle.nl.validate.editable_by; this.editable_by.title.push(project.profileId)
            this.editable_by.subtitle = this.parseJsonSubtitle.nl.validate.editable_by; this.editable_by.subtitle.push(project.profileId)
            this.editable_by.shorttitle = this.parseJsonShorttitle.nl.validate.editable_by; this.editable_by.shorttitle.push(project.profileId)
            this.editable_by.description = this.parseJsonDescription.nl.validate.editable_by; this.editable_by.description.push(project.profileId)
          } else{
            this.editable_by.title = this.parseJsonTitle.nl.validate.editable_by; this.editable_by.title.push(project.profileId)
            this.editable_by.subtitle = this.parseJsonSubtitle.nl.validate.editable_by; this.editable_by.subtitle.push(project.profileId)
            this.editable_by.shorttitle = this.parseJsonShorttitle.nl.validate.editable_by; this.editable_by.shorttitle.push(project.profileId)
            this.editable_by.description = this.parseJsonDescription.nl.validate.editable_by; this.editable_by.description.push(project.profileId)
          }
        }
      )
    } else{
      this.projectCredentials.title = JSON.parse(this.projectCredentials.title.toString());
      this.projectCredentials.subtitle = JSON.parse(this.projectCredentials.subtitle.toString());
      this.projectCredentials.shorttitle = JSON.parse(this.projectCredentials.shorttitle.toString());
      this.projectCredentials.description = JSON.parse(this.projectCredentials.description.toString());
    }
  }

  save() {
    this.projectCredentials.title = JSON.stringify(this.projectCredentials.title);
    this.projectCredentials.subtitle = JSON.stringify(this.projectCredentials.subtitle);
    this.projectCredentials.shorttitle = JSON.stringify(this.projectCredentials.shorttitle);
    this.projectCredentials.description = JSON.stringify(this.projectCredentials.description);
    this.projectService.saveProjects(this.projectCredentials, 'general').subscribe(
      res => {
        this.projectCredentials.title = JSON.parse(this.projectCredentials.title.toString());
        this.projectCredentials.subtitle = JSON.parse(this.projectCredentials.subtitle.toString());
        this.projectCredentials.shorttitle = JSON.parse(this.projectCredentials.shorttitle.toString());
        this.projectCredentials.description = JSON.parse(this.projectCredentials.description.toString());
        this.message.error =  '';
        this.message.succes =  "Uw wijzigingen zijn opgeslaan"
        setTimeout(()=>{ this.message.succes =  "" }, 3000);
        if(res){
          this.router.navigate(['/project', res.id, 'general']);
        }
      },
      err => {
        console.log("Error occured");
        this.message.succes =  '';
        this.message.error = "Uw wijzigingen zijn niet opgeslaan, gelieve opnieuw te proberen"
        setTimeout(()=>{ this.message.error =  "" }, 3000);
      }
    );
  }

  validate(){
      if(this.isAdmin){
        this.save();
      }

  }

}