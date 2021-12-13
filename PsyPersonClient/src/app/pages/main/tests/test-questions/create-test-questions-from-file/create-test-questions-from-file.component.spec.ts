import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateTestQuestionsFromFileModalComponent } from '../create-test-questions-from-file/create-test-questions-from-file.component';

describe('CreateTestQuestionsFromFileComponent', () => {
  let component: CreateTestQuestionsFromFileModalComponent;
  let fixture: ComponentFixture<CreateTestQuestionsFromFileModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateTestQuestionsFromFileModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateTestQuestionsFromFileModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
