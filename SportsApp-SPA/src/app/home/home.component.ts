import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  model: any = {};
  values: any = {};
  registerMode = false;
  constructor(private authService: AuthService, private http: HttpClient) { }

  ngOnInit() {
  }

  registerToggle(){
    this.registerMode = true;
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      console.log('logged in successfully');
    }, error => {
      console.log('Failed to login');
    });
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token;
   }
 
   logout(){
     localStorage.removeItem('token');
     console.log('logged out');
   }

   getValues(){
    this.http.get('http://localhost:5000/api/values').subscribe(response => {
    this.values = response;
    }, error =>{
      console.log(error);
    })
  }

    cancelRegisterMode(registerMode: boolean ){
      this.registerMode = registerMode;
    }
}

