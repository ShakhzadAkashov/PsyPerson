import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrEditSecondLevelDifficultTestQuestionComponent } from './create-or-edit-second-level-difficult-test-question.component';

describe('CreateOrEditSecondLevelDifficultTestQuestionComponent', () => {
  let component: CreateOrEditSecondLevelDifficultTestQuestionComponent;
  let fixture: ComponentFixture<CreateOrEditSecondLevelDifficultTestQuestionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateOrEditSecondLevelDifficultTestQuestionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateOrEditSecondLevelDifficultTestQuestionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
