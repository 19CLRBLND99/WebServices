import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditTemperatureDialogComponent } from './edit-temperature-dialog.component';

describe('EditTemperatureDialogComponent', () => {
  let component: EditTemperatureDialogComponent;
  let fixture: ComponentFixture<EditTemperatureDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditTemperatureDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditTemperatureDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
