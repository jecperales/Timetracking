import { Component, OnInit } from '@angular/core';
import { InfiniteScrollCustomEvent } from '@ionic/angular';
import { Router } from '@angular/router';
import { AlertController } from '@ionic/angular';

import { ActividadesService } from '../services/actividades.service';
import { TipoAtencionService } from '../services/tipo-atencion.service';
import { TorreAtencionService } from '../services/torre-atencion.service';


@Component({
  selector: 'app-actividades',
  templateUrl: './actividades.page.html',
  styleUrls: ['./actividades.page.scss'],
})
export class ActividadesPage implements OnInit {
  
  isBlocked: boolean = false;
  items: any = [];

  user: any = {
    id: Number,
    name: String
  }

  now: Date = new Date();

  //FORM VARIABLES
  activity: any;
  fecha_r: any;
  hora_inicio: any;
  hora_fin: any;
  torre_atencion: any;
  tipo_atencion: any;
  localidad: any;
  comentarios: any;

  torre_atencion_Arr: any = [];
  tipo_atencion_Arr: any = [];


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

  //MOVIMINETO MODEL
  actividad: any = [];
  actividadesDropDown: any = [
    { "id": 1, "descripcion": "PROVEEDOR" },
    { "id": 2, "descripcion": "REDES" },
    { "id": 3, "descripcion": "ENVIO Y RECEPCION" },
    { "id": 4, "descripcion": "REUNION" },
    { "id": 5, "descripcion": "CONFIGURACION DE EQUIPO" },
    { "id": 6, "descripcion": "CONFIGURACION DE IMPRESORAS" },
    { "id": 7, "descripcion": "CONFIGURACION DE EDA" },
    { "id": 8, "descripcion": "CONFIGURACION DE PANTALLAS" },
    { "id": 9, "descripcion": "CONFIGURACION DE BASCULAS" },
    { "id": 10,"descripcion": "CONFIGURACION DE MOVILES" },
    { "id": 11,"descripcion": "GARANTIAS" },
    { "id": 12,"descripcion": "SIN ACTIVIDADES ADICIONALES" }
  ];
  actDpDwValue : any;
  

  actividadInsert: any = {
    id: Number,
    id_usuario: Number,
    actividad: String,
    fecha_registro: Date,
    hora_inicio: String,
    hora_fin: String,
    id_cat_estatus: Number,
    comentarios: String,
    no_ticket: Number,
    id_tipo_atencion: Number,
    localidad: String,
    fecha_registro_real: String
  }

  //GUARDAR/ACTUALIZAR Action
  saveUpdateValue = "Guardar";

  constructor(
    private router:Router,
    private alertCtrl: AlertController,
    private apiAct: ActividadesService,
    private apiAtencion: TipoAtencionService,
    private apiTorres: TorreAtencionService
  ) { 

    this.user.name = "Jose Enrique Cruz";
    this.ingeniero = this.router.getCurrentNavigation()?.extras.state;
    
  }

