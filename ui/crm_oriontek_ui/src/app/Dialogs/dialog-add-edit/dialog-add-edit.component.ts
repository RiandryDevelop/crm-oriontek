import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Client } from 'src/app/models/client.model';
import { ClientService } from '../../Services/client.service';

@Component({
  selector: 'app-dialog-add-edit',
  templateUrl: './dialog-add-edit.component.html',
  styleUrls: ['./dialog-add-edit.component.css']
})
export class DialogAddEditComponent implements OnInit {
  clientForm: FormGroup;
  actionTitle: string = 'New Client';
  actionBtn: string = 'Save';
  showLocationFields: boolean = false;

  constructor(
    private dialogRef: MatDialogRef<DialogAddEditComponent>,
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private clientService: ClientService,
    @Inject(MAT_DIALOG_DATA) public clientData: Client
  ) {
    this.clientForm = this.initializeForm();
  }

  ngOnInit(): void {
    if (this.clientData) {
      this.populateForm(this.clientData);
      this.actionTitle = 'Edit Client';
      this.actionBtn = 'Update';
    }
  }

  // Initialize client form with controls
  private initializeForm(): FormGroup {
    return this.fb.group({
      name: ['', Validators.required],
      locations: this.fb.array([]),
      locationName: [''],
      provinceName: [''],
      municipalityName: [''],
      districtName: [''],
      sectorName: ['']
    });
  }

  // Populate form with data if editing
  private populateForm(client: Client): void {
    this.clientForm.patchValue({ name: client.name });
    this.setExistingLocations(client.locations || []);
  }

  // Add a new location to the FormArray
  addLocation(): void {
    const location = {
      locationName: this.clientForm.get('locationName')?.value,
      provinceName: this.clientForm.get('provinceName')?.value,
      municipalityName: this.clientForm.get('municipalityName')?.value,
      districtName: this.clientForm.get('districtName')?.value,
      sectorName: this.clientForm.get('sectorName')?.value
    };

    if (this.validateLocationFields(location)) {
      this.locations.push(this.fb.group(location));

      // Reset location fields
      this.clientForm.patchValue({
        locationName: '',
        provinceName: '',
        municipalityName: '',
        districtName: '',
        sectorName: ''
      });
      this.showLocationFields = false;
    } else {
      this.showAlert('Please fill in all location fields before saving.', 'Close');
    }
  }

  // Validate location fields to ensure they are not empty
  private validateLocationFields(location: any): boolean {
    return Object.values(location).every(field => 
      typeof field === 'string' && field.trim() !== ''
    );
  }

  // Getter for locations FormArray
  get locations(): FormArray {
    return this.clientForm.get('locations') as FormArray;
  }

  // Populate locations array when editing existing client
  private setExistingLocations(locations: any[]): void {
    locations.forEach(location => {
      this.locations.push(this.fb.group(location));
    });
  }

  // Display feedback to the user
  private showAlert(message: string, action: string): void {
    this.snackBar.open(message, action, {
      horizontalPosition: 'end',
      verticalPosition: 'top',
      duration: 3000
    });
  }

  // Submit form to add or update client
  addEditClient(): void {
    if (this.clientForm.invalid) return;

    const clientModel: Client = {
      clientId: this.clientData?.clientId, // Optional: Use clientId if editing
      name: this.clientForm.value.name,
      locations: this.clientForm.value.locations
    };

    if (this.clientData) {
      this.updateClient(clientModel);
    } else {
      this.createClient(clientModel);
    }
  }

  // Call service to create a new client
  private createClient(client: Client): void {
    this.clientService.createClient(client).subscribe({
      next: () => {
        this.showAlert('Client added successfully', 'Close');
        this.dialogRef.close('Created');
      },
      error: () => {
        this.showAlert('Failed to add client', 'Error');
      }
    });
  }

  // Call service to update an existing client
  private updateClient(client: Client): void {
    this.clientService.updateClient(client).subscribe({
      next: () => {
        this.showAlert('Client updated successfully', 'Close');
        this.dialogRef.close('Updated');
      },
      error: () => {
        this.showAlert('Failed to update client', 'Error');
      }
    });
  }
}
