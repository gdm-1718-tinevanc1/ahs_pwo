import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreModule } from '../core/core.module'
import { ProjectAddModule } from './project-add/project-add.module'
import { ProjectListComponent } from './project-list/project-list.component';
import { ProjectAddComponent } from './project-add/project-add.component';
import { TypeService } from './shared/services/type.service';
import { MaterialModule } from '../core/material/material.module';
import { AppRoutingModule } from '../app-routing.module';
import { FormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { SharedModule } from '../shared/shared.module';
import { FinancingformService } from './shared/services/financingform.service'

@NgModule({
  imports: [
    CommonModule,
    ProjectAddModule,
    MaterialModule,
    CoreModule,
    AppRoutingModule,
    SharedModule,
    FormsModule,
    NgxPaginationModule
  ],
  declarations: [
    ProjectListComponent,
    ProjectAddComponent
  ],
  exports: [
    ProjectListComponent,
    ProjectAddComponent
  ],
  providers: [
    TypeService,
    FinancingformService
  ]
})
export class ProjectsModule { }
