import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserTestingHistoryComponent } from './user-testing-history.component';

describe('UserTestingHistoryComponent', () => {
  let component: UserTestingHistoryComponent;
  let fixture: ComponentFixture<UserTestingHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserTestingHistoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserTestingHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
