import { NgModule } from '@angular/core';
import { flush } from '@angular/core/testing';
import { Routes, RouterModule } from '@angular/router';

import { HomePage } from './home.page';

const routes: Routes = [
  {
    path: 'home',
    component: HomePage,
    children: [
      {
        path: 'actividades',
        loadChildren: () => import('../actividades/actividades.module').then(x => x.ActividadesPageModule)
      },
      {
        path: 'historial',
        loadChildren: () => import('../historial/historial.module').then(x => x.HistorialPageModule)        
      }
    ]
  },
  {
    path:'',
    redirectTo:'home/actividades',
    pathMatch: 'full'
  }
];

// const routes: Routes = [
//   {
//     path: 'home',
//     component: HomePage,
//     children: [
//       {
//         path: 'actividades',
//         loadChildren: () => import('../actividades/actividades.module').then(x => x.ActividadesPageModule)
//       },
//       {
//         path: 'historial',
//         loadChildren: () => import('../historial/historial.module').then(x => x.HistorialPageModule)        
//       },
//       {
//         path:'',
//         redirectTo: 'home/actividades',
//         pathMatch: 'full'
//       }
//     ]
//   },
//   {
//     path:'',
//     redirectTo:'home/actividades',
//     pathMatch: 'full'
//   }
// ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class HomePageRoutingModule {}
