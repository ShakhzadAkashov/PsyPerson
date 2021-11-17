import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoleLookupTableModalComponent } from './role-lookup-table-modal.component';

describe('RoleLookupTableModalComponent', () => {
  let component: RoleLookupTableModalComponent;
  let fixture: ComponentFixture<RoleLookupTableModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RoleLookupTableModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RoleLookupTableModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
