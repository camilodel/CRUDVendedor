import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from '../dialog/dialog.component';

@Component({
  selector: 'app-seller-crud',
  templateUrl: './seller-crud.component.html',
  styleUrls: ['./seller-crud.component.scss']
})
export class SellerCrudComponent implements OnInit {

  constructor(public dialog: MatDialog) { }

  ngOnInit(): void {
  }

  openDialog(): void {
    this.dialog.open(DialogComponent, {
      width: '350px',
      disableClose: true
    });
  }


}
