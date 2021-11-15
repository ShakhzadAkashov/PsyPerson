import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrEditUserModalComponent } from './create-or-edit-user-modal.component';

describe('CreateOrEditUserModalComponent', () => {
  let component: CreateOrEditUserModalComponent;
  let fixture: ComponentFixture<CreateOrEditUserModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateOrEditUserModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateOrEditUserModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
