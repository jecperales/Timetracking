import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { AlertController } from '@ionic/angular';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {

  uid: any;
  password: any;

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

  constructor
  (
    public apiAuth:AuthService, 
    private alertCtrl: AlertController,
    private route: Router
  ) 
  {  }

  ngOnInit() {
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

  login(uid: string, password: string)
  {
    if(uid == undefined || uid.trim()=="")
    {
      this.presentAlert("Datos no válidos.", "", "Introduzca un nombre de usuario"); 
      return;           
    }

    if(password == undefined || password.trim()=="")
    {
      this.presentAlert("Datos no válidos.", "", "Introduzca una contraseña"); 
      return;           
    }

    this.apiAuth.auth(this.uid, this.password).subscribe(
      {
        next: (res) =>{
          if(res){
            this.ingeniero = res;
            //this.showLoading();    
            
            this.route.navigate(['home'],{state: this.ingeniero});
          }
          else{
            this.presentAlert("Autenticación.", "Credenciales incorrectas.", "Usuario y/o contraseña incorrectos.");            
          }    
        },
        error: (err) => {
          console.log("Error: ");
          console.error(err);
        },
        complete: () => {
          console.info("Complete");
          
        }
      }
    );
  }

}
