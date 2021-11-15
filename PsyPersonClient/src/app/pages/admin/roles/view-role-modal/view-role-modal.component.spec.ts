import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewRoleModalComponent } from './view-role-modal.component';

describe('ViewRoleModalComponent', () => {
  let component: ViewRoleModalComponent;
  let fixture: ComponentFixture<ViewRoleModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewRoleModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewRoleModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
