import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrEditFirstLevelDifficultTestQuestionComponent } from './create-or-edit-first-level-difficult-test-question.component';

describe('CreateOrEditFirstLevelDifficultTestQuestionComponent', () => {
  let component: CreateOrEditFirstLevelDifficultTestQuestionComponent;
  let fixture: ComponentFixture<CreateOrEditFirstLevelDifficultTestQuestionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateOrEditFirstLevelDifficultTestQuestionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateOrEditFirstLevelDifficultTestQuestionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
