import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SecondLevelDifficultTypeTestingComponent } from './second-level-difficult-type-testing.component';

describe('SecondLevelDifficultTypeTestingComponent', () => {
  let component: SecondLevelDifficultTypeTestingComponent;
  let fixture: ComponentFixture<SecondLevelDifficultTypeTestingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SecondLevelDifficultTypeTestingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SecondLevelDifficultTypeTestingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
