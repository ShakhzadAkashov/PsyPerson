import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrEditTestModalComponent } from './create-or-edit-test-modal.component';

describe('CreateOrEditTestModalComponent', () => {
  let component: CreateOrEditTestModalComponent;
  let fixture: ComponentFixture<CreateOrEditTestModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateOrEditTestModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateOrEditTestModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
