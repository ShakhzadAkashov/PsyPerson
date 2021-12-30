import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserTestingListForCheckComponent } from './user-testing-list-for-check.component';

describe('UserTestingListForCheckComponent', () => {
  let component: UserTestingListForCheckComponent;
  let fixture: ComponentFixture<UserTestingListForCheckComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserTestingListForCheckComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserTestingListForCheckComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
