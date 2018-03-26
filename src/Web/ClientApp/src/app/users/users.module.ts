import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CoreModule } from '../core/core.module'
import { UsersListComponent } from './users-list/users-list.component';
import { MaterialModule } from '../core/material/material.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { SharedModule } from '../shared/shared.module';
import { UsersAddComponent } from '../users/users-add/users-add.component';
import { AppRoutingModule } from '../app-routing.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MaterialModule,
    NgxPaginationModule,
    CoreModule,
    SharedModule,
    AppRoutingModule
  ],
  declarations: [
    UsersListComponent,
    UsersAddComponent,
  ],
  exports: [
  ],
  providers: []
})
export class UsersModule { }
