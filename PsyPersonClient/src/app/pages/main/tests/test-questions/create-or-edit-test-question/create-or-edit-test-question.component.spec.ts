import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrEditTestQuestionModalComponent } from './create-or-edit-test-question.component';

describe('CreateOrEditTestQuestionComponent', () => {
  let component: CreateOrEditTestQuestionModalComponent;
  let fixture: ComponentFixture<CreateOrEditTestQuestionModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateOrEditTestQuestionModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateOrEditTestQuestionModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
