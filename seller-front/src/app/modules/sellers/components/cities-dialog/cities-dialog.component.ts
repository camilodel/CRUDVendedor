import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { GlobalService } from 'src/app/services/global.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-cities-dialog',
  templateUrl: './cities-dialog.component.html',
  styleUrls: ['./cities-dialog.component.scss']
})
export class CitiesDialogComponent implements OnInit {
  public dataForm: FormGroup;
  public tittleDialog = 'Registrar';
  public buttonDialog = 'Registrar';
  id: string | undefined;

  constructor(
    private formBuilder: FormBuilder,
    private service: GlobalService,
    public dialog: MatDialog,
    public dialogRef: MatDialogRef<CitiesDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public dataModal: any
  ) {
    this.dataForm = this.formBuilder.group({
      description: [''],
    });
  }

  // INICIO GET DATA
  get description(): any {
    return this.dataForm.get('description');
  }
  // FIN GET DATA

  ngOnInit(): void {
  }

  saveData(values: any): void {
    if (this.id === undefined)  {
      this.service.postData('City/CreateCity/', values).subscribe((result) => {
        Swal.fire({
          icon: 'success',
          title:'Excelente!!',
          text: 'Registro guardado correctamente',
          backdrop: `rgba(7,164,181,0.3)`
        });
        this.dialog.closeAll();
        this.service.refreshData();
      }, (error) => {
        Swal.fire({
          icon: 'error',
          title:'Error!!',
          text: 'Problemas al guardar el registro',
          backdrop: `rgba(255,87,87,0.2)`
        });
        console.log(error);
      });
    } else {
      values.id = this.id;
      this.service.putData('Seller/', this.id, values).subscribe((result) => {
        this.dataForm.reset();
        this.tittleDialog = 'Modificar';
        this.buttonDialog = 'Modificar';
        this.id = undefined;
        Swal.fire({
          icon: 'success',
          title:'Excelente!!',
          text: 'Registro modificado correctamente',
          backdrop: `rgba(7,164,181,0.3)`
        });
        this.dialog.closeAll();
        this.service.refreshData();
      }, (error) => {
        Swal.fire({
          icon: 'error',
          title:'Error!!',
          text: 'Problemas al modificar el registro',
          backdrop: `rgba(255,87,87,0.2)`
        });
        console.log(error);
      });
    }

  }

}
