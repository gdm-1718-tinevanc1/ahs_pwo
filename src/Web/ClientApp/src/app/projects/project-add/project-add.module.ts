import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { QuillModule } from 'ngx-quill';

import { FormGeneralComponent } from './form-general/form-general.component';
import { FormAbstractComponent } from './form-abstract/form-abstract.component';
import { FormMetadataComponent } from './form-metadata/form-metadata.component';
import { FormBudgetComponent } from './form-budget/form-budget.component';
import { FormMediaComponent } from './form-media/form-media.component';
import { FormDownloadComponent } from './form-download/form-download.component';
import { MaterialModule } from '../../core/material/material.module';
import { ValidateComponent } from './validate/validate.component';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    QuillModule,
    SharedModule
  ],
  declarations: [
    FormGeneralComponent, 
    FormAbstractComponent, 
    FormMetadataComponent, 
    FormBudgetComponent, 
    FormMediaComponent, 
    FormDownloadComponent, ValidateComponent
  ],
  exports: [
    FormGeneralComponent, 
    FormAbstractComponent, 
    FormMetadataComponent, 
    FormBudgetComponent, 
    FormMediaComponent, 
    FormDownloadComponent
  ]
})
export class ProjectAddModule { }
