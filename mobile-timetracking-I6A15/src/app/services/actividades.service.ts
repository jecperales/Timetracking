import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class ActividadesService {

  urlApi = "https://monitor.grupopissa.com.mx/api-timetracking-revp/api/";

  constructor(private http: HttpClient,) { }


  getActividadesTodayByUId(uid: number)
  {
    return this.http.get(this.urlApi + "Movimientos/Get/" + uid);
  }

  postActividad(actividad: any)
  {
    return this.http.post(this.urlApi + "Movimientos", actividad);
  }

  updateActividad(actividad: any)
  {
    return this.http.put(this.urlApi + "Movimientos", actividad);    
  }

  deleteAcividad(id: number)
  {
    return this.http.delete(this.urlApi + "Movimientos?id=" + id);
  }
}
