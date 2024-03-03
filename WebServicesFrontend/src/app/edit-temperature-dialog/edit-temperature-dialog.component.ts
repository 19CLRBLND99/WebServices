import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-edit-temperature-dialog',
  standalone: true,
  imports: [FormsModule, CommonModule, HttpClientModule],
  templateUrl: './edit-temperature-dialog.component.html',
  styleUrl: './edit-temperature-dialog.component.css'
})
export class EditTemperatureDialogComponent {
  baseUrl: String = 'http://localhost:50000';
  newTemperature: number;
  rooms: any = [];
  onSaveDisabled: boolean = false;

  constructor(
    public dialogRef: MatDialogRef<EditTemperatureDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private httpClient: HttpClient
  ) { }

  getAllRooms(): void {
    this.httpClient.get(this.baseUrl+'/GetAllRooms').subscribe((data: any) => {
      this.rooms = data;
      console.log(this.rooms);
      this.getTemperaturesForRooms();
    });
  }

  getTemperaturesForRooms(): void {
    this.rooms.forEach((room) => {
      this.httpClient.get(this.baseUrl+`/GetRoomWithThermostatByRoomId?roomId=${room.roomId}`).subscribe((temperatureData: any) => {
        room.temperature = temperatureData.thermostat.temperature;
      });
    });
  }

  changeTemperature(roomId: number, newTemperature: number): void {
     this.httpClient.post<any>(this.baseUrl+`/UpdateRoomTemperature?roomId=${roomId}&newTemperature=${newTemperature}`, null)
      .subscribe((data: any) => {
      console.log('Temperature changed successfully');
      this.getAllRooms();
      this.dialogRef.close(); // Schließen des Dialogfensters nach erfolgreichem Speichern
    }, (error) => {
      console.error('Error while changing temperature:', error);
    });
  }

  onSave(): void {
    if (!isNaN(this.newTemperature) && this.newTemperature >= 0) {
      this.changeTemperature(this.data.roomId, this.newTemperature);
      this.dialogRef.close(this.newTemperature);
      window.location.reload();
    } else {
      alert('Geben Sie eine positive Temperatur an!');
    }
  }
  

  onCancel(): void {
    this.dialogRef.close();
  }
}
