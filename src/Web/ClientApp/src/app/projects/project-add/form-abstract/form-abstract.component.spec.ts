import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FormAbstractComponent } from './form-abstract.component';

describe('FormAbstractComponent', () => {
  let component: FormAbstractComponent;
  let fixture: ComponentFixture<FormAbstractComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FormAbstractComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormAbstractComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
