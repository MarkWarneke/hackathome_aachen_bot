import { Component, Input  }        from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { UserService } from './user.service';
import { User } from './user';

@Component({
  selector: 'my-user',
  templateUrl: 'app/user.component.html',
  providers: [UserService]
})
export class UserComponent {


  constructor(private userService: UserService) {}

  currentUser: User;
  errorMessage: string;

    search(term: string) {
        this.userService.getUser(term)
            .then(this.setUser);
      }

    setUser (user: User): void {
       console.log("user", user);
       this.currentUser = user;
   }

}
