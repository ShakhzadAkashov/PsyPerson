import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimpleTypeTestingComponent } from './simple-type-testing.component';

describe('SimpleTypeTestingComponent', () => {
  let component: SimpleTypeTestingComponent;
  let fixture: ComponentFixture<SimpleTypeTestingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimpleTypeTestingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimpleTypeTestingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