  ngOnInit() {
    //this.generateItems();

    this.fecha_r = this.now.toISOString().substring(0,10);
    this.hora_inicio = "00:00";
    this.hora_fin = "23:59";

    this.actividadInsert.id = 0;
    this.actividadInsert.id_usuario = 0;
    this.actividadInsert.actividad = "";
    this.actividadInsert.fecha_registro = this.fecha_r;
    this.actividadInsert.hora_inicio = this.hora_inicio;
    this.actividadInsert.hora_fin = this.hora_fin;
    this.actividadInsert.id_cat_estatus = 0;
    this.actividadInsert.comentarios = "";
    this.actividadInsert.no_ticket = null;
    this.actividadInsert.id_cat_torres = 0;
    this.actividadInsert.id_tipo_atencion = 0;
    this.actividadInsert.localidad = "";
    this.actividadInsert.fecha_registro_real = this.now.toISOString().substring(0,10);

    this.getActividadTodayByUId(this.ingeniero.id);
    this.getTipoAtencion();
    this.getTorres();

    console.log(this.fecha_r);
    console.log(this.hora_inicio);
    console.log(this.hora_fin);
    console.log();
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

  getActividadTodayByUId(uid: number)
  {
    if(uid == undefined || uid == null)
    {
      this.presentAlert("UID no válido.", "", "Hubo un error en la consulta de actividades"); 
      return;           
    }

    this.apiAct.getActividadesTodayByUId(uid).subscribe({
      next: (res) =>{
        if(res){
          this.actividad = res;              
        }
        else{
          this.presentAlert("Alert","","No hay actividades registradas el día de hoy");
        }

      },
      error: (err) =>{
        this.presentAlert("Error.","", "Hubo un error al obtener tus actividades. Si los problemas persisten contacta a tu administrador");
      },
      complete: () =>{
        console.log("Complete Get actividades");
        console.log(this.actividad);
      }
    });
  }

  getTipoAtencion(){
    this.apiAtencion.getTipoAtencion().subscribe({
      next: (res) => {
        if(res){
          this.tipo_atencion_Arr = res;
        }
        else{
          this.presentAlert("Info","","No hay records en el catalogo de atención")
        }
      },
      error: (err) => {
        this.presentAlert("Error Exception","","Ocurrió un error al obetener el catálogo de atencion.");
        console.log(err);
      },
      complete: () => {
        console.log("Complete getTipoAtencion");
        console.log(this.tipo_atencion_Arr);
      }
    });
  }

  getTorres(){
    this.apiTorres.getTorreAtencion().subscribe({
      next: (res) => {
        if(res){
          this.torre_atencion_Arr = res;
        }
        else{
          this.presentAlert("Info","","No hay records en el catálogo de torres de atención");
        }
      },
      error: (err) => {
        this.presentAlert("Error Exception","","Ocurrió un error al obetener el catálogo de torres de atención");
      },
      complete: () => {
        console.log("Complete getTorreAtencion");
        console.log(this.torre_atencion_Arr);
      }
    });
  }

  SaveUpdate(){
    if(this.saveUpdateValue === "Guardar" && this.actividadInsert.id == 0)
    {
      if(this.ValidaDatos())
      {
        this.actividadInsert.id = 0;
        this.actividadInsert.id_usuario = this.ingeniero.id;
        this.actividadInsert.actividad = this.activity;
        this.actividadInsert.fecha_registro = this.fecha_r;
        this.actividadInsert.hora_inicio = this.hora_inicio;
        this.actividadInsert.hora_fin = this.hora_fin;
        this.actividadInsert.id_cat_estatus = 1;
        this.actividadInsert.comentarios = this.comentarios;
        this.actividadInsert.no_ticket = null;
        this.actividadInsert.id_cat_torres = this.torre_atencion.id;
        this.actividadInsert.id_tipo_atencion = this.tipo_atencion.id;
        this.actividadInsert.localidad = this.localidad;
        this.actividadInsert.fecha_registro_real = this.now.toISOString().substring(0,10);

        this.apiAct.postActividad(this.actividadInsert).subscribe({
          next: (res) => {
            if(res){
              this.presentAlert("Insertar","","La nueva actividad se ha registrado con éxito.");
            }
            else{
              this.presentAlert("Insertar","Intenta de nuevo.","Hubo un problema al registar la actividad. Si los problemas presisten consulte a su administrador.");
            }
          },
          error: (err) => {
            this.presentAlert("Error Exception","","Hubo un error desconocido. Favor de consultar a su administrador");
            console.log(err);
          },
          complete: () => {
            this.Limpiar();
            this.getActividadTodayByUId(this.ingeniero.id);
          }
        });
      }
    }
    
    if(this.saveUpdateValue === "Actualizar" && this.actividadInsert.id != 0)
    {
      if(this.HayCambios())
      {
        console.log("Hubo Cambios...");
        console.log(this.actividadInsert);
        if(this.ValidaDatos())
        {
          this.actividadInsert.actividad != this.activity;
          this.actividadInsert.fecha_registro = this.fecha_r;
          this.actividadInsert.hora_inicio = this.hora_inicio;
          this.actividadInsert.hora_fin = this.hora_fin;
          this.actividadInsert.id_cat_torres = this.torre_atencion.id;
          this.actividadInsert.id_tipo_atencion = this.tipo_atencion.id;
          this.actividadInsert.localidad = this.localidad;
          this.actividadInsert.comentarios = this.comentarios;

          console.log("Actividad actualizada...");
          console.log(this.actividadInsert);

          //ACTUALIZAMOS LA ACTIVIDAD Y OBTENEMOS LAS ACTIVIDADES ACTUALIZADAS
          this.apiAct.updateActividad(this.actividadInsert).subscribe({
            next: (res) => {
              if(res)
              {
                this.presentAlert("Actualizar","","La activiad fue actualizada correctamente");
              }
              else
              {
                this.presentAlert("Actualizar","","Hubo un problema al actualizar la actividad. Intente de nuevo. Si los problemas persisten consulte a su administrador");
              }
            },
            error: (err) => {
              this.presentAlert("Exception Error","","Hubo un error desconocido. Si los problemas persisten consulte a su administrador");
            },
            complete: () => {
              this.Limpiar();
              this.getActividadTodayByUId(this.ingeniero.id);
            }
          });          
        }

      }
      else
      {
        this.presentAlert("Actualizar datos","","No hay cambios que actualizar");
      }
    }
  }

  ValidaDatos()
  {
    
    if(this.activity == "" || this.activity == undefined)
    {
      this.presentAlert("Datos no válidos","","La actividad no puede estar vacía.");
      return false;
    }

    if(this.fecha_r == "" || this.fecha_r == undefined)
    {
      console.log(this.fecha_r);
      this.presentAlert("Datos no válidos","","La fecha no puede estar vacía.");
      return false;
    }

    if(this.hora_inicio == "" || this.hora_inicio == undefined || this.hora_inicio == "00:00")
    {
      console.log("Hora Inicio");
      console.log(this.hora_inicio);
      this.presentAlert("Datos no válidos","","La hora de inicio no puede estar vacía.");      
      return false;
    }
    else
    {
      if(this.hora_inicio > this.hora_fin)
      {
        // console.log("Hora Inicio > Hora Fin");
        // console.log(this.hora_inicio);
        // console.log(this.hora_fin);
        this.presentAlert("Validacion de datos","","La hora de inicio no puede ser mayor a la hora de fin");
        return false;
      }
    }
    
    if(this.hora_fin == "" || this.hora_fin == undefined || this.hora_fin == "00:00")
    {      
      console.log("Hora fin");
      console.log(this.hora_fin);
      this.presentAlert("Datos no válidos","","La hora de fin no puede estar vacía.");
      return false;
    }
    else
    {
      if(this.hora_fin < this.hora_inicio)
      {
        // console.log("Hora fin > Hora inicio");
        // console.log(this.hora_fin);
        // console.log(this.hora_inicio);
        this.presentAlert("Validacion de datos","","La hora de fin no puede ser menor a la hora de inicio");
        return false;
      }
    }
    
    if(this.torre_atencion == "" || this.torre_atencion == undefined)
    {
      this.presentAlert("Datos no válidos","","La torre de atención no puede estar vacía.");
      return false;
    }

    if(this.tipo_atencion == "" || this.tipo_atencion == undefined)
    {
      this.presentAlert("Datos no válidos","","El tipo de atención no puede estar vacío.");
      return false;
    }

    if(this.localidad == "" || this.localidad == undefined)
    {
      this.presentAlert("Datos no válidos","","La localidad no puede estar vacía.");
      return false;
    }

    if(this.comentarios == "" || this.comentarios == undefined)
    {
      this.presentAlert("Datos no válidos","","Los comentarios no pueden estar vacíos.");
      return false;
    }

    return true;
  }

  HayCambios()
  {
    if(this.actividadInsert.actividad != this.activity ||
       this.actividadInsert.fecha_registro.substring(0,10) != this.fecha_r ||
       this.actividadInsert.hora_inicio != this.hora_inicio ||
       this.actividadInsert.hora_fin != this.hora_fin ||
       this.actividadInsert.id_cat_torres != this.torre_atencion.id ||
       this.actividadInsert.id_tipo_atencion != this.tipo_atencion.id ||
       this.actividadInsert.localidad != this.localidad ||
       this.actividadInsert.comentarios != this.comentarios
      )
    {
      return true;
    }
    else
    {
      return false;
    }
  }

  Limpiar()
  {
    //CLEAR FORM VARIABLES
    this.activity = "";
    this.fecha_r = this.now.toISOString().substring(0,10);
    this.hora_inicio = "00:00";
    this.hora_fin = "23:59";
    this.torre_atencion = "";
    this.tipo_atencion = "";
    this.localidad = "";
    this.comentarios = "";

    

    this.saveUpdateValue = "Guardar";


    //CLEAR THE OBJETC TO BE INSERTED
    this.actividadInsert.id = 0;
    this.actividadInsert.id_usuario = 0;
    this.actividadInsert.actividad = "";
    this.actividadInsert.fecha_registro = this.now.toISOString().substring(0,10);;
    this.actividadInsert.hora_inicio = "00:00";
    this.actividadInsert.hora_fin = "00:00";
    this.actividadInsert.id_cat_estatus = 0;
    this.actividadInsert.comentarios = "";
    this.actividadInsert.no_ticket = null;
    this.actividadInsert.id_cat_torres = 0;
    this.actividadInsert.id_tipo_atencion = 0;
    this.actividadInsert.localidad = "";
    this.actividadInsert.fecha_registro_real = this.now.toISOString().substring(0,10);
  }

  Editar(act: any)
  {
    console.log("Editando..");
    console.log(act);

    //Cambiamos el action a Update
    this.saveUpdateValue = "Actualizar";

    //CLEAR FORM VARIABLES
    this.activity = "";
    this.fecha_r = this.now.toISOString();
    this.hora_inicio = "00:00";
    this.hora_fin = "00:00";
    this.torre_atencion = "";
    this.tipo_atencion = "";
    this.localidad = "";
    this.comentarios = "";

    //FILL FORM VARIABLES
    this.activity = act.actividad;
    this.fecha_r = act.fecha_registro.substring(0,10);
    this.hora_inicio = act.hora_inicio;
    this.hora_fin = act.hora_fin;
    this.torre_atencion = this.torre_atencion_Arr[this.torre_atencion_Arr.findIndex((item: { id: any; }) => item.id === act.id_cat_torres)]; //this.torre_atencion_Arr[0]; 
    this.tipo_atencion = this.tipo_atencion_Arr[this.tipo_atencion_Arr.findIndex((item: {id: any; }) => item.id === act.id_tipo_atencion)]; //this.tipo_atencion_Arr[0];
    this.localidad = act.localidad;
    this.comentarios = act.comentarios;

    this.actividadInsert = act;

  }

  Eliminar(act: any){
    this.apiAct.deleteAcividad(act.id).subscribe({
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
        this.Limpiar();
        this.getActividadTodayByUId(this.ingeniero.id);
      }
    });
  }

  isWithNoActivity(){
    //console.log(this.actDpDwValue);
    if(this.actDpDwValue.id == 12)
    {
      this.hora_inicio = "00:00";
      this.hora_fin = "00:05";
      this.torre_atencion = this.torre_atencion_Arr[this.torre_atencion_Arr.findIndex((item: { id: any; }) => item.id == 2)]; //Usuario Bimbo 
      this.tipo_atencion = this.tipo_atencion_Arr[this.tipo_atencion_Arr.findIndex((item: {id: any; }) => item.id == 1)]; //Presencial
      this.localidad = "En sitio";
      this.comentarios = "El dia de hoy se atendieron puros tickets";

      this.blockControls();
    }
    else
    {
      this.unblockControls();
      this.Limpiar();
    }
  }

  blockControls()
  {
    this.isBlocked = true;
  }

  unblockControls(){
    this.isBlocked =  false;
  }
}
