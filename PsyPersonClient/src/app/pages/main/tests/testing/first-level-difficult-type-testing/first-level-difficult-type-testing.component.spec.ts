import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FirstLevelDifficultTypeTestingComponent } from './first-level-difficult-type-testing.component';

describe('FirstLevelDifficultTypeTestingComponent', () => {
  let component: FirstLevelDifficultTypeTestingComponent;
  let fixture: ComponentFixture<FirstLevelDifficultTypeTestingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FirstLevelDifficultTypeTestingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FirstLevelDifficultTypeTestingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
