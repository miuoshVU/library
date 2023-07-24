import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddProposedBookComponent } from './add-proposed-book.component';

describe('AddProposedBookComponent', () => {
  let component: AddProposedBookComponent;
  let fixture: ComponentFixture<AddProposedBookComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddProposedBookComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddProposedBookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
