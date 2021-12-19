import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserTestsDetailsModalComponent } from './user-tests-details-modal.component';

describe('UserTestsDetailsModalComponent', () => {
  let component: UserTestsDetailsModalComponent;
  let fixture: ComponentFixture<UserTestsDetailsModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserTestsDetailsModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserTestsDetailsModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
