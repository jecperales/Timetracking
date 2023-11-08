import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TorreAtencionService {

  urlApi = "https://monitor.grupopissa.com.mx/api-timetracking-revp/api/"

  constructor(private http: HttpClient) { }


  getTorreAtencion()
  {    
    return this.http.get(this.urlApi + "Torres");    
  }
}
