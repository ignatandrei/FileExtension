import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FileExtV1Component } from './file-ext-v1/file-ext-v1.component';

const routes: Routes = [
  {path: '', redirectTo: '/static/v1', pathMatch: 'full'},
  {path: 'static/v1', component: FileExtV1Component},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
