import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddBookInstanceComponent } from './add-book-instance.component';

describe('AddBookInstanceComponent', () => {
  let component: AddBookInstanceComponent;
  let fixture: ComponentFixture<AddBookInstanceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddBookInstanceComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddBookInstanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
