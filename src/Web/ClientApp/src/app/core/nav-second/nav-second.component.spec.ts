import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NavSecondComponent } from './nav-second.component';

describe('NavSecondComponent', () => {
  let component: NavSecondComponent;
  let fixture: ComponentFixture<NavSecondComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NavSecondComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NavSecondComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
