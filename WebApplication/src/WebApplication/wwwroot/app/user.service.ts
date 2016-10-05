import { Injectable } from '@angular/core';
import { Headers, Http, Response  } from '@angular/http';
import { Observable } from 'rxjs';

import 'rxjs/add/operator/toPromise';

import { User } from './user';

@Injectable()
export class UserService {
    private remote = 'http://shruggieuserrest.azurewebsites.de/api/users/';  // URL to web API
    private local = 'api/users/';  // URL to web API
    private url = this.local;

    user: User;

    constructor(private http: Http) {
  }   


    search(term: string): Observable<User> {
        return this.http
            .get(`api/users/${term}`)
            .map((r: Response) => r.json().data as User);
    }

    getUser(term: string): Promise<User> {
        return this.http.get(this.url + term)
            .toPromise()
            .then(this.extractData)
            .catch(this.handleError);
    }
  
    private extractData(res: Response) {
        let body = res.json();
        console.log("body", body);
        console.log("new User", new User(body.id, body.chat_s, body.person));
        return new User(body.id, body.chat_s, body.person);
    }

    private handleError(error: any) {
        // In a real world app, we might use a remote logging infrastructure
        // We'd also dig deeper into the error to get a better message
        let errMsg = (error.message) ? error.message :
            error.status ? `${error.status} - ${error.statusText}` : 'Server error';
        console.error(errMsg); // log to console instead
        return Promise.reject(errMsg);
    }
}