import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../../../core/shared/services/project.service'
import { saveAs } from 'file-saver/FileSaver';

import { Router } from '@angular/router'; 
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-form-download',
  templateUrl: './form-download.component.html',
  styleUrls: ['./form-download.component.scss']
})
export class FormDownloadComponent implements OnInit {
  message = {
    error: '',
    succes: '',
    warning: ''
  };
  id: number;

  constructor(
    private projectService:ProjectService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.id = this.route.parent.snapshot.params.id;
  }

  getProjectfile(){
    this.projectService.getProjectfile(this.id).subscribe(
      result => { 
        const filename = `${"projectdossier_" + this.id}`;
        saveAs(result, filename);
      },
      err => {
        console.log(err)
        this.message.error = "Uw kan dit niet downloaden, gelieve opnieuw te proberen"
    
      }
    )
  }
  getProjectsheet(){
    this.projectService.getProjectsheet(this.id).subscribe(
      result => { 
        const filename = `${"projectfiche_" + this.id}`;
        saveAs(result, filename);
      },
      err => {
        console.log(err)
        this.message.error = "Uw kan dit niet downloaden, gelieve opnieuw te proberen"
    
      }
    )
  }

}