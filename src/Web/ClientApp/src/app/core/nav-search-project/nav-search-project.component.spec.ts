import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NavSearchProjectComponent } from './nav-search-project.component';

describe('NavSearchProjectComponent', () => {
  let component: NavSearchProjectComponent;
  let fixture: ComponentFixture<NavSearchProjectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NavSearchProjectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NavSearchProjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
