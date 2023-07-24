import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProposedBooksComponent } from './proposed-books.component';

describe('ProposedBooksComponent', () => {
  let component: ProposedBooksComponent;
  let fixture: ComponentFixture<ProposedBooksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProposedBooksComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(ProposedBooksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
