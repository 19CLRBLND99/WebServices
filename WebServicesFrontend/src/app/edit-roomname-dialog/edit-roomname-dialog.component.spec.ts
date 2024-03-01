import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditRoomnameDialogComponent } from './edit-roomname-dialog.component';

describe('EditRoomnameDialogComponent', () => {
  let component: EditRoomnameDialogComponent;
  let fixture: ComponentFixture<EditRoomnameDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditRoomnameDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditRoomnameDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
