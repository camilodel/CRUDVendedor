import { MaterialModule } from './../../material/material.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SellersRoutingModule } from './sellers-routing.module';
import { SellerCrudComponent } from './components/seller-crud/seller-crud.component';
import { TableComponent } from './components/table/table.component';
import { DialogComponent } from './components/dialog/dialog.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ShowDetailsComponent } from './components/show-details/show-details.component';
import { CitiesDialogComponent } from './components/cities-dialog/cities-dialog.component';


@NgModule({
  declarations: [
    SellerCrudComponent,
    TableComponent,
    DialogComponent,
    ShowDetailsComponent,
    CitiesDialogComponent
  ],
  imports: [
    CommonModule,
    SellersRoutingModule,
    MaterialModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [
    SellerCrudComponent
  ]
})
export class SellersModule { }
