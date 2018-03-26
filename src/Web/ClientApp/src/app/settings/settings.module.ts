import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SettingsComponent } from './settings.component';
import { MaterialModule } from '../core/material/material.module';
import { SharedModule } from '../shared/shared.module';
import { SettingsService } from './shared/services/settings.service';
import { FormsModule } from '@angular/forms';


@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    SharedModule,
    FormsModule
  ],
  declarations: [SettingsComponent],
  providers: [SettingsService]
})
export class SettingsModule { }
