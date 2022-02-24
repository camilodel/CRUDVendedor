import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { GlobalService } from 'src/app/services/global.service';
import Swal from 'sweetalert2';
import { DialogComponent } from '../dialog/dialog.component';
import { ShowDetailsComponent } from '../show-details/show-details.component';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss']
})
export class TableComponent implements OnInit {
  // Cabeceras de la tabla HTML que deben coincidir con el json del back
  displayedColumns: string[] = ['id', 'name', 'lastName', 'identificationNumber', 'cityId', 'actions'];

  // Variable que guarda el objeto de la consutla get al back
  dataSource = new MatTableDataSource<any>();

  constructor(
    private service: GlobalService,
    public dialog: MatDialog,
  ) { }

  ngOnInit(): void {
    this.getData();
    this.refreshData();
  }

  openDialog(data: any): void { // Muestra el dialog para editar la data
    this.dialog.open(DialogComponent, {
      width: '350px',
      data:{method: 'update', data: data}
    }).afterClosed().subscribe((result) => {
      this.service.refreshData();
    });
  }

  getData(): void { // Muestra la data en la tabla
    this.service.getData('Seller/Sellers').subscribe((result) => {
      this.dataSource.data = result;
      console.log(result);
    });
  }

  deleteData(data: any): void { // Elimina el registro seleccionado de la tabla
    const id = data.id;
    Swal.fire({
      title: 'Está seguro de eliminar el registro?',
      text: 'Esta acción no se podrá revertir!!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Si, eliminar!',
      cancelButtonText: 'Cancelar',
    }).then((result) => {
      if (result.isConfirmed) {
        this.service.deleteData('Seller/', id).subscribe((result) => {
          Swal.fire('Eliminado', 'El registro ha sido eliminado', 'success');
          this.service.refreshData();
        }, (error) => {
          Swal.fire('Error!!', 'No se ha podido eliminar el registro', 'error');
          console.log(error);
        });
      }
    });
  }

  showDetails(element: any): void { // Muestra los detalles del registro seleccionado
    const dialogRef = this.dialog.open(ShowDetailsComponent, {
      width: '500px',
      data: { data: element },
    });
    dialogRef.afterClosed().subscribe((result) => {

    });
  }

  refreshData(): any { // Actualiza los datos que se visualizan en la tabla
    this.service.update.subscribe((result) => {
      this.getData();
    });
  }

}
