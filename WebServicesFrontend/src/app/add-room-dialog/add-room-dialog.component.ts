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


  checkForCorrectInput(inputField: HTMLInputElement, saveButton: HTMLButtonElement, thermostatId: HTMLInputElement) {
    const roomName = inputField.value;
    var disableButton;
    var specialChars = /[!"#$%&'()*+,-./:;<=>?@\[\\\]^_`{|}~]/;
    var specialCharsOrDigit = /[\d!"#$%&'()*+,-./:;<=>?@\[\\\]^_`{|}~]/;
    if (roomName.length == 0) {
      disableButton = true;
    } else {
      if (specialCharsOrDigit.test(roomName.charAt(0))) {
        disableButton = true;
        //hier dann irgendiwe dem nutzer sagen, dass ein falsches zeichen am anfang ist
      } else {
        disableButton = specialChars.test(roomName);
      }
    }
    if (disableButton) {
      thermostatId.disabled = true;
      saveButton.disabled = true;
    } else {
      thermostatId.disabled = false;
      saveButton.disabled = this.checkForFreeId(saveButton,thermostatId);
    }
  }

  checkForFreeId(saveButton: HTMLButtonElement, thermostatId: HTMLInputElement): boolean {
    var unused = false;
    if (thermostatId.valueAsNumber > 25) {
      alert("Maximal 25 Thermostate verfügbar!");//hier dann dem benutzer sagen, dass er ne kleinere Zahl eingeben soll
      saveButton.disabled = true;
    } else {
      if (thermostatId.value.length > 0){
      this.httpClient.get(this.baseUrl + "/CheckThermostatId?thermostatId=" + thermostatId.value).subscribe((data: TupleResponse) => {
        console.log(data);
        unused = data.item1.valueOf();
        if (data.item2 != null) {
          var listItems = data.item2.join(",");
          console.log("Thermostat id is already used! Unused Thermostats: " + listItems);
        }
        saveButton.disabled = !unused;
      });}
    }
    this.thermostatId = thermostatId.valueAsNumber;
    return unused;
  }
  
  onSave(): void {
    if (this.data.roomCount >= 25) {
      alert('Es können maximal 25 Räume erstellt werden.');
    } else if (this.roomName) {
      this.createRoom(this.roomName, this.thermostatId ? this.thermostatId.toString() : null); // Raum erstellen
    } else {
      alert('Bitte geben Sie einen Raumnamen ein.');
    }
  }

  createRoom(roomName: string, thermostatId: string | null): void {
    this.httpClient.post<number>(this.baseUrl + '/AddRoom?roomName=' + roomName, null).subscribe((response: number) => {
      console.log('Raum erfolgreich erstellt:', response);
      if (thermostatId) {
        this.assignThermostat(response, thermostatId); // Thermostat zuweisen, falls eine ID vorhanden ist
      } else {
        this.dialogRef.close(true); // Dialogfenster schließen, unabhängig davon, ob ein Thermostat zugewiesen wird oder nicht
      }
      window.location.reload();
    }, error => {
      console.error('Fehler beim Erstellen des Raums:', error);
      // Hier können Sie eine Fehlerbehandlung implementieren, z.B. eine Fehlermeldung anzeigen
    });
  }

  assignThermostat(roomId: number, thermostatId: string): void {
    this.httpClient.post<any>(this.baseUrl + '/AssignThermostatToRoom?roomId=' + roomId + '&thermostatId=' + thermostatId, null).subscribe(response => {
      console.log('Thermostat erfolgreich dem Raum zugewiesen:', response);
      this.dialogRef.close(true);
    }, error => {
      console.error('Fehler beim Zuweisen des Thermostats zum Raum:', error);
      // Hier können Sie eine Fehlerbehandlung implementieren, z.B. eine Fehlermeldung anzeigen
    });
  }


}
export interface TupleResponse {
  item1: boolean;
  item2: any[]; // Hier kannst du den Datentyp der Liste spezifizieren, den deine API zurückgibt
}
