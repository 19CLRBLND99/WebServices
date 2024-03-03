import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-error-dialog',
  standalone: true,
  imports: [],
  templateUrl: './app-error-dialog.component.html',
  styleUrl: './app-error-dialog.component.css'
})
export class AppErrorDialogComponent {
  errorMessage: string;

  constructor(
    public dialogRef: MatDialogRef<AppErrorDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { 
    this.errorMessage = data.errorMessage;
  }

  closeDialog(): void {
    this.dialogRef.close();
  }
}
