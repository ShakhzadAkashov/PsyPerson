import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateTestQuestionsFromFileComponent } from './create-test-questions-from-file.component';

describe('CreateTestQuestionsFromFileComponent', () => {
  let component: CreateTestQuestionsFromFileComponent;
  let fixture: ComponentFixture<CreateTestQuestionsFromFileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateTestQuestionsFromFileComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateTestQuestionsFromFileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
