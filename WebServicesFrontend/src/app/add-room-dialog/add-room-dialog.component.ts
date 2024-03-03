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

  checkForCorrectInput(inputField: HTMLInputElement, saveButton: HTMLButtonElement, thermostatId: HTMLInputElement, unusedIdsField: HTMLDivElement) {
    //there are specific rules for the name input which are checked at every key up event
    const roomName = inputField.value;
    var disableButton;
    var specialChars = /[!"#$%§&'()*+,-./:;<=>?@\[\\\]^´°_`{|}~]/;
    var specialCharsOrDigit = /[\d\s!"#$§%&'()*+,-./:;<=>?@°\[\\\]^_`´{|}~]/;
    if (roomName.length == 0) {
      disableButton = true;
    } else {
      if (specialCharsOrDigit.test(roomName.charAt(0))) {//the first character cannot be a digit or special character
        disableButton = true;
      } else {
        disableButton = specialChars.test(roomName);//after that the name can contain a number but still no special character 
      }
    }
    if (disableButton) {//if the name is incorrect the save button gets disabled and the user cannot put an id in the thermostat id field
      thermostatId.disabled = true;
      saveButton.disabled = true;
      inputField.style.backgroundColor = "#FFCCCB";
    } else {//else the thermostat id field is activated and the button depends on the correctness of the id
      thermostatId.disabled = false;
      inputField.style.backgroundColor = "#FFF";
      saveButton.disabled = this.checkForFreeId(saveButton, thermostatId, unusedIdsField);
    }
  }
  //this method is called on every key up event, so it checks if the id is already in use and informs the user about it
  checkForFreeId(saveButton: HTMLButtonElement, thermostatId: HTMLInputElement, unusedIdsField: HTMLDivElement): boolean {
    var unused = false;
    //just the css part so the alert for the user is understandable
    unusedIdsField.style.wordBreak = "break-word";
    unusedIdsField.style.fontSize = "12px";
    unusedIdsField.style.color = "red";
    thermostatId.style.backgroundColor = "#FFCCCB";
    if (thermostatId.valueAsNumber > 25) {
      unusedIdsField.innerText = "Max of 25 thermostats avaliable!";
      unusedIdsField.hidden = false;
      saveButton.disabled = true;
    } else {
      if (thermostatId.value.length > 0) {//because it is optional the field can be empty
        //if there is a number it gets checked
        this.httpClient.get(this.baseUrl + "/CheckThermostatId?thermostatId=" + thermostatId.value).subscribe((data: TupleResponse) => {
          console.log(data);
          unused = data.item1.valueOf();//first part of the tuple is a boolean and shows if the id is avaliable
          if (data.item2 != null) {//if the second part of the tuple is not null (which means the id is used) then its a list of the unused ids
            var listItems = data.item2.join(",");
            console.log("Thermostat id is already used! Unused Thermostats: " + listItems);
            unusedIdsField.innerText = "Unused Thermostats: " + listItems;
            unusedIdsField.hidden = false;
          }else{
            unusedIdsField.hidden = true;
            thermostatId.style.backgroundColor = "#FFF";
          }
          //because the unused variable shows if the id is avaliable the variable must be negatived, so the button is not disabled
          saveButton.disabled = !unused;
        });
      }else{
        unusedIdsField.hidden = true;
        thermostatId.style.backgroundColor = "#FFF";
      }
    }
    this.thermostatId = thermostatId.valueAsNumber;//later used in the assignment proccess
    return unused;
  }

  createRoom(roomName: string, thermostatId: string | null): void {
    this.httpClient.post<number>(this.baseUrl + '/AddRoom?roomName=' + roomName, null).subscribe((response: number) => {
      console.log('Room successfully created! Response: ', response);
      if (thermostatId) {
        this.assignThermostat(response, thermostatId); // if the id is not null it gets assigned to the room
      } else {
        this.dialogRef.close(true); // close the pop up window although there is no thermostat id
      }
      window.location.reload();
    }, error => {
      console.error('Error while creating room! Error: ', error);
    });
  }
  //if the id field was not empty the thermostat id gets assigned to the room
  assignThermostat(roomId: number, thermostatId: string): void {
    this.httpClient.post<any>(this.baseUrl + '/AssignThermostatToRoom?roomId=' + roomId + '&thermostatId=' + thermostatId, null).subscribe(response => {
      console.log('Thermostat successfully assigned to room! Response: ', response);
      this.dialogRef.close(true);
    }, error => {
      console.error('Error while assigning Thermostat to room! Error: ', error);
    });
  }

  onSave(): void {
    if (this.roomName) {
      this.createRoom(this.roomName, this.thermostatId ? this.thermostatId.toString() : null); //Create Room after clicking on the button
    } else {
      alert('Bitte geben Sie einen Raumnamen ein.'); //alert when there's no input
    }
  }

  onCancel(): void {
    this.dialogRef.close(); //close Pop Up Window after clicking cancel
  }
}
//this is an extra class so the response of the api is correctly converted and can be used later
export interface TupleResponse {
  item1: boolean;
  item2: any[];
}
