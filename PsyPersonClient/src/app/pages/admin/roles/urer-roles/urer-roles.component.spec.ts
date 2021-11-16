import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UrerRolesComponent } from './urer-roles.component';

describe('UrerRolesComponent', () => {
  let component: UrerRolesComponent;
  let fixture: ComponentFixture<UrerRolesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UrerRolesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UrerRolesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
