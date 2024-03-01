import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-edit-temperature-dialog',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './edit-temperature-dialog.component.html',
  styleUrl: './edit-temperature-dialog.component.css'
})
export class EditTemperatureDialogComponent {
  newTemperature: number;

  constructor(
    public dialogRef: MatDialogRef<EditTemperatureDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) { }

  onSave(): void {
    if (!isNaN(this.newTemperature)) {
      this.dialogRef.close(this.newTemperature);
    } else {
      // Handle invalid input
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
