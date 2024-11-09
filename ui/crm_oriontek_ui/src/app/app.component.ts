import { AfterViewInit, Component, ViewChild, OnInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

import { DialogAddEditComponent } from "./shared/components/dialog-add-edit/dialog-add-edit.component";
import { DialogDeleteComponent } from './shared/components/dialog-delete/dialog-delete.component';

import { Client } from "./models/client/client.model";
import { ClientService } from './Services/client.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewInit, OnInit {
  title = 'crm_oriontek-ui';
  displayedColumns: string[] = ['name', 'locationsCount', 'createDate', 'updateDate', 'actions'];
  dataSource = new MatTableDataSource<Client>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private clientService: ClientService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loadClients();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  // Open dialog to add a new client
  openNewClientDialog() {
    this.dialog.open(DialogAddEditComponent, {
      disableClose: true,
      width: '400px',
    }).afterClosed().subscribe(result => {
      if (result === 'created') {
        this.loadClients();
      }
    });
  }

  // Open dialog to edit an existing client
  openEditClientDialog(clientData: Client) {
    this.dialog.open(DialogAddEditComponent, {
      disableClose: true,
      width: '400px',
      data: clientData,
    }).afterClosed().subscribe(result => {
      if (result === 'updated') {
        this.loadClients();
      }
    });
  }

  // Open dialog to confirm client deletion
  openDeleteClientDialog(clientData: Client) {
    this.dialog.open(DialogDeleteComponent, {
      disableClose: true,
      width: '350px',
      data: clientData,
    }).afterClosed().subscribe(result => {
      if (result === 'delete' && clientData.clientId !== undefined) {
        this.clientService.deleteClient(clientData.clientId).subscribe({
          next: () => {
            this.showAlert("Client has been deleted", "Close");
            this.loadClients();
          },
          error: (e) => console.error(e)
        });
      }
    });
  }
  

  // Filter data in the table
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  // Load client data into the table
  loadClients() {
    this.clientService.getClientsPaginated(1, 10).pipe().subscribe({
      next: (data) => {
        this.dataSource.data = data;
      },
      error: (e) => console.error(e)
    });
  }

  // Display a snack bar message
  showAlert(message: string, action: string) {
    this.snackBar.open(message, action, {
      horizontalPosition: 'end',
      verticalPosition: 'top',
      duration: 3000,
    });
  }
}
