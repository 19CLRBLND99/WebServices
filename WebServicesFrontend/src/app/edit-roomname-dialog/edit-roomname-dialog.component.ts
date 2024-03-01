import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';

@Component({
  selector: 'app-edit-roomname-dialog',
  standalone: true,
  imports: [FormsModule, MatFormFieldModule],
  templateUrl: './edit-roomname-dialog.component.html',
  styleUrls: ['./edit-roomname-dialog.component.css']
})
export class RoomNameDialogComponent {
  newRoomName: string;

  constructor(
    public dialogRef: MatDialogRef<RoomNameDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.newRoomName = data.currentName;
  }

  onSave(): void {
    this.dialogRef.close(this.newRoomName);
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}