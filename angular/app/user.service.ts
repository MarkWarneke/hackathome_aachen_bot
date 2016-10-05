import { Injectable }     from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable }     from 'rxjs/Observable';

@Injectable()
export class UserService {
    private url = 'http://shruggieuserrest.azurewebsites.de/api/users/';  // URL to web API
    constructor (private http: Http) {}

    getUser (term: any) {
       return this.http.get(this.url + term)
                    .map(this.extractData)
                    .catch(this.handleError);
    }
  
    private extractData(res: Response) {
    let body = res.json();
    console.log(body);
    return body.data || { };
  }

   private handleError (error: any) {
    // In a real world app, we might use a remote logging infrastructure
    // We'd also dig deeper into the error to get a better message
    let errMsg = (error.message) ? error.message :
      error.status ? `${error.status} - ${error.statusText}` : 'Server error';
    console.error(errMsg); // log to console instead
    return Observable.throw(errMsg);
  }
}