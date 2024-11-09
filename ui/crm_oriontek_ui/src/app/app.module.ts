import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { HttpClientModule } from '@angular/common/http';

// Angular Material Components
import { MatTableModule } from "@angular/material/table";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MatSelectModule } from "@angular/material/select";
import { MatButtonModule } from "@angular/material/button";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatNativeDateModule } from "@angular/material/core";
import { MatSnackBarModule } from "@angular/material/snack-bar";
import { MatIconModule } from "@angular/material/icon";
import { MatDialogModule } from "@angular/material/dialog";
import { MatGridListModule } from "@angular/material/grid-list";

// NgRx Store and Effects
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';

// Feature Modules
import { AuthModule } from './modules/auth/auth.module';
import { ClientsModule } from './modules/clients/clients.module';

// Core Components
import { AppComponent } from './app.component';
import { DialogAddEditComponent } from './shared/components/dialog-add-edit/dialog-add-edit.component';
import { DialogDeleteComponent } from './shared/components/dialog-delete/dialog-delete.component';

// App Routing
import { AppRoutingModule } from './app-routing.module';

@NgModule({
  declarations: [
    AppComponent,
    DialogAddEditComponent,
    DialogDeleteComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    MatGridListModule,
    MatDialogModule,
    MatIconModule,
    MatSnackBarModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatTableModule,
    MatButtonModule,
    StoreModule.forRoot({}, {}),    // Configure the root reducer
    EffectsModule.forRoot([]),      // Configure the root effects
    AppRoutingModule,               // Routing for main navigation
    AuthModule,                     // Authentication feature module
    ClientsModule                   // Clients management feature module
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
