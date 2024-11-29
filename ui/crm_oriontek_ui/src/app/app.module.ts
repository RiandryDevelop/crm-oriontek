import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import  AppComponent  from '../app/app.component';
import { APP_CONFIG, appConfiguration } from './app.configuration';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
  ],
  providers: [
    { provide: APP_CONFIG, useValue: appConfiguration }, // Registrar configuración global
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
