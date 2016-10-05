import { Component }        from '@angular/core';
import { Observable }       from 'rxjs/Observable';
import { UserService } from './user.service';
@Component({
  selector: 'my-user',
  template: `
    <h1>User Demo</h1>
    <p><i>Fetches after each keystroke</i></p>
    <input #term (keyup)="search(term.value)"/>
    <ul>
      <li *ngFor="let item of items | async">{{item}}</li>
    </ul>
  `,
  providers: [UserService]
})
export class UserComponent {
  items: Observable<string[]>;
  constructor (private userService: UserService) {}
  search (term: string) {
    this.items = this.userService.getUser(term);
  }
}
