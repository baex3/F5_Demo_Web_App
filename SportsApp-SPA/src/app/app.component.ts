import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'F5 Sports';
  subtitle = '#1 Source for Sports News Internationally';
  showHome : boolean = false;
  
  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token;
   } 
   
   goHome(showHome : boolean){

      this.showHome = showHome;
   }
}
