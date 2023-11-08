import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TipoAtencionService {

  urlApi = "https://monitor.grupopissa.com.mx/api-timetracking-revp/api/"

  constructor(private http:HttpClient) { }

  getTipoAtencion()
  {
    return this.http.get(this.urlApi + "TipoAtencion");
  }

}
