import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProjectService } from '../../core/shared/services/project.service'
import { PageTitleService } from '../../core/shared/services/page-title.service';
import { Iproject } from '../../core/shared/models/Iproject';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-project-add',
  templateUrl: './project-add.component.html',
  styleUrls: ['./project-add.component.scss']
})
export class ProjectAddComponent implements OnInit {
  id: number;
  project: {};
  valueObject = {
    nl: {
      value: '',
      validate: {
        allow_changes:  false,
        required: true,
        publication_ok: false,
        validated_by: null,
        feedback: '',
        editable_by: [],
        visible_to: null,
        updated_at: Date
      }
    },
    en: {
      value: '',
      validate: {
        allow_changes:  false,
        required: true,
        publication_ok: false,
        validated_by: null,
        feedback: '',
        editable_by: [],
        visible_to: null,
        updated_at: Date
      }
    }
  } 
  constructor(
    private route: ActivatedRoute,
    private projectService:ProjectService,
    private pageTitleService: PageTitleService,
    private translate: TranslateService
  ) { }

  ngOnInit() {
    this.id = this.route.snapshot.params['id'];
    this.pageTitleService.setTitle("Project")
    if(this.id){
      this.projectService.getProjectById(this.id).subscribe(
        project => { 
          this.project = project
          var title = JSON.parse(project.title.toString());
          this.pageTitleService.setSubTitle(title.nl.value)
        }
      )
    } else{
      this.translate.get('Nieuw Project').subscribe((res: string) => {
        this.pageTitleService.setSubTitle(res)
      });
    }
  }
}
