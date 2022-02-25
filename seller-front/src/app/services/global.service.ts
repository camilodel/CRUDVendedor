import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable, Output } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GlobalService {
  @Output() update: EventEmitter<any> = new EventEmitter();

  private api: string = 'http://172.168.3.76:1493/api/';

  constructor(
    private http: HttpClient,
  ) {
    this.http.get(this.api);
  }

  getData( endpoint: string): Observable<any> {
    return this.http.get(this.api + endpoint);
  }

  getById(endpoint: string, id: string): Observable<any> {
    return this.http.get(this.api + endpoint + id);
  }

  postData( endpoint: string, data: any): Observable<any> {
    return this.http.post(this.api + endpoint, data);
  }

  putData(endpoint: string, id: any, data: any): Observable<any> {
    return this.http.put(this.api + endpoint + id, data);
  }

  deleteData(endpoint: string, id: any): Observable<any> {
    return this.http.delete(this.api + endpoint + id);
  }

  refreshData(): any {
    this.update.emit();
  }

}
