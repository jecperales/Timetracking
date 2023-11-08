import { Component, OnInit } from '@angular/core';
import { InfiniteScrollCustomEvent } from '@ionic/angular';

import { HistorialService } from '../services/historial.service';
import { AlertController } from '@ionic/angular';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-historial',
  templateUrl: './historial.page.html',
  styleUrls: ['./historial.page.scss'],
})
export class HistorialPage implements OnInit {

  items: any = [];

  now: Date = new Date();

  user: any = {
    id: Number,
    name: String
  }

  ingeniero :any = {
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

  f1: any;
  f2: any;


  constructor(private apiHistorial: HistorialService, private alertCtrl: AlertController, private router: Router) { 


    this.ingeniero = this.router.getCurrentNavigation()?.extras.state;

  }

  ngOnInit() {
    //this.generateItems();
    //this.getHistorial();
  }

  async presentAlert(header:string, subHeader: string, message: string, ) {
    const alert = await this.alertCtrl.create({
      backdropDismiss: false,
      header: header,
      subHeader: subHeader,
      message: message,
      buttons: ['OK'],
    });

    await alert.present();
  }

  private generateItems() {
    // const count = this.items.length + 1;
    // for (let i = 0; i < 50; i++) {
    //   this.items.push(`Item ${count + i}`);
    // }
  }

  onIonInfinite(ev: any) {
    //this.generateItems();
    setTimeout(() => {
      (ev as InfiniteScrollCustomEvent).target.complete();
    }, 500);
  }

  getHistorial()
  {
    if(this.f1 == "" || this.f1 == undefined)
    {
      this.presentAlert("Fechas incorrectas","","La fecha de inicio no puee ir vacia");
      return;
    }

    if(this.f2 == "" || this.f2 == undefined)
    {
      this.presentAlert("Fechas incorrectas","","La fecha de fin no puee ir vacia");
      return;
    }

    if(this.f1>this.f2){
      this.presentAlert("Fechas incorrectas","","La fecha de inicio no puede ser mayor a la de fin");
      return;
    }

    this.apiHistorial.getUserHistory(this.ingeniero.id, this.f1.substring(0,10), this.f2.substring(0,10)).subscribe({
      next: (res) => {
        if(res)
        {
          console.log("GetHistorial...");
          console.log(this.items);
          this.items = res;
        }
        else
        {
          this.presentAlert("No records","","No se encontraron registros para el rango de fecha especificdado.");
        }

      },
      error: (err) => {
        this.presentAlert("Error Exception","","Ocurrio un problema inesperado. Si persisten los problemas contacte a su administrador");
        console.log(err);
      },
      complete: () => {

      }
    });
  }


  Eliminar(act: any){
    this.apiHistorial.deleteAcividad(act.id).subscribe({
      next: (res) => {
        if(res){
          this.presentAlert("Eliminar","","La ctividad ha sido eliminada con éxito");
        }
        else
        {
          this.presentAlert("Eliminar","","Hubo un problema al eliminar la actividad");
        }
      },
      error: (err) => {
        this.presentAlert("Error Exception","Error desconocido","Hubo un error. Si persisten los problemas contacte a su administrador");
      },
      complete: () => {
        this.getHistorial();
      }
    });
  }
}
