import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FormMetadataComponent } from './form-metadata.component';

describe('FormMetadataComponent', () => {
  let component: FormMetadataComponent;
  let fixture: ComponentFixture<FormMetadataComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FormMetadataComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormMetadataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
