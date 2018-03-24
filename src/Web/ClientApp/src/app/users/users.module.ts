import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CoreModule } from '../core/core.module'
import { UsersListComponent } from './users-list/users-list.component';
import { MaterialModule } from '../core/material/material.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MaterialModule,
    NgxPaginationModule,
    CoreModule,
    SharedModule
  ],
  declarations: [
    UsersListComponent,
  ],
  exports: [
  ],
  providers: []
})
export class UsersModule { }
