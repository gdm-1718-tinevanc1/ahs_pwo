import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FormBudgetComponent } from './form-budget.component';

describe('FormBudgetComponent', () => {
  let component: FormBudgetComponent;
  let fixture: ComponentFixture<FormBudgetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FormBudgetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormBudgetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
