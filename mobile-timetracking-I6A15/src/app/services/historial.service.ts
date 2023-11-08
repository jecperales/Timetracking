import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HistorialService {

  urlApi = "https://monitor.grupopissa.com.mx/api-timetracking-revp/api/";
  params: any = {
    uid:Number,
    f1: String,
    f2: String

  }
  
  constructor(private http: HttpClient) { }



  getUserHistory(uid: Number, f1: String, f2: String)
  {
    // this.params.uid = uid;
    // this.params.f1 = f1;
    // this.params.f2 = f2;
    
    // return this.http.get(this.urlApi + "Movimientos/Hist", this.params);    
    return this.http.get(this.urlApi + "Movimientos/Hist?id_usuario=" + uid + "&f1=" + f1 + "&f2=" + f2);    
  }

  deleteAcividad(id: number)
  {
    return this.http.delete(this.urlApi + "Movimientos?id=" + id);
  }

}
