import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrEditSimpleTypeTestQuestionModalComponent } from './create-or-edit-simple-type-test-question-modal.component';

describe('CreateOrEditTestQuestionComponent', () => {
  let component: CreateOrEditSimpleTypeTestQuestionModalComponent;
  let fixture: ComponentFixture<CreateOrEditSimpleTypeTestQuestionModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateOrEditSimpleTypeTestQuestionModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateOrEditSimpleTypeTestQuestionModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
