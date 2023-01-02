import { Component } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { ILoginResponseModel } from "src/app/models/loginResponseModel";
import { AuthService } from "src/app/services/auth.service";

@Component({
    selector: 'register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
    form:FormGroup;
  
    constructor(private fb:FormBuilder, 
                private authService: AuthService, 
                private router: Router) {
  
    this.form = this.fb.group({
        firstName: ['',[Validators.required]],
        lastName: ['',[Validators.required]],
        email: ['',[Validators.required, Validators.email]],
        password: ['',Validators.required]
        });
    }
  
    register() {

        // Check if the form has errors:
        if(this.form.errors){
            console.log("form has errors");
            return;
        }

        // Get the form values:
        const val = this.form.value;
        
        // Register the user:
        this.authService.register({
            email: val.email,
            password: val.password,
            firstName: val.firstName,
            lastName: val.lastName
        }).subscribe({
            next: (response: ILoginResponseModel) => {
                this.authService.setSession(response);
        }
            
            //redirects to where you want to go - ideally home screen.
            //this.router.navigateByUrl('/');
        });
    }
}