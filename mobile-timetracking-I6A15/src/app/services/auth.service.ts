import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  urlApi = "https://monitor.grupopissa.com.mx/api-timetracking-revp/api/"


  constructor(public http:HttpClient) { }

  auth(uid: string, pwd: string)
  {
    return this.http.get(this.urlApi + "Usuario/Auth?user=" + uid + "&password="+ pwd);
  }

}
