import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-show-details',
  templateUrl: './show-details.component.html',
  styleUrls: ['./show-details.component.scss']
})
export class ShowDetailsComponent implements OnInit {
  public detailsItem: any;

  constructor(@Inject(MAT_DIALOG_DATA) public dataModal: any) { }

  ngOnInit(): void {
    this.getDetails();
  }

  getDetails(): void {
    this.detailsItem = this.dataModal.data;
  }

}
