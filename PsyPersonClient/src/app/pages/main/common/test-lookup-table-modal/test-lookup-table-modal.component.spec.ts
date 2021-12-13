import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestLookupTableModalComponent } from './test-lookup-table-modal.component';

describe('TestLookupTableModalComponent', () => {
  let component: TestLookupTableModalComponent;
  let fixture: ComponentFixture<TestLookupTableModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestLookupTableModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestLookupTableModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
