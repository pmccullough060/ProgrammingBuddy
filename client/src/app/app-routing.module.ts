import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TextEditorComponent } from './components/editor/editor.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/Register/register.component';


const routes: Routes = [
  {
    path: 'editor',
    component: TextEditorComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component: RegisterComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
