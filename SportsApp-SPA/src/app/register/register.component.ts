import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Input() valuesFromHome: any;
  @Output() cancelRegister = new EventEmitter();
  @Output() registerSuccesful = new EventEmitter();
  model: any = {};
  

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

cancel(){
  this.cancelRegister.emit(false);
  this.registerSuccesful.emit(false);
console.log();
}

register(){
  this.authService.register(this.model).subscribe(() => {
    console.log('registration successful');
  }, error => {
      console.log(error)
    });

    this.registerSuccesful.emit(true);
}

}
