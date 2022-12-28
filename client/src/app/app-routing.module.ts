import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TextEditorComponent } from './components/editor/editor.component';
import { LoginComponent } from './components/login/login.component';


const routes: Routes = [
  {
    path: 'editor',
    component: TextEditorComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
