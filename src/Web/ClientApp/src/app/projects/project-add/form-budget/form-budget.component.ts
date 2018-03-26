import { Component, OnInit, Directive, AfterViewInit, Input, ViewChild, ElementRef, ViewChildren, QueryList  } from '@angular/core';
import { TypeService } from '../../shared/services/type.service'
import { ActivatedRoute } from '@angular/router';
import { ProjectService } from '../../../core/shared/services/project.service'
import { Router } from '@angular/router'; 
import { AuthenticationService } from '../../../core/shared/services/authentication.service'
import { SharedService } from '../../../core/shared/services/shared.service'
import { ValidateComponent } from '../validate/validate.component';

@Component({
  selector: 'app-form-budget',
  templateUrl: './form-budget.component.html',
  styleUrls: ['./form-budget.component.scss']
})
export class FormBudgetComponent implements OnInit {
  isAdmin = this.authenticationService.isAdmin;
  @ViewChildren('cmp') components: QueryList<ValidateComponent>;

  valueObject = JSON.stringify(this.sharedService.sharedNode.validateObject);

  id: number;
  message = {
    error: '',
    succes: '',
    warning: ''
  };
  isExpandedValidate = {
    total: false,
    artevelde: false,
    operating: false,
    investment: false,
    staff: false
  }

  projectCredentials = {
    /*id: null,*/
    budget: {},
    budgetValidate: this.valueObject
  };

  editable_by  = {
    budget: []
  }

  constructor(
    private projectService:ProjectService,
    private typeService:TypeService,
    private authenticationService:AuthenticationService,
    private sharedService:SharedService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    let creds = {
        totalBudget: null,
        arteveldeBudget: null,
        operatingBudget: null,
        investmentBudget: null,
        staffBudget: null,
        profileId: this.authenticationService.profileId
    }
    this.id = this.route.parent.snapshot.params.id;
    if(this.id){
      this.projectService.getProjectBudgetById(this.id).subscribe(
        project => { 
          this.projectCredentials = project
          this.projectCredentials.budgetValidate = JSON.parse(this.projectCredentials.budgetValidate.toString());

          this.editable_by.budget = this.projectCredentials.budget.validate.editable_by; this.editable_by.budget.push(project.profileId)

          if(!project.budget){
            this.projectCredentials.budget = {
              totalBudget: null,
              arteveldeBudget: null,
              operatingBudget: null,
              investmentBudget: null,
              staffBudget: null,
              profileId: this.authenticationService.profileId,
            }
          } 
        }
      )
    } else {
      this.projectCredentials.budget = creds
      this.projectCredentials.budgetValidate = JSON.parse(this.projectCredentials.budgetValidate.toString());
    }
  }

  public save() {
    console.log(this.projectCredentials)
    this.projectCredentials.budgetValidate = JSON.stringify(this.projectCredentials.budgetValidate);
    this.projectService.saveProjects(this.projectCredentials, 'budget').subscribe(
      res => {
        this.projectCredentials.budgetValidate = JSON.parse(this.projectCredentials.budgetValidate.toString());
        this.message.error =  '';
        this.message.succes =  "Uw wijzigingen zijn opgeslaan"
        setTimeout(()=>{ this.message.succes =  "" }, 3000);
        console.log(this.projectCredentials)
        if(res){
          this.router.navigate(['/project', res.id, 'budget']);
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
    /* var validates = [];
    this.components.forEach(validate => validates.push(validate.validate));
    this.projectCredentials.budgetValidate.validate = validates[0]
    console.log(this.projectCredentials.budgetValidate)
 */
    if(this.isAdmin){
      this.save();
    }
  }
}
