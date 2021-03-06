import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { GlobalService } from 'src/app/services/global.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.scss']
})
export class DialogComponent implements OnInit {
  public dataForm: FormGroup;
  public tittleDialog = 'Registrar';
  public buttonDialog = 'Registrar';
  id: string | undefined;

  cities: any[];
  public filteredCitiesList;

  constructor(
    private formBuilder: FormBuilder,
    private service: GlobalService,
    public dialog: MatDialog,
    public dialogRef: MatDialogRef<DialogComponent>,
    @Inject(MAT_DIALOG_DATA) public dataModal: any
  ) {
    this.dataForm = this.formBuilder.group({
      name: ['', Validators.required],
      lastName: ['', Validators.required],
      identificationNumber: ['', Validators.required],
      cityId: [''],
    });
  }

  // INICIO GET DATA
  get name(): any {
    return this.dataForm.get('name');
  }
  get lastName(): any {
    return this.dataForm.get('lastName');
  }
  get identificationNumber(): any {
    return this.dataForm.get('identificationNumber');
  }
  get cityId(): any {
    return this.dataForm.get('cityId');
  }
  // FIN GET DATA

  ngOnInit(): void {
    this.updateDataConnection();
    this.getCities();
    console.log(this.getCities());

  }

  getCities(): void {
    this.service.getData('City/Cities').subscribe(
      (result) => {
        this.cities = result;
        this.filteredCitiesList = this.cities.slice();
      },
      (error) => {
        console.log(error);
      }
    );
  }

  saveData(values: any): void {
    if (this.id === undefined)  {
      this.service.postData('Seller/CreateSeller/', values).subscribe((result) => {
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

  editData(item: any): any {
    this.id = item.id;
    this.tittleDialog = 'Modificar';
    this.buttonDialog = 'Modificar';

    this.dataForm.patchValue({
      name: item.name,
      lastName: item.lastName,
      identificationNumber: item.identificationNumber,
      cityId: item.cityId,
    });
    console.log(item);
  }

  updateDataConnection(): void {
    if (this.dataModal) {
      const data: any = this.dataModal.data;
      this.editData(data);
    } else {
      this.dataForm.reset();
    }
  }

}
