import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.page.html',
  styleUrls: ['./home.page.scss'],
})
export class HomePage implements OnInit {

  ingeniero: any = {
    id: Number,
    nombre: String,
    ap_paterno: String,
    ap_materno: String,
    correo: String,
    contrasena: String,
    fecha_registro: Date,
    fecha_modificacion: Date,
    id_proyecto: Number,
    id_perfil: Number,
    estatus:Number,
    telefono: String,
    id_pais: Number
  }


  constructor(private router:Router) {

    this.ingeniero = this.router.getCurrentNavigation()?.extras.state;

    console.log("Home Values");
    console.log(this.ingeniero);

  }

  ngOnInit() {
  }

}
