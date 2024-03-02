import { Component, inject, Inject } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-room-dialog',
  standalone: true,
  imports: [MatFormFieldModule, FormsModule, MatDialogModule, HttpClientModule],
  templateUrl: './add-room-dialog.component.html',
  styleUrl: './add-room-dialog.component.css'
})
export class AddRoomDialogComponent {
  baseUrl: String = 'http://localhost:50000';
  roomName: string; 
  thermostatId: number;

  constructor(
    public dialogRef: MatDialogRef<AddRoomDialogComponent>,
    private httpClient: HttpClient,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSave(): void {
    if (this.roomName) {
      this.createRoom(this.roomName, this.thermostatId ? this.thermostatId.toString() : null); // Raum erstellen
      window.location.reload();
    } else {
      alert('Bitte geben Sie einen Raumnamen ein.');
    }
  }

  createRoom(roomName: string, thermostatId: string | null): void {
    this.httpClient.post<number>(this.baseUrl+'/AddRoom?roomName=' + roomName, null).subscribe((response: number) => {
      console.log('Raum erfolgreich erstellt:', response);
      if (thermostatId) {
        this.assignThermostat(response, thermostatId); // Thermostat zuweisen, falls eine ID vorhanden ist
      } else {
        this.dialogRef.close(true); // Dialogfenster schließen, unabhängig davon, ob ein Thermostat zugewiesen wird oder nicht
      }
    }, error => {
      console.error('Fehler beim Erstellen des Raums:', error);
      // Hier können Sie eine Fehlerbehandlung implementieren, z.B. eine Fehlermeldung anzeigen
    });
  }

  assignThermostat(roomId: number, thermostatId: string): void {
    this.httpClient.post<any>(this.baseUrl+'/AssignThermostatToRoom?roomId=' + roomId + '&thermostatId=' + thermostatId, null).subscribe(response => {
      console.log('Thermostat erfolgreich dem Raum zugewiesen:', response);
      this.dialogRef.close(true);
    }, error => {
      console.error('Fehler beim Zuweisen des Thermostats zum Raum:', error);
      // Hier können Sie eine Fehlerbehandlung implementieren, z.B. eine Fehlermeldung anzeigen
    });
  }
}
