import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrEditRoleModalComponent } from './create-or-edit-role-modal.component';

describe('CreateOrEditRoleModalComponent', () => {
  let component: CreateOrEditRoleModalComponent;
  let fixture: ComponentFixture<CreateOrEditRoleModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateOrEditRoleModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateOrEditRoleModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
